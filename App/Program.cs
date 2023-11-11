
#region USINGS

using System.Reflection;
using Web.Infrastructure;
using Web.Databases.Identity.Seeds;
using Web.Accounts.Register;
using Web.Accounts.Authentication;
using Web.TodoLists.Common.Interfaces;
using Web.Tasks.Common.Interfaces;
using Web.Tasks.Common;
using Web.TodoLists.Common;
using Web.Accounts.Logout;
using Web.Databases.App.Seeds;
using Web.Accounts.Login.Interfaces;
using Web.Accounts.Login;
using Web.Accounts.Logout.Interfaces;
using Web.Accounts.Register.Interfaces;
using Web.Accounts.Claims;
using Web.Accounts.Common.Interfaces;
using Web.Accounts.Common;
using Web.Tags.Common.Interfaces;
using Web.Tags.Common;
using Web.TaskTags.Common.Interfaces;
using Web.TaskTags.Common;
using Web.Boards.Interfaces;
using Web.Boards;
using Web.Accounts.Users.Interfaces;
using Web.Accounts.Users;

#endregion


namespace Web
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
			builder.Services.AddScoped<IAccountService, AccountService>();

			#endregion


			#region MAPPERS

			builder.Services.AddScoped<IAccountMapper, AccountMapper>();

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