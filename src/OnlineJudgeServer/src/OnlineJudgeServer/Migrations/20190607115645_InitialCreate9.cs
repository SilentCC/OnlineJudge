using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineJudgeServer.Migrations
{
    public partial class InitialCreate9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JudgeResult",
                table: "Submits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JudgeResult",
                table: "Submits");
        }
    }
}
