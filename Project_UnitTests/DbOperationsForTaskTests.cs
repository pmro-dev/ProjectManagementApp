using Autofac;
using Autofac.Extras.Moq;
using Moq;
using Project_UnitTests.Helpers;
using Project_DomainEntities;
using Project_Main.Models.DataBases.AppData;

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
			return AutoMock.GetLoose(cfg =>
			{
				cfg.RegisterInstance(this.DataUnitOfWork).As<IDataUnitOfWork>();
			});
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

			var dataUnitOfWork = mock.Create<IDataUnitOfWork>();
			var taskRepo = dataUnitOfWork.TaskRepository;
			await SetupMockAddTask(assertTask);
			await taskRepo.AddAsync(assertTask);
			await dataUnitOfWork.SaveChangesAsync();

			await SetupMockGetTask(assertTask.Id);
			TaskModel? tempTask = await taskRepo.GetAsync(assertTask.Id) ?? throw new AssertionException("Cannot find targeted Task in seeded data for unit tests.");

			this.DbSetTaskMock.Verify(x => x.AddAsync(It.IsAny<TaskModel>(), It.IsAny<CancellationToken>()), Times.Once);
			this.DbSetTaskMock.Verify(x => x.FindAsync(It.IsAny<object>()), Times.Once);
			this.AppDbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
			Assert.That(tempTask, Is.EqualTo(assertTask));
		}

		private async Task SetupMockAddTask(TaskModel assertTask)
		{
			Action action = () => this.AllTasks.Add(assertTask);

			await Task.Run(() =>
			{
				this.DbSetTaskMock.Setup(x => x.AddAsync(It.IsAny<TaskModel>(), default))
					.Callback(() =>
					{
						this.ActionsOnDbToSave.Add(action);
					}).Returns(new ValueTask<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TaskModel>>());
			});
		}

		private async Task SetupMockGetTask(int assertTaskId)
		{
			await Task.Run(() =>
			{
				this.DbSetTaskMock.Setup(x => x.FindAsync(It.IsAny<object>())).Returns(new ValueTask<TaskModel?>(this.AllTasks.SingleOrDefault(t => t.Id == assertTaskId)));
			});
		}

		/// <summary>
		/// Tests <see cref="ContextOperations"/> - Create Task - operation with Null Object to throw exception.
		/// </summary>
		[Test]
		public async Task AttemptToCreateTaskByNullObjectShouldThrowException()
		{
			TaskModel? assertNullTask = null;
			using AutoMock mock = RegisterContextInstance();

			var dataUnitOfWork = mock.Create<IDataUnitOfWork>();
			var taskRepo = dataUnitOfWork.TaskRepository;

			await SetupMockAddTask(assertNullTask!);

			Assert.ThrowsAsync<ArgumentNullException>(async () => await taskRepo.AddAsync(assertNullTask!));
		}

		//		/// <summary>
		//		/// Tests <see cref="ContextOperations"/> - Read Task - operation as success.
		//		/// </summary>
		//		/// <param name="taskId">Id of Task.</param>
		//		[TestCase(1)]
		//		[TestCase(2)]
		//		[TestCase(3)]
		//		[TestCase(4)]
		//		public async Task ReadTaskShouldSucceed(int taskId)
		//		{
		//			using AutoMock mock = RegisterContextInstance();
		//			var assertTask = this.AllTasks.Single(t => t.Id == taskId);
		//			await SetupMockGetTask(assertTask.Id);
		//			var mockContext = mock.Create<IContextOperations>();

		//			var resultTask = await mockContext.ReadTaskAsync(assertTask.Id, AdminId) ?? throw new AssertionException("Cannot find targeted Task in seeded data for unit tests.");
		//			Assert.That(resultTask, Is.EqualTo(assertTask));
		//		}

		//		/// <summary>
		//		/// Tests <see cref="ContextOperations"/> - Get Task - operation that should failed and throw exception.
		//		/// </summary>
		//		/// <param name="listId">Invalid Task id.</param>
		//		[TestCase(-1)]
		//		[TestCase(-10)]
		//		public async Task AttemptToReadTaskWithInvalidIdShouldThrowException(int invalidTaskId)
		//		{
		//			using AutoMock mock = RegisterContextInstance();
		//			await SetupMockReadTaskForException();
		//			var mockContext = mock.Create<IContextOperations>();

		//			Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await mockContext.ReadTaskAsync(invalidTaskId, AdminId));
		//		}

		//		/// <summary>
		//		/// Tests <see cref="ContextOperations.UpdateTaskAsync(TaskModel)"/> - Update Task - operation as success attempt.
		//		/// </summary>
		//		[Test]
		//		public async Task UpdateTaskShouldSucceed()
		//		{
		//			int taskToUpdateId = this.TodoLists.First().Id;
		//			using AutoMock mock = RegisterContextInstance();
		//			await SetupMockGetTask(taskToUpdateId);
		//			var mockContext = mock.Create<IContextOperations>();
		//			TaskModel? taskToUpdate = await mockContext.ReadTaskAsync(taskToUpdateId, AdminId) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
		//			taskToUpdate.Title = "New Title Set";
		//			taskToUpdate.Description = "Lorem Ipsum lorem lorem ipsum Lorem Ipsum lorem lorem ipsum";
		//			taskToUpdate.DueDate = DateTime.Now;
		//			await SetupMockUpdateTask(taskToUpdate);

		//			await mockContext.UpdateTaskAsync(taskToUpdate);
		//			TaskModel? updatedTask = await mockContext.ReadTaskAsync(taskToUpdateId, AdminId) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
		//			Assert.Multiple(() =>
		//			{
		//				Assert.That(updatedTask.Title, Is.EqualTo(taskToUpdate.Title));
		//				Assert.That(updatedTask.Description, Is.EqualTo(taskToUpdate.Description));
		//				Assert.That(updatedTask.DueDate, Is.EqualTo(taskToUpdate.DueDate));
		//			});
		//		}

		//		/// <summary>
		//		/// Tests <see cref="ContextOperations.UpdateTaskAsync(TaskModel)"/> - Update Task - operation with Null Object to throw exception.
		//		/// </summary>
		//		[Test]
		//		public async Task AttemptToUpdateTaskByNullObjectShouldThrowException()
		//		{
		//			int assertTaskId = IdOfFirstTodoList;
		//			using AutoMock mock = RegisterContextInstance();
		//			await SetupMockGetTask(assertTaskId);
		//			var mockContext = mock.Create<IContextOperations>();

		//			Assert.ThrowsAsync<ArgumentNullException>(async () => await mockContext.UpdateTaskAsync(NullTask));
		//		}

		//		/// <summary>
		//		/// Tests <see cref="ContextOperations.DeleteTaskAsync(int)"/> - Delete Task - operation as success attempt.
		//		/// </summary>
		//		/// <param name="assertTaskId">Valid Task Id, that should be deleted.</param>
		//		[TestCase(1)]
		//		[TestCase(2)]
		//		[TestCase(3)]
		//		[TestCase(4)]
		//		public async Task DeleteTaskShouldSucceed(int assertTaskId)
		//		{
		//			var itemsNumberBeforeDelete = this.AllTasks.Count;
		//			using AutoMock mock = RegisterContextInstance();
		//			await SetupMockGetTask(assertTaskId);
		//			await SetupMockDeleteTask();
		//			var mockContext = mock.Create<IContextOperations>();
		//			await mockContext.DeleteTaskAsync(assertTaskId, AdminId);
		//			var itemsNumberAfterDelete = this.AllTasks.Count;
		//			await SetupMockReadTaskReturnNull();

		//			this.AppDbContextMock.Verify(x => x.ReadTaskAsync(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
		//			this.AppDbContextMock.Verify(x => x.DeleteTaskAsync(It.IsAny<TaskModel>()), Times.Once);
		//			Assert.Multiple(() =>
		//			{
		//				Assert.That(itemsNumberAfterDelete, Is.Not.EqualTo(itemsNumberBeforeDelete));
		//				Assert.ThrowsAsync<InvalidOperationException>(async () => await mockContext.ReadTaskAsync(assertTaskId, AdminId));
		//			});
		//		}

		//		/// <summary>
		//		/// Tests <see cref="ContextOperations.DeleteTaskAsync(int)"/> - Delete Task - operation with Id Out Of Range to throw exception.
		//		/// </summary>
		//		/// <param name="id">Id value out of range.</param>
		//		[TestCase(-2)]
		//		[TestCase(-1)]
		//		public async Task DeleteTaskByIdOutOfRangeShouldThrowException(int id)
		//		{
		//			int assertTaskId = IdOfFirstTodoList;
		//			using AutoMock mock = RegisterContextInstance();
		//			await SetupMockGetTask(assertTaskId);
		//			var mockContext = mock.Create<IContextOperations>();

		//			Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => mockContext.DeleteTaskAsync(id, AdminId));
		//		}

		//		/// <summary>
		//		/// Tests <see cref="ContextOperations.DeleteTaskAsync(int)"/> - Delete Task - operation with Invalid Id to throw exception.
		//		/// </summary>
		//		/// <param name="id">Nonexisted Task Id.</param>
		//		[TestCase(40)]
		//		[TestCase(100)]
		//		public async Task DeleteTodoListByInvalidIdShouldThrowException(int id)
		//		{
		//			using AutoMock mock = RegisterContextInstance();
		//			await SetupMockReadTaskReturnNull();
		//			var mockContext = mock.Create<IContextOperations>();

		//			Assert.ThrowsAsync<InvalidOperationException>(() => mockContext.DeleteTodoListAsync(id, AdminId));
		//		}

		//private async Task SetupMockReadTaskForException()
		//{
		//	await Task.Run(() =>
		//	{
		//		this.AppDbContextMock.Setup(x => x.ReadTaskAsync(It.IsAny<int>(), It.IsAny<string>()));
		//	});
		//}

		//private async Task SetupMockReadTaskReturnNull()
		//{
		//	await Task.Run(() =>
		//	{
		//		this.AppDbContextMock.Setup(x => x.ReadTaskAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(() => NullTask);
		//	});
		//}

		//private async Task SetupMockUpdateTask(TaskModel taskToUpdate)
		//{
		//	await Task.Run(() =>
		//	{
		//		this.TaskRepoMock.Setup(x => x.Update(It.IsAny<TaskModel>()))
		//		.Callback<TaskModel>((task) =>
		//		{
		//			var tempTask = this.AllTasks.Find(task => task.Id == taskToUpdate.Id) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
		//			tempTask.Title = taskToUpdate.Title;
		//		}).Returns(Task.FromResult(1));
		//	});
		//}

		//private async Task SetupMockDeleteTask()
		//{
		//	await Task.Run(() => this.AppDbContextMock.Setup(x => x.DeleteTaskAsync(It.IsAny<TaskModel>())).Callback<TaskModel>((taskToDelete) => this.AllTasks.Remove(taskToDelete)).Returns(Task.FromResult(1)));
		//}
	}
}