using System.ComponentModel.DataAnnotations;
using Web.TaskTags.Common.Interfaces;
using Web.TodoLists.Common.Interfaces;
using static Web.Tasks.Common.TaskStatusHelper;

namespace Web.Tasks.Common.Interfaces
{
	public interface ITaskModel
    {
        private const string DataFormat = "{0:dd'/'MM'/'yyyy}";
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }

        [DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }

        [DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
        public DateTime LastModificationDate { get; set; }

        [DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
        public DateTime? ReminderDate { get; set; }

        public TaskStatusType Status { get; set; }

        public int TodoListId { get; set; }

        public ITodoListModel? TodoList { get; set; }

        public string UserId { get; set; }

        public ICollection<ITaskTagModel> TaskTags { get; set; }
    }
}
