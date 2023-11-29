using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace orgcat.postgresdb.Migrations
{
    /// <inheritdoc />
    public partial class Something : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestionResponses_SurveyResponses_SurveyId",
                table: "SurveyQuestionResponses");

            migrationBuilder.RenameColumn(
                name: "SurveyId",
                table: "SurveyQuestionResponses",
                newName: "SurveyResponseId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestionResponses_SurveyId",
                table: "SurveyQuestionResponses",
                newName: "IX_SurveyQuestionResponses_SurveyResponseId");

            migrationBuilder.AddColumn<int>(
                name: "SurveyId",
                table: "SurveyResponses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SurveyId",
                table: "SurveyQuestions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponses_SurveyId",
                table: "SurveyResponses",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_SurveyId",
                table: "SurveyQuestions",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestionResponses_SurveyResponses_SurveyResponseId",
                table: "SurveyQuestionResponses",
                column: "SurveyResponseId",
                principalTable: "SurveyResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Surveys_SurveyId",
                table: "SurveyQuestions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResponses_Surveys_SurveyId",
                table: "SurveyResponses",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestionResponses_SurveyResponses_SurveyResponseId",
                table: "SurveyQuestionResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Surveys_SurveyId",
                table: "SurveyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResponses_Surveys_SurveyId",
                table: "SurveyResponses");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_SurveyResponses_SurveyId",
                table: "SurveyResponses");

            migrationBuilder.DropIndex(
                name: "IX_SurveyQuestions_SurveyId",
                table: "SurveyQuestions");

            migrationBuilder.DropColumn(
                name: "SurveyId",
                table: "SurveyResponses");

            migrationBuilder.RenameColumn(
                name: "SurveyResponseId",
                table: "SurveyQuestionResponses",
                newName: "SurveyId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestionResponses_SurveyResponseId",
                table: "SurveyQuestionResponses",
                newName: "IX_SurveyQuestionResponses_SurveyId");

            migrationBuilder.AlterColumn<string>(
                name: "SurveyId",
                table: "SurveyQuestions",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestionResponses_SurveyResponses_SurveyId",
                table: "SurveyQuestionResponses",
                column: "SurveyId",
                principalTable: "SurveyResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
