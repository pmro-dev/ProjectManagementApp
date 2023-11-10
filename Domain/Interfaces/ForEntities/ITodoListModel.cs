namespace Domain.Interfaces.ForEntities
{
    public interface ITodoListModel
    {
        int Id { get; set; }
        ICollection<ITaskModel> Tasks { get; set; }
        string Title { get; set; }
        string UserId { get; set; }

        bool Equals(object? obj);
        int GetHashCode();
        bool IsTheSame(ITodoListModel obj);
    }
}