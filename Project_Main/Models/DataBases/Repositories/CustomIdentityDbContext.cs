using Microsoft.EntityFrameworkCore;
using Project_IdentityDomainEntities;
using Project_Main.Infrastructure.Helpers;

namespace Project_Main.Models.DataBases.Repositories
{
	public class CustomIdentityDbContext : DbContext
	{
		private readonly ILogger<CustomIdentityDbContext> _logger;

		public CustomIdentityDbContext(DbContextOptions<CustomIdentityDbContext> options, ILogger<CustomIdentityDbContext> logger) : base(options)
		{
			_logger = logger;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<UserRoleModel>().HasKey(ur => new { ur.UserId, ur.RoleId });
			
			_logger.LogInformation(Messages.BuildingSucceedLogger, nameof(OnModelCreating), nameof(CustomIdentityDbContext));
		}
	}
}
