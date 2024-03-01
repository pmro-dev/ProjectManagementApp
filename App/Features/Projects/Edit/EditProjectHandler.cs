using App.Features.Teams.Edit;
using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Projects.Edit;

public class EditProjectHandler :
	IRequestHandler<EditProjectQuery, EditProjectQueryResponse>,
	IRequestHandler<EditTeamWithinProjectCommand, EditTeamWithinProjectCommandResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly IProjectRepository _projectRepository;
	private readonly ILogger<EditProjectHandler> _logger;

	public EditProjectHandler(IDataUnitOfWork dataUnitOfWork, ILogger<EditProjectHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_projectRepository = _dataUnitOfWork.ProjectRepository;
		_logger = logger;
	}

	public Task<EditProjectQueryResponse> Handle(EditProjectQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<EditTeamWithinProjectCommandResponse> Handle(EditTeamWithinProjectCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

}
