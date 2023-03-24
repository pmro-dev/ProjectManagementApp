namespace Project_DomainEntities
{
    public class TaskTagModel
    {
        public int TaskId { get; set; }
        public TaskModel Task { get; set; } = new TaskModel();

        public int TagId { get; set; }
        public TagModel Tag { get; set; } = new TagModel();
    }
}
