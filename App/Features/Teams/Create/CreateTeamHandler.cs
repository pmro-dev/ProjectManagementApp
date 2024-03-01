using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Teams.Create;

public class CreateTeamSchemeHandler :
	IRequestHandler<CreateTeamAsSchemeQuery, CreateTeamAsSchemeQueryResponse>,
	IRequestHandler<CreateTeamAsSchemeCommand, CreateTeamAsSchemeCommandResponse>
{
	private readonly ILogger<CreateTeamSchemeHandler> _logger;
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITeamRepository _teamRepository;

	public CreateTeamSchemeHandler(ILogger<CreateTeamSchemeHandler> logger, IDataUnitOfWork dataUnitOfWork)
	{
		_logger = logger;
		_dataUnitOfWork = dataUnitOfWork;
	}

	public Task<CreateTeamAsSchemeQueryResponse> Handle(CreateTeamAsSchemeQuery request, CancellationToken cancellationToken)
	{

	}

	public Task<CreateTeamAsSchemeCommandResponse> Handle(CreateTeamAsSchemeCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
