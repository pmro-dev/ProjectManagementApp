
#region USINGS

using System.Reflection;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Users.Login.Interfaces;
using App.Features.Users.Authentication;
using App.Features.TodoLists.Common;
using App.Features.Users.Logout;
using App.Features.Tasks.Common;
using App.Features.Users.Common;
using App.Infrastructure;
using App.Features.Users.Login;
using App.Features.Users.Register;
using App.Infrastructure.Databases.Identity.Seeds;
using App.Features.Users.Common.Interfaces;
using App.Infrastructure.Databases.App.Seeds;
using App.Features.Users.Logout.Interfaces;
using App.Features.Boards.Common;
using App.Features.Users.Register.Interfaces;
using Microsoft.AspNetCore.Mvc.Razor;
using App.Common.Views;
using App.Features.Users.Authentication.Interfaces;
using App.Features.Boards.Common.Interfaces;
using App.Common;
using App.Features.Users.Common.Roles;
using App.Features.Users.Common.Roles.Interfaces;

#endregion

namespace App;

public static class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Configuration
			.AddEnvironmentVariables()
			.AddUserSecrets(Assembly.GetExecutingAssembly(), true);

		builder.SetupEnvironmentSettings();

		builder.AddCustomDbContexts();
		builder.SetupUnitOfWorkServices();

		builder.Services.AddHttpContextAccessor();
		builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


		#region IDENTITY SERVICES

		builder.Services.AddScoped<ILoginService, LoginService>();
		builder.Services.AddScoped<ILogoutService, LogoutService>();
		builder.Services.AddScoped<IUserRegisterService, UserRegisterService>();
		builder.Services.AddScoped<IAuthenticationCustomService, AuthenticationCustomService>();
		builder.Services.AddScoped<IClaimsService, ClaimsService>();
		builder.Services.AddScoped<IRoleService, RoleService>();
		builder.Services.AddScoped<IUserService, UserService>();
		builder.Services.AddScoped<ICookieEventsService, CookieEventsService>();
		builder.Services.AddScoped<IIdentityService, IdentityService>();
		builder.Services.AddScoped<ICookieService, CookieService>();

		#endregion


		#region MAPPERS

		builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
		builder.Services.AddScoped<ITaskEntityMapper, TaskEntityMapper>();
		builder.Services.AddScoped<ITodoListMapper, TodoListMapper>();

		#endregion


		#region FACTORIES

		builder.Services.AddScoped<IBoardViewModelsFactory, BoardViewModelsFactory>();
		builder.Services.AddScoped<ITodoListViewModelsFactory, TodoListViewModelsFactory>();
		builder.Services.AddScoped<ITaskViewModelsFactory, TaskViewModelsFactory>();

		builder.Services.AddScoped<ITodoListFactory, TodoListFactory>();
		builder.Services.AddScoped<ITaskEntityFactory, TaskEntityFactory>();

		builder.Services.AddScoped<ILoginFactory, LoginFactory>();
		builder.Services.AddScoped<IRoleFactory, RoleFactory>();
		builder.Services.AddScoped<IUserFactory, UserFactory>();

		#endregion


		builder.Services.AddScoped<ITaskSelector, TaskSelector>();
		builder.Services.AddScoped<ITodoListSelector, TodoListSelector>();

		builder.SetupSeedDataServices();


		#region SETUP AUTHENNTICATION

		builder.SetupBasicAuthenticationWithCookie();
		builder.SetupGoogleAuthentication();

		#endregion


		builder.Services.Configure<RazorViewEngineOptions>(options =>
		{
			var expanderLogger = new LoggerFactory().CreateLogger<ViewLocationExpander>();
			options.ViewLocationExpanders.Add((new ViewLocationExpander(expanderLogger)));
		});

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
			app.UseDeveloperExceptionPage();
		else
			app.UseExceptionHandler(CustomRoutes.ExceptionHandlerPath);

		app.SetupPipeline();


		#region SETUP ROUTES

		app.MapControllerRoute(
			name: CustomRoutes.DefaultRouteName,
			pattern: CustomRoutes.DefaultRoutePattern);

		#endregion


		#region POPULATE DATABASES

		using (var serviceScope = app.Services.CreateScope())
		{
			IIdentityDbSeeder identityDbSeeder = serviceScope.ServiceProvider.GetRequiredService<IIdentityDbSeeder>();
			await identityDbSeeder.EnsurePopulated();

			IDbSeeder dbSeeder = serviceScope.ServiceProvider.GetRequiredService<IDbSeeder>();
			await dbSeeder.EnsurePopulated();
		}

		#endregion


		app.Run();
	}
}