using App.Features.Tasks.Create;
using App.Features.Tasks.Delete;
using App.Features.Tasks.Edit;

namespace App.Features.Tasks.Common.Interfaces;

public interface ITaskEntityMapper
{
	TaskDto TransferToDto(TaskModel taskModel, IDictionary<object, object>? mappedObjects = null);
	TaskEditInputDto TransferToDto(TaskEditInputVM taskEditInputVM);
	TaskDeleteInputDto TransferToDto(TaskDeleteInputVM deleteInputVM);
	TaskDto TransferToDto(TaskCreateInputVM taskInputVM);
	void UpdateModel(TaskModel taskDbModel, TaskEditInputDto taskEditInputDto);
	TaskModel TransferToModel(TaskDto taskDto, IDictionary<object, object>? mappedObjects = null);
}