﻿using App.Common.Interfaces;
using App.Infrastructure.Databases.App;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Project_UnitTests.Helpers;

public static class GenericMockSetup<IModel, TModel> where TModel : class, IModel where IModel : IBasicModelWithTitle
{
    private const int _valueIndicatedSuccess = 1;

    public static void SetupDbContextSaveChangesAsync(Mock<CustomAppDbContext> AppDbContextMock, List<Action> DbOperationsToExecute)
    {
        AppDbContextMock.Setup(ctx => ctx.SaveChangesAsync(default))
            .Callback(() =>
            {
                foreach (Action dbOperation in DbOperationsToExecute)
                {
                    dbOperation.Invoke();
                }
            }).ReturnsAsync(_valueIndicatedSuccess);
    }

    public static async Task SetupAddEntity(TModel assertEntity, List<TModel> AllEntities, Mock<DbSet<TModel>> DbSetTaskMock, List<Action> DbOperationsToExecute)
    {
        await Task.Run(() =>
        {
            void action() => AllEntities.Add(assertEntity);

            DbSetTaskMock.Setup(x => x.AddAsync(It.IsAny<TModel>(), default))
                .Callback(() =>
                {
                    DbOperationsToExecute.Add(action);
                });
        });
    }

    public static async Task SetupAddEntitiesRange(List<TModel> range, List<TModel> AllEntities, Mock<DbSet<TModel>> DbSetEntityMock, List<Action> DbOperationsToExecute)
    {
        await Task.Run(() =>
        {
            void action() => AllEntities.AddRange(range);

            DbSetEntityMock.Setup(x => x.AddRangeAsync(It.IsAny<List<TModel>>(), default))
                .Callback(() =>
                {
                    DbOperationsToExecute.Add(action);
                }).Returns(Task.CompletedTask);
        });
    }

    public static async Task SetupGetEntity(Guid assertEntityId, Mock<DbSet<TModel>> DbSetEntityMock, List<TModel> AllEntities)
    {
        await Task.Run(() =>
        {
            DbSetEntityMock.Setup(x => x.FindAsync(It.IsAny<object>())).Returns(new ValueTask<TModel?>(AllEntities.Find(entity => entity.Id.Equals(assertEntityId))));
        });
    }

    public static async Task SetupUpdateEntity(TModel entityToUpdate, Mock<DbSet<TModel>> DbSetEntityMock, List<TModel> AllEntities, List<Action> DbOperationsToExecute)
    {
        await Task.Run(() =>
        {
            void action()
            {
                var tempEntity = AllEntities.Find(entity => entity.Id == entityToUpdate.Id) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
                tempEntity.Title = entityToUpdate.Title;
            }

            DbSetEntityMock.Setup(x => x.Update(It.IsAny<TModel>()))
                .Callback(() =>
                {
                    DbOperationsToExecute.Add(action);
                });
        });
    }

    public static async Task SetupDeleteEntity(TModel entityToRemove, Mock<DbSet<TModel>> DbSetEntityMock, List<TModel> AllEntities, List<Action> DbOperationsToExecute)
    {
        await Task.Run(() =>
        {
            void action()
            {
                AllEntities.Remove(entityToRemove);
            }

            DbSetEntityMock.Setup(x => x.Remove(It.IsAny<TModel>()))
                .Callback(() =>
                {
                    DbOperationsToExecute.Add(action);
                });
        });
    }
}
