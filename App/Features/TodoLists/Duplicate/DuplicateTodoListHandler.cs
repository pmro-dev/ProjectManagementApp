using App.Features.Exceptions.Throw;
using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.TodoLists.Duplicate;

public class DuplicateTodoListHandler : IRequestHandler<DuplicateTodoListCommand, DuplicateTodoListCommandResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITodoListRepository _todoListRepository;
	private readonly ILogger<DuplicateTodoListHandler> _logger;

	public DuplicateTodoListHandler(IDataUnitOfWork dataUnitOfWork, ILogger<DuplicateTodoListHandler> logger)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_todoListRepository = dataUnitOfWork.TodoListRepository;
		_logger = logger;
	}

	public async Task<DuplicateTodoListCommandResponse> Handle(DuplicateTodoListCommand request, CancellationToken cancellationToken)
	{
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Duplicate), request.TodoListId, nameof(request.TodoListId), _logger);

		await _todoListRepository.DuplicateSingleWithDetailsAsync(request.TodoListId);
		await _dataUnitOfWork.SaveChangesAsync();

		return new DuplicateTodoListCommandResponse();
	}
}
