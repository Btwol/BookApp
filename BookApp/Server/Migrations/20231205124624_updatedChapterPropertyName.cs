using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookApp.Server.Migrations
{
    public partial class updatedChapterPropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PageNumber",
                table: "ParagraphNotes",
                newName: "Chapter");

            migrationBuilder.RenameColumn(
                name: "PageNumber",
                table: "Highlights",
                newName: "Chapter");

            migrationBuilder.RenameColumn(
                name: "ChapterNumber",
                table: "ChapterNotes",
                newName: "Chapter");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Chapter",
                table: "ParagraphNotes",
                newName: "PageNumber");

            migrationBuilder.RenameColumn(
                name: "Chapter",
                table: "Highlights",
                newName: "PageNumber");

            migrationBuilder.RenameColumn(
                name: "Chapter",
                table: "ChapterNotes",
                newName: "ChapterNumber");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "cc20bee5-8ef8-4620-a67c-d4926c58ea04");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ef55a670-5588-48ae-bdad-e6c93c041e0e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d22a273e-4094-4f8a-a5fb-d92f60217890");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "f2232ee9-63b7-457e-95a6-ce0f249b0353");
        }
    }
}
