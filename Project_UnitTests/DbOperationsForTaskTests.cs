using Moq;
using Project_UnitTests.Helpers;
using Project_DomainEntities;
using Project_Main.Models.DataBases.AppData;
using Project_DomainEntities.Helpers;

namespace Project_UnitTests
{
	/// <summary>
	/// Unit Test Class for Database tests with Mocking (DbContext) approach.
	/// </summary>
	public class DatabaseOperationsTests : BaseOperationsSetup
	{

		private static readonly object[] ValidTasksExamples = TasksData.ValidTasksExamples;

		[TestCaseSource(nameof(ValidTasksExamples))]
		public async Task AddTaskShouldSucceed(string taskTitle, string taskDescription, DateTime taskDueDate)
		{
			var assertTask = TasksData.PrepareTask(taskTitle, taskDescription, taskDueDate);

			await MockHelper.SetupAddTask(assertTask, TasksCollection, DbSetTaskMock, UnitOfWorkActionsForSaveChanges);
			await TaskRepo.AddAsync(assertTask);

			await DataUnitOfWork.SaveChangesAsync();
			await MockHelper.SetupGetTask(assertTask.Id, DbSetTaskMock, TasksCollection);
			
			TaskModel resultTask = await TaskRepo.GetAsync(assertTask.Id) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);

			Assert.Multiple(() =>
			{
				DbSetTaskMock.Verify(x => x.AddAsync(It.IsAny<TaskModel>(), It.IsAny<CancellationToken>()), Times.Once);
				DbSetTaskMock.Verify(x => x.FindAsync(It.IsAny<object>()), Times.Once);
				Assert.That(resultTask, Is.EqualTo(assertTask));
			});
		}

		[Test]
		public async Task AttemptToAddTaskAsNullObjectShouldThrowException()
		{
			TaskModel? assertNullTask = null;

			await MockHelper.SetupAddTask(assertNullTask!, TasksCollection, DbSetTaskMock, UnitOfWorkActionsForSaveChanges);

			Assert.ThrowsAsync<ArgumentNullException>(async () => await TaskRepo.AddAsync(assertNullTask!));
		}

		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		public async Task GetTaskShouldSucceed(int taskId)
		{
			var assertTask = TasksCollection.Single(t => t.Id == taskId);

			await MockHelper.SetupGetTask(assertTask.Id, DbSetTaskMock, TasksCollection);

			var resultTask = await TaskRepo.GetAsync(assertTask.Id) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);

