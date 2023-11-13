using App.Features.TodoLists.Common.Interfaces;
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
	private static void ModifyTodoListData(TodoListModel todoListToUpdate)
	{
		todoListToUpdate.Title = "New Title Set";
	}

	private static TodoListModel PrepareTodoList(string todoListTitle)
	{
		int IndexValueOne = 1;
		string TitleSuffix = "NEW";

		return new TodoListModel()
		{
			Id = TodoListsCollection.Last().Id + IndexValueOne,
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

		await GenericMockSetup<TodoListModel>.SetupAddEntity(assertTodoList, TodoListsCollection.Cast<TodoListModel>().ToList(), DbSetTodoListMock, DbOperationsToExecute);
		await TodoListRepo.AddAsync(assertTodoList);

		await DataUnitOfWork.SaveChangesAsync();
		await GenericMockSetup<TodoListModel>.SetupGetEntity(assertTodoList.Id, DbSetTodoListMock, TodoListsCollection.Cast<TodoListModel>().ToList());

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

		await GenericMockSetup<TodoListModel>.SetupGetEntity(assertTodoList.Id, DbSetTodoListMock, TodoListsCollection.Cast<TodoListModel>().ToList());

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

		await GenericMockSetup<TodoListModel>.SetupGetEntity(assertTodoList.Id, DbSetTodoListMock, TodoListsCollection.Cast<TodoListModel>().ToList());

		var resultTodoList = await TodoListRepo.GetWithDetailsAsync(assertTodoList.Id) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);

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
			Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await TodoListRepo.GetWithDetailsAsync(invalidTodoListId))
		);
	}

	[Test]
	public async Task GetAllTodoListsWithDetailsShouldSucceed()
	{
		var expected = TodoListsCollection;
		var actual = await TodoListRepo.GetAllWithDetailsAsync(AdminId);

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

		await GenericMockSetup<TodoListModel>.SetupAddEntity(assertNullTodoList!, TodoListsCollection.Cast<TodoListModel>().ToList(), DbSetTodoListMock, DbOperationsToExecute);

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
		ITodoListModel? assertTask = TodoListsCollection.Single(t => t.Id == todoListId);

		ITodoListModel? taskFromDb = await TodoListRepo.GetByFilterAsync(t => t.Id == todoListId);

		Assert.That(assertTask, Is.EqualTo(taskFromDb));
	}

	[Test]
	[TestCase(3)]
	[TestCase(6)]
	public async Task GetTodoListsByFilterShouldSucceed(int tasksCount)
	{
		var assertTodoLists = TodoListsCollection.Select(t => t.Tasks.Count == tasksCount);

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
		int todoListIdForMockSetup = TodoListsCollection.First().Id;

		await GenericMockSetup<TodoListModel>.SetupGetEntity(todoListIdForMockSetup, DbSetTodoListMock, TodoListsCollection.Cast<TodoListModel>().ToList());

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
		int todoListToUpdateId = TodoListsCollection.First().Id;
		await GenericMockSetup<TodoListModel>.SetupGetEntity(todoListToUpdateId, DbSetTodoListMock, TodoListsCollection.Cast<TodoListModel>().ToList());
		TodoListModel todoListToUpdate = await TodoListRepo.GetAsync(todoListToUpdateId) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);

		ModifyTodoListData(todoListToUpdate);
		await GenericMockSetup<TodoListModel>.SetupUpdateEntity(todoListToUpdate, DbSetTodoListMock, TodoListsCollection.Cast<TodoListModel>().ToList(), DbOperationsToExecute);

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

		await GenericMockSetup<TodoListModel>.SetupUpdateEntity(NullTodoList!, DbSetTodoListMock, TodoListsCollection.Cast<TodoListModel>().ToList(), DbOperationsToExecute);

		Assert.Throws<ArgumentNullException>(() => TodoListRepo.Update(NullTodoList!));
	}

	[Test]
	public async Task UpdateTodoListTasksShouldSucceed()
	{
		int todoListToUpdateId = TodoListsCollection.First().Id;
		var newTasks = TasksData.NewTasksRange;

		ITodoListModel? todoListToUpdate = await TodoListRepo.GetWithDetailsAsync(todoListToUpdateId) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
		todoListToUpdate.Tasks = newTasks;
		TodoListRepo.Update((TodoListModel)todoListToUpdate);
		await DataUnitOfWork.SaveChangesAsync();

		ITodoListModel? updatedTodoList = await TodoListRepo.GetWithDetailsAsync(todoListToUpdateId) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");

		CollectionAssert.AreEqual(updatedTodoList.Tasks, newTasks);
	}

	[TestCase(1)]
	[TestCase(2)]
	[TestCase(3)]
	[TestCase(4)]
	public async Task DeleteTodoListShouldSucceed(int assertTodoListId)
	{
		var itemsNumberBeforeDelete = TodoListsCollection.Count;
		await GenericMockSetup<TodoListModel>.SetupGetEntity(assertTodoListId, DbSetTodoListMock, TodoListsCollection.Cast<TodoListModel>().ToList());
		TodoListModel todoListToRemove = await TodoListRepo.GetAsync(assertTodoListId) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);

		await GenericMockSetup<TodoListModel>.SetupDeleteEntity(todoListToRemove, DbSetTodoListMock, TodoListsCollection.Cast<TodoListModel>().ToList(), DbOperationsToExecute);
		TodoListRepo.Remove(todoListToRemove);
		await DataUnitOfWork.SaveChangesAsync();

		var itemsNumberAfterDelete = TodoListsCollection.Count;

		// I don't know why but I have to "refresh" TodoListsCollection because somehow it can get "deleted" task from memory.
		await GenericMockSetup<TodoListModel>.SetupGetEntity(assertTodoListId, DbSetTodoListMock, TodoListsCollection.Cast<TodoListModel>().ToList());

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

		await GenericMockSetup<TodoListModel>.SetupDeleteEntity(NullTodoList!, DbSetTodoListMock, TodoListsCollection.Cast<TodoListModel>().ToList(), DbOperationsToExecute);

		Assert.Throws<ArgumentNullException>(() => TodoListRepo.Remove(NullTodoList!));
	}

	[Test]
	public async Task AttemptToAddRangeOfTodoListByNullObjectShouldThrowException()
	{
		ICollection<TodoListModel>? nullRange = null;

		await GenericMockSetup<TodoListModel>.SetupAddEntitiesRange(nullRange!, TodoListsCollection.Cast<TodoListModel>().ToList(), DbSetTodoListMock, DbOperationsToExecute);

		Assert.ThrowsAsync<ArgumentNullException>(async () => await TodoListRepo.AddRangeAsync(nullRange!));
	}

	[Test]
	public async Task AddTodoListsAsRangeShouldSucceed()
	{
		var todoListsRange = TodoListsDataService.NewTodoListsRange;

		await GenericMockSetup<TodoListModel>.SetupAddEntitiesRange(todoListsRange, TodoListsCollection.Cast<TodoListModel>().ToList(), DbSetTodoListMock, DbOperationsToExecute);
		await TodoListRepo.AddRangeAsync(todoListsRange);
		await DataUnitOfWork.SaveChangesAsync();

		var todoListsFromDb = await TodoListRepo.GetAllByFilterAsync(t => t.Title.Contains(TodoListsDataService.ListRangeSuffix));

		Assert.That(todoListsFromDb.Count, Is.EqualTo(todoListsRange.Count));
	}

	[Test]
	public async Task AttempToAddTodoListsAsRangeByNullObjectShouldThrowException()
	{
		List<TodoListModel>? nullRange = null;

		await GenericMockSetup<TodoListModel>.SetupAddEntitiesRange(nullRange!, TodoListsCollection.Cast<TodoListModel>().ToList(), DbSetTodoListMock, DbOperationsToExecute);

		Assert.ThrowsAsync<ArgumentNullException>(async () => await TodoListRepo.AddRangeAsync(nullRange!));
	}
}