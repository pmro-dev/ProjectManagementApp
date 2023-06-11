using Project_DomainEntities;

namespace ClassLibrary_SeedData
{
    /// <summary>
    /// Class with properties that contains data.
    /// </summary>
    public class SeedData : ISeedData
    {
        protected const string DueDateFormat = "yyyy MM dd HH':'mm";
        public string AdminId { get; } = "adminId";

        /// <summary>
        /// All Tasks for Database Set.
        /// </summary>
        public List<TaskModel> AllTasks { get; set; }

        /// <summary>
        /// All Todolists for Database Set.
        /// </summary>
        public List<TodoListModel> TodoLists { get; set; }

        /// <summary>
        /// Specific 'UX' Tasks for a list.
        /// </summary>
        public List<TaskModel> TasksUX { get; set; }

        /// <summary>
        /// Specific 'Backend' Tasks for a list.
        /// </summary>
        public List<TaskModel> TasksBackend { get; set; }

        /// <summary>
        /// Specific 'Testing' Tasks for a list.
        /// 
        /// </summary>
        public List<TaskModel> TasksTesting { get; set; }

        /// <summary>
        /// Specific 'Project Management' Tasks for a list.
        /// 
        /// </summary>
        public List<TaskModel> TasksProjectManagement { get; set; }

        /// <summary>
        /// Seeds the data to properties.
        /// </summary>
        /// <exception cref="OperationCanceledException"></exception>
        public SeedData()
        {
            SeedTasks();
            SeedTodoLists();
            SeedAllTasks();

            if (AllTasks is null || TodoLists is null || TasksUX is null || TasksBackend is null || TasksTesting is null || TasksProjectManagement is null)
            {
                throw new InvalidOperationException("Critical error! Some property is not set with data!");
            }
        }

