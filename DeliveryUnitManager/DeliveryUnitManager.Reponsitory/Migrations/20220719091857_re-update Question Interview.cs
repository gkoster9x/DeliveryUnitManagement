using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryUnitManager.Reponsitory.Migrations
{
    public partial class reupdateQuestionInterview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionID",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Questions_QuestionInterviewsID",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_QuestionInterviewsID",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionID",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "QuestionInterviewsID",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Questions");

            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Questions");

            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<long>(
                name: "QuestionInterviewsID",
                table: "Questions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionInterviewsID",
                table: "Questions",
                column: "QuestionInterviewsID");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionID",
                table: "Answers",
                column: "QuestionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionID",
                table: "Answers",
                column: "QuestionID",
                principalTable: "Questions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Questions_QuestionInterviewsID",
                table: "Questions",
                column: "QuestionInterviewsID",
                principalTable: "Questions",
                principalColumn: "ID");
        }
    }
}
