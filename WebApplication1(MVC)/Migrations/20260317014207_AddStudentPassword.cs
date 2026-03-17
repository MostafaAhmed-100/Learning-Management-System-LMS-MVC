using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1_MVC_.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Student_Password",
                table: "students",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Student_Password",
                table: "students");
        }
    }
}
