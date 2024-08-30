using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WSInformatica.Migrations
{
    public partial class InitialCreateBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Automotors_Consulta_ConsultaId",
                table: "Automotors");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Consulta_ConsultaId",
                table: "Personas");

            migrationBuilder.AlterColumn<bool>(
                name: "Resultado",
                table: "Personas",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ConsultaId",
                table: "Personas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Resultado",
                table: "Automotors",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ConsultaId",
                table: "Automotors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Resultado",
                table: "Armas",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Efectivos_IdDependencia",
                table: "Efectivos",
                column: "IdDependencia");

            migrationBuilder.AddForeignKey(
                name: "FK_Automotors_Consulta_ConsultaId",
                table: "Automotors",
                column: "ConsultaId",
                principalTable: "Consulta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Efectivos_Dependencia_IdDependencia",
                table: "Efectivos",
                column: "IdDependencia",
                principalTable: "Dependencia",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Consulta_ConsultaId",
                table: "Personas",
                column: "ConsultaId",
                principalTable: "Consulta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Automotors_Consulta_ConsultaId",
                table: "Automotors");

            migrationBuilder.DropForeignKey(
                name: "FK_Efectivos_Dependencia_IdDependencia",
                table: "Efectivos");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Consulta_ConsultaId",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Efectivos_IdDependencia",
                table: "Efectivos");

            migrationBuilder.AlterColumn<bool>(
                name: "Resultado",
                table: "Personas",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "ConsultaId",
                table: "Personas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "Resultado",
                table: "Automotors",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "ConsultaId",
                table: "Automotors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "Resultado",
                table: "Armas",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddForeignKey(
                name: "FK_Automotors_Consulta_ConsultaId",
                table: "Automotors",
                column: "ConsultaId",
                principalTable: "Consulta",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Consulta_ConsultaId",
                table: "Personas",
                column: "ConsultaId",
                principalTable: "Consulta",
                principalColumn: "Id");
        }
    }
}
