using System.Reflection;
using Project_Main.Models.DataBases.AppData.DbSetup;
using Project_Main.Models.DataBases.Identity.DbSetup;
using Project_Main.Services;
using Project_Main.Infrastructure.Helpers;

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
			builder.Services.AddScoped<ILoginService, LoginService>();
			builder.Services.AddScoped<ILogoutService, LogoutService>();
			builder.Services.AddScoped<IUserRegisterService, UserRegisterService>();
			builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
			builder.Services.AddScoped<IClaimsService, ClaimsService>();

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