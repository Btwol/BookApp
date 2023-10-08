using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookApp.Server.Migrations
{
    public partial class paragraphNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParagraphNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextNodeNumber = table.Column<int>(type: "int", nullable: false),
                    PageNumber = table.Column<int>(type: "int", nullable: false),
                    BookAnalysisId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParagraphNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParagraphNotes_BookAnalyses_BookAnalysisId",
                        column: x => x.BookAnalysisId,
                        principalTable: "BookAnalyses",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_HighlightNotes_BookAnalysisId",
                table: "HighlightNotes",
                column: "BookAnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_ParagraphNotes_BookAnalysisId",
                table: "ParagraphNotes",
                column: "BookAnalysisId");

            migrationBuilder.AddForeignKey(
                name: "FK_HighlightNotes_BookAnalyses_BookAnalysisId",
                table: "HighlightNotes",
                column: "BookAnalysisId",
                principalTable: "BookAnalyses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HighlightNotes_BookAnalyses_BookAnalysisId",
                table: "HighlightNotes");

            migrationBuilder.DropTable(
                name: "ParagraphNotes");

            migrationBuilder.DropIndex(
                name: "IX_HighlightNotes_BookAnalysisId",
                table: "HighlightNotes");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "14fc264f-3fc7-4977-8c51-fb11c9603592");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "addfa90e-71a7-46d2-b6c5-4ad08bdd8a9b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f81d6ce4-1a96-4f0c-9aee-cc569f5a9105");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8b78da23-3807-4e71-a75f-ae01c2d29d67");
        }
    }
}
