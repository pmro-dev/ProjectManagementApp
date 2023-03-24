﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TODO_List_ASPNET_MVC.Models.DataBase;

#nullable disable

namespace TODO_List_ASPNET_MVC.Models.DataBase.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230218193506_ref-task-todolistmodelid")]
    partial class reftasktodolistmodelid
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

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("TodoListId")
                        .HasColumnType("int");

                    b.Property<int?>("TodoListModel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TodoListModel");

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
                    b.HasOne("TODO_Domain_Entities.TodoListModel", "TodoList")
                        .WithMany("Tasks")
                        .HasForeignKey("TodoListModel");

                    b.Navigation("TodoList");
                });

            modelBuilder.Entity("TODO_Domain_Entities.TodoListModel", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
