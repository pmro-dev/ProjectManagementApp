using App.Common.ViewModels;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.TodoLists.Common.Models;
using App.Infrastructure.Databases.App.Interfaces;
using App.Infrastructure.Helpers;
using MediatR;

namespace App.Features.TodoLists.Edit;

public class EditTaskHandler : IRequestHandler<EditTodoListQuery, WrapperViewModel<TodoListEditInputVM, TodoListEditOutputVM>>, IRequestHandler<EditTodoListCommand, bool>
{
	private readonly ILogger<EditTaskHandler> _logger;
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITodoListRepository _todoListRepository;
	private readonly ITodoListMapper _todoListMapper;
	private readonly ITodoListViewModelsFactory _todoListViewModelsFactory;

	public EditTaskHandler(ILogger<EditTaskHandler> logger, IDataUnitOfWork dataUnitOfWork, ITodoListRepository todoListRepository, ITodoListMapper todoListMapper, ITodoListViewModelsFactory todoListViewModelsFactory)
	{
		_logger = logger;
		_dataUnitOfWork = dataUnitOfWork;
		_todoListRepository = todoListRepository;
		_todoListMapper = todoListMapper;
		_todoListViewModelsFactory = todoListViewModelsFactory;
	}

	public async Task<WrapperViewModel<TodoListEditInputVM, TodoListEditOutputVM>> Handle(EditTodoListQuery request, CancellationToken cancellationToken)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Edit), request.TodoListId, nameof(request.TodoListId), _logger);

		TodoListModel? todoListModel = await _todoListRepository.GetAsync(request.TodoListId);
		ExceptionsService.WhenModelIsNullThrowCritical(nameof(EditTodoListQuery), todoListModel, _logger);

		var todoListDto = _todoListMapper.TransferToDto(todoListModel!);
		var editOutputVM = _todoListViewModelsFactory.CreateEditOutputVM(todoListDto);
		var editWrapperVM = _todoListViewModelsFactory.CreateWrapperEditVM();
		editWrapperVM.OutputVM = editOutputVM;

		return editWrapperVM;
	}

	public async Task<bool> Handle(EditTodoListCommand request, CancellationToken cancellationToken)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Edit), request.TodoListId, nameof(request.TodoListId), _logger);
		ExceptionsService.WhenIdsAreNotEqualThrowCritical(nameof(Edit), request.RouteTodoListId, nameof(request.RouteTodoListId), request.TodoListId, nameof(request.TodoListId), _logger);

		TodoListModel? todoListDbModel = await _todoListRepository.GetAsync(request.TodoListId);
		ExceptionsService.WhenModelIsNullThrowCritical(nameof(EditTodoListCommand), todoListDbModel, _logger);

		var editInputDto = _todoListMapper.TransferToDto(request.WrapperVM.InputVM);
		_todoListMapper.UpdateModel(todoListDbModel!, editInputDto);

		_todoListRepository.Update(todoListDbModel!);
		await _dataUnitOfWork.SaveChangesAsync();

		return true;
	}
}
