using App.Common;
using App.Infrastructure.Databases.App;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Project_UnitTests.Helpers
{
	public static class GenericMockSetup<T> where T : BasicModelAbstract
	{
		public static void SetupDbContextSaveChangesAsync(Mock<CustomAppDbContext> AppDbContextMock, List<Action> DbOperationsToExecute)
		{
			AppDbContextMock.Setup(ctx => ctx.SaveChangesAsync(default))
				.Callback(() =>
				{
					foreach (Action dbOperation in DbOperationsToExecute)
					{
						dbOperation.Invoke();
					}
				}).ReturnsAsync(1);
		}

		public static async Task SetupAddEntity(T assertEntity, List<T> AllEntities, Mock<DbSet<T>> DbSetTaskMock, List<Action> DbOperationsToExecute)
		{
			await Task.Run(() =>
			{
				void action() => AllEntities.Add(assertEntity);

				DbSetTaskMock.Setup(x => x.AddAsync(It.IsAny<T>(), default))
					.Callback(() =>
					{
						DbOperationsToExecute.Add(action);
					});
			});
		}

		public static async Task SetupAddEntitiesRange(ICollection<T> range, List<T> AllEntities, Mock<DbSet<T>> DbSetEntityMock, List<Action> DbOperationsToExecute)
		{
			await Task.Run(() =>
			{
				void action() => AllEntities.AddRange(range);

				DbSetEntityMock.Setup(x => x.AddRangeAsync(It.IsAny<ICollection<T>>(), default))
					.Callback(() =>
					{
						DbOperationsToExecute.Add(action);
					}).Returns(Task.CompletedTask);
			});
		}

		public static async Task SetupGetEntity(int assertEntityId, Mock<DbSet<T>> DbSetEntityMock, List<T> AllEntities)
		{
			await Task.Run(() =>
			{
				DbSetEntityMock.Setup(x => x.FindAsync(It.IsAny<object>())).Returns(new ValueTask<T?>(AllEntities.Find(entity => entity.Id == assertEntityId)));
			});
		}

		public static async Task SetupUpdateEntity(T entityToUpdate, Mock<DbSet<T>> DbSetEntityMock, List<T> AllEntities, List<Action> DbOperationsToExecute)
		{
			await Task.Run(() =>
			{
				void action()
				{
					var tempEntity = AllEntities.Find(entity => entity.Id == entityToUpdate.Id) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
					tempEntity.Title = entityToUpdate.Title;
				}

				DbSetEntityMock.Setup(x => x.Update(It.IsAny<T>()))
					.Callback(() =>
					{
						DbOperationsToExecute.Add(action);
					});
			});
		}

		public static async Task SetupDeleteEntity(T entityToRemove, Mock<DbSet<T>> DbSetEntityMock, List<T> AllEntities, List<Action> DbOperationsToExecute)
		{
			await Task.Run(() =>
			{
				void action()
				{
					AllEntities.Remove(entityToRemove);
				}

				DbSetEntityMock.Setup(x => x.Remove(It.IsAny<T>()))
					.Callback(() =>
					{
						DbOperationsToExecute.Add(action);
					});
			});
		}
	}
}
