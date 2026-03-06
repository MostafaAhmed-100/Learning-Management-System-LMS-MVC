using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1_MVC_.Migrations
{
    /// <inheritdoc />
    public partial class AddRestrictions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "courses",
                newName: "CourseTitle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseTitle",
                table: "courses",
                newName: "Title");
        }
    }
}
