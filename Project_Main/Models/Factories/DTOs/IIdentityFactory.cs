using Project_IdentityDomainEntities;
using Project_Main.Models.DTOs;
using Project_Main.Models.Inputs.DTOs;

namespace Project_Main.Models.Factories.DTOs
{
	public interface IIdentityFactory : IBaseEntityFactory<UserModel, UserDto>
	{
		LoginInputDto CreateLoginInputDto();
	}
}
