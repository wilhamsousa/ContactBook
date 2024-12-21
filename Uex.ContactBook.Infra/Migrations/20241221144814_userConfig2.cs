using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uex.ContactBook.Infra.Migrations
{
    /// <inheritdoc />
    public partial class userConfig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "User",
                newName: "Cpf");

            migrationBuilder.RenameColumn(
                name: "CEP",
                table: "User",
                newName: "Cep");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cpf",
                table: "User",
                newName: "CPF");

            migrationBuilder.RenameColumn(
                name: "Cep",
                table: "User",
                newName: "CEP");
        }
    }
}
