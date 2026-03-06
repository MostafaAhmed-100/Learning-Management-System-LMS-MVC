using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1_MVC_.Migrations
{
    /// <inheritdoc />
    public partial class fixNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "students",
                newName: "StudentName");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "students",
                newName: "StudentAge");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "instructors",
                newName: "InstructorPhone");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "instructors",
                newName: "InstructorName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "instructors",
                newName: "InstructorEmail");

            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "instructors",
                newName: "InstructorBio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentName",
                table: "students",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StudentAge",
                table: "students",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "InstructorPhone",
                table: "instructors",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "InstructorName",
                table: "instructors",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "InstructorEmail",
                table: "instructors",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "InstructorBio",
                table: "instructors",
                newName: "Bio");
        }
    }
}
