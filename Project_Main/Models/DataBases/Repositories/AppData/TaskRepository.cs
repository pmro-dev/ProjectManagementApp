using Project_DomainEntities;
using Project_Main.Models.DataBases.Repositories.General;

namespace Project_Main.Models.DataBases.Repositories.AppData
{
    public class TaskRepository : GenericRepository<TaskModel>, ITaskRepository
    {
        public TaskRepository(CustomAppDbContext dbContext, ILogger<TaskRepository> logger) : base(dbContext, logger)
        {
        }
    }
}
