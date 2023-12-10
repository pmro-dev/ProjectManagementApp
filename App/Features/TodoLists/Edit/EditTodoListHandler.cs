using App.Common.Helpers;
using App.Features.Exceptions.Throw;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.TodoLists.Common.Models;
using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.TodoLists.Edit;

public class EditTodoListHandler : 
	IRequestHandler<EditTodoListQuery, EditTodoListQueryResponse>, 
	IRequestHandler<EditTodoListCommand, EditTodoListCommandResponse>
{
	private readonly ILogger<EditTodoListHandler> _logger;
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITodoListRepository _todoListRepository;
	private readonly ITodoListMapper _todoListMapper;
	private readonly ITodoListViewModelsFactory _todoListViewModelsFactory;

	public EditTodoListHandler(ILogger<EditTodoListHandler> logger, IDataUnitOfWork dataUnitOfWork, ITodoListRepository todoListRepository, ITodoListMapper todoListMapper, ITodoListViewModelsFactory todoListViewModelsFactory)
	{
		_logger = logger;
		_dataUnitOfWork = dataUnitOfWork;
		_todoListRepository = todoListRepository;
		_todoListMapper = todoListMapper;
		_todoListViewModelsFactory = todoListViewModelsFactory;
	}

	public async Task<EditTodoListQueryResponse> Handle(EditTodoListQuery request, CancellationToken cancellationToken)
	{
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Edit), request.TodoListId, nameof(request.TodoListId), _logger);

		TodoListModel? todoListModel = await _todoListRepository.GetAsync(request.TodoListId);
		ExceptionsService.WhenEntityIsNullThrowCritical(nameof(EditTodoListQuery), todoListModel, _logger, request.TodoListId);

		var todoListDto = _todoListMapper.TransferToDto(todoListModel!);
		var editOutputVM = _todoListViewModelsFactory.CreateEditOutputVM(todoListDto);
		var data = _todoListViewModelsFactory.CreateWrapperEditVM();
		data.OutputVM = editOutputVM;

		return new EditTodoListQueryResponse(data);
	}

	public async Task<EditTodoListCommandResponse> Handle(EditTodoListCommand request, CancellationToken cancellationToken)
	{
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Edit), request.TodoListId, nameof(request.TodoListId), _logger);
		ExceptionsService.WhenIdsAreNotEqualThrowCritical(nameof(Edit), request.RouteTodoListId, nameof(request.RouteTodoListId), request.TodoListId, nameof(request.TodoListId), _logger);

		if (await _todoListRepository.ContainsAny(todoList => todoList.Title == request.InputVM.Title && todoList.UserId == request.InputVM.UserId))
			return new EditTodoListCommandResponse(ExceptionsMessages.NameTaken, StatusCodesExtension.EntityNameTaken);

		TodoListModel? todoListDbModel = await _todoListRepository.GetAsync(request.TodoListId);
		ExceptionsService.WhenEntityIsNullThrowCritical(nameof(EditTodoListCommand), todoListDbModel, _logger, request.TodoListId);

		var editInputDto = _todoListMapper.TransferToDto(request.InputVM);
		_todoListMapper.UpdateModel(todoListDbModel!, editInputDto);

		_todoListRepository.Update(todoListDbModel!);
		await _dataUnitOfWork.SaveChangesAsync();

		return new EditTodoListCommandResponse();
	}
}
