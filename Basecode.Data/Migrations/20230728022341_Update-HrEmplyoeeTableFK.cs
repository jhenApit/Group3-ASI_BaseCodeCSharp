using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Basecode.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHrEmplyoeeTableFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_JobPostings_JobId",
                table: "Applicants");

            migrationBuilder.DropForeignKey(
                name: "FK_HrEmployees_AspNetUsers_UserId",
                table: "HrEmployees");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_JobId",
                table: "Applicants");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "HrEmployees",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HrEmployees_AspNetUsers_UserId",
                table: "HrEmployees",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HrEmployees_AspNetUsers_UserId",
                table: "HrEmployees");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "HrEmployees",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_JobId",
                table: "Applicants",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_JobPostings_JobId",
                table: "Applicants",
                column: "JobId",
                principalTable: "JobPostings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HrEmployees_AspNetUsers_UserId",
                table: "HrEmployees",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
