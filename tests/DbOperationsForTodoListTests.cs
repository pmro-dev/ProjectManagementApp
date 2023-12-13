using App.Features.TodoLists.Common.Models;
using Moq;
using Project_UnitTests.Data;
using Project_UnitTests.Helpers;
using Project_UnitTests.Services;

namespace Project_UnitTests;

/// <summary>
/// Unit Test Class for Database tests with Mocking (DbContext) approach.
/// </summary>
public class DbOperationsForTodoListTests : BaseOperationsSetup
{
	private static readonly Index LastIndex = ^1;
	private static readonly Index FirstIndex = 0;

	private static void ModifyTodoListData(TodoListModel todoListToUpdate)
	{
		todoListToUpdate.Title = "New Title Set";
	}

	private static TodoListModel PrepareTodoList(string todoListTitle)
	{
		int positionIncrementor = 1;
		string TitleSuffix = "NEW";

		return new TodoListModel()
		{
			Id = TodoListsCollection[LastIndex].Id + positionIncrementor,
			Title = todoListTitle + TitleSuffix,
		};
	}

	private static readonly object[] ValidTodoLists = TodoListsDataService.ValidSimpleTodoLists;

	[Test]
	public async Task ContainsAnyShouldSucceed()
	{
		var result = await TodoListRepo.ContainsAny();

		Assert.That(result, Is.True);
	}

	[TestCaseSource(nameof(ValidTodoLists))]
	public async Task AddTodoListShouldSucceed(string todoListTitle)
	{
		var assertTodoList = PrepareTodoList(todoListTitle);

		await GenericMockSetup<TodoListModel, TodoListModel>.SetupAddEntity(assertTodoList, TodoListsCollection, DbSetTodoListMock, DbOperationsToExecute);
		await TodoListRepo.AddAsync(assertTodoList);

		await DataUnitOfWork.SaveChangesAsync();
		await GenericMockSetup<TodoListModel, TodoListModel>.SetupGetEntity(assertTodoList.Id, DbSetTodoListMock, TodoListsCollection);

		TodoListModel resultTodoList = await TodoListRepo.GetAsync(assertTodoList.Id) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);

