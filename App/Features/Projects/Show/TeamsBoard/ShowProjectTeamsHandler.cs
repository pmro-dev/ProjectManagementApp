using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Projects.Show.TeamsBoard;

public class ShowProjectTeamsHandler :
    IRequestHandler<ShowProjectTeamsQuery, ShowProjectTeamsQueryResponse>
{
    private readonly IDataUnitOfWork _dataUnitOfWork;
    private readonly IProjectRepository _projectRepository;
    private readonly ILogger<ShowProjectTeamsHandler> _logger;

    public ShowProjectTeamsHandler(IDataUnitOfWork dataUnitOfWork, ILogger<ShowProjectTeamsHandler> logger)
    {
        _dataUnitOfWork = dataUnitOfWork;
        _projectRepository = _dataUnitOfWork.ProjectRepository;
        _logger = logger;
    }

	public Task<ShowProjectTeamsQueryResponse> Handle(ShowProjectTeamsQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
