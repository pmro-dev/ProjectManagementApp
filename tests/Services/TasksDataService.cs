using App.Features.Tasks.Common.Models;
using App.Infrastructure.Databases.App.Seeds;
using Castle.Core.Internal;
using Project_UnitTests.Data;

namespace Project_UnitTests.Services;

public static class TasksDataService
{
	private static List<TaskModel> _tasksUX = new();
	private static List<TaskModel> _tasksBackend = new();
	private static List<TaskModel> _tasksTesting = new();
	private static List<TaskModel> _tasksProjectManagement = new();

	private static List<TaskModel> _tasksCollection = new();
	private static int _allTasksCount;
	private static readonly string _baseOfGuid = Guid.NewGuid().ToString();

	public static List<TaskModel> NewTasksRange { get; private set; } = TasksData.NewTasksRange.ToList();
	public static string TaskRangeSuffix { get; private set; } = TasksData.TaskRangeSuffix;

	public static readonly object[] ValidTasksForCreateOperation = TasksData.ValidTasksForCreateOperation;
	public static readonly object[] InvalidTasksForCreateOperation = TasksData.InvalidTasksForCreateOperation;
	public static readonly string AdminId = TasksData.AdminId;

	public static List<TaskModel> GetCollection(SeedData seedBaseData)
	{
		ArgumentNullException.ThrowIfNull(seedBaseData);

		PrepareData(seedBaseData);

		if (_tasksCollection.IsNullOrEmpty()) { throw new InvalidOperationException($"Something went wrong with {nameof(_tasksCollection)} preparation!"); }

		return _tasksCollection;
	}

	private static void PrepareData(SeedData seedBaseData)
	{
		SetIdsForTasks(seedBaseData);
		SetTodoListIdsForTasks();
		SeedAllTasks();
	}

	private static void SeedAllTasks()
	{
		_tasksCollection = new List<TaskModel>()
		{
			_tasksUX[0],
			_tasksUX[1],
			_tasksUX[2],
			_tasksBackend[0],
			_tasksBackend[1],
			_tasksBackend[2],
			_tasksTesting[0],
			_tasksTesting[1],
			_tasksTesting[2],
			_tasksProjectManagement[0],
			_tasksProjectManagement[1],
			_tasksProjectManagement[2],
		};
	}

	private static void SetIdsForTasks(SeedData seedBaseData)
	{
		ArgumentNullException.ThrowIfNull(seedBaseData);

		int iterator = 0;
		int startingIndex = 0;
		int taskIndex = startingIndex;
		int boundaryIndexForCurrentTasks = 0;

		_tasksUX = seedBaseData.TasksUX.ToList();
		_tasksBackend = seedBaseData.TasksBackend.ToList();
		_tasksTesting = seedBaseData.TasksTesting.ToList();
		_tasksProjectManagement = seedBaseData.TasksProjectManagement.ToList();
		_allTasksCount = _tasksUX.Count + _tasksTesting.Count + _tasksBackend.Count + _tasksProjectManagement.Count;

		const int BoundaryIndexForTasksUX = 3;
		const int BoundaryIndexForTasksBackend = 6;
		const int BoundaryIndexForTasksTesting = 9;
		const int BoundaryIndexForTasksProjectManagement = 12;

		while (iterator < _allTasksCount)
		{
			switch (iterator)
			{
				case < BoundaryIndexForTasksUX:
					boundaryIndexForCurrentTasks = _tasksUX.Count;
					_tasksUX[taskIndex++].Id = GetPreparedId(_baseOfGuid, iterator++);
					break;
				case < BoundaryIndexForTasksBackend:
					boundaryIndexForCurrentTasks = _tasksBackend.Count;
					_tasksBackend[taskIndex++].Id = GetPreparedId(_baseOfGuid, iterator++);
					break;
				case < BoundaryIndexForTasksTesting:
					boundaryIndexForCurrentTasks = _tasksTesting.Count;
					_tasksTesting[taskIndex++].Id = GetPreparedId(_baseOfGuid, iterator++);
					break;
				case < BoundaryIndexForTasksProjectManagement:
					boundaryIndexForCurrentTasks = _tasksProjectManagement.Count;
					_tasksProjectManagement[taskIndex++].Id = GetPreparedId(_baseOfGuid, iterator++);
					break;
			}

			if (taskIndex == boundaryIndexForCurrentTasks)
				taskIndex = startingIndex;
		}
	}

	private static void SetTodoListIdsForTasks()
	{
		var sameAmountOfTasks = _tasksUX.Count;
		string baseGuid = Guid.NewGuid().ToString();

		if (_tasksBackend.Count == sameAmountOfTasks &&
			_tasksTesting.Count == sameAmountOfTasks &&
			_tasksProjectManagement.Count == sameAmountOfTasks
			&& sameAmountOfTasks == 3)
		{
			for (int i = 0; i < 3; i++)
			{
				_tasksUX[i].TodoListId = GetPreparedId(baseGuid, 1);
				_tasksBackend[i].TodoListId = GetPreparedId(baseGuid, 2);
				_tasksTesting[i].TodoListId = GetPreparedId(baseGuid, 3);
				_tasksProjectManagement[i].TodoListId = GetPreparedId(baseGuid, 4);
			}
		}
		else { throw new InvalidOperationException("Number of elements in tasks lists have to be the same to execute this part of code!"); }
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
