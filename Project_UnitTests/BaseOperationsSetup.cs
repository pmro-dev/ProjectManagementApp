﻿using Moq;
using Project_DomainEntities;
using Project_Main.Models.DataBases.AppData;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Project_UnitTests.Helpers;
using MockQueryable.Moq;
using Autofac.Extras.Moq;
using Autofac;
using Project_UnitTests.Services;
using Project_UnitTests.Data;
using ClassLibrary_SeedData;

namespace Project_UnitTests
{
    /// <summary>
    /// Class that setup basic properties such Tasks and seeds data / sets data format / sets mock for DbContext
    /// </summary>
    public class BaseOperationsSetup
    {
		#region PROPERTIES

		protected static List<TaskModel> TasksCollection { get; set; }
		protected List<TaskModel> DefaultTasksCollection { get; set; }
		protected static List<TodoListModel> TodoListsCollection { get; set; }
		protected List<TodoListModel> DefaultTodoListsCollection { get; set; }
		protected Mock<DbSet<TaskModel>> DbSetTaskMock { get; set; }
        protected Mock<DbSet<TodoListModel>> DbSetTodoListMock { get; set; }
		protected Mock<ILogger<TodoListRepository>> TodoListRepoLoggerMock { get; set; }
		protected Mock<ILogger<TaskRepository>> TaskRepoLoggerMock { get; set; }
		protected DataUnitOfWork DataUnitOfWork { get; set; }
		protected ITodoListRepository TodoListRepo { get; set; }
		protected ITaskRepository TaskRepo { get; set; }
		protected List<Action> DbOperationsToExecute { get; set; } = new();

		protected SeedData seedBaseData;
		#endregion


		#region FIELDS
		
		protected readonly string AdminId = TodoListsData.AdminId;

		#endregion


		[OneTimeSetUp]
		[Order(1)]
		public void SetupOnce()
        {
			seedBaseData = new SeedData();
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
			TasksCollection = new(DefaultTasksCollection);
			TodoListsCollection = new(DefaultTodoListsCollection);
		}

		private void SetupUnitOfWorkMocks()
		{
			DbSetTaskMock = TasksCollection.AsQueryable().BuildMockDbSet();
			DbSetTodoListMock = TodoListsCollection.AsQueryable().BuildMockDbSet();
			Mock<CustomAppDbContext> dbContextMock = new();

			dbContextMock.Setup(context => context.Set<TaskModel>())
				.Returns(DbSetTaskMock.Object);

			dbContextMock.Setup(context => context.Set<TodoListModel>())
				.Returns(DbSetTodoListMock.Object);

			GenericMockSetup<TaskModel>.SetupDbContextSaveChangesAsync(dbContextMock, DbOperationsToExecute);
			var tempTodoListRepo = new TodoListRepository(dbContextMock.Object, TodoListRepoLoggerMock.Object);
			var tempTaskRepo = new TaskRepository(dbContextMock.Object, TaskRepoLoggerMock.Object);
			DataUnitOfWork = new DataUnitOfWork(dbContextMock.Object, tempTodoListRepo, tempTaskRepo);
		}
	}
}
