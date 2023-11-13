using App.Features.Tasks.Create.Interfaces;
using App.Features.Tasks.Delete.Interfaces;
using App.Features.Tasks.Edit.Interfaces;

namespace App.Features.Tasks.Common.Interfaces;

public interface ITaskEntityMapper
{
	ITaskDto TransferToDto(TaskModel taskModel, IDictionary<object, object>? mappedObjects = null);
	ITaskEditInputDto TransferToDto(ITaskEditInputVM taskEditInputVM);
	ITaskDeleteInputDto TransferToDto(ITaskDeleteInputVM deleteInputVM);
	ITaskDto TransferToDto(ITaskCreateInputVM taskInputVM);
	void UpdateModel(TaskModel taskDbModel, ITaskEditInputDto taskEditInputDto);
	TaskModel TransferToModel(ITaskDto taskDto, IDictionary<object, object>? mappedObjects = null);
}