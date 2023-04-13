﻿using Moq;
using Project_DomainEntities;
using Project_Main.Models.DataBases.AppData;
using Project_Main.Models.DataBases.General;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Project_UnitTests.Helpers;
using MockQueryable.Moq;
using Autofac.Extras.Moq;
using Autofac;

namespace Project_UnitTests
{
    /// <summary>
    /// Class that setup basic properties such Tasks and seeds data / sets data format / sets mock for DbContext
    /// </summary>
    public class BaseOperationsSetup
    {
		#region PROPERTIES

		protected List<TaskModel> TasksCollection { get; set; }
		protected List<TaskModel> DefaultTasksCollection { get; set; }
		protected List<TodoListModel> TodoListsCollection { get; set; }
		protected Mock<DbSet<TaskModel>> DbSetTaskMock { get; set; }
        protected Mock<DbSet<TodoListModel>> DbSetTodoListMock { get; set; }
		protected Mock<ILogger<TodoListRepository>> TodoListRepoLoggerMock { get; set; }
		protected Mock<ILogger<GenericRepository<TodoListModel>>> GenericTodoListRepoLoggerMock { get; set; }
		protected Mock<ILogger<TaskRepository>> TaskRepoLoggerMock { get; set; }
		protected Mock<ILogger<GenericRepository<TaskModel>>> GenericTaskRepoLoggerMock { get; set; }
		protected Mock<ILogger<CustomAppDbContext>> AppDbContextLoggerMock { get; set; }
		protected DataUnitOfWork DataUnitOfWork { get; set; }
		protected ITodoListRepository TodoListRepo { get; set; }
		protected ITaskRepository TaskRepo { get; set; }
		protected List<Action> UnitOfWorkActionsForSaveChanges { get; set; } = new();

		#endregion


		#region FIELDS
        protected const string DueDateFormat = "yyyy MM dd HH':'mm";
		protected const string AdminId = "adminId";

		#endregion


		[OneTimeSetUp]
		[Order(1)]
		public void SetupOnce()
        {
			TasksData.PrepareData();
			AssignDataToCollections();
			InitUnitOfWorkMocks();
		}

		private void AssignDataToCollections()
		{
			TodoListsCollection = TasksData.TodoListsCollection;
			DefaultTasksCollection = TasksData.TasksCollection;
		}

		private void InitUnitOfWorkMocks()
		{
			TodoListRepoLoggerMock = new();
			TaskRepoLoggerMock = new();
			AppDbContextLoggerMock = new();
			GenericTodoListRepoLoggerMock = new();
			GenericTaskRepoLoggerMock = new();
		}

		/// <summary>
		/// SetupOnEachTest mock appContext, TODOLists DbSet and Tasks DbSet for contextOperations.
		/// </summary>
		[SetUp]
        [Order(2)]
        public void SetupOnEachTest()
        {
			ClearUnitOfWorkActionsCache();
			SetupDefaultDataForTasksCollection();
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

		private void ClearUnitOfWorkActionsCache()
		{
			UnitOfWorkActionsForSaveChanges = new();
		}

		private void SetupDefaultDataForTasksCollection()
		{
			TasksCollection = new(DefaultTasksCollection);
		}

		private void SetupUnitOfWorkMocks()
		{
			DbSetTaskMock = TasksCollection.AsQueryable().BuildMockDbSet();
			DbSetTodoListMock = TodoListsCollection.AsQueryable().BuildMockDbSet();
			Mock<CustomAppDbContext> dbContextMock = new();

			dbContextMock.Setup(context => context.Set<TaskModel>())
				.Returns(DbSetTaskMock.Object);

			MockHelper.SetupDbContextSaveChangesAsync(dbContextMock, UnitOfWorkActionsForSaveChanges);
			var tempTodoListRepo = new TodoListRepository(dbContextMock.Object, TodoListRepoLoggerMock.Object);
			var tempTaskRepo = new TaskRepository(dbContextMock.Object, TaskRepoLoggerMock.Object);
			DataUnitOfWork = new(dbContextMock.Object, tempTodoListRepo, tempTaskRepo);
		}
	}
}
