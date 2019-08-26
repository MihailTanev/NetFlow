using Microsoft.EntityFrameworkCore.Migrations;

namespace NetFlow.Web.Data.Migrations
{
    public partial class AddCommentColumnInEnrollmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Enrollments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Enrollments");
        }
    }
}
