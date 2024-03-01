using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Teams.Show;

public class ShowTeamSchemesHandler :
	IRequestHandler<ShowAllTeamSchemesQuery, ShowAllTeamSchemesQueryResponse>,
	IRequestHandler<ShowTeamSchemeQuery, ShowTeamSchemeQueryResponse>
{
	private readonly ILogger<ShowTeamSchemesHandler> _logger;
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITeamRepository _teamRepository;

	public ShowTeamSchemesHandler(IDataUnitOfWork dataUnitOfWork, ITeamRepository teamRepository, ILogger<ShowTeamSchemesHandler> logger)
	{
		_logger = logger;
		_dataUnitOfWork = dataUnitOfWork;
		_teamRepository = teamRepository;
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
