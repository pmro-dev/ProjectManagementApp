﻿using App.Features.Tasks.Common.Interfaces;
using App.Features.TodoLists.Common.Interfaces;
using System.Globalization;
using App.Features.Tasks.Common;
using App.Features.TodoLists.Common.Models;
using App.Common.Helpers;

namespace App.Infrastructure.Databases.App.Seeds;

///<inheritdoc />
public class SeedData : ISeedData
{
	protected const string DueDateFormat = AttributesHelper.DataFormat;
	protected readonly IFormatProvider formatProvider = CultureInfo.InvariantCulture;

	public string AdminId { get; } = "adminId";

	///<inheritdoc />
	public ICollection<ITaskModel> AllTasks { get; set; }

	///<inheritdoc />
	public ICollection<ITodoListModel> TodoLists { get; set; }

	///<inheritdoc />
	public ICollection<ITaskModel> TasksUX { get; set; }

	///<inheritdoc />
	public ICollection<ITaskModel> TasksBackend { get; set; }

	///<inheritdoc />
	public ICollection<ITaskModel> TasksTesting { get; set; }

	///<inheritdoc />
	public ICollection<ITaskModel> TasksProjectManagement { get; set; }

	/// <summary>
	/// Seeds the data to properties.
	/// </summary>
	/// <exception cref="OperationCanceledException"></exception>
	public SeedData()
	{
		SeedTasks();
		SeedTodoLists();
		SeedAllTasks();

		if (AllTasks is null || TodoLists is null || TasksUX is null || TasksBackend is null || TasksTesting is null || TasksProjectManagement is null)
		{
			throw new InvalidOperationException("Critical error! Some property is not set with data!");
		}
	}

