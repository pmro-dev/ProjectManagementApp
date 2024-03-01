using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Teams.Delete;

public class DeleteTeamWithinProjectHandler :
	IRequestHandler<DeleteTeamWithinProjectQuery, DeleteTeamWithinProjectQueryResponse>,
	IRequestHandler<DeleteTeamWithinProjectCommand, DeleteTeamWithinProjectCommandResponse>
{
	private readonly ILogger<DeleteTeamWithinProjectHandler> _logger;
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITeamRepository _teamRepository;

	public DeleteTeamWithinProjectHandler(IDataUnitOfWork dataUnitOfWork, ILogger<DeleteTeamWithinProjectHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_teamRepository = _dataUnitOfWork.TeamRepository;
		_logger = logger;
	}

	public Task<DeleteTeamWithinProjectQueryResponse> Handle(DeleteTeamWithinProjectQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<DeleteTeamWithinProjectCommandResponse> Handle(DeleteTeamWithinProjectCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}