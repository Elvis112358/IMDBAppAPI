using Microsoft.EntityFrameworkCore.Migrations;

namespace EFModels.Migrations
{
    public partial class removePublisher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Publishers_PublisherId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Movies_PublisherId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Movies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    PublisherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.PublisherId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_PublisherId",
                table: "Movies",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Publishers_PublisherId",
                table: "Movies",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "PublisherId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
