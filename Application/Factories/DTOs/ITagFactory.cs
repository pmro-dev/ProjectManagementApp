using Application.DTOs.Entities;
using Domain.Entities;

namespace Application.Factories.DTOs;

public interface ITagFactory
{
	TagDto CreateTagDto();
	TagModel CreateTagModel();
}
