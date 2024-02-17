using App.Infrastructure.Databases.App.Interfaces;
using App.Infrastructure.Databases.Common;

namespace App.Infrastructure.Databases.App;

///<inheritdoc />
public class DataUnitOfWork : UnitOfWork<CustomAppDbContext>, IDataUnitOfWork
{
	protected readonly ILogger<DataUnitOfWork> _logger;

	///<inheritdoc />
	public ITodoListRepository TodoListRepository { get; }

	///<inheritdoc />
	public ITaskRepository TaskRepository { get; }

	///<inheritdoc />
	public IProjectRepository ProjectRepository { get; }

	///<inheritdoc />
	public ITeamRepository TeamRepository { get; }

	///<inheritdoc />
	public IBudgetRepository BudgetRepository { get; }

	///<inheritdoc />
	public IBillingsRepository BillingsRepository { get; }

	///<inheritdoc />
	public IIncomeRepository IncomeRepository { get; }

	///<inheritdoc />
	public ITagRepository TagRepository { get; }

	public DataUnitOfWork(CustomAppDbContext context, ITodoListRepository todoListRepository, ITaskRepository taskRepository, 
		IProjectRepository projectRepository, ITeamRepository teamRepository, IBudgetRepository budgetRepository, 
		IBillingsRepository billingsRepository, IIncomeRepository incomeRepository, ITagRepository tagRepository,
		ILogger<DataUnitOfWork> logger) : base(context, logger)
	{
		TodoListRepository = todoListRepository;
		TaskRepository = taskRepository;
		_logger = logger;
		ProjectRepository = projectRepository;
		TeamRepository = teamRepository;
		BudgetRepository = budgetRepository;
		BillingsRepository = billingsRepository;
		IncomeRepository = incomeRepository;
		TagRepository = tagRepository;
	}
}