		Assert.Multiple(() =>
		{
			DbSetTodoListMock.Verify(x => x.AddAsync(It.IsAny<TodoListModel>(), It.IsAny<CancellationToken>()), Times.Once);
			DbSetTodoListMock.Verify(x => x.FindAsync(It.IsAny<object>()), Times.Once);
			Assert.That(resultTodoList, Is.EqualTo(assertTodoList));
		});
	}

	[TestCase(1)]
	[TestCase(2)]
	[TestCase(3)]
	[TestCase(4)]
	public async Task GetTodoListShouldSucceed(int todoListId)
	{
		var assertTodoList = TodoListsCollection.Single(t => t.Id == todoListId);

		await GenericMockSetup<TodoListModel, TodoListModel>.SetupGetEntity(assertTodoList.Id, DbSetTodoListMock, TodoListsCollection);

		var resultTodoList = await TodoListRepo.GetAsync(assertTodoList.Id) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);

		Assert.Multiple(() =>
		{
			DbSetTodoListMock.Verify(x => x.FindAsync(It.IsAny<object>()), Times.Once);
			Assert.That(resultTodoList, Is.EqualTo(assertTodoList));
		});
	}

	[TestCase(1)]
	[TestCase(2)]
	[TestCase(3)]
	[TestCase(4)]
	public async Task GetTodoListWithDetailsShouldSucceed(int todoListId)
	{
		var assertTodoList = TodoListsCollection.Single(t => t.Id == todoListId);

		await GenericMockSetup<TodoListModel, TodoListModel>.SetupGetEntity(assertTodoList.Id, DbSetTodoListMock, TodoListsCollection);

		var resultTodoList = await TodoListRepo.GetSingleWithDetailsAsync(assertTodoList.Id) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);

		Assert.Multiple(() =>
		{
			CollectionAssert.AreEqual(resultTodoList.Tasks, assertTodoList.Tasks);
			Assert.That(resultTodoList, Is.EqualTo(assertTodoList));
		});
	}

	[TestCase(-1)]
	[TestCase(-10)]
	public Task AttemptToGetTodoListWithDetailsByInvalidIdShouldThrowException(int invalidTodoListId)
	{
		return Task.Run(() =>
			Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await TodoListRepo.GetSingleWithDetailsAsync(invalidTodoListId))
		);
	}

	[Test]
	public async Task GetAllTodoListsWithDetailsShouldSucceed()
	{
		var expected = TodoListsCollection;
		var actual = await TodoListRepo.GetMultipleWithDetailsAsync(AdminId);

		Assert.Multiple(() =>
		{
			Assert.That(actual, Is.Not.EqualTo(null));
			Assert.That(expected.SequenceEqual(actual), Is.True);
		});
	}

	[Test]
	public async Task AttemptToAddTodoListAsNullObjectShouldThrowException()
	{
		TodoListModel? assertNullTodoList = null;

		await GenericMockSetup<TodoListModel, TodoListModel>.SetupAddEntity(assertNullTodoList!, TodoListsCollection, DbSetTodoListMock, DbOperationsToExecute);

		Assert.ThrowsAsync<ArgumentNullException>(async () => await TodoListRepo.AddAsync(assertNullTodoList!));
	}

	[Test]
	public async Task GetAllTodoListsShouldSucceed()
	{
		var assertTodoLists = TodoListsCollection.ToList();

		var resultTodoLists = await TodoListRepo.GetAllAsync();

		CollectionAssert.AreEqual(assertTodoLists, resultTodoLists);
	}

	[Test]
	[TestCase(2)]
	public async Task GetSingleTodoListByFilterShouldSucceed(int todoListId)
	{
		TodoListModel? assertTask = TodoListsCollection.Single(t => t.Id == todoListId);

		TodoListModel? taskFromDb = await TodoListRepo.GetByFilterAsync(t => t.Id == todoListId);

		Assert.That(assertTask, Is.EqualTo(taskFromDb));
	}

	[Test]
	[TestCase(3)]
	[TestCase(6)]
	public async Task GetTodoListsByFilterShouldSucceed(int tasksCount)
	{
		var assertTodoLists = TodoListsCollection.Where(t => t.Tasks.Count == tasksCount);

		var todoListsFromDb = await TodoListRepo.GetAllByFilterAsync(t => t.Tasks.Count == tasksCount);

		CollectionAssert.AreEqual(assertTodoLists, todoListsFromDb);
	}

	[Test]
	[TestCase(null, typeof(ArgumentNullException))]
	[TestCase("", typeof(ArgumentNullException))]
	[TestCase(-1, typeof(ArgumentOutOfRangeException))]
	[TestCase(-5, typeof(ArgumentOutOfRangeException))]
	public async Task AttemptToGetTodoListByInvalidIdShouldThrowException(object id, Type exceptionType)
	{
		int todoListIdForMockSetup = TodoListsCollection[FirstIndex].Id;

		await GenericMockSetup<TodoListModel, TodoListModel>.SetupGetEntity(todoListIdForMockSetup, DbSetTodoListMock, TodoListsCollection);

		switch (exceptionType)
		{
			case Type argExNull when argExNull == typeof(ArgumentNullException):
				Assert.ThrowsAsync<ArgumentNullException>(async () => await TodoListRepo.GetAsync(id!));
				break;
			case Type argExOutOfRange when argExOutOfRange == typeof(ArgumentOutOfRangeException):
				Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await TodoListRepo.GetAsync(id!));
				break;
			case Type argEx when argEx == typeof(ArgumentException):
				Assert.ThrowsAsync<ArgumentException>(async () => await TodoListRepo.GetAsync(id!));
				break;
		}
	}

	[Test]
	public async Task UpdateTodoListShouldSucceed()
	{
		int todoListToUpdateId = TodoListsCollection[FirstIndex].Id;
		await GenericMockSetup<TodoListModel, TodoListModel>.SetupGetEntity(todoListToUpdateId, DbSetTodoListMock, TodoListsCollection);
		TodoListModel todoListToUpdate = await TodoListRepo.GetAsync(todoListToUpdateId) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);

		ModifyTodoListData(todoListToUpdate);
		await GenericMockSetup<TodoListModel, TodoListModel>.SetupUpdateEntity(todoListToUpdate, DbSetTodoListMock, TodoListsCollection, DbOperationsToExecute);

		TodoListRepo.Update(todoListToUpdate);
		await DataUnitOfWork.SaveChangesAsync();

		TodoListModel updatedTask = await TodoListRepo.GetAsync(todoListToUpdateId) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);
		Assert.Multiple(() =>
		{
			Assert.That(updatedTask.Title, Is.EqualTo(todoListToUpdate.Title));
		});
	}

	[Test]
	public async Task AttemptToUpdateTodoListByNullObjectShouldThrowException()
	{
		TodoListModel? NullTodoList = null;

		await GenericMockSetup<TodoListModel, TodoListModel>.SetupUpdateEntity(NullTodoList!, DbSetTodoListMock, TodoListsCollection, DbOperationsToExecute);

		Assert.Throws<ArgumentNullException>(() => TodoListRepo.Update(NullTodoList!));
	}

	[Test]
	public async Task UpdateTodoListTasksShouldSucceed()
	{
		int todoListToUpdateId = TodoListsCollection[FirstIndex].Id;
		var newTasks = TasksData.NewTasksRange;

		TodoListModel? todoListToUpdate = await TodoListRepo.GetSingleWithDetailsAsync(todoListToUpdateId) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
		todoListToUpdate.Tasks = newTasks;
		TodoListRepo.Update(todoListToUpdate);
		await DataUnitOfWork.SaveChangesAsync();

		TodoListModel? updatedTodoList = await TodoListRepo.GetSingleWithDetailsAsync(todoListToUpdateId) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");

		CollectionAssert.AreEqual(updatedTodoList.Tasks, newTasks);
	}

	[TestCase(1)]
	[TestCase(2)]
	[TestCase(3)]
	[TestCase(4)]
	public async Task DeleteTodoListShouldSucceed(int assertTodoListId)
	{
		var itemsNumberBeforeDelete = TodoListsCollection.Count;
		await GenericMockSetup<TodoListModel, TodoListModel>.SetupGetEntity(assertTodoListId, DbSetTodoListMock, TodoListsCollection);
		TodoListModel todoListToRemove = await TodoListRepo.GetAsync(assertTodoListId) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);

		await GenericMockSetup<TodoListModel, TodoListModel>.SetupDeleteEntity(todoListToRemove, DbSetTodoListMock, TodoListsCollection, DbOperationsToExecute);
		TodoListRepo.Remove(todoListToRemove);
		await DataUnitOfWork.SaveChangesAsync();

		var itemsNumberAfterDelete = TodoListsCollection.Count;

		// I don't know why but I have to "refresh" TodoListsCollection because somehow it can get "deleted" task from memory.
		await GenericMockSetup<TodoListModel, TodoListModel>.SetupGetEntity(assertTodoListId, DbSetTodoListMock, TodoListsCollection);

		TodoListModel? resultOfTryToGetRemovedTask = await TodoListRepo.GetAsync(assertTodoListId);

		Assert.Multiple(() =>
		{
			DbSetTodoListMock.Verify(x => x.FindAsync(It.IsAny<int>()), Times.Exactly(2));
			DbSetTodoListMock.Verify(x => x.Remove(It.IsAny<TodoListModel>()), Times.Once);
			Assert.That(itemsNumberAfterDelete, Is.Not.EqualTo(itemsNumberBeforeDelete));
			Assert.That(itemsNumberAfterDelete, Is.LessThan(itemsNumberBeforeDelete));
			Assert.That(resultOfTryToGetRemovedTask, Is.Null);
		});
	}

	[Test]
	public async Task AttemptToDeleteTodoListByNullObjectShouldThrowException()
	{
		TodoListModel? NullTodoList = null;

		await GenericMockSetup<TodoListModel, TodoListModel>.SetupDeleteEntity(NullTodoList!, DbSetTodoListMock, TodoListsCollection, DbOperationsToExecute);

		Assert.Throws<ArgumentNullException>(() => TodoListRepo.Remove(NullTodoList!));
	}

	[Test]
	public async Task AttemptToAddRangeOfTodoListByNullObjectShouldThrowException()
	{
		List<TodoListModel>? nullRange = null;

		await GenericMockSetup<TodoListModel, TodoListModel>.SetupAddEntitiesRange(nullRange!, TodoListsCollection, DbSetTodoListMock, DbOperationsToExecute);

		Assert.ThrowsAsync<ArgumentNullException>(async () => await TodoListRepo.AddRangeAsync(nullRange!));
	}

	[Test]
	public async Task AddTodoListsAsRangeShouldSucceed()
	{
		var todoListsRange = TodoListsDataService.NewTodoListsRange;

		await GenericMockSetup<TodoListModel, TodoListModel>.SetupAddEntitiesRange(todoListsRange, TodoListsCollection, DbSetTodoListMock, DbOperationsToExecute);
		await TodoListRepo.AddRangeAsync(todoListsRange);
		await DataUnitOfWork.SaveChangesAsync();

		var todoListsFromDb = await TodoListRepo.GetAllByFilterAsync(t => t.Title.Contains(TodoListsDataService.ListRangeSuffix));

		Assert.That(todoListsFromDb, Has.Count.EqualTo(todoListsRange.Count));
	}
}