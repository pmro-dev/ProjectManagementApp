#region ADD USINGS

using ClassLibrary_SeedData;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using Project_Main.Controllers.Helpers;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.DataBases.AppData;
using Project_Main.Models.DataBases.Identity;

#endregion

namespace Project_Main
{
	public static class BasicWebBuilderExtensions
	{
		public static void AddCustomDbContexts(this WebApplicationBuilder builder)
		{
			builder.Services.AddDbContext<CustomAppDbContext>(
			options =>
			{
				options.UseSqlServer(builder.Configuration[HelperProgramAndAuth.ConnectionStringMainDb]);
				options.UseExceptionProcessor();
			},
				ServiceLifetime.Scoped);

			builder.Services.AddDbContext<CustomIdentityDbContext>(
			options =>
			{
				options.UseSqlServer(builder.Configuration[HelperProgramAndAuth.ConnectionStringIdentityDb]);
				options.UseExceptionProcessor();
			},
				ServiceLifetime.Scoped);
		}

		public static void SetupUnitOfWorkServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();
			builder.Services.AddScoped<IDataUnitOfWork, DataUnitOfWork>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IRoleRepository, RoleRepository>();
			builder.Services.AddScoped<ITodoListRepository, TodoListRepository>();
			builder.Services.AddScoped<ITaskRepository, TaskRepository>();
		}

		public static void SetupSeedDataServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddScoped<ISeedData, SeedData>();
		}

		public static void SetupEnvironmentSettings(this WebApplicationBuilder builder)
		{
			if (builder.Environment.IsDevelopment())
			{
				builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
			}
			else
			{
				builder.Services.AddControllersWithViews();
			}
		}

		public static void SetupPipeline(this WebApplication app)
		{
			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler(CustomRoutes.ErrorHandlingPath);
				app.UseHsts();
			}

			app.UseStaticFiles();
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
		}
	}
}
