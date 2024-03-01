using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Teams.Create;

public class CreateTeamWithinProjectHandler :
	IRequestHandler<CreateTeamWithinProjectQuery, CreateTeamWithinProjectQueryResponse>,
	IRequestHandler<CreateTeamWithinProjectCommand, CreateTeamWithinProjectCommandResponse>
{
	private readonly ILogger<CreateTeamWithinProjectHandler> _logger;
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITeamRepository _teamRepository;

	public CreateTeamWithinProjectHandler(ILogger<CreateTeamWithinProjectHandler> logger, IDataUnitOfWork dataUnitOfWork)
	{
		_logger = logger;
		_dataUnitOfWork = dataUnitOfWork;
	}

	public Task<CreateTeamWithinProjectQueryResponse> Handle(CreateTeamWithinProjectQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<CreateTeamWithinProjectCommandResponse> Handle(CreateTeamWithinProjectCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
