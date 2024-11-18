using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodBank.API.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModificarEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnderecoCompleto",
                table: "Doadores",
                newName: "Logradouro");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Estoques",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Estoques",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Doadores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "Doadores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Doadores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Doadores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Doadores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Doadores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Doadores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Doacoes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Doacoes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Estoques");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Estoques");

            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Doadores");

            migrationBuilder.DropColumn(
                name: "CEP",
                table: "Doadores");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Doadores");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Doadores");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Doadores");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Doadores");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Doadores");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Doacoes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Doacoes");

            migrationBuilder.RenameColumn(
                name: "Logradouro",
                table: "Doadores",
                newName: "EnderecoCompleto");
        }
    }
}
