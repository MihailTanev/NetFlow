using Microsoft.EntityFrameworkCore.Migrations;

namespace NetFlow.Web.Data.Migrations
{
    public partial class AddPictureColumnInPostTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Posts");
        }
    }
}
