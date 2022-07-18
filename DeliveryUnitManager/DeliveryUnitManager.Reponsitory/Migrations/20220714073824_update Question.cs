using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryUnitManager.Reponsitory.Migrations
{
    public partial class updateQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Questions_QuestionInterviewID",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "QuestionInterviewID",
                table: "Questions",
                newName: "QuestionInterviewsID");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_QuestionInterviewID",
                table: "Questions",
                newName: "IX_Questions_QuestionInterviewsID");

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Projects",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ProjectDevelops",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDevelops", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Questions_QuestionInterviewsID",
                table: "Questions",
                column: "QuestionInterviewsID",
                principalTable: "Questions",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Questions_QuestionInterviewsID",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "ProjectDevelops");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Projects",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "QuestionInterviewsID",
                table: "Questions",
                newName: "QuestionInterviewID");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_QuestionInterviewsID",
                table: "Questions",
                newName: "IX_Questions_QuestionInterviewID");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Questions_QuestionInterviewID",
                table: "Questions",
                column: "QuestionInterviewID",
                principalTable: "Questions",
                principalColumn: "ID");
        }
    }
}
