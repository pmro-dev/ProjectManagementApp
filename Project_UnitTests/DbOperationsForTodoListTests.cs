using Autofac;
using Autofac.Extras.Moq;
using Moq;
using Project_DomainEntities;
using Project_Main.Models.DataBases.AppDb;
using Project_Main.Models.DataBases.Repositories;

namespace Project_UnitTests
{
	/// <summary>
	/// Unit Test Class for Database tests with Mocking (DbContext) approach.
	/// </summary>
	public class DbOperationsForTodoListTests : BaseOperationsSetup
	{
		private const int OnePositionFurther = 1;
		private const int IdOfFirstTodoList = 1;
		private const TodoListModel NullTodoList = null;

		private AutoMock RegisterContextInstance()
		{
			return AutoMock.GetLoose(cfg => cfg.RegisterInstance(this.ContextOperations).As<IContextOperations>());
		}

		/// <summary>
		/// Tests <see cref="ContextOperations"/> - Add TodoList - operation as success attempt.
		/// </summary>
		/// <param name="todoListName">Valid name value for new TodoList.</param>
		[TestCase("Testowa nazwa listy")]
		public async Task AddTodoListShouldSucceed(string todoListName)
		{
			using AutoMock mock = RegisterContextInstance();
			TodoListModel assertTodoList = new()
			{
				Id = this.TodoLists.Last().Id + OnePositionFurther,
				Name = todoListName
			};

			await SetupMockAddList(assertTodoList);
			var mockContext = mock.Create<IContextOperations>();
			await mockContext.AddTodoListAsync(assertTodoList);

			await SetupMockGetTodoListWithDetails(assertTodoList.Id);
			TodoListModel? tempTodoList = await mockContext.GetTodoListWithDetailsAsync(assertTodoList.Id, AdminId) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
            this.DbContextMock.Verify(x => x.AddTodoListAsync(It.IsAny<TodoListModel>()), Times.Once);
			this.DbContextMock.Verify(x => x.GetTodoListWithDetailsAsync(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
			Assert.That(tempTodoList.IsTheSame(assertTodoList), Is.True);
		}

		/// <summary>
		/// Tests <see cref="ContextOperations"/> - Add TodoList - operation with Null Object to throw exception.
		/// </summary>
		[Test]
		public async Task AttemptToCreateTodoListByNullObjectShouldThrowException()
		{
			TodoListModel? assertNullTodoList = null;
			using AutoMock mock = RegisterContextInstance();
			await SetupMockAddList(assertNullTodoList);
			var mockContext = mock.Create<IContextOperations>();

#pragma warning disable CS8604 // Possible null reference argument.
			Assert.ThrowsAsync<ArgumentNullException>(async () => await mockContext.AddTodoListAsync(assertNullTodoList));
#pragma warning restore CS8604 // Possible null reference argument.
		}

		/// <summary>
		/// Tests <see cref="ContextOperations"/> - Get TodoList - operation as success attempt.
		/// </summary>
		/// <param name="listId">Id of TodoList.</param>
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		public async Task GetTodoListShouldSucceed(int listId)
		{
			using AutoMock mock = RegisterContextInstance();
			var assertTodoList = this.TodoLists.Single(l => l.Id == listId);
			await SetupMockGetTodoList(assertTodoList.Id);
			var mockContext = mock.Create<IContextOperations>();

			var resultTodoList = await mockContext.GetTodoListAsync(assertTodoList.Id, AdminId) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
            Assert.That(resultTodoList, Is.EqualTo(assertTodoList));
		}

        /// <summary>
        /// Tests <see cref="ContextOperations"/> - Get TodoList - operation that should failed and throw exception.
        /// </summary>
        /// <param name="listId">Invalid TodoList id.</param>
        [TestCase(-1)]
        [TestCase(-10)]
        public async Task AttemptToGetTodoListWithInvalidIdShouldThrowException(int invalidListId)
        {
            using AutoMock mock = RegisterContextInstance();
            await SetupMockGetTodoListForException();
            var mockContext = mock.Create<IContextOperations>();

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await mockContext.GetTodoListAsync(invalidListId, AdminId));
        }

        /// <summary>
        /// Tests <see cref="ContextOperations"/> - Get TodoList with details - operation as success attempt.
        /// </summary>
        /// <param name="listId">TodoList id.</param>
        [TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		public async Task GetTodoListWithDetailsShouldSucceed(int listId)
		{
			var assertTodoList = this.TodoLists.Single(l => l.Id == listId);
			using AutoMock mock = RegisterContextInstance();
			await SetupMockGetTodoListWithDetails(assertTodoList.Id);
			var mockContext = mock.Create<IContextOperations>();

			var expected = assertTodoList;
			var result = await mockContext.GetTodoListWithDetailsAsync(assertTodoList.Id, AdminId) ?? throw new AssertionException("Cannot find targeted result in seeded data for unit tests.");
            this.DbContextMock.Verify(x => x.GetTodoListWithDetailsAsync(assertTodoList.Id, AdminId), Times.Once);
			Assert.That(expected.IsTheSame(result), Is.True);
		}

        /// <summary>
        /// Tests <see cref="ContextOperations"/> - Get TodoList with details - operation that should failed and throw exception.
        /// </summary>
        /// <param name="listId">Invalid TodoList id.</param>
        [TestCase(-1)]
        [TestCase(-10)]
        public async Task AttemptToGetTodoListWithDetailsWithInvalidIdShouldThrowException(int invalidListId)
		{
            using AutoMock mock = RegisterContextInstance();
            await SetupMockGetTodoListWithDetailsForException();
            var mockContext = mock.Create<IContextOperations>();

			Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await mockContext.GetTodoListWithDetailsAsync(invalidListId, AdminId));
        }

        /// <summary>
        /// Tests <see cref="ContextOperations.GetAllTodoListsAsync"/> - Get All TodoLists - operation as success attempt.
        /// </summary>
        [Test]
		public async Task GetAllTodoListsShouldSucceed()
		{
			using AutoMock mock = RegisterContextInstance();
			await SetupMockGetAllTodoLists();
			var mockContext = mock.Create<IContextOperations>();

			var expected = this.TodoLists;
			var actual = await mockContext.GetAllTodoListsAsync(AdminId);

			this.DbContextMock.Verify(x => x.GetAllTodoListsAsync(It.IsAny<string>()), Times.Once);
			Assert.That(actual, Is.Not.EqualTo(null));
			CollectionAssert.AreEqual(expected, actual);
		}

        /// <summary>
        /// Tests <see cref="ContextOperations.GetAllTodoListsWithDetailsAsync"/> - Get All TodoLists with details - operation as success attempt.
        /// </summary>
        [Test]
		public async Task GetAllTodoListsWithDetailsShouldSucceed()
		{
			using AutoMock mock = RegisterContextInstance();
			await SetupMockGetAllTodoListsWithDetails();
			var mockContext = mock.Create<IContextOperations>();
			var expected = this.TodoLists;
			var actual = await mockContext.GetAllTodoListsWithDetailsAsync(AdminId);

			this.DbContextMock.Verify(x => x.GetAllTodoListsWithDetailsAsync(It.IsAny<string>()), Times.Once);

			Assert.That(actual, Is.Not.EqualTo(null));

			if (actual is null)
			{
				throw new AssertionException("Cannot find actual in seeded data for unit tests.");
			}

			Assert.That(expected.SequenceEqual(actual), Is.True);
		}

		/// <summary>
		/// Tests <see cref="ContextOperations.UpdateTodoListAsync(TodoListModel)"/> - Update TodoList - operation as success attempt.
		/// </summary>
		[Test]
		public async Task UpdateTodoListTasksShouldSucceed()
		{
			int todoListToUpdateId = this.TodoLists.First().Id;
			var newTasks = this.TasksBackend;
			using AutoMock mock = RegisterContextInstance();
			await SetupMockGetTodoListWithDetails(todoListToUpdateId);
			var mockContext = mock.Create<IContextOperations>();
			TodoListModel? todoListToUpdate = await mockContext.GetTodoListWithDetailsAsync(todoListToUpdateId, AdminId) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
            todoListToUpdate.Tasks = newTasks;
			await SetupMockUpdateTodoList(todoListToUpdate);

			await mockContext.UpdateTodoListAsync(todoListToUpdate);
			TodoListModel? updatedTodoList = await mockContext.GetTodoListWithDetailsAsync(todoListToUpdateId, AdminId) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
            Assert.Multiple(() =>
			{
				Assert.That(updatedTodoList.Tasks, Is.EqualTo(newTasks));
				Assert.That(updatedTodoList.Tasks.SequenceEqual(newTasks), Is.True);
			});
		}

		/// <summary>
		/// Tests <see cref="ContextOperations.UpdateTodoListAsync(TodoListModel)"/> - Update TodoList - operation with Null Object to throw exception.
		/// </summary>
		[Test]
		public async Task AttemptToUpdateTodoListByNullObjectShouldThrowException()
		{
			int assertTodoListID = IdOfFirstTodoList;
			using AutoMock mock = RegisterContextInstance();
			await SetupMockGetTodoListWithDetails(assertTodoListID);
			var mockContext = mock.Create<IContextOperations>();

			Assert.ThrowsAsync<ArgumentNullException>(async () => await mockContext.UpdateTodoListAsync(NullTodoList));
		}

		/// <summary>
		/// Tests <see cref="ContextOperations.DeleteTodoListAsync(int)"/> - Delete TodoList - operation as success attempt.
		/// </summary>
		/// <param name="assertTodoListID">Valid TodoList Id, that should be deleted.</param>
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		public async Task DeleteTodoListShouldSucceed(int assertTodoListID)
		{
			var itemsNumberInListBeforeDelete = this.TodoLists.Count;
			var itemsNumberInTasksBeforeDelete = this.AllTasks.Count;
			using AutoMock mock = RegisterContextInstance();
			await SetupMockGetTodoListWithDetails(assertTodoListID);
			await SetupMockDeleteTodoList();
			var mockContext = mock.Create<IContextOperations>();

			await mockContext.DeleteTodoListAsync(assertTodoListID, AdminId);

			var itemsNumberInListAfterDelete = this.TodoLists.Count;
			var itemsNumberInTasksAfterDelete = this.AllTasks.Count;

			this.DbContextMock.Verify(x => x.GetTodoListWithDetailsAsync(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
			this.DbContextMock.Verify(x => x.DeleteTodoListAsync(It.IsAny<TodoListModel>()), Times.Once);
			Assert.Multiple(() =>
			{
				Assert.That(itemsNumberInListAfterDelete, Is.Not.EqualTo(itemsNumberInListBeforeDelete));
				Assert.That(itemsNumberInTasksAfterDelete, Is.Not.EqualTo(itemsNumberInTasksBeforeDelete));
			});
			Assert.ThrowsAsync<InvalidOperationException>(async () => await mockContext.GetTodoListAsync(assertTodoListID, AdminId));
		}

		/// <summary>
		/// Tests <see cref="ContextOperations.DeleteTodoListAsync(int)"/> - Delete TodoList - operation with Id Out Of Range to throw exception.
		/// </summary>
		/// <param name="id">Id value out for range.</param>
		[TestCase(-2)]
		[TestCase(-1)]
		public async Task DeleteTodoListByIdOutOfRangeShouldThrowException(int id)
		{
			int assertTodoListID = IdOfFirstTodoList;
			using AutoMock mock = RegisterContextInstance();
			await SetupMockGetTodoListWithDetails(assertTodoListID);
			var mockContext = mock.Create<IContextOperations>();

			Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => mockContext.DeleteTodoListAsync(id, AdminId));
		}

		/// <summary>
		/// Tests <see cref="ContextOperations.DeleteTodoListAsync(int)"/> - Delete TodoList - operation with Invalid Id to throw exception.
		/// </summary>
		/// <param name="id">Nonexisted TodoList Id.</param>
		[TestCase(1)]
		[TestCase(20)]
		[TestCase(100)]
		public async Task DeleteTodoListByInvalidIdShouldThrowException(int id)
		{
			using AutoMock mock = RegisterContextInstance();
			await SetupMockGetTodoListWithDetailsReturnNull();
			var mockContext = mock.Create<IContextOperations>();

			Assert.ThrowsAsync<InvalidOperationException>(() => mockContext.DeleteTodoListAsync(id, AdminId));
		}

		/// <summary>
		/// Tests <see cref="ContextOperations.DeleteTaskAsync(TaskModel)"/> - Delete Task - operation as success attempt.
		/// </summary>
		/// <param name="listId">Target TodoList Id.</param>
		/// <param name="taskId">Target Task Id.</param>
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		public async Task DeleteTaskFromSpecificTodoListShouldSucceed(int taskId)
		{
			using AutoMock mock = RegisterContextInstance();
            await SetupMockDeleteTask();
			var mockContext = mock.Create<IContextOperations>();
			
			var assertTaskToDelete = this.AllTasks.Single(x => (x.Id == taskId));
			var numberOfTasksBeforeDelete = this.AllTasks.Count;

            await SetupMockReadTask(taskId);

            await mockContext.DeleteTaskAsync(assertTaskToDelete.Id, AdminId);
			var numberOfTasksAfterDelete = this.AllTasks.Count;

			this.DbContextMock.Verify(x => x.DeleteTaskAsync(It.IsAny<TaskModel>()), Times.Once);
			Assert.That(numberOfTasksAfterDelete, Is.Not.EqualTo(numberOfTasksBeforeDelete));
		}

		private async Task SetupMockAddList(TodoListModel? assertTodoList)
		{
			await Task.Run(() =>
			{
#pragma warning disable CS8604 // Possible null reference argument.
				this.DbContextMock.Setup(x => x.AddTodoListAsync(It.IsAny<TodoListModel>())).Callback(() => this.TodoLists.Add(assertTodoList)).Returns(Task.FromResult(1));
#pragma warning restore CS8604 // Possible null reference argument.
			});
		}

		private async Task SetupMockGetTodoListWithDetails(int assertTodoListID)
		{
			await Task.Run(() =>
			{
				this.DbContextMock.Setup(x => x.GetTodoListWithDetailsAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(this.TodoLists.Single(l => l.Id == assertTodoListID));
			});
		}

        private async Task SetupMockGetTodoListWithDetailsForException()
        {
            await Task.Run(() =>
            {
                this.DbContextMock.Setup(x => x.GetTodoListWithDetailsAsync(It.IsAny<int>(), It.IsAny<string>()));
            });
        }

        private async Task SetupMockGetTodoListForException()
        {
            await Task.Run(() =>
            {
                this.DbContextMock.Setup(x => x.GetTodoListAsync(It.IsAny<int>(), It.IsAny<string>()));
            });
        }

        private async Task SetupMockGetTodoListWithDetailsReturnNull()
		{
			await Task.Run(() =>
			{
				this.DbContextMock.Setup(x => x.GetTodoListWithDetailsAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(() => NullTodoList);
			});
		}

		private async Task SetupMockGetTodoList(int listId)
		{
			await Task.Run(() =>
			{
				this.DbContextMock.Setup(x => x.GetTodoListAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(this.TodoLists.Single(l => l.Id == listId));
			});
		}

		private async Task SetupMockGetAllTodoLists()
		{
			await Task.Run(() =>
			{
				this.DbContextMock.Setup(x => x.GetAllTodoListsAsync(AdminId)).ReturnsAsync(this.TodoLists);
			});
		}

		private async Task SetupMockGetAllTodoListsWithDetails()
		{
			await Task.Run(() =>
			{
				this.DbContextMock.Setup(x => x.GetAllTodoListsWithDetailsAsync(AdminId)).ReturnsAsync(this.TodoLists);
			});
		}

		private async Task SetupMockUpdateTodoList(TodoListModel todoListToUpdate)
		{
			await Task.Run(() =>
			{
				this.DbContextMock.Setup(x => x.UpdateTodoListAsync(It.IsAny<TodoListModel>()))
				.Callback<TodoListModel>((todoList) =>
				{
					var listToUpdate = this.TodoLists.Find(todoList => todoList.Id == todoListToUpdate.Id) ?? throw new AssertionException("Cannot find targeted TodoList in seeded data for unit tests.");
                    listToUpdate.Name = todoListToUpdate.Name;
					listToUpdate.Tasks = todoListToUpdate.Tasks;
				}).Returns(Task.FromResult(1));
			});
		}

		private async Task SetupMockDeleteTodoList()
		{
			await Task.Run(() =>
			{
				this.DbContextMock.Setup(x => x.DeleteTodoListAsync(It.IsAny<TodoListModel>())).Callback<TodoListModel>((todoListToDelete) =>
				{
					this.TodoLists.Remove(todoListToDelete);
					this.AllTasks = this.AllTasks.Where(t => t.TodoListId != todoListToDelete.Id).ToList();
				}).Returns(Task.FromResult(1));
			});
		}

		private async Task SetupMockDeleteTask()
		{
			await Task.Run(() => this.DbContextMock.Setup(x => x.DeleteTaskAsync(It.IsAny<TaskModel>())).Callback<TaskModel>((taskToDelete) => this.AllTasks.Remove(taskToDelete)).Returns(Task.FromResult(1)));
		}

        private async Task SetupMockReadTask(int taskId)
        {
            await Task.Run(() => this.DbContextMock.Setup(x => x.ReadTaskAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(this.AllTasks.Single(x => x.Id == taskId)));
        }
    }
}