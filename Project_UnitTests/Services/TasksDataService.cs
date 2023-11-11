using Castle.Core.Internal;
using Project_DomainEntities;
using Project_UnitTests.Data;
using Web.Databases.App.Seeds;

namespace Project_UnitTests.Services
{
	public static class TasksDataService
    {
        private static List<ITaskModel> TasksUX { get; set; } = new List<ITaskModel>();
        private static List<ITaskModel> TasksBackend { get; set; } = new List<ITaskModel>();
		private static List<ITaskModel> TasksTesting { get; set; } = new List<ITaskModel>();
		private static List<ITaskModel> TasksProjectManagement { get; set; } = new List<ITaskModel>();

		private static List<ITaskModel> tasksCollection = new List<ITaskModel>();
		private static int AllTasksCount;

		public static List<ITaskModel> NewTasksRange { get; private set; } = TasksData.NewTasksRange.ToList();
        public static string TaskRangeSuffix { get; private set; } = TasksData.TaskRangeSuffix;

        public static readonly object[] ValidTasksForCreateOperation = TasksData.ValidTasksForCreateOperation;
        public static readonly object[] InvalidTasksForCreateOperation = TasksData.InvalidTasksForCreateOperation;
		public static readonly string AdminId = TasksData.AdminId;

		public static ICollection<ITaskModel> GetCollection(SeedData seedBaseData)
        {
            ArgumentNullException.ThrowIfNull(seedBaseData);

            PrepareData(seedBaseData);

            if (tasksCollection.IsNullOrEmpty()) { throw new InvalidOperationException($"Something went wrong with {nameof(tasksCollection)} preparation!"); }
			
            return tasksCollection;
		}

        private static void PrepareData(SeedData seedBaseData)
        {
			SetIdsForTasks(seedBaseData);
			SetTodoListIdsForTasks();
			SeedAllTasks();
		}

        private static void SeedAllTasks()
        {
            tasksCollection = new List<ITaskModel>()
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

        private static void SetIdsForTasks(SeedData seedBaseData)
        {
            ArgumentNullException.ThrowIfNull(seedBaseData);

            int iterator = 0;
            int startingIndex = 0;
            int taskIndex = startingIndex;
            int boundaryIndexForCurrentTasks = 0;

            TasksUX = seedBaseData.TasksUX.ToList();
            TasksBackend = seedBaseData.TasksBackend.ToList();
            TasksTesting = seedBaseData.TasksTesting.ToList();
            TasksProjectManagement = seedBaseData.TasksProjectManagement.ToList();
            AllTasksCount = TasksUX.Count + TasksTesting.Count + TasksBackend.Count + TasksProjectManagement.Count;

            const int BoundaryIndexForTasksUX = 3;
            const int BoundaryIndexForTasksBackend = 6;
            const int BoundaryIndexForTasksTesting = 9;
            const int BoundaryIndexForTasksProjectManagement = 12;

            while (iterator < AllTasksCount)
            {
                switch (iterator)
                {
                    case < BoundaryIndexForTasksUX:
                        boundaryIndexForCurrentTasks = TasksUX.Count;
                        TasksUX[taskIndex++].Id = iterator++;
                        break;
                    case < BoundaryIndexForTasksBackend:
                        boundaryIndexForCurrentTasks = TasksBackend.Count;
                        TasksBackend[taskIndex++].Id = iterator++;
                        break;
                    case < BoundaryIndexForTasksTesting:
                        boundaryIndexForCurrentTasks = TasksTesting.Count;
                        TasksTesting[taskIndex++].Id = iterator++;
                        break;
                    case < BoundaryIndexForTasksProjectManagement:
                        boundaryIndexForCurrentTasks = TasksProjectManagement.Count;
                        TasksProjectManagement[taskIndex++].Id = iterator++;
                        break;
                }

                if (taskIndex == boundaryIndexForCurrentTasks)
                {
                    taskIndex = 0;
                }
            }
        }

        private static void SetTodoListIdsForTasks()
        {
            var sameAmountOfTasks = TasksUX.Count;

            if (TasksBackend.Count == sameAmountOfTasks && TasksTesting.Count == sameAmountOfTasks && TasksProjectManagement.Count == sameAmountOfTasks)
            {
                for (int i = 0; i < 3; i++)
                {
                    TasksUX[i].TodoListId = 1;
                    TasksBackend[i].TodoListId = 2;
                    TasksTesting[i].TodoListId = 3;
                    TasksProjectManagement[i].TodoListId = 4;
                }
            }
            else { throw new InvalidOperationException("Number of elements in tasks lists have to be the same to execute this part of code!"); }
        }
    }
}
