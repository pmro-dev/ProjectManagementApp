using Project_UnitTests.Helpers;
using Project_DomainEntities;

namespace Project_UnitTests
{
	/// <summary>
	/// Tests class for To Do List Model.
	/// </summary>
	public class TodoListModelTests
	{
		private const int numberOfFails = 1;
		protected const string AdminId = "adminId";

		private static readonly object[] ValidTodoListsExamples = new object[]
		{
			new object[] { "App UX" },
			new object[] { "App Backend" },
			new object[] { "App Testing" }
		};

		private static readonly object[] InvalidTodoListsExamples = new object[]
		{
			new object[] { "Ap" },
			new object[] { "This is too long Name This is too long Name This is too long Name This is too long Name This is too long Name" },
		};

		/// <summary>
		/// Checks that is possible to create To Do List object with Valid data. 
		/// </summary>
		/// <param name="name">To Do List name.</param>
		[Test]
		[TestCaseSource(nameof(ValidTodoListsExamples))]
		public void CreateTodoListObjectWithValidDataShouldSucceed(string name)
		{
			TodoListModel newValidTodoList = new()
			{
				Title = name,
				UserId = AdminId
			};

			var propertiesThatViolatedValidations = DataAnnotationValidator.ValidNewObject(newValidTodoList);

			Assert.Multiple(() =>
			{
				Assert.That(propertiesThatViolatedValidations, Is.Empty);
				Assert.That(newValidTodoList.Tasks, Is.Empty);
			});
		}

		/// <summary>
		/// Checks that is data annotations validations failed on creating To Do List object with Invalid data. 
		/// </summary>
		/// <param name="name">To Do List name.</param>
		[Test]
		[TestCaseSource(nameof(InvalidTodoListsExamples))]
		public void CreateTodoListObjectWithInvalidDataShouldFailed(string name)
		{
			TodoListModel newInvalidTodoList = new()
			{
				Title = name,
				UserId = AdminId
			};

			var propertiesThatViolatedValidations = DataAnnotationValidator.ValidNewObject(newInvalidTodoList);

			Assert.That(propertiesThatViolatedValidations, Has.Count.EqualTo(numberOfFails));
		}
	}
}
