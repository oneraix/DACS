using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    MaGhiChu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayGhi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.MaGhiChu);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    MaLoaiPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiPhong = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.MaLoaiPhong);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    MaPhongHoc = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SoPhong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tang = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomCategoriesMaLoaiPhong = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.MaPhongHoc);
                    table.ForeignKey(
                        name: "FK_Classes_Rooms_RoomCategoriesMaLoaiPhong",
                        column: x => x.RoomCategoriesMaLoaiPhong,
                        principalTable: "Rooms",
                        principalColumn: "MaLoaiPhong");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_RoomCategoriesMaLoaiPhong",
                table: "Classes",
                column: "RoomCategoriesMaLoaiPhong");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
