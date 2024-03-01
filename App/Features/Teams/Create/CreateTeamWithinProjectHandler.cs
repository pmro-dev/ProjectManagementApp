using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Teams.Create;

public class CreateTeamWithinProjectHandler :
	IRequestHandler<CreateTeamWithinProjectQuery, CreateTeamWithinProjectQueryResponse>,
	IRequestHandler<CreateTeamWithinProjectCommand, CreateTeamWithinProjectCommandResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITeamRepository _teamRepository;
	private readonly ILogger<CreateTeamWithinProjectHandler> _logger;

	public CreateTeamWithinProjectHandler(IDataUnitOfWork dataUnitOfWork, ILogger<CreateTeamWithinProjectHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_teamRepository = _dataUnitOfWork.TeamRepository;
		_logger = logger;
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
