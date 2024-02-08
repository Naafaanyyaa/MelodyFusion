using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MelodyFusion.DLL.Migrations
{
    /// <inheritdoc />
    public partial class FixedUserStatisticConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuthenticationStatistic_UserId",
                table: "AuthenticationStatistic");

            migrationBuilder.CreateIndex(
                name: "IX_AuthenticationStatistic_UserId",
                table: "AuthenticationStatistic",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuthenticationStatistic_UserId",
                table: "AuthenticationStatistic");

            migrationBuilder.CreateIndex(
                name: "IX_AuthenticationStatistic_UserId",
                table: "AuthenticationStatistic",
                column: "UserId",
                unique: true);
        }
    }
}
