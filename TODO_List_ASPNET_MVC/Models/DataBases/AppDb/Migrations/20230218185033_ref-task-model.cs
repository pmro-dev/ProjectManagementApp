using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TODO_List_ASPNET_MVC.Models.DataBase.Migrations
{
    public partial class reftaskmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TodoLists_TodoListModelId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "TodoListModelId",
                table: "Tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TodoListId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TodoLists_TodoListModelId",
                table: "Tasks",
                column: "TodoListModelId",
                principalTable: "TodoLists",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TodoLists_TodoListModelId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TodoListId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "TodoListModelId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TodoLists_TodoListModelId",
                table: "Tasks",
                column: "TodoListModelId",
                principalTable: "TodoLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
