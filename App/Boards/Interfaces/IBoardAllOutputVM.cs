using Web.TodoLists.Common.Interfaces;

namespace Web.Boards.Interfaces
{
    public interface IBoardAllOutputVM
    {
        ICollection<ITodoListDto> TodoLists { get; set; }
    }
}