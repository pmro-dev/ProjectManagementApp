using Application.DTOs.Entities;
using Application.DTOs.ForViewModels.Inputs;
using Domain.Entities.ForIdentity;

namespace Application.Factories.DTOs;

public class IdentityFactory : IIdentityFactory
{
	public UserModel CreateModel()
	{
		return new UserModel();
	}

	public UserDto CreateDto()
	{
		return new UserDto();
	}

	public LoginInputDto CreateLoginInputDto()
	{
		return new LoginInputDto();
	}
}
