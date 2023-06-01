using Moq;
using Project_DomainEntities;
using Project_UnitTests.Helpers;

namespace Project_UnitTests
{
    /// <summary>
    /// Unit Test Class for Database tests with Mocking (DbContext) approach.
    /// </summary>
    public class DbOperationsForTodoListTests : BaseOperationsSetup
	{
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		public async Task GetTodoListShouldSucceed(int todoListId)
		{
			var assertTodoList = TodoListsCollection.Single(t => t.Id == todoListId);

			await GenericMockSetup<TodoListModel>.SetupGetEntity(assertTodoList.Id, DbSetTodoListMock, TodoListsCollection);

			var resultTodoList = await TodoListRepo.GetAsync(assertTodoList.Id) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);

			Assert.Multiple(() =>
			{
				DbSetTodoListMock.Verify(x => x.FindAsync(It.IsAny<object>()), Times.Once);
				Assert.That(resultTodoList, Is.EqualTo(assertTodoList));
			});
		}

		[Test]
		public async Task AttemptToAddTodoListAsNullObjectShouldThrowException()
		{
			TodoListModel? assertNullTodoList = null;

			await GenericMockSetup<TodoListModel>.SetupAddEntity(assertNullTodoList!, TodoListsCollection, DbSetTodoListMock, DbOperationsToExecute);

			Assert.ThrowsAsync<ArgumentNullException>(async () => await TodoListRepo.AddAsync(assertNullTodoList!));
		}

		[Test]
		public async Task GetAllTodoListsShouldSucceed()
		{
			var assertTodoLists = TodoListsCollection.ToList();

			var resultTodoLists = await TodoListRepo.GetAllAsync();

			CollectionAssert.AreEqual(assertTodoLists, resultTodoLists);
		}

		[Test]
		[TestCase(2)]
		public async Task GetSingleTodoListByFilterShouldSucceed(int todoListId)
		{
			TodoListModel? assertTask = TodoListsCollection.Find(t => t.Id == todoListId);

			TodoListModel? taskFromDb = await TodoListRepo.GetSingleByFilterAsync(t => t.Id == todoListId);

			Assert.That(assertTask, Is.EqualTo(taskFromDb));
		}

		[Test]
		[TestCase(3)]
		[TestCase(6)]
		public async Task GetTodoListsByFilterShouldSucceed(int tasksCount)
		{
			var assertTodoLists = TodoListsCollection.FindAll(t => t.Tasks.Count == tasksCount);

			var todoListsFromDb = await TodoListRepo.GetAllByFilterAsync(t => t.Tasks.Count == tasksCount);

			CollectionAssert.AreEqual(assertTodoLists, todoListsFromDb);
		}

