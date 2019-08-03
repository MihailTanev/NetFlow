using Microsoft.EntityFrameworkCore.Migrations;

namespace NetFlow.Web.Data.Migrations
{
    public partial class AddPictureColumnInCourseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Courses");
        }
    }
}
