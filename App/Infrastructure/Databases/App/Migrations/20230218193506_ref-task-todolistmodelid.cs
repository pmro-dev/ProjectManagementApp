using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Databases.App.Migrations;

public partial class reftasktodolistmodelid : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey(
			name: "FK_Tasks_TodoLists_TodoListModelId",
			table: "Tasks");

		migrationBuilder.RenameColumn(
			name: "TodoListModelId",
			table: "Tasks",
			newName: "TodoListModel");

		migrationBuilder.RenameIndex(
			name: "IX_Tasks_TodoListModelId",
			table: "Tasks",
			newName: "IX_Tasks_TodoListModel");

		migrationBuilder.AddForeignKey(
			name: "FK_Tasks_TodoLists_TodoListModel",
			table: "Tasks",
			column: "TodoListModel",
			principalTable: "TodoLists",
			principalColumn: "Id");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey(
			name: "FK_Tasks_TodoLists_TodoListModel",
			table: "Tasks");

		migrationBuilder.RenameColumn(
			name: "TodoListModel",
			table: "Tasks",
			newName: "TodoListModelId");

		migrationBuilder.RenameIndex(
			name: "IX_Tasks_TodoListModel",
			table: "Tasks",
			newName: "IX_Tasks_TodoListModelId");

		migrationBuilder.AddForeignKey(
			name: "FK_Tasks_TodoLists_TodoListModelId",
			table: "Tasks",
			column: "TodoListModelId",
			principalTable: "TodoLists",
			principalColumn: "Id");
	}
}
