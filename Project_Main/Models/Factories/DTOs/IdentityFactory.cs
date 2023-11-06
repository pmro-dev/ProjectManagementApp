using Project_IdentityDomainEntities;
using Project_Main.Models.DTOs;
using Project_Main.Models.Inputs.DTOs;

namespace Project_Main.Models.Factories.DTOs
{
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
}
