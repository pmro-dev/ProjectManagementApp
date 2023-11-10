
using Application.DTOs.Entities;
using Domain.Entities;

namespace Application.Factories.DTOs;

public interface ITaskTagFactory
{
	TaskTagDto CreateTaskTagDto();
	TaskTagModel CreateTaskTagModel();
}
