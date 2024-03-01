using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Teams.Edit;

public class EditTeamSchemeHandler :
	IRequestHandler<EditTeamSchemeQuery, EditTeamSchemeQueryResponse>,
	IRequestHandler<EditTeamSchemeCommand, EditTeamSchemeCommandResponse>
{
	private readonly ILogger<EditTeamSchemeHandler> _logger;
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITeamRepository _teamRepository;

	public EditTeamSchemeHandler(IDataUnitOfWork dataUnitOfWork, ILogger<EditTeamSchemeHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_teamRepository = _dataUnitOfWork.TeamRepository;
		_logger = logger;
	}

	public Task<EditTeamSchemeQueryResponse> Handle(EditTeamSchemeQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<EditTeamSchemeCommandResponse> Handle(EditTeamSchemeCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
