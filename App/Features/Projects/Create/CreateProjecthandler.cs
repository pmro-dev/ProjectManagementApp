using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Projects.Create;

public class CreateProjecthandler :
	IRequestHandler<CreateProjectQuery, CreateProjectQueryResponse>,
	IRequestHandler<CreateProjectCommand, CreateProjectCommandResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly IProjectRepository _projectRepository;
	private readonly ILogger<CreateProjecthandler> _logger;

	public CreateProjecthandler(IDataUnitOfWork dataUnitOfWork, ILogger<CreateProjecthandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_projectRepository = _dataUnitOfWork.ProjectRepository;
		_logger = logger;
	}

	public Task<CreateProjectQueryResponse> Handle(CreateProjectQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<CreateProjectCommandResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
