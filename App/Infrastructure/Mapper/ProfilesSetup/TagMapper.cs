using App.Features.Tags.Common.Interfaces;
using App.Features.Tags.Common.Models;
using AutoMapper;

namespace App.Infrastructure.Mapper.ProfilesSetup
{
	public static class TagMapper
	{
		public static void SetProfiles(Profile profile)
		{
			#region Model - DTO

			profile.CreateMap<TagModel, TagDto>()
				.ForMember(desc => desc.TaskTags, opt => opt.MapFrom(src => src.TaskTags))
				.ForMember(desc => desc.ProjectTags, opt => opt.MapFrom(src => src.ProjectTags))
				.ForMember(desc => desc.TodoListTags, opt => opt.MapFrom(src => src.TodoListTags))
				.ForMember(desc => desc.BudgetTags, opt => opt.MapFrom(src => src.BudgetTags));

			profile.CreateMap<ITagModel, ITagDto>()
				.Include<TagModel, TagDto>()
				.ForMember(desc => desc.TaskTags, opt => opt.MapFrom(src => src.TaskTags))
				.ForMember(desc => desc.ProjectTags, opt => opt.MapFrom(src => src.ProjectTags))
				.ForMember(desc => desc.TodoListTags, opt => opt.MapFrom(src => src.TodoListTags))
				.ForMember(desc => desc.BudgetTags, opt => opt.MapFrom(src => src.BudgetTags))
				.ConstructUsing((src, context) => context.Mapper.Map<TagModel, TagDto>((TagModel)src));
			#endregion


			#region DTO - Model

			profile.CreateMap<TagDto, TagModel>()
				.ForMember(desc => desc.TaskTags, opt => opt.MapFrom(src => src.TaskTags))
				.ForMember(desc => desc.ProjectTags, opt => opt.MapFrom(src => src.ProjectTags))
				.ForMember(desc => desc.TodoListTags, opt => opt.MapFrom(src => src.TodoListTags))
				.ForMember(desc => desc.BudgetTags, opt => opt.MapFrom(src => src.BudgetTags));

			profile.CreateMap<ITagDto, ITagModel>()
				.Include<TagDto, TagModel>()
				.ForMember(desc => desc.TaskTags, opt => opt.MapFrom(src => src.TaskTags))
				.ForMember(desc => desc.ProjectTags, opt => opt.MapFrom(src => src.ProjectTags))
				.ForMember(desc => desc.TodoListTags, opt => opt.MapFrom(src => src.TodoListTags))
				.ForMember(desc => desc.BudgetTags, opt => opt.MapFrom(src => src.BudgetTags))
				.ConstructUsing((src, context) => context.Mapper.Map<TagDto, TagModel>((TagDto)src));
			#endregion
		}
	}
}
