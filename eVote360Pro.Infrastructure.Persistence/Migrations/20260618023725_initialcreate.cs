using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eVote360Pro.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ciudadanos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroIdentificacion = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudadanos", x => x.Id);
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
                name: "PartidosPoliticos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Siglas = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartidosPoliticos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PuestosElectivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuestosElectivos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesAlianzas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartidoOrigenId = table.Column<int>(type: "int", nullable: false),
                    PartidoDestinoId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaRespuesta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesAlianzas", x => x.Id);
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
                    EleccionId = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    FechaGeneracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Utilizado = table.Column<bool>(type: "bit", nullable: false),
                    CiudadanoId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodigosVerificacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodigosVerificacion_Ciudadanos_CiudadanoId",
                        column: x => x.CiudadanoId,
                        principalTable: "Ciudadanos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CodigosVerificacion_Ciudadanos_CiudadanoId1",
                        column: x => x.CiudadanoId1,
                        principalTable: "Ciudadanos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CodigosVerificacion_Elecciones_EleccionId",
                        column: x => x.EleccionId,
                        principalTable: "Elecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Votos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EleccionId = table.Column<int>(type: "int", nullable: false),
                    CiudadanoId = table.Column<int>(type: "int", nullable: false),
                    FechaVotacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVoto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CiudadanoId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votos_Ciudadanos_CiudadanoId",
                        column: x => x.CiudadanoId,
                        principalTable: "Ciudadanos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votos_Ciudadanos_CiudadanoId1",
                        column: x => x.CiudadanoId1,
                        principalTable: "Ciudadanos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Votos_Elecciones_EleccionId",
                        column: x => x.EleccionId,
                        principalTable: "Elecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlianzasPoliticas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartidoOrigenId = table.Column<int>(type: "int", nullable: false),
                    PartidoDestinoId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaRespuesta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Vigente = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlianzasPoliticas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlianzasPoliticas_PartidosPoliticos_PartidoDestinoId",
                        column: x => x.PartidoDestinoId,
                        principalTable: "PartidosPoliticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlianzasPoliticas_PartidosPoliticos_PartidoOrigenId",
                        column: x => x.PartidoOrigenId,
                        principalTable: "PartidosPoliticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_Candidatos_PartidosPoliticos_PartidoPoliticoId",
                        column: x => x.PartidoPoliticoId,
                        principalTable: "PartidosPoliticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EleccionPuestoElectivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EleccionId = table.Column<int>(type: "int", nullable: false),
                    PuestoElectivoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleccionPuestoElectivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EleccionPuestoElectivos_Elecciones_EleccionId",
                        column: x => x.EleccionId,
                        principalTable: "Elecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EleccionPuestoElectivos_PuestosElectivos_PuestoElectivoId",
                        column: x => x.PuestoElectivoId,
                        principalTable: "PuestosElectivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AsignacionesDirigentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    PartidoPoliticoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignacionesDirigentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AsignacionesDirigentes_PartidosPoliticos_PartidoPoliticoId",
                        column: x => x.PartidoPoliticoId,
                        principalTable: "PartidosPoliticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AsignacionesDirigentes_Usuarios_UsuarioId",
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
                    EleccionId = table.Column<int>(type: "int", nullable: false),
                    PartidoPoliticoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignacionesCandidatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AsignacionesCandidatos_Candidatos_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "Candidatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AsignacionesCandidatos_Elecciones_EleccionId",
                        column: x => x.EleccionId,
                        principalTable: "Elecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AsignacionesCandidatos_PartidosPoliticos_PartidoPoliticoId",
                        column: x => x.PartidoPoliticoId,
                        principalTable: "PartidosPoliticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AsignacionesCandidatos_PuestosElectivos_PuestoElectivoId",
                        column: x => x.PuestoElectivoId,
                        principalTable: "PuestosElectivos",
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
                    CandidatoId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_VotoDetalles_PuestosElectivos_PuestoElectivoId",
                        column: x => x.PuestoElectivoId,
                        principalTable: "PuestosElectivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VotoDetalles_Votos_VotoId",
                        column: x => x.VotoId,
                        principalTable: "Votos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlianzasPoliticas_PartidoDestinoId",
                table: "AlianzasPoliticas",
                column: "PartidoDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlianzasPoliticas_PartidoOrigenId",
                table: "AlianzasPoliticas",
                column: "PartidoOrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesCandidatos_CandidatoId",
                table: "AsignacionesCandidatos",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesCandidatos_EleccionId",
                table: "AsignacionesCandidatos",
                column: "EleccionId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesCandidatos_PartidoPoliticoId",
                table: "AsignacionesCandidatos",
                column: "PartidoPoliticoId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesCandidatos_PuestoElectivoId",
                table: "AsignacionesCandidatos",
                column: "PuestoElectivoId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesDirigentes_PartidoPoliticoId",
                table: "AsignacionesDirigentes",
                column: "PartidoPoliticoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesDirigentes_UsuarioId",
                table: "AsignacionesDirigentes",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_PartidoPoliticoId",
                table: "Candidatos",
                column: "PartidoPoliticoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ciudadanos_NumeroIdentificacion",
                table: "Ciudadanos",
                column: "NumeroIdentificacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CodigosVerificacion_CiudadanoId",
                table: "CodigosVerificacion",
                column: "CiudadanoId");

            migrationBuilder.CreateIndex(
                name: "IX_CodigosVerificacion_CiudadanoId1",
                table: "CodigosVerificacion",
                column: "CiudadanoId1");

            migrationBuilder.CreateIndex(
                name: "IX_CodigosVerificacion_Codigo",
                table: "CodigosVerificacion",
                column: "Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_CodigosVerificacion_EleccionId",
                table: "CodigosVerificacion",
                column: "EleccionId");

            migrationBuilder.CreateIndex(
                name: "IX_EleccionPuestoElectivos_EleccionId",
                table: "EleccionPuestoElectivos",
                column: "EleccionId");

            migrationBuilder.CreateIndex(
                name: "IX_EleccionPuestoElectivos_PuestoElectivoId",
                table: "EleccionPuestoElectivos",
                column: "PuestoElectivoId");

            migrationBuilder.CreateIndex(
                name: "IX_PartidosPoliticos_Siglas",
                table: "PartidosPoliticos",
                column: "Siglas",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PuestosElectivos_Nombre",
                table: "PuestosElectivos",
                column: "Nombre",
                unique: true);

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
                name: "IX_Votos_CiudadanoId",
                table: "Votos",
                column: "CiudadanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Votos_CiudadanoId1",
                table: "Votos",
                column: "CiudadanoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Votos_EleccionId",
                table: "Votos",
                column: "EleccionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlianzasPoliticas");

            migrationBuilder.DropTable(
                name: "AsignacionesCandidatos");

            migrationBuilder.DropTable(
                name: "AsignacionesDirigentes");

            migrationBuilder.DropTable(
                name: "CodigosVerificacion");

            migrationBuilder.DropTable(
                name: "EleccionPuestoElectivos");

            migrationBuilder.DropTable(
                name: "SolicitudesAlianzas");

            migrationBuilder.DropTable(
                name: "VotoDetalles");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Candidatos");

            migrationBuilder.DropTable(
                name: "PuestosElectivos");

            migrationBuilder.DropTable(
                name: "Votos");

            migrationBuilder.DropTable(
                name: "PartidosPoliticos");

            migrationBuilder.DropTable(
                name: "Ciudadanos");

            migrationBuilder.DropTable(
                name: "Elecciones");
        }
    }
}
