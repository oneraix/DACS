using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class ChuyenIssueCategoryThanhString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueDetails_IssueCategories_IssueCategoryId",
                table: "IssueDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueCategories",
                table: "IssueCategories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "IssueCategories");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "IssueCategories",
                newName: "IssueCategoryName");

            migrationBuilder.AlterColumn<string>(
                name: "IssueCategoryId",
                table: "IssueDetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "IssueCategoryId",
                table: "IssueCategories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueCategories",
                table: "IssueCategories",
                column: "IssueCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueDetails_IssueCategories_IssueCategoryId",
                table: "IssueDetails",
                column: "IssueCategoryId",
                principalTable: "IssueCategories",
                principalColumn: "IssueCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueDetails_IssueCategories_IssueCategoryId",
                table: "IssueDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueCategories",
                table: "IssueCategories");

            migrationBuilder.DropColumn(
                name: "IssueCategoryId",
                table: "IssueCategories");

            migrationBuilder.RenameColumn(
                name: "IssueCategoryName",
                table: "IssueCategories",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "IssueCategoryId",
                table: "IssueDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "IssueCategories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueCategories",
                table: "IssueCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueDetails_IssueCategories_IssueCategoryId",
                table: "IssueDetails",
                column: "IssueCategoryId",
                principalTable: "IssueCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
