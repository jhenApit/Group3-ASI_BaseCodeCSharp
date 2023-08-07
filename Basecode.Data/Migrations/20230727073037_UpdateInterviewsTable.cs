using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Basecode.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInterviewsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeEnd",
                table: "Interviews",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeStart",
                table: "Interviews",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeEnd",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "TimeStart",
                table: "Interviews");
        }
    }
}
