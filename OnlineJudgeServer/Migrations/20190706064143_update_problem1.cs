using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineJudgeServer.Migrations
{
    public partial class update_problem1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemoryLimit",
                table: "Problems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeLimit",
                table: "Problems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Submits_ProblemId",
                table: "Submits",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_Submits_UserId",
                table: "Submits",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submits_Problems_ProblemId",
                table: "Submits",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submits_Users_UserId",
                table: "Submits",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submits_Problems_ProblemId",
                table: "Submits");

            migrationBuilder.DropForeignKey(
                name: "FK_Submits_Users_UserId",
                table: "Submits");

            migrationBuilder.DropIndex(
                name: "IX_Submits_ProblemId",
                table: "Submits");

            migrationBuilder.DropIndex(
                name: "IX_Submits_UserId",
                table: "Submits");

            migrationBuilder.DropColumn(
                name: "MemoryLimit",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "TimeLimit",
                table: "Problems");
        }
    }
}
