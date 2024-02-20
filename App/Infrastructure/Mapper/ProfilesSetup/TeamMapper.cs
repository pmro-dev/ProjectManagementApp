using App.Features.Teams.Common.Interfaces;
using App.Features.Teams.Common.Models;
using AutoMapper;

namespace App.Infrastructure.Mapper.ProfilesSetup;

public static class TeamMapper
{
	public static void SetProfiles(Profile profile)
	{
		#region Model - DTO

		profile.CreateMap<TeamModel, TeamDto>()
			.ForMember(desc => desc.TeamMembers, opt => opt.MapFrom(src => src.TeamMembers))
			.ForMember(desc => desc.Projects, opt => opt.MapFrom(src => src.Projects))
			.ForMember(desc => desc.TeamProjects, opt => opt.MapFrom(src => src.TeamProjects))
			.ForMember(desc => desc.TodoLists, opt => opt.MapFrom(src => src.TodoLists));

		profile.CreateMap<ITeamModel, ITeamDto>()
			.Include<TeamModel, TeamDto>()
			.ForMember(desc => desc.TeamMembers, opt => opt.MapFrom(src => src.TeamMembers))
			.ForMember(desc => desc.Projects, opt => opt.MapFrom(src => src.Projects))
			.ForMember(desc => desc.TeamProjects, opt => opt.MapFrom(src => src.TeamProjects))
			.ForMember(desc => desc.TodoLists, opt => opt.MapFrom(src => src.TodoLists))
			.ConstructUsing((src, context) => context.Mapper.Map<TeamModel, TeamDto>((TeamModel)src));
		#endregion


		#region DTO - Model

		profile.CreateMap<TeamDto, TeamModel>()
			.ForMember(desc => desc.TeamMembers, opt => opt.MapFrom(src => src.TeamMembers))
			.ForMember(desc => desc.Projects, opt => opt.MapFrom(src => src.Projects))
			.ForMember(desc => desc.TeamProjects, opt => opt.MapFrom(src => src.TeamProjects))
			.ForMember(desc => desc.TodoLists, opt => opt.MapFrom(src => src.TodoLists));

		profile.CreateMap<ITeamDto, ITeamModel>()
			.Include<TeamDto, TeamModel>()
			.ForMember(desc => desc.TeamMembers, opt => opt.MapFrom(src => src.TeamMembers))
			.ForMember(desc => desc.Projects, opt => opt.MapFrom(src => src.Projects))
			.ForMember(desc => desc.TeamProjects, opt => opt.MapFrom(src => src.TeamProjects))
			.ForMember(desc => desc.TodoLists, opt => opt.MapFrom(src => src.TodoLists))
			.ConstructUsing((src, context) => context.Mapper.Map<TeamDto, TeamModel>((TeamDto)src));
		#endregion
	}
}
