using Project_DomainEntities;
using Project_Main.Infrastructure.DTOs;

namespace Project_Main.Services.DTO.Builders
{
    public interface ITaskTagFactory : IEntityFactory<ITaskTagModel, ITaskTagDto>
    {
    }
}