		[Test]
		[TestCase(null, typeof(ArgumentNullException))]
		[TestCase("", typeof(ArgumentNullException))]
		[TestCase(-1, typeof(ArgumentOutOfRangeException))]
		[TestCase(-5, typeof(ArgumentOutOfRangeException))]
		public async Task AttemptToGetTodoListByInvalidIdShouldThrowException(object id, Type exceptionType)
		{
			int todoListIdForMockSetup = TodoListsCollection.First().Id;

			await GenericMockSetup<TodoListModel>.SetupGetEntity(todoListIdForMockSetup, DbSetTodoListMock, TodoListsCollection);

			switch (exceptionType)
			{
				case Type argExNull when argExNull == typeof(ArgumentNullException):
					Assert.ThrowsAsync<ArgumentNullException>(async () => await TodoListRepo.GetAsync(id!));
					break;
				case Type argExOutOfRange when argExOutOfRange == typeof(ArgumentOutOfRangeException):
					Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await TodoListRepo.GetAsync(id!));
					break;
				case Type argEx when argEx == typeof(ArgumentException):
					Assert.ThrowsAsync<ArgumentException>(async () => await TodoListRepo.GetAsync(id!));
					break;
			}
		}

		[Test]
		public async Task AttemptToUpdateTodoListByNullObjectShouldThrowException()
		{
			TodoListModel? NullTodoList = null;

			await GenericMockSetup<TodoListModel>.SetupUpdateEntity(NullTodoList!, DbSetTodoListMock, TodoListsCollection, DbOperationsToExecute);

			Assert.ThrowsAsync<ArgumentNullException>(async () => await TodoListRepo.Update(NullTodoList!));
		}

		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		public async Task DeleteTodoListShouldSucceed(int assertTodoListId)
		{
			var itemsNumberBeforeDelete = TodoListsCollection.Count;
			await GenericMockSetup<TodoListModel>.SetupGetEntity(assertTodoListId, DbSetTodoListMock, TodoListsCollection);
			TodoListModel todoListToRemove = await TodoListRepo.GetAsync(assertTodoListId) ?? throw new AssertionException(Messages.MessageInvalidRepositoryResult);

			await GenericMockSetup<TodoListModel>.SetupDeleteEntity(todoListToRemove, DbSetTodoListMock, TodoListsCollection, DbOperationsToExecute);
			await TodoListRepo.Remove(todoListToRemove);
			await DataUnitOfWork.SaveChangesAsync();

			var itemsNumberAfterDelete = TodoListsCollection.Count;

			// I don't know why but I have to "refresh" TodoListsCollection because somehow it can get "deleted" task from memory.
			await GenericMockSetup<TodoListModel>.SetupGetEntity(assertTodoListId, DbSetTodoListMock, TodoListsCollection);

			TodoListModel? resultOfTryToGetRemovedTask = await TodoListRepo.GetAsync(assertTodoListId);

			Assert.Multiple(() =>
			{
				DbSetTodoListMock.Verify(x => x.FindAsync(It.IsAny<int>()), Times.Exactly(2));
				DbSetTodoListMock.Verify(x => x.Remove(It.IsAny<TodoListModel>()), Times.Once);
				Assert.That(itemsNumberAfterDelete, Is.Not.EqualTo(itemsNumberBeforeDelete));
				Assert.That(itemsNumberAfterDelete, Is.LessThan(itemsNumberBeforeDelete));
				Assert.That(resultOfTryToGetRemovedTask, Is.Null);
			});
		}

		//		private const int OnePositionFurther = 1;
		//		private const int IdOfFirstTodoList = 1;
		//		private const TodoListModel NullTodoList = null;

		//		private AutoMock RegisterContextInstance()
		//		{
		//			return AutoMock.GetLoose(cfg => cfg.RegisterInstance(this.ContextOperations).As<IContextOperations>());
		//		}

		//		/// <summary>
		//		/// Tests <see cref="ContextOperations"/> - AddAsync TodoList - operation as success attempt.
		//		/// </summary>
		//		/// <param name="todoListName">Valid name value for new TodoList.</param>
		//		[TestCase("Testowa nazwa listy")]
		//		public async Task AddTodoListShouldSucceed(string todoListName)
		//		{
		//			using AutoMock mock = RegisterContextInstance();
		//			TodoListModel assertTodoList = new()
		//			{
		//				Id = this.TodoListsCollection.Last().Id + OnePositionFurther,
		//				Name = todoListName
		//			};

		//			await SetupMockAddList(assertTodoList);
		//			var mockContext = mock.Create<IContextOperations>();
		//			await mockContext.AddTodoListAsync(assertTodoList);

		//			await SetupMockGetTodoListWithDetails(assertTodoList.Id);
		//			TodoListModel? tempTodoList = await mockContext.GetTodoListWithDetailsAsync(assertTodoList.Id, AdminId) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
		//            this.MainDbContextMock.Verify(x => x.AddTodoListAsync(It.IsAny<TodoListModel>()), Times.Once);
		//			this.MainDbContextMock.Verify(x => x.GetTodoListWithDetailsAsync(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
		//			Assert.That(tempTodoList.IsTheSame(assertTodoList), Is.True);
		//		}

		//		/// <summary>
		//		/// Tests <see cref="ContextOperations"/> - AddAsync TodoList - operation with Null Object to throw exception.
		//		/// </summary>
		//		[Test]
		//		public async Task AttemptToCreateTodoListByNullObjectShouldThrowException()
		//		{
		//			TodoListModel? assertNullTodoList = null;
		//			using AutoMock mock = RegisterContextInstance();
		//			await SetupMockAddList(assertNullTodoList);
		//			var mockContext = mock.Create<IContextOperations>();

		//#pragma warning disable CS8604 // Possible null reference argument.
		//			Assert.ThrowsAsync<ArgumentNullException>(async () => await mockContext.AddTodoListAsync(assertNullTodoList));
		//#pragma warning restore CS8604 // Possible null reference argument.
		//		}

		//		/// <summary>
		//		/// Tests <see cref="ContextOperations"/> - Get TodoList - operation as success attempt.
		//		/// </summary>
		//		/// <param name="listId">Id of TodoList.</param>
		//		[TestCase(1)]
		//		[TestCase(2)]
		//		[TestCase(3)]
		//		[TestCase(4)]
		//		public async Task GetTodoListShouldSucceed(int listId)
		//		{
		//			using AutoMock mock = RegisterContextInstance();
		//			var assertTodoList = this.TodoListsCollection.Single(l => l.Id == listId);
		//			await SetupMockGetTodoList(assertTodoList.Id);
		//			var mockContext = mock.Create<IContextOperations>();

		//			var resultTodoList = await mockContext.GetTodoListAsync(assertTodoList.Id, AdminId) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
		//            Assert.That(resultTodoList, Is.EqualTo(assertTodoList));
		//		}

		//        /// <summary>
		//        /// Tests <see cref="ContextOperations"/> - Get TodoList - operation that should failed and throw exception.
		//        /// </summary>
		//        /// <param name="listId">Invalid TodoList id.</param>
		//        [TestCase(-1)]
		//        [TestCase(-10)]
		//        public async Task AttemptToGetTodoListWithInvalidIdShouldThrowException(int invalidListId)
		//        {
		//            using AutoMock mock = RegisterContextInstance();
		//            await SetupMockGetTodoListForException();
		//            var mockContext = mock.Create<IContextOperations>();

		//            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await mockContext.GetTodoListAsync(invalidListId, AdminId));
		//        }

		//        /// <summary>
		//        /// Tests <see cref="ContextOperations"/> - Get TodoList with details - operation as success attempt.
		//        /// </summary>
		//        /// <param name="listId">TodoList id.</param>
		//        [TestCase(1)]
		//		[TestCase(2)]
		//		[TestCase(3)]
		//		[TestCase(4)]
		//		public async Task GetTodoListWithDetailsShouldSucceed(int listId)
		//		{
		//			var assertTodoList = this.TodoListsCollection.Single(l => l.Id == listId);
		//			using AutoMock mock = RegisterContextInstance();
		//			await SetupMockGetTodoListWithDetails(assertTodoList.Id);
		//			var mockContext = mock.Create<IContextOperations>();

		//			var expected = assertTodoList;
		//			var result = await mockContext.GetTodoListWithDetailsAsync(assertTodoList.Id, AdminId) ?? throw new AssertionException("Cannot find targeted result in seeded data for unit tests.");
		//            this.MainDbContextMock.Verify(x => x.GetTodoListWithDetailsAsync(assertTodoList.Id, AdminId), Times.Once);
		//			Assert.That(expected.IsTheSame(result), Is.True);
		//		}

		//        /// <summary>
		//        /// Tests <see cref="ContextOperations"/> - Get TodoList with details - operation that should failed and throw exception.
		//        /// </summary>
		//        /// <param name="listId">Invalid TodoList id.</param>
		//        [TestCase(-1)]
		//        [TestCase(-10)]
		//        public async Task AttemptToGetTodoListWithDetailsWithInvalidIdShouldThrowException(int invalidListId)
		//		{
		//            using AutoMock mock = RegisterContextInstance();
		//            await SetupMockGetTodoListWithDetailsForException();
		//            var mockContext = mock.Create<IContextOperations>();

		//			Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await mockContext.GetTodoListWithDetailsAsync(invalidListId, AdminId));
		//        }

		//        /// <summary>
		//        /// Tests <see cref="ContextOperations.GetAllTodoListsAsync"/> - Get All TodoListsCollection - operation as success attempt.
		//        /// </summary>
		//        [Test]
		//		public async Task GetAllTodoListsShouldSucceed()
		//		{
		//			using AutoMock mock = RegisterContextInstance();
		//			await SetupMockGetAllTodoLists();
		//			var mockContext = mock.Create<IContextOperations>();

		//			var expected = this.TodoListsCollection;
		//			var actual = await mockContext.GetAllTodoListsAsync(AdminId);

		//			this.MainDbContextMock.Verify(x => x.GetAllTodoListsAsync(It.IsAny<string>()), Times.Once);
		//			Assert.That(actual, Is.Not.EqualTo(null));
		//			CollectionAssert.AreEqual(expected, actual);
		//		}

		//        /// <summary>
		//        /// Tests <see cref="ContextOperations.GetAllTodoListsWithDetailsAsync"/> - Get All TodoListsCollection with details - operation as success attempt.
		//        /// </summary>
		//        [Test]
		//		public async Task GetAllTodoListsWithDetailsShouldSucceed()
		//		{
		//			using AutoMock mock = RegisterContextInstance();
		//			await SetupMockGetAllTodoListsWithDetails();
		//			var mockContext = mock.Create<IContextOperations>();
		//			var expected = this.TodoListsCollection;
		//			var actual = await mockContext.GetAllTodoListsWithDetailsAsync(AdminId);

		//			this.MainDbContextMock.Verify(x => x.GetAllTodoListsWithDetailsAsync(It.IsAny<string>()), Times.Once);

		//			Assert.That(actual, Is.Not.EqualTo(null));

		//			if (actual is null)
		//			{
		//				throw new AssertionException("Cannot find actual in seeded data for unit tests.");
		//			}

		//			Assert.That(expected.SequenceEqual(actual), Is.True);
		//		}

		//		/// <summary>
		//		/// Tests <see cref="ContextOperations.UpdateTodoListAsync(TodoListModel)"/> - Update TodoList - operation as success attempt.
		//		/// </summary>
		//		[Test]
		//		public async Task UpdateTodoListTasksShouldSucceed()
		//		{
		//			int todoListToUpdateId = this.TodoListsCollection.First().Id;
		//			var newTasks = this.TasksBackend;
		//			using AutoMock mock = RegisterContextInstance();
		//			await SetupMockGetTodoListWithDetails(todoListToUpdateId);
		//			var mockContext = mock.Create<IContextOperations>();
		//			TodoListModel? todoListToUpdate = await mockContext.GetTodoListWithDetailsAsync(todoListToUpdateId, AdminId) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
		//            todoListToUpdate.Tasks = newTasks;
		//			await SetupMockUpdateTodoList(todoListToUpdate);

		//			await mockContext.UpdateTodoListAsync(todoListToUpdate);
		//			TodoListModel? updatedTodoList = await mockContext.GetTodoListWithDetailsAsync(todoListToUpdateId, AdminId) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
		//            Assert.Multiple(() =>
		//			{
		//				Assert.That(updatedTodoList.Tasks, Is.EqualTo(newTasks));
		//				Assert.That(updatedTodoList.Tasks.SequenceEqual(newTasks), Is.True);
		//			});
		//		}

		//		/// <summary>
		//		/// Tests <see cref="ContextOperations.UpdateTodoListAsync(TodoListModel)"/> - Update TodoList - operation with Null Object to throw exception.
		//		/// </summary>
		//		[Test]
		//		public async Task AttemptToUpdateTodoListByNullObjectShouldThrowException()
		//		{
		//			int assertTodoListID = IdOfFirstTodoList;
		//			using AutoMock mock = RegisterContextInstance();
		//			await SetupMockGetTodoListWithDetails(assertTodoListID);
		//			var mockContext = mock.Create<IContextOperations>();

		//			Assert.ThrowsAsync<ArgumentNullException>(async () => await mockContext.UpdateTodoListAsync(NullTodoList));
		//		}

		//		/// <summary>
		//		/// Tests <see cref="ContextOperations.DeleteTodoListAsync(int)"/> - Delete TodoList - operation as success attempt.
		//		/// </summary>
		//		/// <param name="assertTodoListID">Valid TodoList Id, that should be deleted.</param>
		//		[TestCase(1)]
		//		[TestCase(2)]
		//		[TestCase(3)]
		//		[TestCase(4)]
		//		public async Task DeleteTodoListShouldSucceed(int assertTodoListID)
		//		{
		//			var itemsNumberInListBeforeDelete = this.TodoListsCollection.Count;
		//			var itemsNumberInTasksBeforeDelete = this.TasksCollection.Count;
		//			using AutoMock mock = RegisterContextInstance();
		//			await SetupMockGetTodoListWithDetails(assertTodoListID);
		//			await SetupMockDeleteTodoList();
		//			var mockContext = mock.Create<IContextOperations>();

		//			await mockContext.DeleteTodoListAsync(assertTodoListID, AdminId);

		//			var itemsNumberInListAfterDelete = this.TodoListsCollection.Count;
		//			var itemsNumberInTasksAfterDelete = this.TasksCollection.Count;

		//			this.MainDbContextMock.Verify(x => x.GetTodoListWithDetailsAsync(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
		//			this.MainDbContextMock.Verify(x => x.DeleteTodoListAsync(It.IsAny<TodoListModel>()), Times.Once);
		//			Assert.Multiple(() =>
		//			{
		//				Assert.That(itemsNumberInListAfterDelete, Is.Not.EqualTo(itemsNumberInListBeforeDelete));
		//				Assert.That(itemsNumberInTasksAfterDelete, Is.Not.EqualTo(itemsNumberInTasksBeforeDelete));
		//			});
		//			Assert.ThrowsAsync<InvalidOperationException>(async () => await mockContext.GetTodoListAsync(assertTodoListID, AdminId));
		//		}

		//		/// <summary>
		//		/// Tests <see cref="ContextOperations.DeleteTodoListAsync(int)"/> - Delete TodoList - operation with Id Out Of Range to throw exception.
		//		/// </summary>
		//		/// <param name="id">Id value out for range.</param>
		//		[TestCase(-2)]
		//		[TestCase(-1)]
		//		public async Task DeleteTodoListByIdOutOfRangeShouldThrowException(int id)
		//		{
		//			int assertTodoListID = IdOfFirstTodoList;
		//			using AutoMock mock = RegisterContextInstance();
		//			await SetupMockGetTodoListWithDetails(assertTodoListID);
		//			var mockContext = mock.Create<IContextOperations>();

		//			Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => mockContext.DeleteTodoListAsync(id, AdminId));
		//		}

		//		/// <summary>
		//		/// Tests <see cref="ContextOperations.DeleteTodoListAsync(int)"/> - Delete TodoList - operation with Invalid Id to throw exception.
		//		/// </summary>
		//		/// <param name="id">Nonexisted TodoList Id.</param>
		//		[TestCase(1)]
		//		[TestCase(20)]
		//		[TestCase(100)]
		//		public async Task DeleteTodoListByInvalidIdShouldThrowException(int id)
		//		{
		//			using AutoMock mock = RegisterContextInstance();
		//			await SetupMockGetTodoListWithDetailsReturnNull();
		//			var mockContext = mock.Create<IContextOperations>();

		//			Assert.ThrowsAsync<InvalidOperationException>(() => mockContext.DeleteTodoListAsync(id, AdminId));
		//		}

		//		/// <summary>
		//		/// Tests <see cref="ContextOperations.DeleteTaskAsync(TaskModel)"/> - Delete Task - operation as success attempt.
		//		/// </summary>
		//		/// <param name="listId">Target TodoList Id.</param>
		//		/// <param name="taskId">Target Task Id.</param>
		//		[TestCase(1)]
		//		[TestCase(2)]
		//		[TestCase(3)]
		//		public async Task DeleteTaskFromSpecificTodoListShouldSucceed(int taskId)
		//		{
		//			using AutoMock mock = RegisterContextInstance();
		//            await SetupMockDeleteTask();
		//			var mockContext = mock.Create<IContextOperations>();

		//			var assertTaskToDelete = this.TasksCollection.Single(x => (x.Id == taskId));
		//			var numberOfTasksBeforeDelete = this.TasksCollection.Count;

		//            await SetupMockReadTask(taskId);

		//            await mockContext.DeleteTaskAsync(assertTaskToDelete.Id, AdminId);
		//			var numberOfTasksAfterDelete = this.TasksCollection.Count;

		//			this.MainDbContextMock.Verify(x => x.DeleteTaskAsync(It.IsAny<TaskModel>()), Times.Once);
		//			Assert.That(numberOfTasksAfterDelete, Is.Not.EqualTo(numberOfTasksBeforeDelete));
		//		}

		//		private async Task SetupMockAddList(TodoListModel? assertTodoList)
		//		{
		//			await Task.Run(() =>
		//			{
		//#pragma warning disable CS8604 // Possible null reference argument.
		//				this.MainDbContextMock.Setup(x => x.AddTodoListAsync(It.IsAny<TodoListModel>())).Callback(() => this.TodoListsCollection.Add(assertTodoList)).Returns(Task.FromResult(1));
		//#pragma warning restore CS8604 // Possible null reference argument.
		//			});
		//		}

		//		private async Task SetupMockGetTodoListWithDetails(int assertTodoListID)
		//		{
		//			await Task.Run(() =>
		//			{
		//				this.MainDbContextMock.Setup(x => x.GetTodoListWithDetailsAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(this.TodoListsCollection.Single(l => l.Id == assertTodoListID));
		//			});
		//		}

		//        private async Task SetupMockGetTodoListWithDetailsForException()
		//        {
		//            await Task.Run(() =>
		//            {
		//                this.MainDbContextMock.Setup(x => x.GetTodoListWithDetailsAsync(It.IsAny<int>(), It.IsAny<string>()));
		//            });
		//        }

		//        private async Task SetupMockGetTodoListForException()
		//        {
		//            await Task.Run(() =>
		//            {
		//                this.MainDbContextMock.Setup(x => x.GetTodoListAsync(It.IsAny<int>(), It.IsAny<string>()));
		//            });
		//        }

		//        private async Task SetupMockGetTodoListWithDetailsReturnNull()
		//		{
		//			await Task.Run(() =>
		//			{
		//				this.MainDbContextMock.Setup(x => x.GetTodoListWithDetailsAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(() => NullTodoList);
		//			});
		//		}

		//		private async Task SetupMockGetTodoList(int listId)
		//		{
		//			await Task.Run(() =>
		//			{
		//				this.MainDbContextMock.Setup(x => x.GetTodoListAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(this.TodoListsCollection.Single(l => l.Id == listId));
		//			});
		//		}

		//		private async Task SetupMockGetAllTodoLists()
		//		{
		//			await Task.Run(() =>
		//			{
		//				this.MainDbContextMock.Setup(x => x.GetAllTodoListsAsync(AdminId)).ReturnsAsync(this.TodoListsCollection);
		//			});
		//		}

		//		private async Task SetupMockGetAllTodoListsWithDetails()
		//		{
		//			await Task.Run(() =>
		//			{
		//				this.MainDbContextMock.Setup(x => x.GetAllTodoListsWithDetailsAsync(AdminId)).ReturnsAsync(this.TodoListsCollection);
		//			});
		//		}

		//		private async Task SetupMockUpdateTodoList(TodoListModel todoListToUpdate)
		//		{
		//			await Task.Run(() =>
		//			{
		//				this.MainDbContextMock.Setup(x => x.UpdateTodoListAsync(It.IsAny<TodoListModel>()))
		//				.Callback<TodoListModel>((todoList) =>
		//				{
		//					var listToUpdate = this.TodoListsCollection.Find(todoList => todoList.Id == todoListToUpdate.Id) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
		//                    listToUpdate.Name = todoListToUpdate.Name;
		//					listToUpdate.Tasks = todoListToUpdate.Tasks;
		//				}).Returns(Task.FromResult(1));
		//			});
		//		}

		//		private async Task SetupMockDeleteTodoList()
		//		{
		//			await Task.Run(() =>
		//			{
		//				this.MainDbContextMock.Setup(x => x.DeleteTodoListAsync(It.IsAny<TodoListModel>())).Callback<TodoListModel>((todoListToDelete) =>
		//				{
		//					this.TodoListsCollection.Remove(todoListToDelete);
		//					this.TasksCollection = this.TasksCollection.Where(t => t.TodoListId != todoListToDelete.Id).ToList();
		//				}).Returns(Task.FromResult(1));
		//			});
		//		}

		//		private async Task SetupMockDeleteTask()
		//		{
		//			await Task.Run(() => this.MainDbContextMock.Setup(x => x.DeleteTaskAsync(It.IsAny<TaskModel>())).Callback<TaskModel>((taskToDelete) => this.TasksCollection.Remove(taskToDelete)).Returns(Task.FromResult(1)));
		//		}

		//        private async Task SetupMockReadTask(int taskId)
		//        {
		//            await Task.Run(() => this.MainDbContextMock.Setup(x => x.ReadTaskAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(this.TasksCollection.Single(x => x.Id == taskId)));
		//        }
	}
}