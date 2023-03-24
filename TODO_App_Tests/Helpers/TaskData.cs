namespace TODO_App_Tests.Helpers
{
    public static class TaskData
    {
        private const string DueDateFormat = "yyyy MM dd HH':'mm";

        public static readonly object[] ValidTasksExamples = new object[]
        {
            new object[] { "New Top Bar", "Please design new top bar, with rounded corners and with white background.", DateTime.ParseExact("2023 10 27 09:30", DueDateFormat, null)},
            new object[] { "Customer Profile", "Hi, we need to implement customer profile with view on his data.", DateTime.ParseExact("2023 08 15 10:30", DueDateFormat, null)},
            new object[] { "Live Team Chat", "Our client need internal chat for teams working on some projects.", DateTime.ParseExact("2023 08 29 14:00", DueDateFormat, null)}
        };

        public static readonly object[] InvalidTasksExamples = new object[]
        {
            new object[] { "Ne", "Description says that Title for this task is too short.", DateTime.ParseExact("2023 10 27 09:30", DueDateFormat, null)},
            new object[] { "This is too long Title, This is too long title, This is too long title, This is too long title, This is too long title.", "Please design new top bar, with rounded corners and with white background.", DateTime.ParseExact("2023 10 27 09:30", DueDateFormat, null)},
            new object[] { "Title says that description is too short.", "Hi,", DateTime.ParseExact("2023 08 15 10:30", DueDateFormat, null)},
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
				", DateTime.ParseExact("2023 08 15 10:30", DueDateFormat, null)
            }
        };
    }
}
