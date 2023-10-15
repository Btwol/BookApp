using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookApp.Server.Migrations
{
    public partial class NoteTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnalysisNoteTag",
                columns: table => new
                {
                    AnalysisNotesId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisNoteTag", x => new { x.AnalysisNotesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_AnalysisNoteTag_AnalysisNotes_AnalysisNotesId",
                        column: x => x.AnalysisNotesId,
                        principalTable: "AnalysisNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AnalysisNoteTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ChapterNoteTag",
                columns: table => new
                {
                    ChapterNotesId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterNoteTag", x => new { x.ChapterNotesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ChapterNoteTag_ChapterNotes_ChapterNotesId",
                        column: x => x.ChapterNotesId,
                        principalTable: "ChapterNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ChapterNoteTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "HighlightNoteTag",
                columns: table => new
                {
                    HighlightNotesId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighlightNoteTag", x => new { x.HighlightNotesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_HighlightNoteTag_HighlightNotes_HighlightNotesId",
                        column: x => x.HighlightNotesId,
                        principalTable: "HighlightNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_HighlightNoteTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ParagraphNoteTag",
                columns: table => new
                {
                    ParagraphNotesId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParagraphNoteTag", x => new { x.ParagraphNotesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ParagraphNoteTag_ParagraphNotes_ParagraphNotesId",
                        column: x => x.ParagraphNotesId,
                        principalTable: "ParagraphNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ParagraphNoteTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "383827ba-f384-4259-a937-ca696c82e818");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ce36f127-fa44-4845-8e62-cc247bb69b4f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8d0ce2eb-2e56-4e5b-9617-8f8c370d53af");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e63c084a-e803-438c-98b8-5e82b68f20cc");

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisNoteTag_TagsId",
                table: "AnalysisNoteTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterNoteTag_TagsId",
                table: "ChapterNoteTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_HighlightNoteTag_TagsId",
                table: "HighlightNoteTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_ParagraphNoteTag_TagsId",
                table: "ParagraphNoteTag",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalysisNoteTag");

            migrationBuilder.DropTable(
                name: "ChapterNoteTag");

            migrationBuilder.DropTable(
                name: "HighlightNoteTag");

            migrationBuilder.DropTable(
                name: "ParagraphNoteTag");

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
        }
    }
}
