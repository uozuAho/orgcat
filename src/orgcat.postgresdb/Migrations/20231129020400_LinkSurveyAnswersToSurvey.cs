using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace orgcat.postgresdb.Migrations
{
    /// <inheritdoc />
    public partial class LinkSurveyAnswersToSurvey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestionResponses_SurveyId",
                table: "SurveyQuestionResponses",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestionResponses_SurveyResponses_SurveyId",
                table: "SurveyQuestionResponses",
                column: "SurveyId",
                principalTable: "SurveyResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestionResponses_SurveyResponses_SurveyId",
                table: "SurveyQuestionResponses");

            migrationBuilder.DropIndex(
                name: "IX_SurveyQuestionResponses_SurveyId",
                table: "SurveyQuestionResponses");
        }
    }
}
