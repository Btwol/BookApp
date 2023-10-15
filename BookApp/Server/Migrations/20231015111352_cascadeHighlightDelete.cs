using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookApp.Server.Migrations
{
    public partial class cascadeHighlightDelete : Migration
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
                value: "b54c4f5c-d7c9-4bd9-bed4-c6b2b0867afd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "92554c41-6ce5-4622-b2b8-e18069525ba8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e4c2bbf4-c99b-41ae-81c0-e783af384434");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "073ef02f-281d-4044-983c-eefae74a933e");

            migrationBuilder.AddForeignKey(
                name: "FK_Highlights_BookAnalyses_BookAnalysisId",
                table: "Highlights",
                column: "BookAnalysisId",
                principalTable: "BookAnalyses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
    }
}
