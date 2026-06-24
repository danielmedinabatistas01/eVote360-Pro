using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eVote360Pro.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixAlianzaPoliticaColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionDirigente_PartidoPolitico_PartidoPoliticoId",
                table: "AsignacionDirigente");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionDirigente_Usuarios_UsuarioId",
                table: "AsignacionDirigente");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesCandidatos_Candidatos_CandidatoId",
                table: "AsignacionesCandidatos");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesCandidatos_Elecciones_EleccionId",
                table: "AsignacionesCandidatos");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesCandidatos_PuestoElectivo_PuestoElectivoId",
                table: "AsignacionesCandidatos");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidatos_PartidoPolitico_PartidoPoliticoId",
                table: "Candidatos");

            migrationBuilder.DropForeignKey(
                name: "FK_CodigosVerificacion_Ciudadano_CiudadanoId",
                table: "CodigosVerificacion");

            migrationBuilder.DropForeignKey(
                name: "FK_CodigosVerificacion_Ciudadano_CiudadanoId1",
                table: "CodigosVerificacion");

            migrationBuilder.DropForeignKey(
                name: "FK_CodigosVerificacion_Elecciones_EleccionId",
                table: "CodigosVerificacion");

            migrationBuilder.DropForeignKey(
                name: "FK_EleccionPuestoElectivo_PuestoElectivo_PuestoElectivoId",
                table: "EleccionPuestoElectivo");

            migrationBuilder.DropForeignKey(
                name: "FK_EleccionPuestoElectivo_PuestoElectivo_PuestoElectivoId1",
                table: "EleccionPuestoElectivo");

            migrationBuilder.DropForeignKey(
                name: "FK_VotoDetalles_PuestoElectivo_PuestoElectivoId",
                table: "VotoDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_Votos_Candidatos_CandidatoId",
                table: "Votos");

            migrationBuilder.DropForeignKey(
                name: "FK_Votos_Ciudadano_CiudadanoId",
                table: "Votos");

            migrationBuilder.DropTable(
                name: "AlianzaPoliticaPartidoPolitico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PuestoElectivo",
                table: "PuestoElectivo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartidoPolitico",
                table: "PartidoPolitico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ciudadano",
                table: "Ciudadano");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AsignacionDirigente",
                table: "AsignacionDirigente");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionDirigente_UsuarioId",
                table: "AsignacionDirigente");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "AlianzasPoliticas");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "AlianzasPoliticas");

            migrationBuilder.RenameTable(
                name: "PuestoElectivo",
                newName: "PuestosElectivos");

            migrationBuilder.RenameTable(
                name: "PartidoPolitico",
                newName: "PartidosPoliticos");

            migrationBuilder.RenameTable(
                name: "Ciudadano",
                newName: "Ciudadanos");

            migrationBuilder.RenameTable(
                name: "AsignacionDirigente",
                newName: "AsignacionesDirigentes");

            migrationBuilder.RenameColumn(
                name: "CandidatoId",
                table: "Votos",
                newName: "CiudadanoId1");

            migrationBuilder.RenameIndex(
                name: "IX_Votos_CandidatoId",
                table: "Votos",
                newName: "IX_Votos_CiudadanoId1");

            migrationBuilder.RenameIndex(
                name: "IX_AsignacionDirigente_PartidoPoliticoId",
                table: "AsignacionesDirigentes",
                newName: "IX_AsignacionesDirigentes_PartidoPoliticoId");

            migrationBuilder.AddColumn<int>(
                name: "PartidoPoliticoId",
                table: "AsignacionesCandidatos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PuestoElectivoId1",
                table: "AsignacionesCandidatos",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "AlianzasPoliticas",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRespuesta",
                table: "AlianzasPoliticas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSolicitud",
                table: "AlianzasPoliticas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PartidoDestinoId",
                table: "AlianzasPoliticas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartidoOrigenId",
                table: "AlianzasPoliticas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Vigente",
                table: "AlianzasPoliticas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "PuestosElectivos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Siglas",
                table: "PartidosPoliticos",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "PartidosPoliticos",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroIdentificacion",
                table: "Ciudadanos",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Ciudadanos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Apellido",
                table: "Ciudadanos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PuestosElectivos",
                table: "PuestosElectivos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartidosPoliticos",
                table: "PartidosPoliticos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ciudadanos",
                table: "Ciudadanos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AsignacionesDirigentes",
                table: "AsignacionesDirigentes",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_CodigosVerificacion_Codigo",
                table: "CodigosVerificacion",
                column: "Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesCandidatos_PartidoPoliticoId",
                table: "AsignacionesCandidatos",
                column: "PartidoPoliticoId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesCandidatos_PuestoElectivoId1",
                table: "AsignacionesCandidatos",
                column: "PuestoElectivoId1");

            migrationBuilder.CreateIndex(
                name: "IX_AlianzasPoliticas_PartidoDestinoId",
                table: "AlianzasPoliticas",
                column: "PartidoDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlianzasPoliticas_PartidoOrigenId",
                table: "AlianzasPoliticas",
                column: "PartidoOrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_PuestosElectivos_Nombre",
                table: "PuestosElectivos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartidosPoliticos_Siglas",
                table: "PartidosPoliticos",
                column: "Siglas",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ciudadanos_NumeroIdentificacion",
                table: "Ciudadanos",
                column: "NumeroIdentificacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesDirigentes_UsuarioId",
                table: "AsignacionesDirigentes",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AlianzasPoliticas_PartidosPoliticos_PartidoDestinoId",
                table: "AlianzasPoliticas",
                column: "PartidoDestinoId",
                principalTable: "PartidosPoliticos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AlianzasPoliticas_PartidosPoliticos_PartidoOrigenId",
                table: "AlianzasPoliticas",
                column: "PartidoOrigenId",
                principalTable: "PartidosPoliticos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesCandidatos_Candidatos_CandidatoId",
                table: "AsignacionesCandidatos",
                column: "CandidatoId",
                principalTable: "Candidatos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesCandidatos_Elecciones_EleccionId",
                table: "AsignacionesCandidatos",
                column: "EleccionId",
                principalTable: "Elecciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesCandidatos_PartidosPoliticos_PartidoPoliticoId",
                table: "AsignacionesCandidatos",
                column: "PartidoPoliticoId",
                principalTable: "PartidosPoliticos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesCandidatos_PuestosElectivos_PuestoElectivoId",
                table: "AsignacionesCandidatos",
                column: "PuestoElectivoId",
                principalTable: "PuestosElectivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesCandidatos_PuestosElectivos_PuestoElectivoId1",
                table: "AsignacionesCandidatos",
                column: "PuestoElectivoId1",
                principalTable: "PuestosElectivos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesDirigentes_PartidosPoliticos_PartidoPoliticoId",
                table: "AsignacionesDirigentes",
                column: "PartidoPoliticoId",
                principalTable: "PartidosPoliticos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesDirigentes_Usuarios_UsuarioId",
                table: "AsignacionesDirigentes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidatos_PartidosPoliticos_PartidoPoliticoId",
                table: "Candidatos",
                column: "PartidoPoliticoId",
                principalTable: "PartidosPoliticos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CodigosVerificacion_Ciudadanos_CiudadanoId",
                table: "CodigosVerificacion",
                column: "CiudadanoId",
                principalTable: "Ciudadanos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CodigosVerificacion_Ciudadanos_CiudadanoId1",
                table: "CodigosVerificacion",
                column: "CiudadanoId1",
                principalTable: "Ciudadanos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CodigosVerificacion_Elecciones_EleccionId",
                table: "CodigosVerificacion",
                column: "EleccionId",
                principalTable: "Elecciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EleccionPuestoElectivo_PuestosElectivos_PuestoElectivoId",
                table: "EleccionPuestoElectivo",
                column: "PuestoElectivoId",
                principalTable: "PuestosElectivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EleccionPuestoElectivo_PuestosElectivos_PuestoElectivoId1",
                table: "EleccionPuestoElectivo",
                column: "PuestoElectivoId1",
                principalTable: "PuestosElectivos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VotoDetalles_PuestosElectivos_PuestoElectivoId",
                table: "VotoDetalles",
                column: "PuestoElectivoId",
                principalTable: "PuestosElectivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votos_Ciudadanos_CiudadanoId",
                table: "Votos",
                column: "CiudadanoId",
                principalTable: "Ciudadanos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votos_Ciudadanos_CiudadanoId1",
                table: "Votos",
                column: "CiudadanoId1",
                principalTable: "Ciudadanos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlianzasPoliticas_PartidosPoliticos_PartidoDestinoId",
                table: "AlianzasPoliticas");

            migrationBuilder.DropForeignKey(
                name: "FK_AlianzasPoliticas_PartidosPoliticos_PartidoOrigenId",
                table: "AlianzasPoliticas");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesCandidatos_Candidatos_CandidatoId",
                table: "AsignacionesCandidatos");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesCandidatos_Elecciones_EleccionId",
                table: "AsignacionesCandidatos");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesCandidatos_PartidosPoliticos_PartidoPoliticoId",
                table: "AsignacionesCandidatos");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesCandidatos_PuestosElectivos_PuestoElectivoId",
                table: "AsignacionesCandidatos");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesCandidatos_PuestosElectivos_PuestoElectivoId1",
                table: "AsignacionesCandidatos");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesDirigentes_PartidosPoliticos_PartidoPoliticoId",
                table: "AsignacionesDirigentes");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesDirigentes_Usuarios_UsuarioId",
                table: "AsignacionesDirigentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidatos_PartidosPoliticos_PartidoPoliticoId",
                table: "Candidatos");

            migrationBuilder.DropForeignKey(
                name: "FK_CodigosVerificacion_Ciudadanos_CiudadanoId",
                table: "CodigosVerificacion");

            migrationBuilder.DropForeignKey(
                name: "FK_CodigosVerificacion_Ciudadanos_CiudadanoId1",
                table: "CodigosVerificacion");

            migrationBuilder.DropForeignKey(
                name: "FK_CodigosVerificacion_Elecciones_EleccionId",
                table: "CodigosVerificacion");

            migrationBuilder.DropForeignKey(
                name: "FK_EleccionPuestoElectivo_PuestosElectivos_PuestoElectivoId",
                table: "EleccionPuestoElectivo");

            migrationBuilder.DropForeignKey(
                name: "FK_EleccionPuestoElectivo_PuestosElectivos_PuestoElectivoId1",
                table: "EleccionPuestoElectivo");

            migrationBuilder.DropForeignKey(
                name: "FK_VotoDetalles_PuestosElectivos_PuestoElectivoId",
                table: "VotoDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_Votos_Ciudadanos_CiudadanoId",
                table: "Votos");

            migrationBuilder.DropForeignKey(
                name: "FK_Votos_Ciudadanos_CiudadanoId1",
                table: "Votos");

            migrationBuilder.DropTable(
                name: "SolicitudesAlianzas");

            migrationBuilder.DropIndex(
                name: "IX_CodigosVerificacion_Codigo",
                table: "CodigosVerificacion");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionesCandidatos_PartidoPoliticoId",
                table: "AsignacionesCandidatos");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionesCandidatos_PuestoElectivoId1",
                table: "AsignacionesCandidatos");

            migrationBuilder.DropIndex(
                name: "IX_AlianzasPoliticas_PartidoDestinoId",
                table: "AlianzasPoliticas");

            migrationBuilder.DropIndex(
                name: "IX_AlianzasPoliticas_PartidoOrigenId",
                table: "AlianzasPoliticas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PuestosElectivos",
                table: "PuestosElectivos");

            migrationBuilder.DropIndex(
                name: "IX_PuestosElectivos_Nombre",
                table: "PuestosElectivos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartidosPoliticos",
                table: "PartidosPoliticos");

            migrationBuilder.DropIndex(
                name: "IX_PartidosPoliticos_Siglas",
                table: "PartidosPoliticos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ciudadanos",
                table: "Ciudadanos");

            migrationBuilder.DropIndex(
                name: "IX_Ciudadanos_NumeroIdentificacion",
                table: "Ciudadanos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AsignacionesDirigentes",
                table: "AsignacionesDirigentes");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionesDirigentes_UsuarioId",
                table: "AsignacionesDirigentes");

            migrationBuilder.DropColumn(
                name: "PartidoPoliticoId",
                table: "AsignacionesCandidatos");

            migrationBuilder.DropColumn(
                name: "PuestoElectivoId1",
                table: "AsignacionesCandidatos");

            migrationBuilder.DropColumn(
                name: "FechaRespuesta",
                table: "AlianzasPoliticas");

            migrationBuilder.DropColumn(
                name: "FechaSolicitud",
                table: "AlianzasPoliticas");

            migrationBuilder.DropColumn(
                name: "PartidoDestinoId",
                table: "AlianzasPoliticas");

            migrationBuilder.DropColumn(
                name: "PartidoOrigenId",
                table: "AlianzasPoliticas");

            migrationBuilder.DropColumn(
                name: "Vigente",
                table: "AlianzasPoliticas");

            migrationBuilder.RenameTable(
                name: "PuestosElectivos",
                newName: "PuestoElectivo");

            migrationBuilder.RenameTable(
                name: "PartidosPoliticos",
                newName: "PartidoPolitico");

            migrationBuilder.RenameTable(
                name: "Ciudadanos",
                newName: "Ciudadano");

            migrationBuilder.RenameTable(
                name: "AsignacionesDirigentes",
                newName: "AsignacionDirigente");

            migrationBuilder.RenameColumn(
                name: "CiudadanoId1",
                table: "Votos",
                newName: "CandidatoId");

            migrationBuilder.RenameIndex(
                name: "IX_Votos_CiudadanoId1",
                table: "Votos",
                newName: "IX_Votos_CandidatoId");

            migrationBuilder.RenameIndex(
                name: "IX_AsignacionesDirigentes_PartidoPoliticoId",
                table: "AsignacionDirigente",
                newName: "IX_AsignacionDirigente_PartidoPoliticoId");

            migrationBuilder.AlterColumn<bool>(
                name: "Estado",
                table: "AlianzasPoliticas",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "AlianzasPoliticas",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "AlianzasPoliticas",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "PuestoElectivo",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Siglas",
                table: "PartidoPolitico",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "PartidoPolitico",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "NumeroIdentificacion",
                table: "Ciudadano",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Ciudadano",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Apellido",
                table: "Ciudadano",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PuestoElectivo",
                table: "PuestoElectivo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartidoPolitico",
                table: "PartidoPolitico",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ciudadano",
                table: "Ciudadano",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AsignacionDirigente",
                table: "AsignacionDirigente",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionDirigente_UsuarioId",
                table: "AsignacionDirigente",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AlianzaPoliticaPartidoPolitico_PartidosId",
                table: "AlianzaPoliticaPartidoPolitico",
                column: "PartidosId");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionDirigente_PartidoPolitico_PartidoPoliticoId",
                table: "AsignacionDirigente",
                column: "PartidoPoliticoId",
                principalTable: "PartidoPolitico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionDirigente_Usuarios_UsuarioId",
                table: "AsignacionDirigente",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesCandidatos_Candidatos_CandidatoId",
                table: "AsignacionesCandidatos",
                column: "CandidatoId",
                principalTable: "Candidatos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesCandidatos_Elecciones_EleccionId",
                table: "AsignacionesCandidatos",
                column: "EleccionId",
                principalTable: "Elecciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesCandidatos_PuestoElectivo_PuestoElectivoId",
                table: "AsignacionesCandidatos",
                column: "PuestoElectivoId",
                principalTable: "PuestoElectivo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidatos_PartidoPolitico_PartidoPoliticoId",
                table: "Candidatos",
                column: "PartidoPoliticoId",
                principalTable: "PartidoPolitico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CodigosVerificacion_Ciudadano_CiudadanoId",
                table: "CodigosVerificacion",
                column: "CiudadanoId",
                principalTable: "Ciudadano",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CodigosVerificacion_Ciudadano_CiudadanoId1",
                table: "CodigosVerificacion",
                column: "CiudadanoId1",
                principalTable: "Ciudadano",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CodigosVerificacion_Elecciones_EleccionId",
                table: "CodigosVerificacion",
                column: "EleccionId",
                principalTable: "Elecciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EleccionPuestoElectivo_PuestoElectivo_PuestoElectivoId",
                table: "EleccionPuestoElectivo",
                column: "PuestoElectivoId",
                principalTable: "PuestoElectivo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EleccionPuestoElectivo_PuestoElectivo_PuestoElectivoId1",
                table: "EleccionPuestoElectivo",
                column: "PuestoElectivoId1",
                principalTable: "PuestoElectivo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VotoDetalles_PuestoElectivo_PuestoElectivoId",
                table: "VotoDetalles",
                column: "PuestoElectivoId",
                principalTable: "PuestoElectivo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votos_Candidatos_CandidatoId",
                table: "Votos",
                column: "CandidatoId",
                principalTable: "Candidatos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Votos_Ciudadano_CiudadanoId",
                table: "Votos",
                column: "CiudadanoId",
                principalTable: "Ciudadano",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
