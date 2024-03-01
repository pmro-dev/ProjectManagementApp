using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Teams.Create;

public class CreateTeamSchemeHandler :
	IRequestHandler<CreateTeamAsSchemeQuery, CreateTeamAsSchemeQueryResponse>,
	IRequestHandler<CreateTeamAsSchemeCommand, CreateTeamAsSchemeCommandResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITeamRepository _teamRepository;
	private readonly ILogger<CreateTeamSchemeHandler> _logger;

	public CreateTeamSchemeHandler(IDataUnitOfWork dataUnitOfWork, ILogger<CreateTeamSchemeHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_teamRepository = _dataUnitOfWork.TeamRepository;
		_logger = logger;
	}

	public Task<CreateTeamAsSchemeQueryResponse> Handle(CreateTeamAsSchemeQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();

	}

	public Task<CreateTeamAsSchemeCommandResponse> Handle(CreateTeamAsSchemeCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
