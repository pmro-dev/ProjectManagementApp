using App.Infrastructure.Mapper.ProfilesSetup;
using AutoMapper;

namespace App.Infrastructure.Mapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
		UserMapper.SetProfiles(this);

		RoleMapper.SetProfiles(this);

		LoginVMMapper.SetProfiles(this);
	
		RegisterVMMapper.SetProfiles(this);

		ProjectMapper.SetProfiles(this);

		BudgetMapper.SetProfiles(this);

		IncomeMapper.SetProfiles(this);

		BillingMapper.SetProfiles(this);

		TagMapper.SetProfiles(this);
	}
}
