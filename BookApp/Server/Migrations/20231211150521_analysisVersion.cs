using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookApp.Server.Migrations
{
    public partial class analysisVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnalysisVersionId",
                table: "BookAnalyses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AnalysisVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnalysisSummaryVersion = table.Column<int>(type: "int", nullable: false),
                    TagsVersion = table.Column<int>(type: "int", nullable: false),
                    AnalysisNotesVersion = table.Column<int>(type: "int", nullable: false),
                    ChapterNotesVersion = table.Column<int>(type: "int", nullable: false),
                    ParagraphNotesVersion = table.Column<int>(type: "int", nullable: false),
                    HighlightNotesVersion = table.Column<int>(type: "int", nullable: false),
                    HighlightVersion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisVersions", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d59d61de-7986-4711-9b1f-ccd6c548c88c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c72d95d2-0b03-46a6-82f4-d253895630d6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "becac254-7ccd-4950-b970-96a102ef4dc2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b5cff2c4-64bd-4bf1-bd1b-156d496f4bd1");

            migrationBuilder.CreateIndex(
                name: "IX_BookAnalyses_AnalysisVersionId",
                table: "BookAnalyses",
                column: "AnalysisVersionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAnalyses_AnalysisVersions_AnalysisVersionId",
                table: "BookAnalyses",
                column: "AnalysisVersionId",
                principalTable: "AnalysisVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAnalyses_AnalysisVersions_AnalysisVersionId",
                table: "BookAnalyses");

            migrationBuilder.DropTable(
                name: "AnalysisVersions");

            migrationBuilder.DropIndex(
                name: "IX_BookAnalyses_AnalysisVersionId",
                table: "BookAnalyses");

            migrationBuilder.DropColumn(
                name: "AnalysisVersionId",
                table: "BookAnalyses");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "270d3f82-046d-4f45-8983-4b8348452e3c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "2ad02dfa-62b1-4340-9d10-8f2e0d22d9bf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d0cffc93-43fc-45b2-a8fe-a84da25fc064");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "53be0030-5f96-43e3-9f34-04fb3b8c8477");
        }
    }
}
