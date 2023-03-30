using Project_DomainEntities;

namespace Project_Main.Models.DataBases.Repositories
{
	public class TaskRepository : GenericRepository<TaskModel>, ITaskRepository
	{
		public TaskRepository(CustomAppDbContext dbContext, ILogger<TaskRepository> logger) : base(dbContext, logger)
		{
		}
	}
}
