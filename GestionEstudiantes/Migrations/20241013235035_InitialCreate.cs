using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionEstudiantes.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    CursoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.CursoId);
                });

            migrationBuilder.CreateTable(
                name: "Estudites",
                columns: table => new
                {
                    EstuduanteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudites", x => x.EstuduanteId);
                });

            migrationBuilder.CreateTable(
                name: "CursosEstudiantes",
                columns: table => new
                {
                    EstudianteId = table.Column<int>(type: "int", nullable: false),
                    cursoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursosEstudiantes", x => new { x.EstudianteId, x.cursoId });
                    table.ForeignKey(
                        name: "FK_CursosEstudiantes_Curso_cursoId",
                        column: x => x.cursoId,
                        principalTable: "Curso",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursosEstudiantes_Estudites_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudites",
                        principalColumn: "EstuduanteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CursosEstudiantes_cursoId",
                table: "CursosEstudiantes",
                column: "cursoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CursosEstudiantes");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Estudites");
        }
    }
}
