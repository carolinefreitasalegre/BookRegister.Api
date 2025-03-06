using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegisterBoook.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "books",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "books",
                newName: "Guid");
        }
    }
}
