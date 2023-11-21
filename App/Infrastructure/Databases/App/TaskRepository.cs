using App.Features.Tasks.Common.Models;
using App.Infrastructure.Databases.App.Interfaces;
using App.Infrastructure.Databases.Common;

namespace App.Infrastructure.Databases.App;

public class TaskRepository : GenericRepository<TaskModel>, ITaskRepository
{
	///<inheritdoc />
	public TaskRepository(CustomAppDbContext dbContext, ILogger<TaskRepository> logger) : base(dbContext, logger)
	{
	}
}
