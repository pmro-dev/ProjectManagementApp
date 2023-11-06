using System.Reflection;
using Project_Main.Models.DataBases.AppData.DbSetup;
using Project_Main.Models.DataBases.Identity.DbSetup;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Services.Identity;
using Project_Main.Services.DTO;
using Project_DomainEntities;
using Project_Main.Models.Inputs.DTOs;
using Project_Main.Models.DTOs;
using Project_Main.Models.Factories.ViewModels;
using Project_Main.Models.Factories.DTOs;

namespace Project_Main
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


            #region DTO SERVICES

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
			builder.Services.AddScoped<IIdentityFactory, IdentityFactory>();

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