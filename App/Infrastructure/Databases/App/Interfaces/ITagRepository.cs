using App.Features.Tags.Common.Models;
using App.Infrastructure.Databases.Common.Interfaces;

namespace App.Infrastructure.Databases.App.Interfaces;

public interface ITagRepository : IGenericRepository<TagModel>
{
}
