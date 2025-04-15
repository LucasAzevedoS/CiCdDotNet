using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoColeta.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Cpf = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    NumeroCasa = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "Coletores",
                columns: table => new
                {
                    IdColetor = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NomeColetor = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coletores", x => x.IdColetor);
                });

            migrationBuilder.CreateTable(
                name: "Residuos",
                columns: table => new
                {
                    IdResiduo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Peso = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    EhReciclavel = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    IdCliente = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ClienteModelIdCliente = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residuos", x => x.IdResiduo);
                    table.ForeignKey(
                        name: "FK_Residuos_Clientes_ClienteModelIdCliente",
                        column: x => x.ClienteModelIdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK_Residuos_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pontos",
                columns: table => new
                {
                    IdPonto = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Local = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CapacidadeMaxima = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Status = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    IdColetor = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ColetorModelIdColetor = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pontos", x => x.IdPonto);
                    table.ForeignKey(
                        name: "FK_Pontos_Coletores_ColetorModelIdColetor",
                        column: x => x.ColetorModelIdColetor,
                        principalTable: "Coletores",
                        principalColumn: "IdColetor");
                    table.ForeignKey(
                        name: "FK_Pontos_Coletores_IdColetor",
                        column: x => x.IdColetor,
                        principalTable: "Coletores",
                        principalColumn: "IdColetor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coletas",
                columns: table => new
                {
                    IdColeta = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DataColeta = table.Column<DateTime>(type: "date", nullable: false),
                    IdPonto = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coletas", x => x.IdColeta);
                    table.ForeignKey(
                        name: "FK_Coletas_Pontos_IdPonto",
                        column: x => x.IdPonto,
                        principalTable: "Pontos",
                        principalColumn: "IdPonto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Cpf",
                table: "Clientes",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coletas_IdPonto",
                table: "Coletas",
                column: "IdPonto");

            migrationBuilder.CreateIndex(
                name: "IX_Pontos_ColetorModelIdColetor",
                table: "Pontos",
                column: "ColetorModelIdColetor");

            migrationBuilder.CreateIndex(
                name: "IX_Pontos_IdColetor",
                table: "Pontos",
                column: "IdColetor");

            migrationBuilder.CreateIndex(
                name: "IX_Residuos_ClienteModelIdCliente",
                table: "Residuos",
                column: "ClienteModelIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Residuos_IdCliente",
                table: "Residuos",
                column: "IdCliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coletas");

            migrationBuilder.DropTable(
                name: "Residuos");

            migrationBuilder.DropTable(
                name: "Pontos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Coletores");
        }
    }
}
