#region ADD USINGS

using ClassLibrary_SeedData;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using Project_Main.Models.DataBases.AppData;
using Project_Main.Models.DataBases.Identity;
using Web.Infrastructure.Helpers;

#endregion

namespace Web.Infrastructure
{
	/// <summary>
	/// Web Builder extensions that allows to setup basic services such as Databases, Unit Of Work's elements, Data Seeding and pipeline. 
	/// </summary>
	public static class BasicWebBuilderExtensions
	{
		/// <summary>
		/// Add Custom Databases context's services for Main Db and Identity Db.
		/// </summary>
		/// <param name="builder">App builder.</param>
		public static void AddCustomDbContexts(this WebApplicationBuilder builder)
		{
			builder.Services.AddDbContext<CustomAppDbContext>(
			options =>
			{
				options.UseSqlServer(builder.Configuration[ConfigConstants.ConnectionStringMainDb]);
				options.UseExceptionProcessor();
			},
				ServiceLifetime.Scoped);

			builder.Services.AddDbContext<CustomIdentityDbContext>(
			options =>
			{
				options.UseSqlServer(builder.Configuration[ConfigConstants.ConnectionStringIdentityDb]);
				options.UseExceptionProcessor();
			},
				ServiceLifetime.Scoped);
		}

		/// <summary>
		/// Add Unit Of Work's services such as Data UOF and Identity UOW.
		/// </summary>
		/// <param name="builder">App builder.</param>
		public static void SetupUnitOfWorkServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();
			builder.Services.AddScoped<IDataUnitOfWork, DataUnitOfWork>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IRoleRepository, RoleRepository>();
			builder.Services.AddScoped<ITodoListRepository, TodoListRepository>();
			builder.Services.AddScoped<ITaskRepository, TaskRepository>();
		}

		/// <summary>
		/// Add Data seeding service for main Db.
		/// </summary>
		/// <param name="builder">App builder.</param>
		public static void SetupSeedDataServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddScoped<ISeedData, SeedData>();
		}

		/// <summary>
		/// Setup basic environment settings such as development environment.
		/// </summary>
		/// <param name="builder">App builder.</param>
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

		/// <summary>
		/// Setup a whole app pipeline.
		/// </summary>
		/// <param name="app">App.</param>
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
