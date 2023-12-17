using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Semester_3_Projekt.Migrations
{
    /// <inheritdoc />
    public partial class batchlog_addition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "dValue",
                table: "BatchLogs",
                type: "double",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dValue",
                table: "BatchLogs");
        }
    }
}
