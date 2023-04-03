using Project_DomainEntities;
using Project_Main.Models.DataBases.General;

namespace Project_Main.Models.DataBases.AppData
{
    public class TaskRepository : GenericRepository<TaskModel>, ITaskRepository
    {
        public TaskRepository(CustomAppDbContext dbContext, ILogger<TaskRepository> logger) : base(dbContext, logger)
        {
        }
    }
}
