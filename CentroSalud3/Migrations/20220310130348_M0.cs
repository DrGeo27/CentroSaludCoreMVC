using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CentroSalud3.Migrations
{
    public partial class M0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    MedicoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicoNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicoConsulta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicoTelefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicoEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumPacientes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.MedicoId);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PacienteFxNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PacienteEdad = table.Column<int>(type: "int", nullable: false),
                    PacienteSexo = table.Column<int>(type: "int", nullable: false),
                    PacienteTelefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PacienteEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.PacienteId);
                });

            migrationBuilder.CreateTable(
                name: "PacientesConMedicos",
                columns: table => new
                {
                    PacienteConMedicoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(type: "int", nullable: true),
                    MedicoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacientesConMedicos", x => x.PacienteConMedicoId);
                    table.ForeignKey(
                        name: "FK_PacientesConMedicos_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "MedicoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PacientesConMedicos_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PacientesConMedicos_MedicoId",
                table: "PacientesConMedicos",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_PacientesConMedicos_PacienteId",
                table: "PacientesConMedicos",
                column: "PacienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PacientesConMedicos");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
