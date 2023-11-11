﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Web.Databases.App;

#nullable disable

namespace App.Databases.App.Migrations;

[DbContext(typeof(CustomAppDbContext))]
partial class AppDbContextModelSnapshot : ModelSnapshot
{
	protected override void BuildModel(ModelBuilder modelBuilder)
	{
#pragma warning disable 612, 618
		modelBuilder
			.HasAnnotation("ProductVersion", "6.0.14")
			.HasAnnotation("Relational:MaxIdentifierLength", 128);

		SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

		modelBuilder.Entity("Project_DomainEntities.TagModel", b =>
			{
				b.Property<int>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("int");

				SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

				b.Property<string>("Title")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.HasKey("Id");

				b.ToTable("Tags", (string)null);
			});

		modelBuilder.Entity("Project_DomainEntities.TaskModel", b =>
			{
				b.Property<int>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("int");

				SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

				b.Property<DateTime>("CreationDate")
					.HasColumnType("datetime2");

				b.Property<string>("Description")
					.IsRequired()
					.HasMaxLength(300)
					.HasColumnType("nvarchar(300)");

				b.Property<DateTime>("DueDate")
					.HasColumnType("datetime2");

				b.Property<DateTime>("LastModificationDate")
					.HasColumnType("datetime2");

				b.Property<DateTime?>("ReminderDate")
					.HasColumnType("datetime2");

				b.Property<string>("Status")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.Property<string>("Title")
					.IsRequired()
					.HasMaxLength(70)
					.HasColumnType("nvarchar(70)");

				b.Property<int>("TodoListId")
					.HasColumnType("int");

				b.Property<string>("UserId")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.HasKey("Id");

				b.HasIndex("TodoListId");

				b.ToTable("Tasks", (string)null);
			});

		modelBuilder.Entity("Project_DomainEntities.TaskTagModel", b =>
			{
				b.Property<int>("TaskId")
					.HasColumnType("int");

				b.Property<int>("TagId")
					.HasColumnType("int");

				b.HasKey("TaskId", "TagId");

				b.HasIndex("TagId");

				b.ToTable("TaskTags", (string)null);
			});

		modelBuilder.Entity("Project_DomainEntities.TodoListModel", b =>
			{
				b.Property<int>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("int");

				SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

				b.Property<string>("Title")
					.IsRequired()
					.HasMaxLength(70)
					.HasColumnType("nvarchar(70)");

				b.Property<string>("UserId")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.HasKey("Id");

				b.ToTable("TodoLists", (string)null);
			});

		modelBuilder.Entity("Project_DomainEntities.TaskModel", b =>
			{
				b.HasOne("Project_DomainEntities.TodoListModel", "TodoList")
					.WithMany("Tasks")
					.HasForeignKey("TodoListId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.Navigation("TodoList");
			});

		modelBuilder.Entity("Project_DomainEntities.TaskTagModel", b =>
			{
				b.HasOne("Project_DomainEntities.TagModel", "Tag")
					.WithMany("TaskTags")
					.HasForeignKey("TagId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.HasOne("Project_DomainEntities.TaskModel", "Task")
					.WithMany("TaskTags")
					.HasForeignKey("TaskId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();

				b.Navigation("Tag");

				b.Navigation("Task");
			});

		modelBuilder.Entity("Project_DomainEntities.TagModel", b =>
			{
				b.Navigation("TaskTags");
			});

		modelBuilder.Entity("Project_DomainEntities.TaskModel", b =>
			{
				b.Navigation("TaskTags");
			});

		modelBuilder.Entity("Project_DomainEntities.TodoListModel", b =>
			{
				b.Navigation("Tasks");
			});
#pragma warning restore 612, 618
	}
}
