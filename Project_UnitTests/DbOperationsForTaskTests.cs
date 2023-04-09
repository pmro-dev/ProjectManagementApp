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
			await MockHelper.SetupAddTask(assertTask, AllTasks, DbSetTaskMock, ActionsOnDbToSave);
			await taskRepo.AddAsync(assertTask);
			await dataUnitOfWork.SaveChangesAsync();

			await MockHelper.SetupGetTask(assertTask.Id, DbSetTaskMock, AllTasks);
			TaskModel tempTask = await taskRepo.GetAsync(assertTask.Id) ?? throw new AssertionException("Cannot find targeted Task in seeded data for unit tests.");

			Assert.Multiple(() =>
			{
				this.DbSetTaskMock.Verify(x => x.AddAsync(It.IsAny<TaskModel>(), It.IsAny<CancellationToken>()), Times.Once);
				this.DbSetTaskMock.Verify(x => x.FindAsync(It.IsAny<object>()), Times.Once);
				this.AppDbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
				Assert.That(tempTask, Is.EqualTo(assertTask));
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

			await MockHelper.SetupAddTask(assertNullTask!, AllTasks, DbSetTaskMock, ActionsOnDbToSave);

			Assert.ThrowsAsync<ArgumentNullException>(async () => await taskRepo.AddAsync(assertNullTask!));
		}

		/// <summary>
		/// Tests <see cref="ContextOperations"/> - Read Task - operation as success.
		/// </summary>
		/// <param name="taskId">Id of Task.</param>
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		public async Task GetTaskShouldSucceed(int taskId)
		{
			using AutoMock mock = RegisterContextInstance();
			var assertTask = this.AllTasks.Single(t => t.Id == taskId);
			await MockHelper.SetupGetTask(assertTask.Id, DbSetTaskMock, AllTasks);

			var dataUnitOfWork = mock.Create<IDataUnitOfWork>();
			var taskRepo = dataUnitOfWork.TaskRepository;

			var resultTask = await taskRepo.GetAsync(assertTask.Id) ?? throw new AssertionException("Cannot find targeted Task in seeded data for unit tests.");
			Assert.That(resultTask, Is.EqualTo(assertTask));
		}

		[Test]
		[TestCase(null, typeof(ArgumentNullException))]
		[TestCase("", typeof(ArgumentNullException))]
		[TestCase(-1, typeof(ArgumentOutOfRangeException))]
		[TestCase(-5, typeof(ArgumentOutOfRangeException))]
		public async Task AttemptToGetTaskByInvalidIdShouldThrowException(object id, Type exceptionType)
		{
			int taskIdForMockSetup = this.AllTasks.First().Id;

			using AutoMock mock = RegisterContextInstance();
			var dataUnitOfWork = mock.Create<IDataUnitOfWork>();
			var taskRepo = dataUnitOfWork.TaskRepository;

			await MockHelper.SetupGetTask(taskIdForMockSetup, DbSetTaskMock, AllTasks);

			if (Equals(exceptionType, typeof(ArgumentNullException)))
			{
				Assert.ThrowsAsync<ArgumentNullException>(async () => await taskRepo.GetAsync(id!));
			}
			else if (Equals(exceptionType, typeof(ArgumentOutOfRangeException)))
			{
				Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await taskRepo.GetAsync(id!));
			}
			else
			{
				Assert.ThrowsAsync<ArgumentException>(async () => await taskRepo.GetAsync(id!));
			}
		}

		/// <summary>
		/// Tests <see cref="ContextOperations.UpdateTaskAsync(TaskModel)"/> - Update Task - operation as success attempt.
		/// </summary>
		[Test]
		public async Task UpdateTaskShouldSucceed()
		{
			int taskToUpdateId = this.TodoLists.First().Id;
			using AutoMock mock = RegisterContextInstance();
			await MockHelper.SetupGetTask(taskToUpdateId, DbSetTaskMock, AllTasks);

			var dataUnitOfWork = mock.Create<IDataUnitOfWork>();
			var taskRepo = dataUnitOfWork.TaskRepository;

			TaskModel taskToUpdate = await taskRepo.GetAsync(taskToUpdateId) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
			taskToUpdate.Title = "New Title Set";
			taskToUpdate.Description = "Lorem Ipsum lorem lorem ipsum Lorem Ipsum lorem lorem ipsum";
			taskToUpdate.DueDate = DateTime.Now;

			await MockHelper.SetupUpdateTask(taskToUpdate, DbSetTaskMock, AllTasks, ActionsOnDbToSave);
			await taskRepo.Update(taskToUpdate);
			await dataUnitOfWork.SaveChangesAsync();

			TaskModel updatedTask = await taskRepo.GetAsync(taskToUpdateId) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
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
			int assertTaskId = this.TodoLists.First().Id;
			using AutoMock mock = RegisterContextInstance();
			await MockHelper.SetupGetTask(assertTaskId, DbSetTaskMock, AllTasks);

			var dataUnitOfWork = mock.Create<IDataUnitOfWork>();
			var taskRepo = dataUnitOfWork.TaskRepository;
			TaskModel? NullTask = null;

			Assert.ThrowsAsync<ArgumentNullException>(async () => await taskRepo.Update(NullTask!));
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
			var dataUnitOfWork = mock.Create<IDataUnitOfWork>();
			var taskRepo = dataUnitOfWork.TaskRepository;
			await MockHelper.SetupGetTask(assertTaskId, DbSetTaskMock, AllTasks);

			TaskModel taskToRemove = await taskRepo.GetAsync(assertTaskId) ?? throw new AssertionException("Cannot find targeted Task in seeded data for unit tests.");
			
			await MockHelper.SetupDeleteTask(taskToRemove, DbSetTaskMock, AllTasks, ActionsOnDbToSave);
			await taskRepo.Remove(taskToRemove);
			await dataUnitOfWork.SaveChangesAsync();

			var itemsNumberAfterDelete = this.AllTasks.Count;

			// I don't now why but I have to "refresh" this.AllTasks because somehow it can get "deleted" task from memory.
			await MockHelper.SetupGetTask(assertTaskId, DbSetTaskMock, AllTasks);
			TaskModel? resultOfGetRemovedTask = await TaskRepo.GetAsync(assertTaskId);

			Assert.Multiple(() =>
			{
				this.DbSetTaskMock.Verify(x => x.FindAsync(It.IsAny<int>()), Times.Exactly(2));
				this.DbSetTaskMock.Verify(x => x.Remove(It.IsAny<TaskModel>()), Times.Once);
				Assert.That(itemsNumberAfterDelete, Is.Not.EqualTo(itemsNumberBeforeDelete));
				Assert.That(itemsNumberAfterDelete, Is.LessThan(itemsNumberBeforeDelete));
				Assert.That(resultOfGetRemovedTask, Is.Null);
			});
		}

		public async Task AttemptToDeleteTaskByNullObjectShouldThrowException()
		{
			TaskModel? NullTask = null;
			using AutoMock mock = RegisterContextInstance();
			await MockHelper.SetupDeleteTask(NullTask!, DbSetTaskMock, AllTasks, ActionsOnDbToSave);

			var dataUnitOfWork = mock.Create<IDataUnitOfWork>();
			var taskRepo = dataUnitOfWork.TaskRepository;

			Assert.ThrowsAsync<ArgumentNullException>(async () => await taskRepo.Remove(NullTask!));
		}

		public async Task AttemptToUpdateTaskByNullObjectShouldThrowException()
		{
			TaskModel? NullTask = null;
			using AutoMock mock = RegisterContextInstance();
			await MockHelper.SetupUpdateTask(NullTask!, DbSetTaskMock, AllTasks, ActionsOnDbToSave);

			var dataUnitOfWork = mock.Create<IDataUnitOfWork>();
			var taskRepo = dataUnitOfWork.TaskRepository;

			Assert.ThrowsAsync<ArgumentNullException>(async () => await taskRepo.Update(NullTask!));
		}

		public async Task AttemptToAddRangeOfTasksByNullObjectShouldThrowException()
		{
			IEnumerable<TaskModel>? nullRange = null;
			using AutoMock mock = RegisterContextInstance();
			await MockHelper.SetupAddRangeOfTasks(nullRange!, AllTasks, DbSetTaskMock, ActionsOnDbToSave);

			var dataUnitOfWork = mock.Create<IDataUnitOfWork>();
			var taskRepo = dataUnitOfWork.TaskRepository;

			Assert.ThrowsAsync<ArgumentNullException>(async () => await taskRepo.AddRangeAsync(nullRange!));
		}
	}
}