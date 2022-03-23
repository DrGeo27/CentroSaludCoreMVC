using Microsoft.EntityFrameworkCore.Migrations;

namespace CentroSalud3.Migrations
{
    public partial class M1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PacienteTelefono",
                table: "Pacientes",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MedicoTelefono",
                table: "Medicos",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Enfermeras",
                columns: table => new
                {
                    EnfermeraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnfermeraNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnfermeraConsulta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnfermeraTelefono = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    EnfermeraEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnfermeraNumPacientes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enfermeras", x => x.EnfermeraId);
                });

            migrationBuilder.CreateTable(
                name: "Medicaciones",
                columns: table => new
                {
                    MedicacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicacionNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicacionDosis = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MedicacionGrupo = table.Column<int>(type: "int", nullable: false),
                    MedicacionDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumPacientesPautados = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicaciones", x => x.MedicacionId);
                });

            migrationBuilder.CreateTable(
                name: "PacientesConEnfermeras",
                columns: table => new
                {
                    PacienteConEnfermeraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(type: "int", nullable: true),
                    EnfermeraId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacientesConEnfermeras", x => x.PacienteConEnfermeraId);
                    table.ForeignKey(
                        name: "FK_PacientesConEnfermeras_Enfermeras_EnfermeraId",
                        column: x => x.EnfermeraId,
                        principalTable: "Enfermeras",
                        principalColumn: "EnfermeraId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PacientesConEnfermeras_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PacientesConMedicaciones",
                columns: table => new
                {
                    PacienteConMedicacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(type: "int", nullable: true),
                    MedicacionId = table.Column<int>(type: "int", nullable: true),
                    MedicoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacientesConMedicaciones", x => x.PacienteConMedicacionId);
                    table.ForeignKey(
                        name: "FK_PacientesConMedicaciones_Medicaciones_MedicacionId",
                        column: x => x.MedicacionId,
                        principalTable: "Medicaciones",
                        principalColumn: "MedicacionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PacientesConMedicaciones_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "MedicoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PacientesConMedicaciones_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PacientesConEnfermeras_EnfermeraId",
                table: "PacientesConEnfermeras",
                column: "EnfermeraId");

            migrationBuilder.CreateIndex(
                name: "IX_PacientesConEnfermeras_PacienteId",
                table: "PacientesConEnfermeras",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_PacientesConMedicaciones_MedicacionId",
                table: "PacientesConMedicaciones",
                column: "MedicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_PacientesConMedicaciones_MedicoId",
                table: "PacientesConMedicaciones",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_PacientesConMedicaciones_PacienteId",
                table: "PacientesConMedicaciones",
                column: "PacienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PacientesConEnfermeras");

            migrationBuilder.DropTable(
                name: "PacientesConMedicaciones");

            migrationBuilder.DropTable(
                name: "Enfermeras");

            migrationBuilder.DropTable(
                name: "Medicaciones");

            migrationBuilder.AlterColumn<string>(
                name: "PacienteTelefono",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldMaxLength: 9,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MedicoTelefono",
                table: "Medicos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldMaxLength: 9);
        }
    }
}
