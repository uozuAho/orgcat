using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace orgcat.postgresdb.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "SurveyQuestionResponses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SurveyQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SurveyId = table.Column<string>(type: "text", nullable: false),
                    QuestionText = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestionResponses_QuestionId",
                table: "SurveyQuestionResponses",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestionResponses_SurveyQuestions_QuestionId",
                table: "SurveyQuestionResponses",
                column: "QuestionId",
                principalTable: "SurveyQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestionResponses_SurveyQuestions_QuestionId",
                table: "SurveyQuestionResponses");

            migrationBuilder.DropTable(
                name: "SurveyQuestions");

            migrationBuilder.DropIndex(
                name: "IX_SurveyQuestionResponses_QuestionId",
                table: "SurveyQuestionResponses");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "SurveyQuestionResponses");
        }
    }
}
