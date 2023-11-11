using Web.Tasks.Create.Interfaces;
using Web.Tasks.Delete.Interfaces;
using Web.Tasks.Edit.Interfaces;

namespace Web.Tasks.Common.Interfaces
{
	public interface ITaskEntityMapper
	{
		ITaskDto TransferToDto(TaskModel taskModel, IDictionary<object, object>? mappedObjects = null);
		ITaskEditInputDto TransferToDto(ITaskEditInputVM taskEditInputVM);
		ITaskDeleteInputDto TransferToDto(ITaskDeleteInputVM deleteInputVM);
		ITaskDto TransferToDto(ITaskCreateInputVM taskInputVM);
		void UpdateModel(TaskModel taskDbModel, ITaskEditInputDto taskEditInputDto);
		TaskModel TransferToModel(ITaskDto taskDto, IDictionary<object, object>? mappedObjects = null);
	}
}