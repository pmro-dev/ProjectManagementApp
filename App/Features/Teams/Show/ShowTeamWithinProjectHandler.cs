using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Teams.Show;

public class ShowTeamWithinProjectHandler :
	IRequestHandler<ShowAllTeamsWithinProjectQuery, ShowAllTeamsWithinProjectQueryResponse>,
	IRequestHandler<ShowTeamWithinProjectQuery, ShowTeamWithinProjectQueryResponse>
{
	private readonly ILogger<ShowTeamWithinProjectHandler> _logger;
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITeamRepository _teamRepository;

	public ShowTeamWithinProjectHandler(IDataUnitOfWork dataUnitOfWork, ITeamRepository teamRepository, ILogger<ShowTeamWithinProjectHandler> logger)
	{
		_logger = logger;
		_dataUnitOfWork = dataUnitOfWork;
		_teamRepository = teamRepository;
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
