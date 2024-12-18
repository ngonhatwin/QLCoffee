using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectPersonal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditFKUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__refresh_t__user___44FF419A",
                table: "refresh_tokens");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_user_id",
                table: "refresh_tokens",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK__refresh_t__user___44FF419A",
                table: "refresh_tokens",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__refresh_t__user___44FF419A",
                table: "refresh_tokens");

            migrationBuilder.DropIndex(
                name: "IX_refresh_tokens_user_id",
                table: "refresh_tokens");

            migrationBuilder.AddForeignKey(
                name: "FK__refresh_t__user___44FF419A",
                table: "refresh_tokens",
                column: "token_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
