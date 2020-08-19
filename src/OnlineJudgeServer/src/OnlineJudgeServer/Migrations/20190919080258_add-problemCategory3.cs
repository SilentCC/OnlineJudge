using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineJudgeServer.Migrations
{
    public partial class addproblemCategory3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Problems_CategoryId",
                table: "Problems",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Problems_ProgramCategories_CategoryId",
                table: "Problems",
                column: "CategoryId",
                principalTable: "ProgramCategories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Problems_ProgramCategories_CategoryId",
                table: "Problems");

            migrationBuilder.DropIndex(
                name: "IX_Problems_CategoryId",
                table: "Problems");
        }
    }
}
