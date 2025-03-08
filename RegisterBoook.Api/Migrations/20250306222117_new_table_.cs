using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegisterBoook.Api.Migrations
{
    /// <inheritdoc />
    public partial class new_table_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutorId",
                table: "books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AutorId",
                table: "books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
