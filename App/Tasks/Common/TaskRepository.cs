using Web.Databases.App;
using Web.Databases.Common;
using Web.Tasks.Common.Interfaces;

namespace Web.Tasks.Common
{
	public class TaskRepository : GenericRepository<TaskModel>, ITaskRepository
	{
		///<inheritdoc />
		public TaskRepository(CustomAppDbContext dbContext, ILogger<TaskRepository> logger) : base(dbContext, logger)
		{
		}
	}
}
