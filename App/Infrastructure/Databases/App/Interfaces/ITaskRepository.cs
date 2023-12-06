using App.Features.Tasks.Common.Models;
using App.Infrastructure.Databases.Common.Interfaces;

namespace App.Infrastructure.Databases.App.Interfaces;

///<inheritdoc />
public interface ITaskRepository : IGenericRepository<TaskModel, int>
{
}
