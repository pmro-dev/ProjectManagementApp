using Project_UnitTests.Helpers;
using Project_DomainEntities;
using Project_DomainEntities.Helpers;

namespace Project_UnitTests
{
	/// <summary>
	/// Tests class for Task Model.
	/// </summary>
	public class TaskModelTests
	{
		private const int numberOfFails = 1;
		private static readonly object[] ValidTasksExamples = TasksData.ValidTasksExamples;
		private static readonly object[] InvalidTasksExamples = TasksData.InvalidTasksExamples;
		protected const string AdminId = "adminId";

		/// <summary>
		/// Checks that is possible to create Task object with Valid data. 
		/// </summary>
		/// <param name="title">Task title.</param>
		/// <param name="description">Task description.</param>
		/// <param name="dueDate">Task deadline date.</param>
		[Test]
		[TestCaseSource(nameof(ValidTasksExamples))]
		public void CreateTaskObjectWithValidDataShouldSucceed(string title, string description, DateTime dueDate)
		{
			TaskModel newValidTask = new()
			{
				Title = title,
				Description = description,
				DueDate = dueDate,
				UserId = AdminId
			};

			var propertiesThatViolatedValidations = DataAnnotationValidator.ValidNewObject(newValidTask);

			Assert.Multiple(() =>
			{
				Assert.That(propertiesThatViolatedValidations, Is.Empty);
				Assert.That(newValidTask.Status, Is.EqualTo(TaskStatusHelper.TaskStatusType.NotStarted));
				Assert.That(newValidTask.TodoListId, Is.EqualTo(0));
			});
		}

		/// <summary>
		/// Checks that is data annotations validations failed on creating Task object with Invalid data. 
		/// </summary>
		/// <param name="title">Task title.</param>
		/// <param name="description">Task description.</param>
		/// <param name="dueDate">Task deadline date.</param>
		[Test]
		[TestCaseSource(nameof(InvalidTasksExamples))]
		public void CreateTaskObjectWithInvalidDataShouldFailed(string title, string description, DateTime dueDate)
		{
			TaskModel newInvalidTask = new()
			{
				Title = title,
				Description = description,
				DueDate = dueDate,
				UserId = AdminId
			};

			var propertiesThatViolatedValidations = DataAnnotationValidator.ValidNewObject(newInvalidTask);

			Assert.That(propertiesThatViolatedValidations, Has.Count.EqualTo(numberOfFails));
		}
	}
}
