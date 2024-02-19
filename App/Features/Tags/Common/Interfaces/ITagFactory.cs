using App.Common.Interfaces;
using App.Features.Tags.Common.Models;

namespace App.Features.Tags.Common.Interfaces;

public interface ITagFactory : IBaseEntityFactory<TagModel, TagDto>
{
}
