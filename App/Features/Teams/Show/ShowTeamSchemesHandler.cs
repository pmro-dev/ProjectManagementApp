using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Teams.Show;

public class ShowTeamSchemesHandler :
	IRequestHandler<ShowAllTeamSchemesQuery, ShowAllTeamSchemesQueryResponse>,
	IRequestHandler<ShowTeamSchemeQuery, ShowTeamSchemeQueryResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITeamRepository _teamRepository;
	private readonly ILogger<ShowTeamSchemesHandler> _logger;

	public ShowTeamSchemesHandler(IDataUnitOfWork dataUnitOfWork, ILogger<ShowTeamSchemesHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_teamRepository = _dataUnitOfWork.TeamRepository;
		_logger = logger;
	}

	public Task<ShowAllTeamSchemesQueryResponse> Handle(ShowAllTeamSchemesQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<ShowTeamSchemeQueryResponse> Handle(ShowTeamSchemeQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
