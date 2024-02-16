using App.Features.Exceptions.Throw;
using App.Features.Users.Common.Interfaces;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Roles.Interfaces;
using App.Features.Users.Common.Roles.Models;
using App.Infrastructure.Databases.Identity.Interfaces;
using App.Infrastructure.Databases.Identity.Seeds;
using AutoMapper;

namespace App.Features.Users.Common.Roles;

public class RoleService : IRoleService
{
    private readonly ILogger<RoleService> _logger;
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;
    private readonly IUserFactory _userFactory;

    public RoleService(ILogger<RoleService> logger, IRoleRepository roleRepository, IMapper mapper, IUserFactory userFactory)
    {
        _logger = logger;
        _roleRepository = roleRepository;
        _mapper = mapper;
        _userFactory = userFactory;
    }

    public async Task AddDefaultRoleToUserAsync(UserDto userDto)
    {
        RoleModel? roleModel = await _roleRepository.GetByFilterAsync(r => r.Name == IdentityDbSeeder.DefaultRole);
        ExceptionsService.WhenRoleNotFoundInDbThrow(nameof(AddDefaultRoleToUserAsync), roleModel, IdentityDbSeeder.DefaultRole, _logger);

        RoleDto? roleDto = _mapper.Map<RoleDto>(roleModel);

        var userRoleDto = _userFactory.CreateUserRoleDto();
        userRoleDto.UserId = userDto.Id;
        userRoleDto.RoleId = roleDto.Id;
        userDto.UserRoles.Add(userRoleDto);
    }
}
