
#region USINGS

using System.Reflection;
using App.Features.Tasks.Common.TaskTags.Common;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Users.Login.Interfaces;
using App.Features.Users.Authentication;
using App.Features.Tags.Common;
using App.Features.TodoLists.Common;
using App.Features.Users.Logout;
using App.Features.TodoLists.Common.Models;
using App.Features.Tasks.Common.TaskTags.Common.Interfaces;
using App.Features.Tasks.Common;
using App.Features.Users.Common;
using App.Features.Boards.Interfaces;
using App.Features.Users.Common.Claims;
using App.Infrastructure;
using App.Features.Users.Login;
using App.Features.Users.Register;
using App.Infrastructure.Databases.Identity.Seeds;
using App.Features.Users.Common.Interfaces;
using App.Features.Tags.Common.Interfaces;
using App.Infrastructure.Databases.App.Seeds;
using App.Features.Users.Logout.Interfaces;
using App.Features.Boards.Common;
using App.Features.Users.Interfaces;
using App.Features.Users.Register.Interfaces;
using Microsoft.AspNetCore.Mvc.Razor;
using App.Common.Views;

#endregion


namespace App
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Configuration.AddEnvironmentVariables().AddUserSecrets(Assembly.GetExecutingAssembly(), true);


			#region SETUP LOGGERS

			builder.Logging.ClearProviders();
			builder.Logging.AddConsole();

			#endregion


			builder.SetupEnvironmentSettings();

			builder.AddCustomDbContexts();
			builder.SetupUnitOfWorkServices();

			builder.Services.AddHttpContextAccessor();


			#region IDENTITY SERVICES

			builder.Services.AddScoped<ILoginService, LoginService>();
			builder.Services.AddScoped<ILogoutService, LogoutService>();
			builder.Services.AddScoped<IUserRegisterService, UserRegisterService>();
			builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
			builder.Services.AddScoped<IClaimsService, ClaimsService>();
			builder.Services.AddScoped<IUserService, UserService>();

			#endregion


			#region MAPPERS

			builder.Services.AddScoped<IUserMapper, UserMapper>();

			builder.Services.AddTransient<ITaskDto, TaskDto>();
			builder.Services.AddTransient<ITaskModel, TaskModel>();
			//builder.Services.AddScoped<ITaskCreateInputDto, TaskCreateInputDto>();
			builder.Services.AddScoped<ITaskEntityMapper, TaskEntityMapper>();

			builder.Services.AddTransient<ITodoListDto, TodoListDto>();
			builder.Services.AddTransient<ITodoListModel, TodoListModel>();
			builder.Services.AddScoped<ITodoListMapper, TodoListMapper>();

			builder.Services.AddTransient<ITagDto, TagDto>();
			builder.Services.AddTransient<ITagModel, TagModel>();

			builder.Services.AddTransient<ITaskTagDto, TaskTagDto>();
			builder.Services.AddTransient<ITaskTagModel, TaskTagModel>();

			#endregion


			#region FACTORIES

			builder.Services.AddScoped<IBoardViewModelsFactory, BoardViewModelsFactory>();
			builder.Services.AddScoped<ITodoListViewModelsFactory, TodoListViewModelsFactory>();
			builder.Services.AddScoped<ITaskViewModelsFactory, TaskViewModelsFactory>();

			builder.Services.AddScoped<ITodoListFactory, TodoListFactory>();
			builder.Services.AddScoped<ITaskEntityFactory, TaskEntityFactory>();

			builder.Services.AddScoped<ILoginFactory, LoginFactory>();
			builder.Services.AddScoped<IUserFactory, UserFactory>();

			#endregion


			builder.SetupSeedDataServices();


			#region SETUP AUTHENNTICATION

			builder.SetupBasicAuthenticationWithCookie();
			builder.SetupGoogleAuthentication();

			#endregion

			builder.Services.Configure<RazorViewEngineOptions>(options =>
			{
				options.ViewLocationExpanders.Add(new ViewLocationExpander());
			});

			var app = builder.Build();
			app.SetupPipeline();


			#region SETUP ROUTES

			app.MapControllerRoute(
				name: CustomRoutes.DefaultRouteName,
				pattern: CustomRoutes.DefaultRoutePattern);

			#endregion


			#region POPULATE DATABASES

			await IdentitySeedData.EnsurePopulated(app, app.Logger);
			await DbSeeder.EnsurePopulated(app, app.Logger);

			#endregion


			app.Run();
		}
	}
}