        private void SeedTasks()
        {
            TasksUX = new List<TaskModel>()
            {
                new TaskModel{
                    Title = "New Top Bar",
                    Description = "Please design new top bar, with rounded corners and with white background.",
                    DueDate = DateTime.ParseExact("2023 10 27 09:30", DueDateFormat, null),
                    LastModificationDate = DateTime.ParseExact("2023 10 15 15:30", DueDateFormat, null),
                    CreationDate = DateTime.ParseExact("2023 10 15 15:30", DueDateFormat, null),
                    UserId = AdminId
                },
                new TaskModel{
                    Title = "Customer Profile",
                    Description = "Hi, we need to implement customer profile with view on his data.",
                    DueDate = DateTime.ParseExact("2023 08 15 10:30", DueDateFormat, null),
                    LastModificationDate = DateTime.ParseExact("2023 08 01 15:30", DueDateFormat, null),
                    CreationDate = DateTime.ParseExact("2023 08 01 15:30", DueDateFormat, null),
                    UserId = AdminId
                },
                new TaskModel{
                    Title = "New Buttons Location",
                    Description = "Move additional option buttons from left side to bottom-right, and resize it to be smmaller by 20%.",
                    DueDate = DateTime.ParseExact("2023 02 04 14:00", DueDateFormat, null),
                    LastModificationDate = DateTime.ParseExact("2023 01 15 15:30", DueDateFormat, null),
                    CreationDate = DateTime.ParseExact("2023 01 15 15:30", DueDateFormat, null),
                    UserId = AdminId
                },
            };

            TasksBackend = new List<TaskModel>()
            {
                new TaskModel{
                    Title = "Add Authentication",
                    Description = "Need to implement OAuth2.",
                    DueDate = DateTime.ParseExact("2023 09 24 09:30", DueDateFormat, null),
                    LastModificationDate = DateTime.ParseExact("2023 01 15 15:30", DueDateFormat, null),
                    CreationDate = DateTime.ParseExact("2023 01 15 15:30", DueDateFormat, null),
                    UserId = AdminId
                },
                new TaskModel{
                    Title = "Iimplement Database service",
                    Description = "We need to implement Azure Cloude Database approach.",
                    DueDate = DateTime.ParseExact("2023 09 13 10:30", DueDateFormat, null),
                    LastModificationDate = DateTime.ParseExact("2023 09 02 15:30", DueDateFormat, null),
                    CreationDate = DateTime.ParseExact("2023 09 02 15:30", DueDateFormat, null),
                    UserId = AdminId
                },
                new TaskModel{
                    Title = "Live Team Chat",
                    Description = "Our client need internal chat for teams working on some projects.",
                    DueDate = DateTime.ParseExact("2023 08 29 14:00", DueDateFormat, null),
                    LastModificationDate = DateTime.ParseExact("2023 08 03 15:30", DueDateFormat, null),
                    CreationDate = DateTime.ParseExact("2023 08 03 15:30", DueDateFormat, null),
                    UserId = AdminId
                },
            };

            TasksTesting = new List<TaskModel>()
            {
                new TaskModel{
                    Title = "Integration Tests",
                    Description = "Hi, we need to test integration between our app and new database.",
                    DueDate = DateTime.ParseExact("2023 07 20 09:30", DueDateFormat, null),
                    LastModificationDate = DateTime.ParseExact("2023 06 16 15:30", DueDateFormat, null),
                    CreationDate = DateTime.ParseExact("2023 06 16 15:30", DueDateFormat, null),
                    UserId = AdminId
                },
                new TaskModel{
                    Title = "Map Feature",
                    Description = "Needs to test map coordination's calculations.",
                    DueDate = DateTime.ParseExact("2023 08 15 10:30", DueDateFormat, null),
                    LastModificationDate = DateTime.ParseExact("2023 07 27 15:30", DueDateFormat, null),
                    CreationDate = DateTime.ParseExact("2023 07 27 15:30", DueDateFormat, null),
                    UserId = AdminId
                },
                new TaskModel{
                    Title = "Helper Validator",
                    Description = "Implement tests for user data validators.",
                    DueDate = DateTime.ParseExact("2023 02 04 14:00", DueDateFormat, null),
                    LastModificationDate = DateTime.ParseExact("2023 01 20 15:30", DueDateFormat, null),
                    CreationDate = DateTime.ParseExact("2023 01 20 15:30", DueDateFormat, null),
                    UserId = AdminId
                },
            };
            TasksProjectManagement = new List<TaskModel>()
            {
                new TaskModel{
                    Title = "Project Opportunities",
                    Description = "Prepare and discusse future opportunities for project.",
                    DueDate = DateTime.ParseExact("2023 10 15 09:30", DueDateFormat, null),
                    LastModificationDate = DateTime.ParseExact("2023 10 02 15:30", DueDateFormat, null),
                    CreationDate = DateTime.ParseExact("2023 10 02 15:30", DueDateFormat, null),
                    UserId = AdminId
                },
                new TaskModel{
                    Title = "Meeting with Client",
                    Description = "Prepare raport with details for meeting, organize hotel and transport.",
                    DueDate = DateTime.ParseExact("2023 10 10 10:30", DueDateFormat, null),
                    LastModificationDate = DateTime.ParseExact("2023 09 19 15:30", DueDateFormat, null),
                    CreationDate = DateTime.ParseExact("2023 09 19 15:30", DueDateFormat, null),
                    UserId = AdminId
                },
                new TaskModel{
                    Title = "Risks for Project X",
                    Description = "Prepare full report about project x's risks.",
                    DueDate = DateTime.ParseExact("2023 09 28 14:00", DueDateFormat, null),
                    LastModificationDate = DateTime.ParseExact("2023 09 15 15:30", DueDateFormat, null),
                    CreationDate = DateTime.ParseExact("2023 09 15 15:30", DueDateFormat, null),
                    UserId = AdminId
                },
            };
        }

        private void SeedTodoLists()
        {
            TodoLists = new List<TodoListModel>()
            {
                new TodoListModel{
                    Title = "App UX",
                    Tasks = TasksUX,
                    UserId = AdminId
                },
                new TodoListModel{
                    Title = "App Backend",
                    Tasks = TasksBackend,
                    UserId = AdminId
                },
                new TodoListModel{
                    Title = "App Testing",
                    Tasks = TasksTesting,
                    UserId = AdminId
                },
                new TodoListModel{
                    Title = "Project Management",
                    Tasks = TasksProjectManagement,
                    UserId = AdminId
                },
            };
        }

        private void SeedAllTasks()
        {
            AllTasks = new List<TaskModel>();

            AllTasks.AddRange(TasksUX);
            AllTasks.AddRange(TasksBackend);
            AllTasks.AddRange(TasksTesting);
            AllTasks.AddRange(TasksProjectManagement);
		}
    }
}