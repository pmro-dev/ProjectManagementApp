using App.Infrastructure.Mapper.ProfilesSetup;
using AutoMapper;

namespace App.Infrastructure.Mapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
		UserMapper.SetProfiles(this);

		RoleMapper.SetProfiles(this);

		LoginMapper.SetProfiles(this);
	
		RegisterMapper.SetProfiles(this);
	}
}
