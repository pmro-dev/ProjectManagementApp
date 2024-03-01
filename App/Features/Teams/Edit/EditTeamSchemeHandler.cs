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

	public EditTeamSchemeHandler(ILogger<EditTeamSchemeHandler> logger, IDataUnitOfWork dataUnitOfWork, ITeamRepository teamRepository)
	{
		_logger = logger;
		_dataUnitOfWork = dataUnitOfWork;
		_teamRepository = teamRepository;
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
