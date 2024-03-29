﻿// <auto-generated />
using App.Infrastructure.Databases.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Databases.Identity.Migrations;

[DbContext(typeof(CustomIdentityDbContext))]
[Migration("20230311163421_InitCustomIdentityDb")]
partial class InitCustomIdentityDb
{
	protected override void BuildTargetModel(ModelBuilder modelBuilder)
	{
#pragma warning disable 612, 618
		modelBuilder
			.HasAnnotation("ProductVersion", "6.0.14")
			.HasAnnotation("Relational:MaxIdentifierLength", 128);

		SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

		modelBuilder.Entity("Identity_Domain_Entities.RoleModel", b =>
			{
				b.Property<string>("Id")
					.HasColumnType("nvarchar(450)");

				b.Property<string>("DataVersion")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.Property<string>("Name")
					.IsRequired()
					.HasMaxLength(70)
					.HasColumnType("nvarchar(70)");

				b.HasKey("Id");

				b.ToTable("Roles");
			});

		modelBuilder.Entity("Identity_Domain_Entities.UserModel", b =>
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
					.HasMaxLength(50)
					.HasColumnType("nvarchar(50)");

				b.Property<string>("Lastname")
					.IsRequired()
					.HasMaxLength(50)
					.HasColumnType("nvarchar(50)");

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
					.HasMaxLength(30)
					.HasColumnType("nvarchar(30)");

				b.HasKey("UserId");

				b.ToTable("Users");
			});

		modelBuilder.Entity("Identity_Domain_Entities.UserRoleModel", b =>
			{
				b.Property<string>("UserId")
					.HasColumnType("nvarchar(450)");

				b.Property<string>("RoleId")
					.HasColumnType("nvarchar(450)");

				b.HasKey("UserId", "RoleId");

				b.HasIndex("RoleId");

				b.ToTable("UserRoles");
			});

		modelBuilder.Entity("Identity_Domain_Entities.UserRoleModel", b =>
			{
				b.HasOne("Identity_Domain_Entities.RoleModel", "Role")
					.WithMany("UserRoles")
					.HasForeignKey("RoleId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.HasOne("Identity_Domain_Entities.UserModel", "User")
					.WithMany("UserRoles")
					.HasForeignKey("UserId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.Navigation("Role");

				b.Navigation("User");
			});

		modelBuilder.Entity("Identity_Domain_Entities.RoleModel", b =>
			{
				b.Navigation("UserRoles");
			});

		modelBuilder.Entity("Identity_Domain_Entities.UserModel", b =>
			{
				b.Navigation("UserRoles");
			});
#pragma warning restore 612, 618
	}
}
