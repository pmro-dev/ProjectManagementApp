using App.Features.Billings.Common.Interfaces;
using App.Features.Billings.Common.Models;
using AutoMapper;

namespace App.Infrastructure.Mapper.ProfilesSetup;

public static class BillingMapper
{
	public static void SetProfiles(Profile profile)
	{
		#region Model - DTO

		profile.CreateMap<BillingModel, BillingDto>();

		profile.CreateMap<IBillingModel, IBillingDto>()
			.Include<BillingModel, BillingDto>()
			.ConstructUsing((src, context) => context.Mapper.Map<BillingModel, BillingDto>((BillingModel)src));
		#endregion


		#region DTO - Model

		profile.CreateMap<BillingDto, BillingModel>();

		profile.CreateMap<IBillingDto, IBillingModel>()
			.Include<BillingDto, BillingModel>()
			.ConstructUsing((src, context) => context.Mapper.Map<BillingDto, BillingModel>((BillingDto)src));
		#endregion
	}
}
