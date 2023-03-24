using Autofac;
using Autofac.Extras.Moq;
using Moq;
using Project_UnitTests.Helpers;
using Project_DomainEntities;
using Project_Main.Models.DataBases.Repositories;

namespace Project_UnitTests
{
	/// <summary>
	/// Unit Test Class for Database tests with Mocking (DbContext) approach.
	/// </summary>
	public class DatabaseOperationsTests : BaseOperationsSetup
	{
		private const int OnePositionFurther = 1;
		private const int IdOfFirstTodoList = 1;
		private const TaskModel NullTask = null;

		private static readonly object[] ValidTasksExamples = TaskData.ValidTasksExamples;

        private AutoMock RegisterContextInstance()
		{
			return AutoMock.GetLoose(cfg => cfg.RegisterInstance(this.ContextOperations).As<IContextOperations>());
		}

        /// <summary>
        /// Tests <see cref="ContextOperations"/> - Create Task - operation as succees.
        /// </summary>
        /// <param name="taskName">Valid name value for new Task.</param>
        [TestCaseSource(nameof(ValidTasksExamples))]
		public async Task AddTaskShouldSucceed(string taskTitle, string taskDescription, DateTime taskDueDate)
		{
            using AutoMock mock = RegisterContextInstance();

			TaskModel assertTask = new()
			{
				Id = this.AllTasks.Last().Id + OnePositionFurther,
				Title = taskTitle + "NEW",
				Description = taskDescription,
				DueDate = taskDueDate,
				TodoListId = 0
            };

			await SetupMockCreateTask(assertTask);
			var mockContext = mock.Create<IContextOperations>();
			await mockContext.CreateTaskAsync(assertTask);

			await SetupMockReadTask(assertTask.Id);
			TaskModel? tempTask = await mockContext.ReadTaskAsync(assertTask.Id, AdminId) ?? throw new AssertionException("Cannot find targeted Task in seeded data for unit tests.");
            this.DbContextMock.Verify(x => x.CreateTaskAsync(It.IsAny<TaskModel>()), Times.Once);
			this.DbContextMock.Verify(x => x.ReadTaskAsync(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
			Assert.That(tempTask, Is.EqualTo(assertTask));
		}

		/// <summary>
		/// Tests <see cref="ContextOperations"/> - Create Task - operation with Null Object to throw exception.
		/// </summary>
		[Test]
		public async Task AttemptToCreateTaskByNullObjectShouldThrowException()
		{
			TaskModel? assertNullTask = null;
			using AutoMock mock = RegisterContextInstance();
			await SetupMockCreateTask(assertNullTask);
			var mockContext = mock.Create<IContextOperations>();

#pragma warning disable CS8604 // Possible null reference argument.
			Assert.ThrowsAsync<ArgumentNullException>(async () => await mockContext.CreateTaskAsync(assertNullTask));
#pragma warning restore CS8604 // Possible null reference argument.
		}

		/// <summary>
		/// Tests <see cref="ContextOperations"/> - Read Task - operation as success.
		/// </summary>
		/// <param name="taskId">Id of Task.</param>
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		public async Task ReadTaskShouldSucceed(int taskId)
		{
			using AutoMock mock = RegisterContextInstance();
			var assertTask = this.AllTasks.Single(t => t.Id == taskId);
			await SetupMockReadTask(assertTask.Id);
			var mockContext = mock.Create<IContextOperations>();

			var resultTask = await mockContext.ReadTaskAsync(assertTask.Id, AdminId) ?? throw new AssertionException("Cannot find targeted Task in seeded data for unit tests.");
            Assert.That(resultTask, Is.EqualTo(assertTask));
		}

        /// <summary>
        /// Tests <see cref="ContextOperations"/> - Get Task - operation that should failed and throw exception.
        /// </summary>
        /// <param name="listId">Invalid Task id.</param>
        [TestCase(-1)]
        [TestCase(-10)]
        public async Task AttemptToReadTaskWithInvalidIdShouldThrowException(int invalidTaskId)
        {
            using AutoMock mock = RegisterContextInstance();
            await SetupMockReadTaskForException();
            var mockContext = mock.Create<IContextOperations>();

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await mockContext.ReadTaskAsync(invalidTaskId, AdminId));
        }

		/// <summary>
		/// Tests <see cref="ContextOperations.UpdateTaskAsync(TaskModel)"/> - Update Task - operation as success attempt.
		/// </summary>
		[Test]
		public async Task UpdateTaskShouldSucceed()
		{
			int taskToUpdateId = this.TodoLists.First().Id;
			using AutoMock mock = RegisterContextInstance();
			await SetupMockReadTask(taskToUpdateId);
			var mockContext = mock.Create<IContextOperations>();
			TaskModel? taskToUpdate = await mockContext.ReadTaskAsync(taskToUpdateId, AdminId) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
			taskToUpdate.Title = "New Title Set";
			taskToUpdate.Description = "Lorem Ipsum lorem lorem ipsum Lorem Ipsum lorem lorem ipsum";
			taskToUpdate.DueDate = DateTime.Now;
            await SetupMockUpdateTask(taskToUpdate);

			await mockContext.UpdateTaskAsync(taskToUpdate);
			TaskModel? updatedTask = await mockContext.ReadTaskAsync(taskToUpdateId, AdminId) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
			Assert.Multiple(() =>
			{
                Assert.That(updatedTask.Title, Is.EqualTo(taskToUpdate.Title));
                Assert.That(updatedTask.Description, Is.EqualTo(taskToUpdate.Description));
                Assert.That(updatedTask.DueDate, Is.EqualTo(taskToUpdate.DueDate));
            });
		}

		/// <summary>
		/// Tests <see cref="ContextOperations.UpdateTaskAsync(TaskModel)"/> - Update Task - operation with Null Object to throw exception.
		/// </summary>
		[Test]
		public async Task AttemptToUpdateTaskByNullObjectShouldThrowException()
		{
			int assertTaskId = IdOfFirstTodoList;
			using AutoMock mock = RegisterContextInstance();
			await SetupMockReadTask(assertTaskId);
			var mockContext = mock.Create<IContextOperations>();

			Assert.ThrowsAsync<ArgumentNullException>(async () => await mockContext.UpdateTaskAsync(NullTask));
		}

		/// <summary>
		/// Tests <see cref="ContextOperations.DeleteTaskAsync(int)"/> - Delete Task - operation as success attempt.
		/// </summary>
		/// <param name="assertTaskId">Valid Task Id, that should be deleted.</param>
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		public async Task DeleteTaskShouldSucceed(int assertTaskId)
		{
			var itemsNumberBeforeDelete = this.AllTasks.Count;
			using AutoMock mock = RegisterContextInstance();
			await SetupMockReadTask(assertTaskId);
			await SetupMockDeleteTask();
			var mockContext = mock.Create<IContextOperations>();
			await mockContext.DeleteTaskAsync(assertTaskId, AdminId);
			var itemsNumberAfterDelete = this.AllTasks.Count;
            await SetupMockReadTaskReturnNull();

            this.DbContextMock.Verify(x => x.ReadTaskAsync(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
			this.DbContextMock.Verify(x => x.DeleteTaskAsync(It.IsAny<TaskModel>()), Times.Once);
			Assert.Multiple(() =>
			{
				Assert.That(itemsNumberAfterDelete, Is.Not.EqualTo(itemsNumberBeforeDelete));
                Assert.ThrowsAsync<InvalidOperationException>(async () => await mockContext.ReadTaskAsync(assertTaskId, AdminId));
            });
		}

		/// <summary>
		/// Tests <see cref="ContextOperations.DeleteTaskAsync(int)"/> - Delete Task - operation with Id Out Of Range to throw exception.
		/// </summary>
		/// <param name="id">Id value out for range.</param>
		[TestCase(-2)]
		[TestCase(-1)]
		public async Task DeleteTaskByIdOutOfRangeShouldThrowException(int id)
		{
			int assertTaskId = IdOfFirstTodoList;
			using AutoMock mock = RegisterContextInstance();
			await SetupMockReadTask(assertTaskId);
			var mockContext = mock.Create<IContextOperations>();

			Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => mockContext.DeleteTaskAsync(id, AdminId));
		}

		/// <summary>
		/// Tests <see cref="ContextOperations.DeleteTaskAsync(int)"/> - Delete Task - operation with Invalid Id to throw exception.
		/// </summary>
		/// <param name="id">Nonexisted Task Id.</param>
		[TestCase(40)]
		[TestCase(100)]
		public async Task DeleteTodoListByInvalidIdShouldThrowException(int id)
		{
			using AutoMock mock = RegisterContextInstance();
			await SetupMockReadTaskReturnNull();
			var mockContext = mock.Create<IContextOperations>();

			Assert.ThrowsAsync<InvalidOperationException>(() => mockContext.DeleteTodoListAsync(id, AdminId));
		}

		private async Task SetupMockCreateTask(TaskModel? assertTask)
		{
			await Task.Run(() =>
			{
#pragma warning disable CS8604 // Possible null reference argument.
				this.DbContextMock.Setup(x => x.CreateTaskAsync(It.IsAny<TaskModel>())).Callback(() => this.AllTasks.Add(assertTask)).Returns(Task.FromResult(1));
#pragma warning restore CS8604 // Possible null reference argument.
			});
		}

		private async Task SetupMockReadTask(int assertTaskId)
		{
			await Task.Run(() =>
			{
				this.DbContextMock.Setup(x => x.ReadTaskAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(this.AllTasks.Single(t => t.Id == assertTaskId));
			});
		}

        private async Task SetupMockReadTaskForException()
        {
            await Task.Run(() =>
            {
                this.DbContextMock.Setup(x => x.ReadTaskAsync(It.IsAny<int>(), It.IsAny<string>()));
            });
        }

        private async Task SetupMockReadTaskReturnNull()
		{
			await Task.Run(() =>
			{
				this.DbContextMock.Setup(x => x.ReadTaskAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(() => NullTask);
			});
		}
		
		private async Task SetupMockUpdateTask(TaskModel taskToUpdate)
		{
			await Task.Run(() =>
			{
				this.DbContextMock.Setup(x => x.UpdateTaskAsync(It.IsAny<TaskModel>()))
				.Callback<TaskModel>((task) =>
				{
					var tempTask = this.AllTasks.Find(task => task.Id == taskToUpdate.Id) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
					tempTask.Title = taskToUpdate.Title;
				}).Returns(Task.FromResult(1));
			});
		}

		private async Task SetupMockDeleteTask()
		{
			await Task.Run(() => this.DbContextMock.Setup(x => x.DeleteTaskAsync(It.IsAny<TaskModel>())).Callback<TaskModel>((taskToDelete) => this.AllTasks.Remove(taskToDelete)).Returns(Task.FromResult(1)));
		}
	}
}