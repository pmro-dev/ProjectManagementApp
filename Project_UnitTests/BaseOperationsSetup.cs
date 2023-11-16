using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Project_UnitTests.Helpers;
using MockQueryable.Moq;
using Autofac.Extras.Moq;
using Autofac;
using Project_UnitTests.Services;
using Project_UnitTests.Data;
using App.Infrastructure.Databases.App;
using App.Features.Tasks.Common;
using App.Infrastructure.Databases.App.Interfaces;
using App.Features.TodoLists.Common.Models;
using App.Features.TodoLists.Common.Interfaces;
using App.Infrastructure.Databases.App.Seeds;
using App.Features.Tasks.Common.Interfaces;

namespace Project_UnitTests;

/// <summary>
/// Class that setup basic properties such Tasks and seeds data / sets data format / sets mock for DbContext
/// </summary>
public class BaseOperationsSetup
{
	#region PROPERTIES

	protected static List<ITaskModel> TasksCollection { get; set; }
	protected List<ITaskModel> DefaultTasksCollection { get; set; }
	protected static List<ITodoListModel> TodoListsCollection { get; set; }
	protected List<ITodoListModel> DefaultTodoListsCollection { get; set; }
	protected Mock<DbSet<TaskModel>> DbSetTaskMock { get; set; }
	protected Mock<DbSet<TodoListModel>> DbSetTodoListMock { get; set; }
	protected Mock<ILogger<TodoListRepository>> TodoListRepoLoggerMock { get; set; }
	protected Mock<ILogger<TaskRepository>> TaskRepoLoggerMock { get; set; }
	protected Mock<ILogger<DataUnitOfWork>> DataUnitOfWorkLoggerMock { get; set; } = new();
	protected Mock<ILogger<SeedData>> SeedDataLoggerMock { get; set; } = new();
	protected DataUnitOfWork DataUnitOfWork { get; set; }
	protected ITodoListRepository TodoListRepo { get; set; }
	protected ITaskRepository TaskRepo { get; set; }
	protected List<Action> DbOperationsToExecute { get; set; } = new();

	protected SeedData seedBaseData;
	#endregion


	#region FIELDS

	protected const int FirstItemIndex = 0;
	protected readonly string AdminId = TodoListsData.AdminId;
	protected static readonly Index LastItemIndex = ^1;

	#endregion


	[OneTimeSetUp]
	[Order(1)]
	public void SetupOnce()
	{
		seedBaseData = new SeedData(SeedDataLoggerMock.Object);
		SetDefaultDataCollection(seedBaseData);
		InitUnitOfWorkMocks();
	}

	private void SetDefaultDataCollection(SeedData seedBaseData)
	{
		DefaultTodoListsCollection = TodoListsDataService.GetCollection(seedBaseData);
		DefaultTasksCollection = TasksDataService.GetCollection(seedBaseData);
	}

	private void InitUnitOfWorkMocks()
	{
		TodoListRepoLoggerMock = new();
		TaskRepoLoggerMock = new();
	}


	[SetUp]
	[Order(2)]
	public void SetupBeforeEachTest()
	{
		ClearUnitOfWorkOperationsCache();
		SetupDefaultDataForCollections();
		SetupUnitOfWorkMocks();

		using AutoMock mock = RegisterMockInstance();
		var unitOfWork = mock.Create<IDataUnitOfWork>();
		TaskRepo = unitOfWork.TaskRepository;
		TodoListRepo = unitOfWork.TodoListRepository;
	}

	private AutoMock RegisterMockInstance()
	{
		return AutoMock.GetLoose(builder =>
		{
			builder.RegisterInstance(DataUnitOfWork).As<IDataUnitOfWork>();
		});
	}

	private void ClearUnitOfWorkOperationsCache()
	{
		DbOperationsToExecute = new();
	}

	private void SetupDefaultDataForCollections()
	{
		TasksCollection = new List<ITaskModel>(DefaultTasksCollection);
		TodoListsCollection = new List<ITodoListModel>(DefaultTodoListsCollection);
	}

	private void SetupUnitOfWorkMocks()
	{
		DbSetTaskMock = TasksCollection.Cast<TaskModel>().AsQueryable().BuildMockDbSet();
		DbSetTodoListMock = TodoListsCollection.Cast<TodoListModel>().AsQueryable().BuildMockDbSet();
		Mock<CustomAppDbContext> dbContextMock = new();

		dbContextMock.Setup(context => context.Set<TaskModel>())
			.Returns(DbSetTaskMock.Object);

		dbContextMock.Setup(context => context.Set<TodoListModel>())
			.Returns(DbSetTodoListMock.Object);

		GenericMockSetup<ITaskModel, TaskModel>.SetupDbContextSaveChangesAsync(dbContextMock, DbOperationsToExecute);
		var tempTodoListRepo = new TodoListRepository(dbContextMock.Object, TodoListRepoLoggerMock.Object);
		var tempTaskRepo = new TaskRepository(dbContextMock.Object, TaskRepoLoggerMock.Object);
		DataUnitOfWork = new DataUnitOfWork(dbContextMock.Object, tempTodoListRepo, tempTaskRepo, DataUnitOfWorkLoggerMock.Object);
	}
}
