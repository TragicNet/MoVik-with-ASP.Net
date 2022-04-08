using Microsoft.EntityFrameworkCore.Migrations;

namespace MoVik.Migrations.MoVikDb
{
    public partial class MenuItemCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuItem",
                columns: table => new
                {
                    MenuId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItem", x => x.MenuId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItem");
        }
    }
}
