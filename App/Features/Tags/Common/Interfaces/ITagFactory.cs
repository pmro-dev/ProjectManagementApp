using App.Features.Tags.Common.Models;

namespace App.Features.Tags.Common.Interfaces;

public interface ITagFactory
{
	TagDto CreateTagDto();
	TagModel CreateTagModel();
}
