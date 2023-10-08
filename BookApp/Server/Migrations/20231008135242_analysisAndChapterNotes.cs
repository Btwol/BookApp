using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookApp.Server.Migrations
{
    public partial class analysisAndChapterNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnalysisNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookAnalysisId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalysisNotes_BookAnalyses_BookAnalysisId",
                        column: x => x.BookAnalysisId,
                        principalTable: "BookAnalyses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChapterNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChapterNumber = table.Column<int>(type: "int", nullable: false),
                    BookAnalysisId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChapterNotes_BookAnalyses_BookAnalysisId",
                        column: x => x.BookAnalysisId,
                        principalTable: "BookAnalyses",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "696a892b-c9a4-4a6e-aab0-b34c7e48af75");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "f27474cd-a173-4f66-b638-e06b28c0fa12");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7bdf70ba-7cef-4036-b46e-58a1c55df59c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "235d60c6-c6b8-4af7-a9e0-12b421e61a8b");

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisNotes_BookAnalysisId",
                table: "AnalysisNotes",
                column: "BookAnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterNotes_BookAnalysisId",
                table: "ChapterNotes",
                column: "BookAnalysisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalysisNotes");

            migrationBuilder.DropTable(
                name: "ChapterNotes");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "acd30ac1-cb91-46cd-8033-a13106c41226");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "522247cc-027d-4c52-ad6e-120d8d20204c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "989d2b56-1441-4750-b366-86a4903baeb1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "cff97c18-8f31-43ac-905a-ee959ea3e95a");
        }
    }
}
