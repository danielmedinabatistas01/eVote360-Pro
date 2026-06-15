using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eVote360Pro.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ActMdeloVoos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaVoto",
                table: "Votos",
                newName: "FechaVotacion");

            migrationBuilder.AddColumn<int>(
                name: "EleccionId",
                table: "CodigosVerificacion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaGeneracion",
                table: "CodigosVerificacion",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_CodigosVerificacion_EleccionId",
                table: "CodigosVerificacion",
                column: "EleccionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CodigosVerificacion_Elecciones_EleccionId",
                table: "CodigosVerificacion",
                column: "EleccionId",
                principalTable: "Elecciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodigosVerificacion_Elecciones_EleccionId",
                table: "CodigosVerificacion");

            migrationBuilder.DropIndex(
                name: "IX_CodigosVerificacion_EleccionId",
                table: "CodigosVerificacion");

            migrationBuilder.DropColumn(
                name: "EleccionId",
                table: "CodigosVerificacion");

            migrationBuilder.DropColumn(
                name: "FechaGeneracion",
                table: "CodigosVerificacion");

            migrationBuilder.RenameColumn(
                name: "FechaVotacion",
                table: "Votos",
                newName: "FechaVoto");
        }
    }
}
