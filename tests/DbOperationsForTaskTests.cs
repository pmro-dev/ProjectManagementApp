using Moq;
using Project_UnitTests.Helpers;
using Project_UnitTests.Services;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.Models;
using static App.Features.Tasks.Common.Helpers.TaskStatusHelper;

namespace Project_UnitTests;

/// <summary>
/// Unit Test Class for Database tests with Mocking (DbContext) approach.
/// </summary>
public class DatabaseOperationsTests : BaseOperationsSetup
{
    private static readonly Index _firstIndex = 1;
    private static readonly Index _lastIndex = ^1;
	private static readonly int _valueOneIndicator = 1;
	private static readonly int _defaultId = 0;

	private static void ModifyTaskData(TaskModel taskToUpdate)
    {
        taskToUpdate.Title = "New Title Set";
        taskToUpdate.Description = "Lorem Ipsum lorem lorem ipsum Lorem Ipsum lorem lorem ipsum";
        taskToUpdate.Deadline = DateTime.Now;
    }

    private static TaskModel PrepareTask(string taskTitle, string taskDescription, DateTime taskDueDate)
    {
        string TitleSuffix = "NEW";

        return new TaskModel()
        {
            Id = TasksCollection[_lastIndex].Id + _valueOneIndicator,
            Title = taskTitle + TitleSuffix,
            Description = taskDescription,
            Deadline = taskDueDate,
            TodoListId = _defaultId
		};
    }

    private static readonly object[] ValidTasksExamples = TasksDataService.ValidTasksForCreateOperation;

    [Test]
    public async Task ContainsAnyShouldSucceed()
    {
        bool result = await TaskRepo.ContainsAny();

        Assert.That(result, Is.True);
    }

    [TestCaseSource(nameof(ValidTasksExamples))]
    public async Task AddTaskShouldSucceed(string taskTitle, string taskDescription, DateTime taskDueDate)
    {
        var assertTask = PrepareTask(taskTitle, taskDescription, taskDueDate);

        await GenericMockSetup<ITaskModel, TaskModel>.SetupAddEntity(assertTask, TasksCollection, DbSetTaskMock, DbOperationsToExecute);
        await TaskRepo.AddAsync(assertTask);

        await DataUnitOfWork.SaveChangesAsync();
        await GenericMockSetup<ITaskModel, TaskModel>.SetupGetEntity(assertTask.Id, DbSetTaskMock, TasksCollection);

        TaskModel resultTask = await TaskRepo.GetAsync(assertTask.Id) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);

