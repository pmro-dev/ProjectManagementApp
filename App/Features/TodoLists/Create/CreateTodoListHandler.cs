using MediatR;
using App.Features.TodoLists.Common.Interfaces;
using App.Infrastructure.Databases.App.Interfaces;
using App.Features.TodoLists.Create.Models;
using App.Common.Helpers;

namespace App.Features.TodoLists.Create;

public class CreateTodoListHandler : 
	IRequestHandler<CreateTodoListQuery, CreateTodoListQueryResponse>, 
	IRequestHandler<CreateTodoListCommand, CreateTodoListCommandResponse>
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

    public async Task<CreateTodoListQueryResponse> Handle(CreateTodoListQuery request, CancellationToken cancellationToken)
	{
		return await Task.Factory.StartNew(() =>
		{
			TodoListCreateOutputVM outputVM = _todoListViewModelsFactory.CreateCreateOutputVM(request.UserId);
			var data = _todoListViewModelsFactory.CreateWrapperCreateVM();
			data.OutputVM = outputVM;

			return new CreateTodoListQueryResponse(data);
		});
	}

	public async Task<CreateTodoListCommandResponse> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
	{
        var todoListDto = _todoListMapper.TransferToDto(request.InputVM);
        if (await _todoListRepository.ContainsAny(todoList => todoList.Title == todoListDto.Title && todoList.UserId == todoListDto.UserId))
			return new CreateTodoListCommandResponse(MessagesPacket.NameTaken, StatusCodesExtension.EntityNameTaken);

        var todoListModel = _todoListMapper.TransferToModel(todoListDto);
		await _todoListRepository.AddAsync(todoListModel);
		await _dataUnitOfWork.SaveChangesAsync();

		return new CreateTodoListCommandResponse();
	}
}
