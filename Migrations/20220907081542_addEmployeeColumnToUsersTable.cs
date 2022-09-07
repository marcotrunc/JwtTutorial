using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtTutorial.Migrations
{
    public partial class addEmployeeColumnToUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "employee",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "employee",
                table: "Users");
        }
    }
}
