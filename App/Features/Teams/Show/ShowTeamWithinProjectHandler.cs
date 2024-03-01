using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Teams.Show;

public class ShowTeamWithinProjectHandler :
	IRequestHandler<ShowAllTeamsWithinProjectQuery, ShowAllTeamsWithinProjectQueryResponse>,
	IRequestHandler<ShowTeamWithinProjectQuery, ShowTeamWithinProjectQueryResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITeamRepository _teamRepository;
	private readonly ILogger<ShowTeamWithinProjectHandler> _logger;

	public ShowTeamWithinProjectHandler(IDataUnitOfWork dataUnitOfWork, ILogger<ShowTeamWithinProjectHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_teamRepository = _dataUnitOfWork.TeamRepository;
		_logger = logger;
	}

	public Task<ShowAllTeamsWithinProjectQueryResponse> Handle(ShowAllTeamsWithinProjectQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<ShowTeamWithinProjectQueryResponse> Handle(ShowTeamWithinProjectQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
