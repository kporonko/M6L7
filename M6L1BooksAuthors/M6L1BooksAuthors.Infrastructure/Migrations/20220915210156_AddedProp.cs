using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace M6L1BooksAuthors.Migrations
{
    public partial class AddedProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddedProp",
                table: "Author",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedProp",
                table: "Author");
        }
    }
}
