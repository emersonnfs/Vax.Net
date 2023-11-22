using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vax.Migrations
{
    /// <inheritdoc />
    public partial class ThirdCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Enderecos_EnderecoId",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Telefones_TelefoneId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_EnderecoId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_TelefoneId",
                table: "Usuarios");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Telefones",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Enderecos",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Telefones_UsuarioId",
                table: "Telefones",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_UsuarioId",
                table: "Enderecos",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Usuarios_UsuarioId",
                table: "Enderecos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Telefones_Usuarios_UsuarioId",
                table: "Telefones",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Usuarios_UsuarioId",
                table: "Enderecos");

            migrationBuilder.DropForeignKey(
                name: "FK_Telefones_Usuarios_UsuarioId",
                table: "Telefones");

            migrationBuilder.DropIndex(
                name: "IX_Telefones_UsuarioId",
                table: "Telefones");

            migrationBuilder.DropIndex(
                name: "IX_Enderecos_UsuarioId",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Telefones");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Enderecos");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EnderecoId",
                table: "Usuarios",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_TelefoneId",
                table: "Usuarios",
                column: "TelefoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Enderecos_EnderecoId",
                table: "Usuarios",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Telefones_TelefoneId",
                table: "Usuarios",
                column: "TelefoneId",
                principalTable: "Telefones",
                principalColumn: "Id");
        }
    }
}
