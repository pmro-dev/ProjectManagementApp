using App.Features.Users.Common.Models.Interfaces;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Roles.Models.Interfaces;
using App.Features.Users.Common.Roles.Models;
using AutoMapper;

namespace App.Infrastructure.Mapper.ProfilesSetup
{
	public static class UserMapper
	{
        public static void SetProfiles(Profile profile)
        {
			profile.CreateMap<UserModel, UserDto>()
				.ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles));

			profile.CreateMap<IUserModel, IUserDto>()
				.Include<UserModel, UserDto>()
				.ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles))
				.ConstructUsing((src, context) => context.Mapper.Map<UserModel, UserDto>((UserModel)src));

			profile.CreateMap<UserDto, UserModel>()
				.ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles));

			profile.CreateMap<IUserDto, IUserModel>()
				.Include<UserDto, UserModel>()
				.ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles))
				.ConstructUsing((src, context) => context.Mapper.Map<UserDto, UserModel>((UserDto)src));

			profile.CreateMap<UserRoleModel, UserRoleDto>()
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
				.ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

			profile.CreateMap<IUserRoleModel, IUserRoleDto>()
				.Include<UserRoleModel, UserRoleDto>()
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
				.ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
				.ConstructUsing((src, context) => context.Mapper.Map<UserRoleModel, UserRoleDto>((UserRoleModel)src));

			profile.CreateMap<UserRoleDto, UserRoleModel>()
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
				.ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

			profile.CreateMap<IUserRoleDto, IUserRoleModel>()
				.Include<UserRoleDto, UserRoleModel>()
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
				.ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
				.ConstructUsing((src, context) => context.Mapper.Map<UserRoleDto, UserRoleModel>((UserRoleDto)src));
		}
    }
}
