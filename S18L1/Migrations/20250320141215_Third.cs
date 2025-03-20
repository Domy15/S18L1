using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace S18L1.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Stundents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Stundents_UserId",
                table: "Stundents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stundents_AspNetUsers_UserId",
                table: "Stundents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stundents_AspNetUsers_UserId",
                table: "Stundents");

            migrationBuilder.DropIndex(
                name: "IX_Stundents_UserId",
                table: "Stundents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Stundents");
        }
    }
}
