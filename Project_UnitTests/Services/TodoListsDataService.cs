using ClassLibrary_SeedData;
using Project_DomainEntities;
using Project_UnitTests.Data;

namespace Project_UnitTests.Services
{
    public static class TodoListsDataService
    {
        public static List<TodoListModel> NewTodoListsRange { get; private set; } = TodoListsData.NewTodoListsRange;

        public static string ListRangeSuffix { get; private set; } = TodoListsData.ListRangeSuffix;

        public static readonly object[] ValidSimpleTodoLists = TodoListsData.ValidSimpleTodoLists;

        public static readonly object[] InvalidTodoLists = TodoListsData.InvalidTodoLists;

		public static List<TodoListModel> GetCollection(SeedData seedBaseData)
        {
            SetIdsForTodoLists(seedBaseData);
            return seedBaseData.TodoLists;
        }

        private static void SetIdsForTodoLists(SeedData seedData)
        {
            int startingIdForLists = 1;

            foreach (var todoList in seedData.TodoLists)
            {
                todoList.Id = startingIdForLists++;
            }
        }
    }
}
