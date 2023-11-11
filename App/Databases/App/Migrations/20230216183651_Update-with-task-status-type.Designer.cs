﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Web.Databases.App;
#nullable disable

namespace App.Databases.App.Migrations;

[DbContext(typeof(CustomAppDbContext))]
[Migration("20230216183651_Update-with-task-status-type")]
partial class Updatewithtaskstatustype
{
	protected override void BuildTargetModel(ModelBuilder modelBuilder)
	{
#pragma warning disable 612, 618
		modelBuilder
			.HasAnnotation("ProductVersion", "6.0.14")
			.HasAnnotation("Relational:MaxIdentifierLength", 128);

		SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

		modelBuilder.Entity("TODO_Domain_Entities.TaskModel", b =>
			{
				b.Property<int>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("int");

				SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

				b.Property<string>("Description")
					.IsRequired()
					.HasMaxLength(300)
					.HasColumnType("nvarchar(300)");

				b.Property<DateTime?>("DueDate")
					.IsRequired()
					.HasColumnType("datetime2");

				b.Property<int>("Status")
					.HasColumnType("int");

				b.Property<string>("Title")
					.IsRequired()
					.HasMaxLength(50)
					.HasColumnType("nvarchar(50)");

				b.Property<int>("TodoListModelId")
					.HasColumnType("int");

				b.HasKey("Id");

				b.HasIndex("TodoListModelId");

				b.ToTable("Tasks");
			});

		modelBuilder.Entity("TODO_Domain_Entities.TodoListModel", b =>
			{
				b.Property<int>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnType("int");

				SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

				b.Property<string>("Name")
					.IsRequired()
					.HasMaxLength(60)
					.HasColumnType("nvarchar(60)");

				b.HasKey("Id");

				b.ToTable("TodoLists");
			});

		modelBuilder.Entity("TODO_Domain_Entities.TaskModel", b =>
			{
				b.HasOne("TODO_Domain_Entities.TodoListModel", null)
					.WithMany("Tasks")
					.HasForeignKey("TodoListModelId")
					.OnDelete(DeleteBehavior.Cascade)
					.IsRequired();
			});

		modelBuilder.Entity("TODO_Domain_Entities.TodoListModel", b =>
			{
				b.Navigation("Tasks");
			});
#pragma warning restore 612, 618
	}
}
