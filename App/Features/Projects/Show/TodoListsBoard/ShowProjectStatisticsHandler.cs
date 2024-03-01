using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Projects.Show.TodoListsBoard;

public class ShowProjectTodoListsHandler :
    IRequestHandler<ShowProjectTodoListsQuery, ShowProjectTodoListsQueryResponse>
{
    private readonly IDataUnitOfWork _dataUnitOfWork;
    private readonly IProjectRepository _projectRepository;
    private readonly ILogger<ShowProjectTodoListsHandler> _logger;

    public ShowProjectTodoListsHandler(IDataUnitOfWork dataUnitOfWork, ILogger<ShowProjectTodoListsHandler> logger)
    {
        _dataUnitOfWork = dataUnitOfWork;
        _projectRepository = _dataUnitOfWork.ProjectRepository;
        _logger = logger;
    }

	public Task<ShowProjectTodoListsQueryResponse> Handle(ShowProjectTodoListsQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
