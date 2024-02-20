using App.Features.Budgets.Common.Interfaces;
using App.Features.Budgets.Common.Models;
using AutoMapper;

namespace App.Infrastructure.Mapper.ProfilesSetup;

public static class BudgetMapper
{
	public static void SetProfiles(Profile profile)
	{
		#region Model - DTO

		profile.CreateMap<BudgetModel, BudgetDto>()
			.ForMember(desc => desc.BudgetTags, opt => opt.MapFrom(src => src.BudgetTags));

		profile.CreateMap<IBudgetModel, IBudgetDto>()
			.Include<BudgetModel, BudgetDto>()
			.ForMember(desc => desc.BudgetTags, opt => opt.MapFrom(src => src.BudgetTags))
			.ConstructUsing((src, context) => context.Mapper.Map<BudgetModel, BudgetDto>((BudgetModel)src));
		#endregion


		#region DTO - Model

		profile.CreateMap<BudgetDto, BudgetModel>()
			.ForMember(desc => desc.BudgetTags, opt => opt.MapFrom(src => src.BudgetTags));

		profile.CreateMap<IBudgetDto, IBudgetModel>()
			.Include<BudgetDto, BudgetModel>()
			.ForMember(desc => desc.BudgetTags, opt => opt.MapFrom(src => src.BudgetTags))
			.ConstructUsing((src, context) => context.Mapper.Map<BudgetDto, BudgetModel > ((BudgetDto)src));
		#endregion


		#region Join Tables

			#region Model - DTO
				profile.CreateMap<BudgetTagModel, BudgetTagDto>();

				profile.CreateMap<IBudgetTagModel, IBudgetTagDto>()
					.Include<BudgetTagModel, BudgetTagDto>()
					.ConstructUsing((src, context) => context.Mapper.Map<BudgetTagModel, BudgetTagDto>((BudgetTagModel)src));
		#endregion


			#region DTO - Model

				profile.CreateMap<BudgetTagDto, BudgetTagModel>();

				profile.CreateMap<IBudgetTagDto, IBudgetTagModel>()
					.Include<BudgetTagDto, BudgetTagModel>()
					.ConstructUsing((src, context) => context.Mapper.Map<BudgetTagDto, BudgetTagModel>((BudgetTagDto)src));
			#endregion
		#endregion
	}
}
