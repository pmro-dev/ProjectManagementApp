using Application.DTOs.Entities;
using Application.DTOs.ForViewModels.Inputs;
using Domain.Entities.ForIdentity;

namespace Application.Factories.DTOs;

public interface IIdentityFactory : IBaseEntityFactory<UserModel, UserDto>
{
	LoginInputDto CreateLoginInputDto();
}
