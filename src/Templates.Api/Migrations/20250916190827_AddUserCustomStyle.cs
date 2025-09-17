using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Templates.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserCustomStyle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomStyle",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomStyle",
                table: "Users");
        }
    }
}
