using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TODO_List_ASPNET_MVC.Models.DataBase.Migrations
{
    public partial class reftasktodolistmodelid2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TodoLists_TodoListModel",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TodoListModel",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TodoListModel",
                table: "Tasks");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TodoListId",
                table: "Tasks",
                column: "TodoListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TodoLists_TodoListId",
                table: "Tasks",
                column: "TodoListId",
                principalTable: "TodoLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TodoLists_TodoListId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TodoListId",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "TodoListModel",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TodoListModel",
                table: "Tasks",
                column: "TodoListModel");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TodoLists_TodoListModel",
                table: "Tasks",
                column: "TodoListModel",
                principalTable: "TodoLists",
                principalColumn: "Id");
        }
    }
}
