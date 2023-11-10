namespace Infrastructure.Mappers;

public class AccountMapper : IAccountMapper
{
	private readonly IIdentityFactory _identityFactory;

	public AccountMapper(IIdentityFactory identityFactory)
	{
		_identityFactory = identityFactory;
	}

	public ILoginInputDto TransferToDto(LoginInputVM loginInputVM)
	{
		var loginInputDto = _identityFactory.CreateLoginInputDto();
		loginInputDto.Username = loginInputVM.Name;
		loginInputDto.Password = loginInputVM.Password;

		return loginInputDto;
	}

	public IUserDto TransferToUserDto(RegisterInputVM registerInputVM)
	{
		var userDto = _identityFactory.CreateDto();

		userDto.UserId = string.Empty;
		userDto.DataVersion = string.Empty;
		userDto.Provider = string.Empty;
		userDto.NameIdentifier = string.Empty;
		userDto.Username = registerInputVM.Name;
		userDto.Password = registerInputVM.Password;
		userDto.Email = registerInputVM.Email;
		userDto.FirstName = registerInputVM.Name;
		userDto.LastName = registerInputVM.Name;
		userDto.UserRoles = new List<IUserRoleModel>();

		return userDto;
	}

	public IUserDto TransferToUserDto(IUserModel userModel)
	{
		var userDto = _identityFactory.CreateDto();

		userDto.UserId = userModel.UserId;
		userDto.DataVersion = userModel.DataVersion;
		userDto.Provider = userModel.Provider;
		userDto.NameIdentifier = userModel.NameIdentifier;
		userDto.Username = userModel.Username;
		userDto.Password = userModel.Password;
		userDto.Email = userModel.Email;
		userDto.FirstName = userModel.FirstName;
		userDto.LastName = userModel.LastName;
		userDto.UserRoles = userModel.UserRoles;

		return userDto;
	}

	public IUserModel TransferToModel(IUserDto userDto)
	{
		var userModel = _identityFactory.CreateModel();

		userModel.NameIdentifier = userModel.UserId;
		userModel.Username = userDto.Username;
		userModel.Email = userDto.Email;
		userModel.FirstName = userDto.FirstName;
		userModel.LastName = userDto.LastName;
		userModel.Password = userDto.Password;
		userModel.Provider = userDto.Provider;
		userModel.UserRoles = userDto.UserRoles;

		foreach (var role in userModel.UserRoles)
		{
			role.User = userModel;
		}

		return userModel;
	}

	public IUserModel TransferToUserModel(ILoginInputDto loginInputDto)
	{
		var userModel = _identityFactory.CreateModel();

		userModel.Username = loginInputDto.Username;
		userModel.Password = loginInputDto.Password;

		return userModel;
	}
}
