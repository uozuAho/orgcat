using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace orgcat.postgresdb.Migrations
{
    /// <inheritdoc />
    public partial class MakeResponseIdUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponses_ResponseId",
                table: "SurveyResponses",
                column: "ResponseId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SurveyResponses_ResponseId",
                table: "SurveyResponses");
        }
    }
}
