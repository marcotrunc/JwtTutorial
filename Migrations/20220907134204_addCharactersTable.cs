using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtTutorial.Migrations
{
    public partial class addCharactersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "employee",
                table: "Users",
                newName: "Employee");

            migrationBuilder.AlterColumn<int>(
                name: "Employee",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RpgClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuperHeroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_SuperHeroes_SuperHeroId",
                        column: x => x.SuperHeroId,
                        principalTable: "SuperHeroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_SuperHeroId",
                table: "Characters",
                column: "SuperHeroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.RenameColumn(
                name: "Employee",
                table: "Users",
                newName: "employee");

            migrationBuilder.AlterColumn<int>(
                name: "employee",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
