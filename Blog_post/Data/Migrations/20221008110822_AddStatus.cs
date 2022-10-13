using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog_post.Data.Migrations
{
    public partial class AddStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_posts_StatusId",
                table: "posts",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_status_StatusId",
                table: "posts",
                column: "StatusId",
                principalTable: "status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_status_StatusId",
                table: "posts");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropIndex(
                name: "IX_posts_StatusId",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "posts");
        }
    }
}
