using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAppData.Migrations
{
    /// <inheritdoc />
    public partial class imagefeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "tblSubject",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "tblSubject");
        }
    }
}
