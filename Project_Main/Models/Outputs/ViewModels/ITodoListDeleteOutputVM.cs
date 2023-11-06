namespace Project_Main.Models.Outputs.ViewModels
{
    public interface ITodoListDeleteOutputVM
    {
        public int Id { get; set; }
        public int TasksCount { get; set; }
        public string Title { get; set; }
    }
}