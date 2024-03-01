using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Teams.Delete;

public class DeleteTeamSchemeHandler :
	IRequestHandler<DeleteTeamSchemeQuery, DeleteTeamSchemeQueryResponse>,
	IRequestHandler<DeleteTeamSchemeCommand, DeleteTeamSchemeCommandResponse>
{
	private readonly ILogger<DeleteTeamWithinProjectHandler> _logger;
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITeamRepository _teamRepository;

	public DeleteTeamSchemeHandler(IDataUnitOfWork dataUnitOfWork, ILogger<DeleteTeamWithinProjectHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_teamRepository = _dataUnitOfWork.TeamRepository;
		_logger = logger;
	}

	public Task<DeleteTeamSchemeQueryResponse> Handle(DeleteTeamSchemeQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<DeleteTeamSchemeCommandResponse> Handle(DeleteTeamSchemeCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}