using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Web.Databases.Identity;

#nullable disable

namespace App.Databases.Identity.Migrations;

[DbContext(typeof(CustomIdentityDbContext))]
partial class IdentityDbContextModelSnapshot : ModelSnapshot
{
	protected override void BuildModel(ModelBuilder modelBuilder)
	{
#pragma warning disable 612, 618
		modelBuilder
			.HasAnnotation("ProductVersion", "6.0.14")
			.HasAnnotation("Relational:MaxIdentifierLength", 128);

		SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

		modelBuilder.Entity("Project_IdentityDomainEntities.RoleModel", b =>
			{
				b.Property<string>("Id")
					.HasColumnType("nvarchar(450)");

				b.Property<string>("DataVersion")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.Property<string>("Description")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.Property<string>("Name")
					.IsRequired()
					.HasMaxLength(70)
					.HasColumnType("nvarchar(70)");

				b.HasKey("Id");

				b.ToTable("Roles", (string)null);
			});

		modelBuilder.Entity("Project_IdentityDomainEntities.UserModel", b =>
			{
				b.Property<string>("UserId")
					.HasColumnType("nvarchar(450)");

				b.Property<string>("DataVersion")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.Property<string>("Email")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.Property<string>("FirstName")
					.IsRequired()
					.HasMaxLength(70)
					.HasColumnType("nvarchar(70)");

				b.Property<string>("LastName")
					.IsRequired()
					.HasMaxLength(70)
					.HasColumnType("nvarchar(70)");

				b.Property<string>("NameIdentifier")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.Property<string>("Password")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.Property<string>("Provider")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.Property<string>("Username")
					.IsRequired()
					.HasMaxLength(100)
					.HasColumnType("nvarchar(100)");

				b.HasKey("UserId");

				b.ToTable("Users", (string)null);
			});

		modelBuilder.Entity("Project_IdentityDomainEntities.UserRoleModel", b =>
			{
				b.Property<string>("UserId")
					.HasColumnType("nvarchar(450)");

				b.Property<string>("RoleId")
					.HasColumnType("nvarchar(450)");

				b.HasKey("UserId", "RoleId");

				b.HasIndex("RoleId");

				b.ToTable("UserRoles", (string)null);
			});

		modelBuilder.Entity("Project_IdentityDomainEntities.UserRoleModel", b =>
			{
				b.HasOne("Project_IdentityDomainEntities.RoleModel", "Role")
					.WithMany("UserRoles")
					.HasForeignKey("RoleId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.HasOne("Project_IdentityDomainEntities.UserModel", "User")
					.WithMany("UserRoles")
					.HasForeignKey("UserId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.Navigation("Role");

				b.Navigation("User");
			});

		modelBuilder.Entity("Project_IdentityDomainEntities.RoleModel", b =>
			{
				b.Navigation("UserRoles");
			});

		modelBuilder.Entity("Project_IdentityDomainEntities.UserModel", b =>
			{
				b.Navigation("UserRoles");
			});
#pragma warning restore 612, 618
	}
}
