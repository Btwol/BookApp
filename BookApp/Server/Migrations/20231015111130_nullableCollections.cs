using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookApp.Server.Migrations
{
    public partial class nullableCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Highlights_BookAnalyses_BookAnalysisId",
                table: "Highlights");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ddec597b-95fb-42ac-9c57-f230ea018dfc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "0889c33c-128a-42a0-baad-365e4b8b3698");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "0f47faa1-d8b8-4edf-b4ac-d25dc6bdfcf6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "870e62ec-3ec9-41ff-82a2-680bfa57f3ed");

            migrationBuilder.AddForeignKey(
                name: "FK_Highlights_BookAnalyses_BookAnalysisId",
                table: "Highlights",
                column: "BookAnalysisId",
                principalTable: "BookAnalyses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Highlights_BookAnalyses_BookAnalysisId",
                table: "Highlights");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Highlights_BookAnalyses_BookAnalysisId",
                table: "Highlights",
                column: "BookAnalysisId",
                principalTable: "BookAnalyses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
