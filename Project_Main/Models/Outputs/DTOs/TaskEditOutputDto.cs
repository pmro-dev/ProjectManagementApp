//using Microsoft.AspNetCore.Mvc.Rendering;
//using static Project_DomainEntities.Helpers.TaskStatusHelper;
//using System.ComponentModel.DataAnnotations;

//namespace Project_Main.Models.Outputs.DTOs
//{
//    public class TaskEditOutputDto : ITaskEditOutputDto
//    {
//        private const string DataFormat = "{0:yyyy-MM-dd}";
//        private const int defaultId = 0;

//        public int Id { get; set; }

//        public string UserId { get; set; } = string.Empty;

//        public string Title { get; set; } = string.Empty;

//        [DataType(DataType.MultilineText)]
//        public string Description { get; set; } = string.Empty;

//        [DataType(DataType.Date)]
//        [DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
//        public DateTime DueDate { get; set; } = DateTime.Now;

//        [DataType(DataType.Date)]
//        [DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
//        public DateTime? ReminderDate { get; set; } = null;

//        public TaskStatusType Status { get; set; } = TaskStatusType.NotStarted;

//        public int TodoListId { get; set; } = defaultId;

//        public SelectList? StatusSelector { get; set; }

//        public SelectList? TodoListsSelector { get; set; }
//    }
//}
