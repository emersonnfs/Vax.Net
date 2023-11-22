using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vax.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Enderecos_EnderecoId",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "EnderecoId",
                table: "Usuarios",
                type: "NUMBER(10)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AddColumn<int>(
                name: "TelefoneId",
                table: "Usuarios",
                type: "NUMBER(10)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Enderecos_EnderecoId",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Telefones_TelefoneId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_TelefoneId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "TelefoneId",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "EnderecoId",
                table: "Usuarios",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Enderecos_EnderecoId",
                table: "Usuarios",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
