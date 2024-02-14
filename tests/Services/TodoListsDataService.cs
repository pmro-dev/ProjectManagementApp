using App.Features.TodoLists.Common.Models;
using App.Infrastructure.Databases.App.Seeds;
using Project_UnitTests.Data;

namespace Project_UnitTests.Services;

public static class TodoListsDataService
{
	public static List<TodoListModel> NewTodoListsRange { get; private set; } = TodoListsData.NewTodoListsRange;

	public static string ListRangeSuffix { get; private set; } = TodoListsData.ListRangeSuffix;

	public static readonly object[] ValidSimpleTodoLists = TodoListsData.ValidSimpleTodoLists;

	public static readonly object[] InvalidTodoLists = TodoListsData.InvalidTodoLists;

	private static readonly string _baseOfGuid = Guid.NewGuid().ToString();

	public static List<TodoListModel> GetCollection(SeedData seedBaseData)
	{
		SetIdsForTodoLists(seedBaseData);
		return seedBaseData.TodoLists.ToList();
	}

	private static void SetIdsForTodoLists(SeedData seedData)
	{
		int startingId = 1;

		foreach (var todoList in seedData.TodoLists)
		{
			todoList.Id = GetPreparedId(_baseOfGuid, startingId++);
		}
	}

	private static Guid GetPreparedId(string baseGuid, int iterator)
	{
		if (iterator < 10)
		{
			return Guid.Parse(baseGuid.Insert(baseGuid.Length - 1, Convert.ToString(iterator)));
		}
		else
		{
			var temp = baseGuid.Insert(baseGuid.Length - 1, Convert.ToString(iterator));
			temp = temp.Remove(baseGuid.Length - 1, 1);

			return Guid.Parse(temp);
		}
	}
}
