using App.Features.Users.Login.Models.Interfaces;
using App.Features.Users.Login.Models;
using AutoMapper;

namespace App.Infrastructure.Mapper.ProfilesSetup;

public static class LoginVMMapper
{
	public static void SetProfiles(Profile profile)
	{
		profile.CreateMap<LoginInputVM, LoginInputDto>()
			.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
			.ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

		profile.CreateMap<LoginInputVM, ILoginInputDto>()
			.Include<LoginInputVM, LoginInputDto>()
			.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
			.ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
			.ConstructUsing((src, context) => context.Mapper.Map<LoginInputVM, LoginInputDto>(src));
	}
}
