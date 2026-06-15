using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eVote360Pro.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlianzasPoliticas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlianzasPoliticas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ciudadano",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroIdentificacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudadano", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Elecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FechaRealizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoEleccion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elecciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartidoPolitico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Siglas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartidoPolitico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PuestoElectivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuestoElectivo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RolUsuario = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    PartidoPoliticoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CodigosVerificacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CiudadanoId = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Utilizado = table.Column<bool>(type: "bit", nullable: false),
                    CiudadanoId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodigosVerificacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodigosVerificacion_Ciudadano_CiudadanoId",
                        column: x => x.CiudadanoId,
                        principalTable: "Ciudadano",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CodigosVerificacion_Ciudadano_CiudadanoId1",
                        column: x => x.CiudadanoId1,
                        principalTable: "Ciudadano",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AlianzaPoliticaPartidoPolitico",
                columns: table => new
                {
                    AlianzasId = table.Column<int>(type: "int", nullable: false),
                    PartidosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlianzaPoliticaPartidoPolitico", x => new { x.AlianzasId, x.PartidosId });
                    table.ForeignKey(
                        name: "FK_AlianzaPoliticaPartidoPolitico_AlianzasPoliticas_AlianzasId",
                        column: x => x.AlianzasId,
                        principalTable: "AlianzasPoliticas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlianzaPoliticaPartidoPolitico_PartidoPolitico_PartidosId",
                        column: x => x.PartidosId,
                        principalTable: "PartidoPolitico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FotoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    PartidoPoliticoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidatos_PartidoPolitico_PartidoPoliticoId",
                        column: x => x.PartidoPoliticoId,
                        principalTable: "PartidoPolitico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EleccionPuestoElectivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EleccionId = table.Column<int>(type: "int", nullable: false),
                    PuestoElectivoId = table.Column<int>(type: "int", nullable: false),
                    EleccionId1 = table.Column<int>(type: "int", nullable: true),
                    PuestoElectivoId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleccionPuestoElectivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EleccionPuestoElectivo_Elecciones_EleccionId",
                        column: x => x.EleccionId,
                        principalTable: "Elecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EleccionPuestoElectivo_Elecciones_EleccionId1",
                        column: x => x.EleccionId1,
                        principalTable: "Elecciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EleccionPuestoElectivo_PuestoElectivo_PuestoElectivoId",
                        column: x => x.PuestoElectivoId,
                        principalTable: "PuestoElectivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EleccionPuestoElectivo_PuestoElectivo_PuestoElectivoId1",
                        column: x => x.PuestoElectivoId1,
                        principalTable: "PuestoElectivo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AsignacionDirigente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    PartidoPoliticoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignacionDirigente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AsignacionDirigente_PartidoPolitico_PartidoPoliticoId",
                        column: x => x.PartidoPoliticoId,
                        principalTable: "PartidoPolitico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AsignacionDirigente_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AsignacionesCandidatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidatoId = table.Column<int>(type: "int", nullable: false),
                    PuestoElectivoId = table.Column<int>(type: "int", nullable: false),
                    EleccionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignacionesCandidatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AsignacionesCandidatos_Candidatos_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "Candidatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AsignacionesCandidatos_Elecciones_EleccionId",
                        column: x => x.EleccionId,
                        principalTable: "Elecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AsignacionesCandidatos_PuestoElectivo_PuestoElectivoId",
                        column: x => x.PuestoElectivoId,
                        principalTable: "PuestoElectivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Votos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EleccionId = table.Column<int>(type: "int", nullable: false),
                    CiudadanoId = table.Column<int>(type: "int", nullable: false),
                    FechaVoto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CandidatoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votos_Candidatos_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "Candidatos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Votos_Ciudadano_CiudadanoId",
                        column: x => x.CiudadanoId,
                        principalTable: "Ciudadano",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votos_Elecciones_EleccionId",
                        column: x => x.EleccionId,
                        principalTable: "Elecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VotoDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VotoId = table.Column<int>(type: "int", nullable: false),
                    PuestoElectivoId = table.Column<int>(type: "int", nullable: false),
                    CandidatoId = table.Column<int>(type: "int", nullable: true),
                    VotoId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotoDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VotoDetalles_Candidatos_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "Candidatos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VotoDetalles_PuestoElectivo_PuestoElectivoId",
                        column: x => x.PuestoElectivoId,
                        principalTable: "PuestoElectivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VotoDetalles_Votos_VotoId",
                        column: x => x.VotoId,
                        principalTable: "Votos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VotoDetalles_Votos_VotoId1",
                        column: x => x.VotoId1,
                        principalTable: "Votos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlianzaPoliticaPartidoPolitico_PartidosId",
                table: "AlianzaPoliticaPartidoPolitico",
                column: "PartidosId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionDirigente_PartidoPoliticoId",
                table: "AsignacionDirigente",
                column: "PartidoPoliticoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionDirigente_UsuarioId",
                table: "AsignacionDirigente",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesCandidatos_CandidatoId",
                table: "AsignacionesCandidatos",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesCandidatos_EleccionId",
                table: "AsignacionesCandidatos",
                column: "EleccionId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesCandidatos_PuestoElectivoId",
                table: "AsignacionesCandidatos",
                column: "PuestoElectivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_PartidoPoliticoId",
                table: "Candidatos",
                column: "PartidoPoliticoId");

            migrationBuilder.CreateIndex(
                name: "IX_CodigosVerificacion_CiudadanoId",
                table: "CodigosVerificacion",
                column: "CiudadanoId");

            migrationBuilder.CreateIndex(
                name: "IX_CodigosVerificacion_CiudadanoId1",
                table: "CodigosVerificacion",
                column: "CiudadanoId1");

            migrationBuilder.CreateIndex(
                name: "IX_EleccionPuestoElectivo_EleccionId",
                table: "EleccionPuestoElectivo",
                column: "EleccionId");

            migrationBuilder.CreateIndex(
                name: "IX_EleccionPuestoElectivo_EleccionId1",
                table: "EleccionPuestoElectivo",
                column: "EleccionId1");

            migrationBuilder.CreateIndex(
                name: "IX_EleccionPuestoElectivo_PuestoElectivoId",
                table: "EleccionPuestoElectivo",
                column: "PuestoElectivoId");

            migrationBuilder.CreateIndex(
                name: "IX_EleccionPuestoElectivo_PuestoElectivoId1",
                table: "EleccionPuestoElectivo",
                column: "PuestoElectivoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_CorreoElectronico",
                table: "Usuarios",
                column: "CorreoElectronico",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_NombreUsuario",
                table: "Usuarios",
                column: "NombreUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VotoDetalles_CandidatoId",
                table: "VotoDetalles",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_VotoDetalles_PuestoElectivoId",
                table: "VotoDetalles",
                column: "PuestoElectivoId");

            migrationBuilder.CreateIndex(
                name: "IX_VotoDetalles_VotoId",
                table: "VotoDetalles",
                column: "VotoId");

            migrationBuilder.CreateIndex(
                name: "IX_VotoDetalles_VotoId1",
                table: "VotoDetalles",
                column: "VotoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Votos_CandidatoId",
                table: "Votos",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Votos_CiudadanoId",
                table: "Votos",
                column: "CiudadanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Votos_EleccionId",
                table: "Votos",
                column: "EleccionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlianzaPoliticaPartidoPolitico");

            migrationBuilder.DropTable(
                name: "AsignacionDirigente");

            migrationBuilder.DropTable(
                name: "AsignacionesCandidatos");

            migrationBuilder.DropTable(
                name: "CodigosVerificacion");

            migrationBuilder.DropTable(
                name: "EleccionPuestoElectivo");

            migrationBuilder.DropTable(
                name: "VotoDetalles");

            migrationBuilder.DropTable(
                name: "AlianzasPoliticas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "PuestoElectivo");

            migrationBuilder.DropTable(
                name: "Votos");

            migrationBuilder.DropTable(
                name: "Candidatos");

            migrationBuilder.DropTable(
                name: "Ciudadano");

            migrationBuilder.DropTable(
                name: "Elecciones");

            migrationBuilder.DropTable(
                name: "PartidoPolitico");
        }
    }
}
