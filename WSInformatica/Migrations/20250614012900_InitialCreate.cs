using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WSInformatica.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consulta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDespachante = table.Column<int>(type: "int", nullable: false),
                    IdSolicitante = table.Column<int>(type: "int", nullable: false),
                    Movil = table.Column<int>(type: "int", nullable: false),
                    Lugar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Idjuridiccion = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consulta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dependencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoAutomotor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAutomotor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Arma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultaId = table.Column<int>(type: "int", nullable: true),
                    NumArma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calibre = table.Column<int>(type: "int", nullable: true),
                    Resultado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arma_Consulta_ConsultaId",
                        column: x => x.ConsultaId,
                        principalTable: "Consulta",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Automotor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultaId = table.Column<int>(type: "int", nullable: false),
                    TipoAutomotorId = table.Column<int>(type: "int", nullable: true),
                    Dominio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chasis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Motor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resultado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automotor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Automotor_Consulta_ConsultaId",
                        column: x => x.ConsultaId,
                        principalTable: "Consulta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultaId = table.Column<int>(type: "int", nullable: false),
                    Dni = table.Column<int>(type: "int", nullable: true),
                    Nombre1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Clase = table.Column<int>(type: "int", nullable: true),
                    Resultado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persona_Consulta_ConsultaId",
                        column: x => x.ConsultaId,
                        principalTable: "Consulta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Efectivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Legajo = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdDependencia = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Efectivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Efectivo_Dependencia_IdDependencia",
                        column: x => x.IdDependencia,
                        principalTable: "Dependencia",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEfectivo = table.Column<int>(type: "int", nullable: true),
                    EsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Efectivo_IdEfectivo",
                        column: x => x.IdEfectivo,
                        principalTable: "Efectivo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arma_ConsultaId",
                table: "Arma",
                column: "ConsultaId");

            migrationBuilder.CreateIndex(
                name: "IX_Automotor_ConsultaId",
                table: "Automotor",
                column: "ConsultaId");

            migrationBuilder.CreateIndex(
                name: "IX_Efectivo_IdDependencia",
                table: "Efectivo",
                column: "IdDependencia");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_ConsultaId",
                table: "Persona",
                column: "ConsultaId");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdEfectivo",
                table: "User",
                column: "IdEfectivo",
                unique: true,
                filter: "[IdEfectivo] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arma");

            migrationBuilder.DropTable(
                name: "Automotor");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "TipoAutomotor");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Consulta");

            migrationBuilder.DropTable(
                name: "Efectivo");

            migrationBuilder.DropTable(
                name: "Dependencia");
        }
    }
}
