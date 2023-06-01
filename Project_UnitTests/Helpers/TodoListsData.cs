using Project_DomainEntities;
using Project_Main.Models.DataBases.AppData.DbSetup;

namespace Project_UnitTests.Helpers
{
	public static class TodoListsData
	{
		public static List<TodoListModel> TodoListsCollection { get; set; } = new();

		public static readonly object[] ValidTodoLists = new object[]
		{
			new object[] { "App UX"},
			new object[] { "App Backend"},
			new object[] { "App Testing" },
			new object[] { "Project Management" }
		};

		public static void PrepareData(SeedData seedBaseData)
		{
			SetIdsForTodoLists(seedBaseData);
			TodoListsCollection = seedBaseData.TodoLists;
		}

		private static void SetIdsForTodoLists(SeedData seedData)
		{
			int startingIdForLists = 1;

			foreach (var todoList in seedData.TodoLists)
			{
				todoList.Id = startingIdForLists++;
			}
		}

		public static readonly string ListRangeSuffix = "List Range Test";

		public static List<TodoListModel> PrepareRange()
		{
			return new List<TodoListModel>()
			{
				new TodoListModel(){ Title = "First " + ListRangeSuffix, },
				new TodoListModel(){ Title = "Second " + ListRangeSuffix, },
				new TodoListModel(){ Title = "Third " + ListRangeSuffix, },
				new TodoListModel(){ Title = "Fourth " + ListRangeSuffix, },
				new TodoListModel(){ Title = "Fifth " + ListRangeSuffix, }
			};
		}
	}
}