        Assert.Multiple(() =>
        {
            DbSetTaskMock.Verify(x => x.AddAsync(It.IsAny<TaskModel>(), It.IsAny<CancellationToken>()), Times.Once);
            DbSetTaskMock.Verify(x => x.FindAsync(It.IsAny<object>()), Times.Once);
            Assert.That(resultTask, Is.EqualTo(assertTask));
        });
    }

    [Test]
    public async Task AttemptToAddTaskAsNullObjectShouldThrowException()
    {
        TaskModel? assertNullTask = null;

        await GenericMockSetup<ITaskModel, TaskModel>.SetupAddEntity(assertNullTask!, TasksCollection, DbSetTaskMock, DbOperationsToExecute);

        Assert.ThrowsAsync<ArgumentNullException>(async () => await TaskRepo.AddAsync(assertNullTask!));
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    public async Task GetTaskShouldSucceed(int taskId)
    {
        var assertTask = TasksCollection.Single(t => t.Id == taskId);

        await GenericMockSetup<ITaskModel, TaskModel>.SetupGetEntity(assertTask.Id, DbSetTaskMock, TasksCollection);

        var resultTask = await TaskRepo.GetAsync(assertTask.Id) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);

        Assert.Multiple(() =>
        {
            DbSetTaskMock.Verify(x => x.FindAsync(It.IsAny<object>()), Times.Once);
            Assert.That(resultTask, Is.EqualTo(assertTask));
        });
    }

    [Test]
    public async Task GetAllTasksShouldSucceed()
    {
        var assertTasks = TasksCollection.ToList();

        var resultTasks = await TaskRepo.GetAllAsync();

        CollectionAssert.AreEqual(assertTasks, resultTasks);
    }

    [Test]
    [TestCase(2)]
    public async Task GetSingleTaskByFilterShouldSucceed(int taskId)
    {
        ITaskModel? assertTask = TasksCollection.Single(t => t.Id == taskId);

        ITaskModel? taskFromDb = await TaskRepo.GetByFilterAsync(t => t.Id == taskId);

        Assert.That(assertTask, Is.EqualTo(taskFromDb));
    }

    [Test]
    [TestCase(TaskStatusType.InProgress)]
    [TestCase(TaskStatusType.NotStarted)]
    public async Task GetTasksByFilterShouldSucceed(TaskStatusType taskStatus)
    {
        var assertTasks = TasksCollection.Where(t => t.Status.ToString() == taskStatus.ToString());

        var tasksFromDb = await TaskRepo.GetAllByFilterAsync(t => t.Status.ToString() == taskStatus.ToString());

        CollectionAssert.AreEqual(assertTasks, tasksFromDb);
    }

    [Test]
    [TestCase(null, typeof(ArgumentNullException))]
    [TestCase("", typeof(ArgumentNullException))]
    [TestCase(-1, typeof(ArgumentOutOfRangeException))]
    [TestCase(-5, typeof(ArgumentOutOfRangeException))]
    public async Task AttemptToGetTaskByInvalidIdShouldThrowException(object id, Type exceptionType)
    {
        int taskIdForMockSetup = TasksCollection[_firstIndex].Id;

        await GenericMockSetup<ITaskModel, TaskModel>.SetupGetEntity(taskIdForMockSetup, DbSetTaskMock, TasksCollection);

        switch (exceptionType)
        {
            case Type argExNull when argExNull == typeof(ArgumentNullException):
                Assert.ThrowsAsync<ArgumentNullException>(async () => await TaskRepo.GetAsync(id!));
                break;
            case Type argExOutOfRange when argExOutOfRange == typeof(ArgumentOutOfRangeException):
                Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await TaskRepo.GetAsync(id!));
                break;
            case Type argEx when argEx == typeof(ArgumentException):
                Assert.ThrowsAsync<ArgumentException>(async () => await TaskRepo.GetAsync(id!));
                break;
        }
    }

    [Test]
    public async Task UpdateTaskShouldSucceed()
    {
        int taskToUpdateId = TasksCollection[_firstIndex].Id;
        await GenericMockSetup<ITaskModel, TaskModel>.SetupGetEntity(taskToUpdateId, DbSetTaskMock, TasksCollection);
        TaskModel taskToUpdate = await TaskRepo.GetAsync(taskToUpdateId) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);

        ModifyTaskData(taskToUpdate);
        await GenericMockSetup<ITaskModel, TaskModel>.SetupUpdateEntity(taskToUpdate, DbSetTaskMock, TasksCollection, DbOperationsToExecute);

        TaskRepo.Update(taskToUpdate);
        await DataUnitOfWork.SaveChangesAsync();

        TaskModel updatedTask = await TaskRepo.GetAsync(taskToUpdateId) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);
        Assert.Multiple(() =>
        {
            Assert.That(updatedTask.Title, Is.EqualTo(taskToUpdate.Title));
            Assert.That(updatedTask.Description, Is.EqualTo(taskToUpdate.Description));
            Assert.That(updatedTask.Deadline, Is.EqualTo(taskToUpdate.Deadline));
        });
    }

    [Test]
    public async Task AttemptToUpdateTaskByNullObjectShouldThrowException()
    {
        TaskModel? NullTask = null;

        await GenericMockSetup<ITaskModel, TaskModel>.SetupUpdateEntity(NullTask!, DbSetTaskMock, TasksCollection, DbOperationsToExecute);

        Assert.Throws<ArgumentNullException>(() => TaskRepo.Update(NullTask!));
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    public async Task DeleteTaskShouldSucceed(int assertTaskId)
    {
        var itemsNumberBeforeDelete = TasksCollection.Count;
        await GenericMockSetup<ITaskModel, TaskModel>.SetupGetEntity(assertTaskId, DbSetTaskMock, TasksCollection);
        TaskModel taskToRemove = await TaskRepo.GetAsync(assertTaskId) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);

        await GenericMockSetup<ITaskModel, TaskModel>.SetupDeleteEntity(taskToRemove, DbSetTaskMock, TasksCollection, DbOperationsToExecute);
        TaskRepo.Remove(taskToRemove);
        await DataUnitOfWork.SaveChangesAsync();

        var itemsNumberAfterDelete = TasksCollection.Count;

        // I don't know why but I have to "refresh" TasksCollection because somehow it can get "deleted" task from memory.
        await GenericMockSetup<ITaskModel, TaskModel>.SetupGetEntity(assertTaskId, DbSetTaskMock, TasksCollection);

        TaskModel? resultOfTryToGetRemovedTask = await TaskRepo.GetAsync(assertTaskId);

        Assert.Multiple(() =>
        {
            DbSetTaskMock.Verify(x => x.FindAsync(It.IsAny<int>()), Times.Exactly(2));
            DbSetTaskMock.Verify(x => x.Remove(It.IsAny<TaskModel>()), Times.Once);
            Assert.That(itemsNumberAfterDelete, Is.Not.EqualTo(itemsNumberBeforeDelete));
            Assert.That(itemsNumberAfterDelete, Is.LessThan(itemsNumberBeforeDelete));
            Assert.That(resultOfTryToGetRemovedTask, Is.Null);
        });
    }

    [Test]
    public async Task AttemptToDeleteTaskByNullObjectShouldThrowException()
    {
        TaskModel? NullTask = null;

        await GenericMockSetup<ITaskModel, TaskModel>.SetupDeleteEntity(NullTask!, DbSetTaskMock, TasksCollection, DbOperationsToExecute);

        Assert.Throws<ArgumentNullException>(() => TaskRepo.Remove(NullTask!));
    }

    [Test]
    public async Task AttemptToAddRangeOfTasksByNullObjectShouldThrowException()
    {
        List<TaskModel>? nullRange = null;

        await GenericMockSetup<ITaskModel, TaskModel>.SetupAddEntitiesRange(nullRange!, TasksCollection, DbSetTaskMock, DbOperationsToExecute);

        Assert.ThrowsAsync<ArgumentNullException>(async () => await TaskRepo.AddRangeAsync(nullRange!));
    }

    [Test]
    public async Task AddTasksAsRangeShouldSucceed()
    {
        var tasksRange = TasksDataService.NewTasksRange.ToList();

        await GenericMockSetup<ITaskModel, TaskModel>.SetupAddEntitiesRange(tasksRange, TasksCollection, DbSetTaskMock, DbOperationsToExecute);
        await TaskRepo.AddRangeAsync(tasksRange);
        await DataUnitOfWork.SaveChangesAsync();

        var tasksFromDb = await TaskRepo.GetAllByFilterAsync(t => t.Title.Contains(TasksDataService.TaskRangeSuffix));

        Assert.Multiple(() =>
        {
            DbSetTaskMock.Verify(x => x.AddRangeAsync(It.IsAny<ICollection<TaskModel>>(), default), Times.Once);
            Assert.That(tasksFromDb, Has.Count.EqualTo(tasksRange.Count));
        });
    }
}