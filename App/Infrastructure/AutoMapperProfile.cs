using App.Features.Users.Common.Models;
using App.Features.Users.Common.Models.Interfaces;
using App.Features.Users.Common.Roles.Models;
using App.Features.Users.Common.Roles.Models.Interfaces;
using App.Features.Users.Login.Models;
using App.Features.Users.Login.Models.Interfaces;
using App.Features.Users.Register.Models;
using AutoMapper;

namespace App.Infrastructure;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserModel, UserDto>()
            .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles));

        CreateMap<IUserModel, IUserDto>()
            .Include<UserModel, UserDto>()
            .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles))
            .ConstructUsing((src, context) => context.Mapper.Map<UserModel, UserDto>((UserModel)src));

        CreateMap<UserDto, UserModel>()
            .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles));

        CreateMap<IUserDto, IUserModel>()
            .Include<UserDto, UserModel>()
            .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles))
            .ConstructUsing((src, context) => context.Mapper.Map<UserDto, UserModel>((UserDto)src));

        CreateMap<UserRoleModel, UserRoleDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

        CreateMap<IUserRoleModel, IUserRoleDto>()
            .Include<UserRoleModel, UserRoleDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ConstructUsing((src, context) => context.Mapper.Map<UserRoleModel, UserRoleDto>((UserRoleModel)src));

        CreateMap<UserRoleDto, UserRoleModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

        CreateMap<IUserRoleDto, IUserRoleModel>()
            .Include<UserRoleDto, UserRoleModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ConstructUsing((src, context) => context.Mapper.Map<UserRoleDto, UserRoleModel>((UserRoleDto)src));

        CreateMap<RoleDto, RoleModel>()
            .ForMember(dest => dest.RoleUsers, opt => opt.MapFrom(src => src.UserRoles));

        CreateMap<IRoleDto, IRoleModel>()
            .Include<RoleDto, RoleModel>()
            .ForMember(dest => dest.RoleUsers, opt => opt.MapFrom(src => src.UserRoles))
            .ConstructUsing((src, context) => context.Mapper.Map<RoleDto, RoleModel>((RoleDto)src));

        CreateMap<RoleModel, RoleDto>()
            .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.RoleUsers));

        CreateMap<IRoleModel, IRoleDto>()
            .Include<RoleModel, RoleDto>()
            .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.RoleUsers))
            .ConstructUsing((src, context) => context.Mapper.Map<RoleModel, RoleDto>((RoleModel)src));

        CreateMap<LoginInputVM, LoginInputDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

        CreateMap<LoginInputVM, ILoginInputDto>()
            .Include<LoginInputVM, LoginInputDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ConstructUsing((src, context) => context.Mapper.Map<LoginInputVM, LoginInputDto>(src));

        CreateMap<RegisterInputVM, UserDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Provider, opt => opt.Ignore())
            .ForMember(dest => dest.NameIdentifier, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
            .ForMember(dest => dest.DataVersion, opt => opt.Ignore());

        CreateMap<RegisterInputVM, IUserDto>()
            .Include<RegisterInputVM, UserDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Provider, opt => opt.Ignore())
            .ForMember(dest => dest.NameIdentifier, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
            .ForMember(dest => dest.DataVersion, opt => opt.Ignore())
            .ConstructUsing((src, context) => context.Mapper.Map<RegisterInputVM, UserDto>(src));
    }
}
