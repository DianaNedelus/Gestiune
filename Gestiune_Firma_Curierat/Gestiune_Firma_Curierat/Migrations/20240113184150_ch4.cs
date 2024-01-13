using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestiune_Firma_Curierat.Migrations
{
    /// <inheritdoc />
    public partial class ch4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Colete");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Colete",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
