using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Teams.Edit;

public class EditTeamWithinProjectHandler :
	IRequestHandler<EditTeamWithinProjectQuery, EditTeamWithinProjectQueryResponse>,
	IRequestHandler<EditTeamWithinProjectCommand, EditTeamWithinProjectCommandResponse>
{
	private readonly ILogger<EditTeamWithinProjectHandler> _logger;
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITeamRepository _teamRepository;

	public EditTeamWithinProjectHandler(IDataUnitOfWork dataUnitOfWork, ITeamRepository teamRepository, ILogger<EditTeamWithinProjectHandler> logger)
	{
		_logger = logger;
		_dataUnitOfWork = dataUnitOfWork;
		_teamRepository = teamRepository;
	}

	Task<EditTeamWithinProjectQueryResponse> IRequestHandler<EditTeamWithinProjectQuery, EditTeamWithinProjectQueryResponse>.Handle(EditTeamWithinProjectQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	Task<EditTeamWithinProjectCommandResponse> IRequestHandler<EditTeamWithinProjectCommand, EditTeamWithinProjectCommandResponse>.Handle(EditTeamWithinProjectCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
