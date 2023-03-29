using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project_DomainEntities;
using Project_Main.Infrastructure.Helpers;
using static Project_DomainEntities.Helpers.TaskStatusHelper;

namespace Project_Main.Models.DataBases.Repositories
{
	public class CustomDbContext : DbContext
	{
		private readonly ILogger<CustomDbContext> _logger;

		/// <summary>
		/// Initilizes object and Ensures that database is created.
		/// </summary>
		public CustomDbContext(DbContextOptions<CustomDbContext> options, ILogger<CustomDbContext> logger) : base(options)
		{
			_logger = logger;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			var todoItemBuilder = modelBuilder.Entity<TaskModel>();

			todoItemBuilder.Property(x => x.Status)
				.HasConversion(new EnumToStringConverter<TaskStatusType>());

			modelBuilder.Entity<TaskTagModel>().HasKey(tt => new { tt.TaskId, tt.TagId });

			_logger.LogInformation(Messages.BuildingSucceedLogger, nameof(OnModelCreating), nameof(CustomDbContext));
		}
	}
}
