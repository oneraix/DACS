using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoomScheduleColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonHoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiangVien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhongHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ngay = table.Column<DateTime>(type: "date", nullable: false),
                    ThoiGianBatDau = table.Column<TimeSpan>(type: "time", nullable: false),
                    ThoiGianKetThuc = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomSchedules", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomSchedules");
        }
    }
}