	private void SeedTasks()
	{
		TasksUX = new List<ITaskModel>()
		{
			new TaskModel{
				Title = "New Top Bar",
				Description = "Please design new top bar, with rounded corners and with white background.",
				DueDate = DateTime.ParseExact("2023-10-27T09:30", DueDateFormat, formatProvider),
				LastModificationDate = DateTime.ParseExact("2023-10-15T15:30", DueDateFormat, formatProvider),
				CreationDate = DateTime.ParseExact("2023-10-15T15:30", DueDateFormat, formatProvider),
				UserId = AdminId
			},
			new TaskModel{
				Title = "Customer Profile",
				Description = "Hi, we need to implement customer profile with view on his data.",
				DueDate = DateTime.ParseExact("2023-08-15T10:30", DueDateFormat, formatProvider),
				LastModificationDate = DateTime.ParseExact("2023-08-01T15:30", DueDateFormat, formatProvider),
				CreationDate = DateTime.ParseExact("2023-08-01T15:30", DueDateFormat, formatProvider),
				UserId = AdminId
			},
			new TaskModel{
				Title = "New Buttons Location",
				Description = "Move additional option buttons from left side to bottom-right, and resize it to be smmaller by 20%.",
				DueDate = DateTime.ParseExact("2023-02-04T14:00", DueDateFormat, formatProvider),
				LastModificationDate = DateTime.ParseExact("2023-01-15T15:30", DueDateFormat, formatProvider),
				CreationDate = DateTime.ParseExact("2023-01-15T15:30", DueDateFormat, formatProvider),
				UserId = AdminId
			},
		};

		TasksBackend = new List<ITaskModel>()
		{
			new TaskModel{
				Title = "Add Authentication",
				Description = "Need to implement OAuth2.",
				DueDate = DateTime.ParseExact("2023-09-24T09:30", DueDateFormat, formatProvider),
				LastModificationDate = DateTime.ParseExact("2023-01-15T15:30", DueDateFormat, formatProvider),
				CreationDate = DateTime.ParseExact("2023-01-15T15:30", DueDateFormat, formatProvider),
				UserId = AdminId
			},
			new TaskModel{
				Title = "Iimplement Database service",
				Description = "We need to implement Azure Cloude Database approach.",
				DueDate = DateTime.ParseExact("2023-09-13T10:30", DueDateFormat, formatProvider),
				LastModificationDate = DateTime.ParseExact("2023-09-02T15:30", DueDateFormat, formatProvider),
				CreationDate = DateTime.ParseExact("2023-09-02T15:30", DueDateFormat, formatProvider),
				UserId = AdminId
			},
			new TaskModel{
				Title = "Live Team Chat",
				Description = "Our client need internal chat for teams working on some projects.",
				DueDate = DateTime.ParseExact("2023-08-29T14:00", DueDateFormat, formatProvider),
				LastModificationDate = DateTime.ParseExact("2023-08-03T15:30", DueDateFormat, formatProvider),
				CreationDate = DateTime.ParseExact("2023-08-03T15:30", DueDateFormat, formatProvider),
				UserId = AdminId
			},
		};

		TasksTesting = new List<ITaskModel>()
		{
			new TaskModel{
				Title = "Integration Tests",
				Description = "Hi, we need to test integration between our app and new database.",
				DueDate = DateTime.ParseExact("2023-07-20T09:30", DueDateFormat,formatProvider),
				LastModificationDate = DateTime.ParseExact("2023-06-16T15:30", DueDateFormat, formatProvider),
				CreationDate = DateTime.ParseExact("2023-06-16T15:30", DueDateFormat, formatProvider),
				UserId = AdminId
			},
			new TaskModel{
				Title = "Map Feature",
				Description = "Needs to test map coordination's calculations.",
				DueDate = DateTime.ParseExact("2023-08-15T10:30", DueDateFormat, formatProvider),
				LastModificationDate = DateTime.ParseExact("2023-07-27T15:30", DueDateFormat, formatProvider),
				CreationDate = DateTime.ParseExact("2023-07-27T15:30", DueDateFormat, formatProvider),
				UserId = AdminId
			},
			new TaskModel{
				Title = "Helper Validator",
				Description = "Implement tests for user data validators.",
				DueDate = DateTime.ParseExact("2023-02-04T14:00", DueDateFormat, formatProvider),
				LastModificationDate = DateTime.ParseExact("2023-01-20T15:30", DueDateFormat, formatProvider),
				CreationDate = DateTime.ParseExact("2023-01-20T15:30", DueDateFormat, formatProvider),
				UserId = AdminId
			},
		};
		TasksProjectManagement = new List<ITaskModel>()
		{
			new TaskModel{
				Title = "Project Opportunities",
				Description = "Prepare and discusse future opportunities for project.",
				DueDate = DateTime.ParseExact("2023-10-15T09:30", DueDateFormat, formatProvider),
				LastModificationDate = DateTime.ParseExact("2023-10-02T15:30", DueDateFormat, formatProvider),
				CreationDate = DateTime.ParseExact("2023-10-02T15:30", DueDateFormat, formatProvider),
				UserId = AdminId
			},
			new TaskModel{
				Title = "Meeting with Client",
				Description = "Prepare raport with details for meeting, organize hotel and transport.",
				DueDate = DateTime.ParseExact("2023-10-10T10:30", DueDateFormat, formatProvider),
				LastModificationDate = DateTime.ParseExact("2023-09-19T15:30", DueDateFormat, formatProvider),
				CreationDate = DateTime.ParseExact("2023-09-19T15:30", DueDateFormat, formatProvider),
				UserId = AdminId
			},
			new TaskModel{
				Title = "Risks for Project X",
				Description = "Prepare full report about project x's risks.",
				DueDate = DateTime.ParseExact("2023-09-28T14:00", DueDateFormat, formatProvider),
				LastModificationDate = DateTime.ParseExact("2023-09-15T15:30", DueDateFormat, formatProvider),
				CreationDate = DateTime.ParseExact("2023-09-15T15:30", DueDateFormat, formatProvider),
				UserId = AdminId
			},
		};
	}

	private void SeedTodoLists()
	{
		TodoLists = new List<ITodoListModel>()
		{
			new TodoListModel{
				Title = "App UX",
				Tasks = TasksUX,
				UserId = AdminId
			},
			new TodoListModel{
				Title = "App Backend",
				Tasks = TasksBackend,
				UserId = AdminId
			},
			new TodoListModel{
				Title = "App Testing",
				Tasks = TasksTesting,
				UserId = AdminId
			},
			new TodoListModel{
				Title = "Project Management",
				Tasks = TasksProjectManagement,
				UserId = AdminId
			},
		};
	}

	private void SeedAllTasks()
	{
		AllTasks = new List<ITaskModel>();

		Parallel.ForEach(TasksUX, taskModel => AllTasks.Add(taskModel));
		Parallel.ForEach(TasksBackend, taskModel => AllTasks.Add(taskModel));
		Parallel.ForEach(TasksTesting, taskModel => AllTasks.Add(taskModel));
		Parallel.ForEach(TasksProjectManagement, taskModel => AllTasks.Add(taskModel));
		//AllTasks.AddRange(TasksUX);
		//AllTasks.AddRange(TasksBackend);
		//AllTasks.AddRange(TasksTesting);
		//AllTasks.AddRange(TasksProjectManagement);
	}
}