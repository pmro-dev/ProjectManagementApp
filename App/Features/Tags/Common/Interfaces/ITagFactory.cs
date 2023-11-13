using App.Features.Tags.Common;

namespace App.Features.Tags.Common.Interfaces;

public interface ITagFactory
{
	TagDto CreateTagDto();
	TagModel CreateTagModel();
}