			Assert.Multiple(() =>
			{
				DbSetTaskMock.Verify(x => x.FindAsync(It.IsAny<object>()), Times.Once);
				Assert.That(resultTask, Is.EqualTo(assertTask));
			});
		}

		[Test]
		public async Task GetAllTasksShouldSucceed()
		{
			var assertTasks = TasksCollection.ToList();

			var resultTasks = await TaskRepo.GetAllAsync();
			
			CollectionAssert.AreEqual(assertTasks, resultTasks);
		}

		[Test]
		[TestCase(2)]
		public async Task GetSingleTaskByFilterShouldSucceed(int taskId)
		{
			TaskModel? assertTask = TasksCollection.Find(t => t.Id == taskId);
			
			TaskModel? taskFromDb = await TaskRepo.GetSingleByFilterAsync(t => t.Id == taskId);

			Assert.That(assertTask, Is.EqualTo(taskFromDb));
		}

		[Test]
		[TestCase(TaskStatusHelper.TaskStatusType.InProgress)]
		[TestCase(TaskStatusHelper.TaskStatusType.NotStarted)]
		public async Task GetTasksByFilterShouldSucceed(TaskStatus taskStatus)
		{
			var assertTasks = TasksCollection.FindAll(t => t.Status.ToString() == taskStatus.ToString());
			
			var tasksFromDb = await TaskRepo.GetAllByFilterAsync(t => t.Status.ToString() == taskStatus.ToString());

			CollectionAssert.AreEqual(assertTasks, tasksFromDb);
		}

		[Test]
		[TestCase(null, typeof(ArgumentNullException))]
		[TestCase("", typeof(ArgumentNullException))]
		[TestCase(-1, typeof(ArgumentOutOfRangeException))]
		[TestCase(-5, typeof(ArgumentOutOfRangeException))]
		public async Task AttemptToGetTaskByInvalidIdShouldThrowException(object id, Type exceptionType)
		{
			int taskIdForMockSetup = TasksCollection.First().Id;

			await MockHelper.SetupGetTask(taskIdForMockSetup, DbSetTaskMock, TasksCollection);

			switch (exceptionType)
			{
				case Type argExNull when argExNull == typeof(ArgumentNullException):
					Assert.ThrowsAsync<ArgumentNullException>(async () => await TaskRepo.GetAsync(id!));
					break;
				case Type argExOutOfRange when argExOutOfRange == typeof(ArgumentOutOfRangeException):
					Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await TaskRepo.GetAsync(id!));
					break;
				case Type argEx when argEx == typeof(ArgumentException):
					Assert.ThrowsAsync<ArgumentException>(async () => await TaskRepo.GetAsync(id!));
					break;
			}
		}

		[Test]
		public async Task UpdateTaskShouldSucceed()
		{
			int taskToUpdateId = TasksCollection.First().Id;
			await MockHelper.SetupGetTask(taskToUpdateId, DbSetTaskMock, TasksCollection);
			TaskModel taskToUpdate = await TaskRepo.GetAsync(taskToUpdateId) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);

			TasksData.ModifyTaskData(taskToUpdate);
			await MockHelper.SetupUpdateTask(taskToUpdate, DbSetTaskMock, TasksCollection, UnitOfWorkActionsForSaveChanges);

			await TaskRepo.Update(taskToUpdate);
			await DataUnitOfWork.SaveChangesAsync();

			TaskModel updatedTask = await TaskRepo.GetAsync(taskToUpdateId) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);
			Assert.Multiple(() =>
			{
				Assert.That(updatedTask.Title, Is.EqualTo(taskToUpdate.Title));
				Assert.That(updatedTask.Description, Is.EqualTo(taskToUpdate.Description));
				Assert.That(updatedTask.DueDate, Is.EqualTo(taskToUpdate.DueDate));
			});
		}

		[Test]
		public async Task AttemptToUpdateTaskByNullObjectShouldThrowException()
		{
			TaskModel? NullTask = null;

			await MockHelper.SetupUpdateTask(NullTask!, DbSetTaskMock, TasksCollection, UnitOfWorkActionsForSaveChanges);

			Assert.ThrowsAsync<ArgumentNullException>(async () => await TaskRepo.Update(NullTask!));
		}

		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		public async Task DeleteTaskShouldSucceed(int assertTaskId)
		{
			var itemsNumberBeforeDelete = TasksCollection.Count;
			await MockHelper.SetupGetTask(assertTaskId, DbSetTaskMock, TasksCollection);
			TaskModel taskToRemove = await TaskRepo.GetAsync(assertTaskId) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);

			await MockHelper.SetupDeleteTask(taskToRemove, DbSetTaskMock, TasksCollection, UnitOfWorkActionsForSaveChanges);
			await TaskRepo.Remove(taskToRemove);
			await DataUnitOfWork.SaveChangesAsync();

			var itemsNumberAfterDelete = TasksCollection.Count;

			// I don't know why but I have to "refresh" TasksCollection because somehow it can get "deleted" task from memory.
			await MockHelper.SetupGetTask(assertTaskId, DbSetTaskMock, TasksCollection);

			TaskModel? resultOfTryToGetRemovedTask = await TaskRepo.GetAsync(assertTaskId);

			Assert.Multiple(() =>
			{
				DbSetTaskMock.Verify(x => x.FindAsync(It.IsAny<int>()), Times.Exactly(2));
				DbSetTaskMock.Verify(x => x.Remove(It.IsAny<TaskModel>()), Times.Once);
				Assert.That(itemsNumberAfterDelete, Is.Not.EqualTo(itemsNumberBeforeDelete));
				Assert.That(itemsNumberAfterDelete, Is.LessThan(itemsNumberBeforeDelete));
				Assert.That(resultOfTryToGetRemovedTask, Is.Null);
			});
		}

		[Test]
		public async Task AttemptToDeleteTaskByNullObjectShouldThrowException()
		{
			TaskModel? NullTask = null;

			await MockHelper.SetupDeleteTask(NullTask!, DbSetTaskMock, TasksCollection, UnitOfWorkActionsForSaveChanges);

			Assert.ThrowsAsync<ArgumentNullException>(async () => await TaskRepo.Remove(NullTask!));
		}

		[Test]
		public async Task AttemptToAddRangeOfTasksByNullObjectShouldThrowException()
		{
			IEnumerable<TaskModel>? nullRange = null;

			await MockHelper.SetupAddTasksRange(nullRange!, TasksCollection, DbSetTaskMock, UnitOfWorkActionsForSaveChanges);

			Assert.ThrowsAsync<ArgumentNullException>(async () => await TaskRepo.AddRangeAsync(nullRange!));
		}

		[Test]
		public async Task AddTasksAsRangeShouldSucceed()
		{
			var tasksRange = TasksData.PrepareTasksRange();

			await MockHelper.SetupAddTasksRange(tasksRange, TasksCollection, DbSetTaskMock, UnitOfWorkActionsForSaveChanges);
			await TaskRepo.AddRangeAsync(tasksRange);
			await DataUnitOfWork.SaveChangesAsync();

			var tasksFromDb = await TaskRepo.GetAllByFilterAsync(t => t.Title.Contains(TasksData.RangeSuffix));

			Assert.That(tasksFromDb.Count(), Is.EqualTo(tasksRange.Count));
		}

		[Test]
		public async Task AttempToAddTasksAsRangeByNullObjectShouldThrowException()
		{
			List<TaskModel>? nullRange = null;

			await MockHelper.SetupAddTasksRange(nullRange!, TasksCollection, DbSetTaskMock, UnitOfWorkActionsForSaveChanges);

			Assert.ThrowsAsync<ArgumentNullException>(async () => await TaskRepo.AddRangeAsync(nullRange!));
		}

		[Test]
		public async Task ContainsAnyShouldSucceed()
		{
			var result = await TaskRepo.ContainsAny();

			Assert.That(result, Is.True);
		}
	}
}