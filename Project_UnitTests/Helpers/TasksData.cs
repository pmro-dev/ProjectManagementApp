using Project_DomainEntities;
using Project_Main.Models.DataBases.AppData.DbSetup;

namespace Project_UnitTests.Helpers
{
    public static class TasksData
    {
		private static List<TaskModel> TasksUX { get; set; } = new();
		private static List<TaskModel> TasksBackend { get; set; } = new();
		private static List<TaskModel> TasksTesting { get; set; } = new();
		private static List<TaskModel> TasksProjectManagement { get; set; } = new();
		public static List<TodoListModel> TodoListsCollection { get; set; } = new();
		public static List<TaskModel> TasksCollection { get; set; } = new();

		private static readonly SeedData seedData = new();

		private const int startingIndex = 0;
		private const int BoundaryIndexForTasksUX = 3;
		private const int BoundaryIndexForTasksBackend = 6;
		private const int BoundaryIndexForTasksTesting = 9;
		private const int BoundaryIndexForTasksProjectManagement = 12;
		private static int startingIdForLists = 1;
		private static int numberOfAllTasks;
		private const string DueDateFormat = "yyyy MM dd HH':'mm";

        public static readonly object[] ValidTasksExamples = new object[]
        {
            new object[] { "New Top Bar", "Please design new top bar, with rounded corners and with white background.", DateTime.ParseExact("2023 10 27 09:30", DueDateFormat, null)},
            new object[] { "Customer Profile", "Hi, we need to implement customer profile with view on his data.", DateTime.ParseExact("2023 08 15 10:30", DueDateFormat, null)},
            new object[] { "Live Team Chat", "Our client need internal chat for teams working on some projects.", DateTime.ParseExact("2023 08 29 14:00", DueDateFormat, null)}
        };

        public static readonly object[] InvalidTasksExamples = new object[]
        {
            new object[] { "Ne", "Description says that Title for this task is too short.", DateTime.ParseExact("2023 10 27 09:30", DueDateFormat, null)},
            new object[] { "This is too long Title, This is too long title, This is too long title, This is too long title, This is too long title.", "Please design new top bar, with rounded corners and with white background.", DateTime.ParseExact("2023 10 27 09:30", DueDateFormat, null)},
            new object[] { "Title says that description is too short.", "Hi,", DateTime.ParseExact("2023 08 15 10:30", DueDateFormat, null)},
            new object[]
            {
                "Title says that the Description is too long",
                @"
					Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, 
					totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. 
					Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui 
					ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, 
					sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, 
					quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure 
					reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?
				", DateTime.ParseExact("2023 08 15 10:30", DueDateFormat, null)
            }
        };

		public static void PrepareData()
		{
			SetIdsForTasks(seedData);
			SetIdsForLists();
			SetTodoListIdsForTasks();
			TodoListsCollection = seedData.TodoLists;
			TasksCollection = SeedAllTasks();
		}

		private static List<TaskModel> SeedAllTasks()
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

		private static void SetIdsForTasks(SeedData seedData)
		{
			int iterator = 0;
			TasksUX = seedData.TasksUX;
			TasksBackend = seedData.TasksBackend;
			TasksTesting = seedData.TasksTesting;
			TasksProjectManagement = seedData.TasksProjectManagement;
			int taskIndex = startingIndex;
			numberOfAllTasks = TasksUX.Count + TasksTesting.Count + TasksBackend.Count + TasksProjectManagement.Count;
			int boundaryIndexForCurrentTasks = 0;

			while (iterator < numberOfAllTasks)
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

		private static void SetIdsForLists()
		{
			startingIdForLists = 1;

			foreach (var list in seedData.TodoLists)
			{
				list.Id = startingIdForLists++;
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
			else
			{
				throw new InvalidOperationException("Number of elements in tasks lists have to be the same to execute this part of code!");
			}
		}

		private const int OnePositionFurther = 1;
		private const string TitleSuffix = "NEW";

		public static TaskModel PrepareTask(string taskTitle, string taskDescription, DateTime taskDueDate)
		{
			return new TaskModel()
			{
				Id = TasksCollection.Last().Id + OnePositionFurther,
				Title = taskTitle + TitleSuffix,
				Description = taskDescription,
				DueDate = taskDueDate,
				TodoListId = 0
			};
		}

		public static List<TaskModel> PrepareTasksRange()
		{
			return new List<TaskModel>()
			{
				new TaskModel(){ Title = "First Range Test", Description = "First Description", DueDate = DateTime.ParseExact("2023 10 27 10:30", DueDateFormat, null)},
				new TaskModel(){ Title = "Second Range Test", Description = "Second Description", DueDate = DateTime.ParseExact("2023 08 22 09:00", DueDateFormat, null)},
				new TaskModel(){ Title = "Third Range Test", Description = "Third Description", DueDate = DateTime.ParseExact("2023 09 12 09:30", DueDateFormat, null)},
				new TaskModel(){ Title = "Fourth Range Test", Description = "Fourth Description", DueDate = DateTime.ParseExact("2023 11 07 12:30", DueDateFormat, null)},
				new TaskModel(){ Title = "Fifth Range Test", Description = "Fifth Description", DueDate = DateTime.ParseExact("2023 06 30 11:00", DueDateFormat, null)},
			};
		}

		public static readonly string RangeSuffix = "Range Test";

		public static TaskModel PrepareUpdatedTask(TaskModel taskToUpdate)
		{
			taskToUpdate.Title = "New Title Set";
			taskToUpdate.Description = "Lorem Ipsum lorem lorem ipsum Lorem Ipsum lorem lorem ipsum";
			taskToUpdate.DueDate = DateTime.Now;

			return taskToUpdate;
		}
	}
}
