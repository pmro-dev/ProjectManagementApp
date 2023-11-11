using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Databases.App.Migrations;

public partial class addlastmodificationdatetotaskmodel : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<DateTime>(
			name: "LastModificationDate",
			table: "Tasks",
			type: "datetime2",
			nullable: false,
			defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "LastModificationDate",
			table: "Tasks");
	}
}
