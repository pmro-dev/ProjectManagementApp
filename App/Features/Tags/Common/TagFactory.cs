using App.Features.Tags.Common.Interfaces;
using App.Features.Tags.Common.Models;

namespace App.Features.Tags.Common;

public class TagFactory : ITagFactory
{
	public TagDto CreateDto()
	{
		return new TagDto();
	}

	public TagModel CreateModel()
	{
		return new TagModel();
	}
}
