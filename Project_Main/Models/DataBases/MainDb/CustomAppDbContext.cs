using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project_DomainEntities;
using Project_Main.Infrastructure.Helpers;
using static Project_DomainEntities.Helpers.TaskStatusHelper;

namespace Project_Main.Models.DataBases.AppData
{
    public class CustomAppDbContext : DbContext
    {
        private readonly ILogger<CustomAppDbContext>? _logger;

        /// <summary>
        /// Initilizes object and Ensures that database is created.
        /// </summary>
        public CustomAppDbContext(DbContextOptions<CustomAppDbContext> options, ILogger<CustomAppDbContext> logger) : base(options)
        {
            _logger = logger;
        }

        public CustomAppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoListModel>()
                .ToTable("TodoLists");
            modelBuilder.Entity<TaskModel>()
                .ToTable("Tasks");
            modelBuilder.Entity<TagModel>()
                .ToTable("Tags");
            modelBuilder.Entity<TaskTagModel>()
                .ToTable("TaskTags");

            var todoItemBuilder = modelBuilder.Entity<TaskModel>();
            todoItemBuilder.Property(x => x.Status)
                .HasConversion(new EnumToStringConverter<TaskStatusType>());

            modelBuilder.Entity<TaskTagModel>().HasKey(tt => new { tt.TaskId, tt.TagId });

            _logger?.LogInformation(Messages.BuildingSucceedLogger, nameof(OnModelCreating), nameof(CustomAppDbContext));
        }
    }
}
