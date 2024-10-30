using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTraker.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBoardUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_AspNetUsers_OwnerId",
                table: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_Boards_OwnerId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Boards");

            migrationBuilder.CreateTable(
                name: "BoardUser",
                columns: table => new
                {
                    BoardsId = table.Column<int>(type: "int", nullable: false),
                    CollaboratorsId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardUser", x => new { x.BoardsId, x.CollaboratorsId });
                    table.ForeignKey(
                        name: "FK_BoardUser_AspNetUsers_CollaboratorsId",
                        column: x => x.CollaboratorsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardUser_Boards_BoardsId",
                        column: x => x.BoardsId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BoardUser_CollaboratorsId",
                table: "BoardUser",
                column: "CollaboratorsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardUser");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Boards",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_OwnerId",
                table: "Boards",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_AspNetUsers_OwnerId",
                table: "Boards",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
