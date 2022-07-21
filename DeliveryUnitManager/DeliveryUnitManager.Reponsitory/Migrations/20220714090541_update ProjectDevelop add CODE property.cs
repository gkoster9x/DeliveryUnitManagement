using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryUnitManager.Reponsitory.Migrations
{
    public partial class updateProjectDevelopaddCODEproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ProjectDevelops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "ProjectDevelops");
        }
    }
}
