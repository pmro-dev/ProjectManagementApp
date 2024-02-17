using App.Infrastructure.Databases.Common.Interfaces;

namespace App.Infrastructure.Databases.App.Interfaces;

///<inheritdoc />
public interface IDataUnitOfWork : IUnitOfWork
{
	public ITodoListRepository TodoListRepository { get; }

	public ITaskRepository TaskRepository { get; }

	public IProjectRepository ProjectRepository { get; }

	public ITeamRepository TeamRepository { get; }

	public IBudgetRepository BudgetRepository { get; }

	public IBillingsRepository BillingsRepository { get; }

	public IIncomeRepository IncomeRepository { get; }

	public ITagRepository TagRepository { get; }
}
