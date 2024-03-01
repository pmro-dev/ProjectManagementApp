using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Projects.Delete;

public class DeleteProjectHandler :
	IRequestHandler<DeleteProjectQuery, DeleteProjectQueryResponse>,
	IRequestHandler<DeleteProjectCommand, DeleteProjectCommandResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly IProjectRepository _projectRepository;
	private readonly ILogger<DeleteProjectHandler> _logger;

	public DeleteProjectHandler(IDataUnitOfWork dataUnitOfWork, ILogger<DeleteProjectHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_projectRepository = _dataUnitOfWork.ProjectRepository;
		_logger = logger;
	}

	public Task<DeleteProjectQueryResponse> Handle(DeleteProjectQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<DeleteProjectCommandResponse> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
