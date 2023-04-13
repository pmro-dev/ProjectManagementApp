using Microsoft.EntityFrameworkCore;
using Moq;
using Project_DomainEntities;
using Project_Main.Models.DataBases.AppData;

namespace Project_UnitTests.Helpers
{
	public static class MockHelper
	{
		public static void SetupDbContextSaveChangesAsync(Mock<CustomAppDbContext> AppDbContextMock, List<Action> ActionsOnDbToSave)
		{
			AppDbContextMock.Setup(ctx => ctx.SaveChangesAsync(default))
				.Callback(() =>
				{
					foreach (Action dbOperation in ActionsOnDbToSave)
					{
						dbOperation.Invoke();
					}
				}).ReturnsAsync(1);
		}

		public static async Task SetupAddTask(TaskModel assertTask, List<TaskModel> AllTasks, Mock<DbSet<TaskModel>> DbSetTaskMock, List<Action> ActionsOnDbToSave)
		{
			await Task.Run(() =>
			{
				void action() => AllTasks.Add(assertTask);

				DbSetTaskMock.Setup(x => x.AddAsync(It.IsAny<TaskModel>(), default))
					.Callback(() =>
					{
						ActionsOnDbToSave.Add(action);
					});
			});
		}

		public static async Task SetupAddTasksRange(IEnumerable<TaskModel> range, List<TaskModel> AllTasks, Mock<DbSet<TaskModel>> DbSetTaskMock, List<Action> ActionsOnDbToSave)
		{
			await Task.Run(() =>
			{
				void action() => AllTasks.AddRange(range);

				DbSetTaskMock.Setup(x => x.AddRangeAsync(It.IsAny<IEnumerable<TaskModel>>(), default))
					.Callback(() =>
					{
						ActionsOnDbToSave.Add(action);
					}).Returns(Task.CompletedTask);
			});
		}

		public static async Task SetupGetTask(int assertTaskId, Mock<DbSet<TaskModel>> DbSetTaskMock, List<TaskModel> AllTasks)
		{
			await Task.Run(() =>
			{
				DbSetTaskMock.Setup(x => x.FindAsync(It.IsAny<object>())).Returns(new ValueTask<TaskModel?>(AllTasks.Find(t => t.Id == assertTaskId)));
			});
		}

		public static async Task SetupUpdateTask(TaskModel taskToUpdate, Mock<DbSet<TaskModel>> DbSetTaskMock, List<TaskModel> AllTasks, List<Action> ActionsOnDbToSave)
		{
			await Task.Run(() =>
			{
				void action()
				{
					var tempTask = AllTasks.Find(task => task.Id == taskToUpdate.Id) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
					tempTask.Title = taskToUpdate.Title;
				}

				DbSetTaskMock.Setup(x => x.Update(It.IsAny<TaskModel>()))
					.Callback(() =>
					{
						ActionsOnDbToSave.Add(action);
					});
			});
		}

		public static async Task SetupDeleteTask(TaskModel taskToRemove, Mock<DbSet<TaskModel>> DbSetTaskMock, List<TaskModel> AllTasks, List<Action> ActionsOnDbToSave)
		{
			await Task.Run(() =>
			{
				void action()
				{
					AllTasks.Remove(taskToRemove);
				}

				DbSetTaskMock.Setup(x => x.Remove(It.IsAny<TaskModel>()))
					.Callback(() =>
					{
						ActionsOnDbToSave.Add(action);
					});
			});
		}
	}
}
