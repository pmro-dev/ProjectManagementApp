using App.Features.Projects.Common.Models;
using AutoMapper;
using App.Features.Projects.Common.Interfaces;

namespace App.Infrastructure.Mapper.ProfilesSetup;

public static class ProjectMapper
{
	public static void SetProfiles(Profile profile)
	{
		profile.CreateMap<ProjectModel, ProjectDto>()
			//.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
			//.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
			//.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
			//.ForMember(dest => dest.Deadline opt => opt.MapFrom(src => src.Deadline))
			//.ForMember(dest => dest.Teams, opt => opt.MapFrom(src => src.Teams))
			//.ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.Budget))
			//.ForMember(dest => dest.Clients, opt => opt.MapFrom(src => src.Clients))
			//.ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
			//.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
			//.ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(src => src.LastUpdated))
			//.ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
			//.ForMember(dest => dest.RowVersion, opt => opt.MapFrom(src => src.RowVersion))
			//.ForMember(dest => dest.TodoLists, opt => opt.MapFrom(src => src.TodoLists))
			.ForMember(dest => dest.ProjectTeams, opt => opt.MapFrom(src => src.ProjectTeams))
			//.ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
			.ForMember(dest => dest.ProjectTags, opt => opt.MapFrom(src => src.ProjectTags))
			.ForMember(dest => dest.Owner, opt => opt.Ignore());


		profile.CreateMap<IProjectModel, IProjectDto>()
			.Include<ProjectModel, ProjectDto>()
			.ForMember(dest => dest.ProjectTeams, opt => opt.MapFrom(src => src.ProjectTeams))
			.ForMember(dest => dest.ProjectTags, opt => opt.MapFrom(src => src.ProjectTags))
			.ConstructUsing((src, context) => context.Mapper.Map<ProjectModel, ProjectDto>((ProjectModel)src));


		profile.CreateMap<ProjectDto, ProjectModel>()
			.ForMember(dest => dest.ProjectTeams, opt => opt.MapFrom(src => src.ProjectTeams))
			.ForMember(dest => dest.ProjectTags, opt => opt.MapFrom(src => src.ProjectTags));


		profile.CreateMap<IProjectDto, IProjectModel>()
			.Include<ProjectDto, ProjectModel>()
			.ForMember(dest => dest.ProjectTeams, opt => opt.MapFrom(src => src.ProjectTeams))
			.ForMember(dest => dest.ProjectTags, opt => opt.MapFrom(src => src.ProjectTags))
			.ConstructUsing((src, context) => context.Mapper.Map<ProjectDto, ProjectModel>((ProjectDto)src));


		profile.CreateMap<ProjectTeamModel, ProjectTeamDto>();


		profile.CreateMap<IProjectTeamModel, IProjectTeamDto>()
			.Include<ProjectTeamModel, ProjectTeamDto>()
			.ConstructUsing((src, context) => context.Mapper.Map<ProjectTeamModel, ProjectTeamDto>((ProjectTeamModel)src));


		profile.CreateMap<ProjectTeamDto, ProjectTeamModel>();


		profile.CreateMap<IProjectTeamDto, IProjectTeamModel>()
			.Include<ProjectTeamDto, ProjectTeamModel>()
			.ConstructUsing((src, context) => context.Mapper.Map<ProjectTeamDto, ProjectTeamModel>((ProjectTeamDto)src));


		profile.CreateMap<ProjectTagModel, ProjectTagDto>();


		profile.CreateMap<ProjectTagModel, IProjectTagDto>()
			.Include<ProjectTagModel, ProjectTagDto>()
			.ConstructUsing((src, context) => context.Mapper.Map<ProjectTagModel, ProjectTagDto>((ProjectTagModel)src));


		profile.CreateMap<ProjectTeamDto, ProjectTeamModel>();


		profile.CreateMap<IProjectTagDto, IProjectTagModel>()
			.Include<ProjectTagDto, ProjectTagModel>()
			.ConstructUsing((src, context) => context.Mapper.Map<ProjectTagDto, ProjectTagModel>((ProjectTagDto)src));
	}
}
