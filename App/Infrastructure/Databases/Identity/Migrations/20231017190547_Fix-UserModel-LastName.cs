using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Databases.Identity.Migrations;

public partial class FixUserModelLastName : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.RenameColumn(
			name: "Lastname",
			table: "Users",
			newName: "LastName");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.RenameColumn(
			name: "LastName",
			table: "Users",
			newName: "Lastname");
	}
}
