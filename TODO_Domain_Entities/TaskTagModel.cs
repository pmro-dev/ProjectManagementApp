namespace TODO_Domain_Entities
{
    public class TaskTagModel
    {
        public int TaskId { get; set; }
        public TaskModel Task { get; set; } = new TaskModel();

        public int TagId { get; set; }
        public TagModel Tag { get; set; } = new TagModel();
    }
}
