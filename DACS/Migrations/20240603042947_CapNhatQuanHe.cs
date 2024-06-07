using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class CapNhatQuanHe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhongHoc",
                table: "RoomSchedules",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_RoomSchedules_PhongHoc",
                table: "RoomSchedules",
                column: "PhongHoc");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomSchedules_Classes_PhongHoc",
                table: "RoomSchedules",
                column: "PhongHoc",
                principalTable: "Classes",
                principalColumn: "MaPhongHoc",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomSchedules_Classes_PhongHoc",
                table: "RoomSchedules");

            migrationBuilder.DropIndex(
                name: "IX_RoomSchedules_PhongHoc",
                table: "RoomSchedules");

            migrationBuilder.AlterColumn<string>(
                name: "PhongHoc",
                table: "RoomSchedules",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
