using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndDemoday.Migrations
{
    /// <inheritdoc />
    public partial class SetExplicitTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");
        }
    }
}
