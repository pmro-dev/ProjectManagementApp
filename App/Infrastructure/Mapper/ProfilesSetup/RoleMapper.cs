using App.Features.Users.Common.Roles.Models.Interfaces;
using App.Features.Users.Common.Roles.Models;
using AutoMapper;

namespace App.Infrastructure.Mapper.ProfilesSetup;

public static class RoleMapper
{
	public static void SetProfiles(Profile profile)
	{
		profile.CreateMap<RoleDto, RoleModel>()
			.ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles));

		profile.CreateMap<IRoleDto, IRoleModel>()
			.Include<RoleDto, RoleModel>()
			.ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles))
			.ConstructUsing((src, context) => context.Mapper.Map<RoleDto, RoleModel>((RoleDto)src));

		profile.CreateMap<RoleModel, RoleDto>()
			.ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles));

		profile.CreateMap<IRoleModel, IRoleDto>()
			.Include<RoleModel, RoleDto>()
			.ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles))
			.ConstructUsing((src, context) => context.Mapper.Map<RoleModel, RoleDto>((RoleModel)src));
	}
}
