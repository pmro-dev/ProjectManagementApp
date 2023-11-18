using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Databases.App.Migrations
{
    public partial class FixAfterInterfacesChangedToModels2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TodoListModelId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TodoListModelId",
                table: "Tasks");
        }
    }
}
