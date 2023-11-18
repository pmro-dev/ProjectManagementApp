using App.Common.Helpers;
using App.Features.Tasks.Common;
using System.Globalization;

namespace Project_UnitTests.Data;

public static class TasksData
{
	private const string DueDateFormat = AttributesHelper.DataFormat;
	private static readonly IFormatProvider _formatProvider = CultureInfo.InvariantCulture;
	public const string AdminId = "adminId";

    public static readonly object[] ValidTasksForCreateOperation = new object[]
	{
		new object[] { "New Top Bar", "Please design new top bar, with rounded corners and with white background.", DateTime.ParseExact("2023-10-27T09:30", DueDateFormat, _formatProvider) },
		new object[] { "Customer Profile", "Hi, we need to implement customer profile with view on his data.", DateTime.ParseExact("2023-08-15T10:30", DueDateFormat, _formatProvider) },
		new object[] { "Live Team Chat", "Our client need internal chat for teams working on some projects.", DateTime.ParseExact("2023-08-29T14:00", DueDateFormat, _formatProvider) }
	};

	public static readonly object[] InvalidTasksForCreateOperation = new object[]
	{
		new object[] { "Ne", "Description says that Title for this task is too short.", DateTime.ParseExact("2023-10-27T09:30", DueDateFormat, _formatProvider) },
		new object[] { "This is too long Title, This is too long title, This is too long title, This is too long title, This is too long title.", "Please design new top bar, with rounded corners and with white background.", DateTime.ParseExact("2023-10-27T09:30", DueDateFormat, _formatProvider) },
		new object[] { "Title says that description is too short.", "Hi,", DateTime.ParseExact("2023-08-15T10:30", DueDateFormat, _formatProvider) },
		new object[]
		{
			"Title says that the Description is too long",
			@"
					Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, 
					totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. 
					Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui 
					ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, 
					sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, 
					quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure 
					reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?
				", DateTime.ParseExact("2023-08-15T10:30", DueDateFormat, _formatProvider)
		}
	};

	public static readonly string TaskRangeSuffix = "Task Range Test";

	public static ICollection<TaskModel> NewTasksRange { get; private set; } = new List<TaskModel>()
	{
			new() { Title = "First " + TaskRangeSuffix, Description = "First Description", DueDate = DateTime.ParseExact("2023-10-27T10:30", DueDateFormat, _formatProvider) },
			new() { Title = "Second " + TaskRangeSuffix, Description = "Second Description", DueDate = DateTime.ParseExact("2023-08-22T09:00", DueDateFormat, _formatProvider) },
			new() { Title = "Third " + TaskRangeSuffix, Description = "Third Description", DueDate = DateTime.ParseExact("2023-09-12T09:30", DueDateFormat, _formatProvider) },
			new() { Title = "Fourth " + TaskRangeSuffix, Description = "Fourth Description", DueDate = DateTime.ParseExact("2023-11-07T12:30", DueDateFormat, _formatProvider	) },
			new() { Title = "Fifth " + TaskRangeSuffix, Description = "Fifth Description", DueDate = DateTime.ParseExact("2023-06-30T11:00", DueDateFormat, _formatProvider) }
	};
}
