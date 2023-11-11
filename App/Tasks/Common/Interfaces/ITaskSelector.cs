using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Tasks.Common.Interfaces
{
    public interface ITaskSelector
    {
        SelectList Create(ITaskDto taskDto);
    }
}
