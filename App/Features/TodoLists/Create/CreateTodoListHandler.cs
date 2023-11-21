using App.Common.ViewModels;
using MediatR;
using App.Features.TodoLists.Common.Interfaces;
using App.Infrastructure.Databases.App.Interfaces;
using App.Features.TodoLists.Create.Models;

namespace App.Features.TodoLists.Create;

public class CreateTodoListHandler : IRequestHandler<CreateTodoListQuery, WrapperViewModel<TodoListCreateInputVM, TodoListCreateOutputVM>>, IRequestHandler<CreateTodoListCommand, bool>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITodoListRepository _todoListRepository;
	private readonly ILogger<CreateTodoListHandler> _logger;
	private readonly ITodoListMapper _todoListMapper;
	private readonly ITodoListViewModelsFactory _todoListViewModelsFactory;

	public CreateTodoListHandler(IDataUnitOfWork dataUnitOfWork, ITodoListRepository todoListRepository, ILogger<CreateTodoListHandler> logger, 
		ITodoListMapper todoListMapper, ITodoListViewModelsFactory todoListViewModelsFactory)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_todoListRepository = todoListRepository;
		_logger = logger;
		_todoListMapper = todoListMapper;
		_todoListViewModelsFactory = todoListViewModelsFactory;
	}

	public Task<WrapperViewModel<TodoListCreateInputVM, TodoListCreateOutputVM>> Handle(CreateTodoListQuery request, CancellationToken cancellationToken)
	{
		return Task.Factory.StartNew(() =>
		{
			TodoListCreateOutputVM createOutputVM = _todoListViewModelsFactory.CreateCreateOutputVM(request.UserId);
			var createWrapperVM = _todoListViewModelsFactory.CreateWrapperCreateVM();
			createWrapperVM.OutputVM = createOutputVM;

			return createWrapperVM;
		});
	}

	public async Task<bool> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
	{
		var todoListDto = _todoListMapper.TransferToDto(request.WrapperVM.InputVM);

		if (await _todoListRepository.CheckThatAnyWithSameNameExistAsync(todoListDto.Title))
		{
			return false;
			//TODO currently this code is in a controller under Create action, need to change it and return
			// some error to a new result oobject
			//ModelState.AddModelError(string.Empty, MessagesPacket.NameTaken);
			//return View(createWrapperVM);
		}

		todoListDto.UserId = request.UserId;

		var todoListModel = _todoListMapper.TransferToModel(todoListDto);
		await _todoListRepository.AddAsync(todoListModel);
		await _dataUnitOfWork.SaveChangesAsync();

		return true;
	}
}
