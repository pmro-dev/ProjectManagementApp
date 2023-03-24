using Moq;
using Autofac.Extras.Moq;
using Autofac;
using Microsoft.Extensions.Logging;
using TODO_Domain_Entities;
using TODO_List_ASPNET_MVC.Models.DataBases.AppDb;
using TODO_List_ASPNET_MVC.Models.DataBases.Repositories;

namespace TODO_App_Tests
{
    /// <summary>
    /// Class that setup basic properties such Tasks and seeds data / sets data format / sets mock for DbContext
    /// </summary>
    public class BaseOperationsSetup
    {
        protected List<TaskModel> AllTasks { get; set; }
        protected List<TodoListModel> TodoLists { get; set; }
        protected List<TaskModel> TasksUX { get; set; }
        protected List<TaskModel> TasksBackend { get; set; }
        protected List<TaskModel> TasksTesting { get; set; }
        protected List<TaskModel> TasksProjectManagement { get; set; }

        protected Mock<IAppDbContext> DbContextMock { get; set; }
        protected Mock<ILogger<ContextOperations>> LoggerMock { get; set; }
        protected ContextOperations ContextOperations { get; set; }

        private const int startingIndex = 0;
        private const int BoundaryIndexForTasksUX = 3;
        private const int BoundaryIndexForTasksBackend = 6;
        private const int BoundaryIndexForTasksTesting = 9;
        private const int BoundaryIndexForTasksProjectManagement = 12;
        protected const string DueDateFormat = "yyyy MM dd HH':'mm";
		protected const string AdminId = "adminId";

		private int startingIdForLists = 1;
        private int numberOfAllTasks;

        /// <summary>
        /// SetUp mock appContext, TODOLists DbSet and Tasks DbSet for contextOperations.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            SeedData seedData = new();

            SetIdsForTasks(seedData);

            this.TodoLists = seedData.TodoLists;

            SetIdsForLists();

            this.AllTasks = SeedAllTasks();

            var sameAmountOfTasks = this.TasksUX.Count;

            if (this.TasksBackend.Count == sameAmountOfTasks && this.TasksTesting.Count == sameAmountOfTasks && this.TasksProjectManagement.Count == sameAmountOfTasks)
            {
                for (int i = 0; i < 3; i++)
                {
                    this.TasksUX[i].TodoListId = 1;
                    this.TasksBackend[i].TodoListId = 2;
                    this.TasksTesting[i].TodoListId = 3;
                    this.TasksProjectManagement[i].TodoListId = 4;
                }
            }
            else
            {
                throw new InvalidOperationException("Number of elements in tasks lists have to be the same to execute this part of code!");
            }

            this.DbContextMock = new Mock<IAppDbContext>();
            this.LoggerMock = new Mock<ILogger<ContextOperations>>();
            this.ContextOperations = new ContextOperations(this.DbContextMock.Object, this.LoggerMock.Object);

            using AutoMock mock = AutoMock.GetLoose(cfg => cfg.RegisterInstance(this.ContextOperations).As<IContextOperations>());
        }

        private List<TaskModel> SeedAllTasks()
        {
            return new List<TaskModel>()
            {
                TasksUX[0],
                TasksUX[1],
                TasksUX[2],
                TasksBackend[0],
                TasksBackend[1],
                TasksBackend[2],
                TasksTesting[0],
                TasksTesting[1],
                TasksTesting[2],
                TasksProjectManagement[0],
                TasksProjectManagement[1],
                TasksProjectManagement[2],
            };
        }

        private void SetIdsForTasks(SeedData seedData)
        {
            int iterator = 0;
            this.TasksUX = seedData.TasksUX;
            this.TasksBackend = seedData.TasksBackend;
            this.TasksTesting = seedData.TasksTesting;
            this.TasksProjectManagement = seedData.TasksProjectManagement;
            int taskIndex = startingIndex;
            numberOfAllTasks = this.TasksUX.Count + this.TasksTesting.Count + this.TasksBackend.Count + this.TasksProjectManagement.Count;
            int boundaryIndexForCurrentTasks = 0;

            while (iterator < numberOfAllTasks)
            {
                switch (iterator)
                {
                    case < BoundaryIndexForTasksUX:
                        boundaryIndexForCurrentTasks = this.TasksUX.Count;
                        this.TasksUX[taskIndex++].Id = iterator++;
                        break;
                    case < BoundaryIndexForTasksBackend:
                        boundaryIndexForCurrentTasks = this.TasksBackend.Count;
                        this.TasksBackend[taskIndex++].Id = iterator++;
                        break;
                    case < BoundaryIndexForTasksTesting:
                        boundaryIndexForCurrentTasks = this.TasksTesting.Count;
                        this.TasksTesting[taskIndex++].Id = iterator++;
                        break;
                    case < BoundaryIndexForTasksProjectManagement:
                        boundaryIndexForCurrentTasks = this.TasksProjectManagement.Count;
                        this.TasksProjectManagement[taskIndex++].Id = iterator++;
                        break;
                }

                if (taskIndex == boundaryIndexForCurrentTasks)
                {
                    taskIndex = 0;
                }
            }
        }

        private void SetIdsForLists()
        {
            startingIdForLists = 1;

            foreach (var list in this.TodoLists)
            {
                list.Id = startingIdForLists++;
            }
        }
    }
}
