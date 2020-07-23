using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineJudgeServer.Migrations
{
    public partial class addproblemCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Problems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProgramCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TotalProblemNum = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramCategories", x => x.CategoryId);
                });

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

            migrationBuilder.DropTable(
                name: "ProgramCategories");

            migrationBuilder.DropIndex(
                name: "IX_Problems_CategoryId",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Problems");
        }
    }
}
