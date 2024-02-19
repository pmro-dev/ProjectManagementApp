using App.Features.Incomes.Common.Interfaces;
using App.Features.Incomes.Common.Models;
using AutoMapper;

namespace App.Infrastructure.Mapper.ProfilesSetup;

public static class IncomeMapper
{
	public static void SetProfiles(Profile profile)
	{
		#region Model - DTO

		profile.CreateMap<IncomeModel, IncomeDto>();

		profile.CreateMap<IIncomeModel, IIncomeDto>()
			.Include<IncomeModel, IncomeDto>()
			.ConstructUsing((src, context) => context.Mapper.Map<IncomeModel, IncomeDto>((IncomeModel)src));
		#endregion


		#region DTO - Model

		profile.CreateMap<IncomeDto, IncomeModel>();

		profile.CreateMap<IIncomeDto, IIncomeModel>()
			.Include<IncomeDto, IncomeModel>()
			.ConstructUsing((src, context) => context.Mapper.Map<IncomeDto, IncomeModel> ((IncomeDto)src));
		#endregion
	}
}