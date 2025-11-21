using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WSInformatica.Migrations
{
    /// <inheritdoc />
    public partial class AddLatitudLongitudToConsulta2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitud",
                table: "Consulta",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitud",
                table: "Consulta",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitud",
                table: "Consulta");

            migrationBuilder.DropColumn(
                name: "Longitud",
                table: "Consulta");
        }
    }
}
