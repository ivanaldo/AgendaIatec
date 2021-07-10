using Microsoft.EntityFrameworkCore.Migrations;

namespace Agenda.Migrations
{
    public partial class criarBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    eventoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    data = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    local = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    participantes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    exclusivo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento", x => x.eventoId);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioCadastro",
                columns: table => new
                {
                    usuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    senha = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    data = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    sexo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioCadastro", x => x.usuarioId);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosEventos",
                columns: table => new
                {
                    idusuario = table.Column<int>(type: "int", nullable: false),
                    idevento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_UsuariosEventos_Evento_idevento",
                        column: x => x.idevento,
                        principalTable: "Evento",
                        principalColumn: "eventoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosEventos_UsuarioCadastro_idusuario",
                        column: x => x.idusuario,
                        principalTable: "UsuarioCadastro",
                        principalColumn: "usuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosEventos_idevento",
                table: "UsuariosEventos",
                column: "idevento");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosEventos_idusuario",
                table: "UsuariosEventos",
                column: "idusuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuariosEventos");

            migrationBuilder.DropTable(
                name: "Evento");

            migrationBuilder.DropTable(
                name: "UsuarioCadastro");
        }
    }
}
