using App.Features.Users.Common.Models.Interfaces;
using App.Features.Users.Common.Models;
using App.Features.Users.Register.Models;
using AutoMapper;

namespace App.Infrastructure.Mapper.ProfilesSetup;

public static class RegisterVMMapper
{
	public static void SetProfiles(Profile profile)
	{
		profile.CreateMap<RegisterInputVM, UserDto>()
			.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Name))
			.ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
			.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
			.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))
			.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name))
			.ForMember(dest => dest.Provider, opt => opt.Ignore())
			.ForMember(dest => dest.NameIdentifier, opt => opt.Ignore())
			.ForMember(dest => dest.Id, opt => opt.Ignore())
			.ForMember(dest => dest.UserRoles, opt => opt.Ignore())
			.ForMember(dest => dest.RowVersion, opt => opt.Ignore());

		profile.CreateMap<RegisterInputVM, IUserDto>()
			.Include<RegisterInputVM, UserDto>()
			.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Name))
			.ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
			.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
			.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))
			.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name))
			.ForMember(dest => dest.Provider, opt => opt.Ignore())
			.ForMember(dest => dest.NameIdentifier, opt => opt.Ignore())
			.ForMember(dest => dest.Id, opt => opt.Ignore())
			.ForMember(dest => dest.UserRoles, opt => opt.Ignore())
			.ForMember(dest => dest.RowVersion, opt => opt.Ignore())
			.ConstructUsing((src, context) => context.Mapper.Map<RegisterInputVM, UserDto>(src));
	}
}
