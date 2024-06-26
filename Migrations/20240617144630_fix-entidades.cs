using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pj_banco_quest.Migrations
{
    /// <inheritdoc />
    public partial class fixentidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataExpiracao",
                table: "Simulados",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Questoes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataExpiracao",
                table: "Simulados");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Questoes");
        }
    }
}
