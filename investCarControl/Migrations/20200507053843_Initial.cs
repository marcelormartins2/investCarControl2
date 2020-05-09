using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InvestCarControl.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "despesa",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    descricao = table.Column<string>(type: "varchar(50)", nullable: true),
                    data = table.Column<DateTime>(type: "datetime", nullable: false),
                    valor = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_despesa", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "fabricante",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(20)", nullable: true),
                    site = table.Column<string>(type: "varchar(45)", nullable: true),
                    prioridade = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fabricante", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "parceiro",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(45)", nullable: true),
                    email = table.Column<string>(type: "varchar(45)", nullable: true),
                    telefone = table.Column<string>(type: "varchar(45)", nullable: true),
                    endereço = table.Column<string>(type: "varchar(45)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parceiro", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "modelocar",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(45)", nullable: true),
                    fabricante_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modelocar", x => x.id);
                    table.ForeignKey(
                        name: "fk_modeloCar_fabricante1",
                        column: x => x.fabricante_id,
                        principalTable: "fabricante",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "responsavel",
                columns: table => new
                {
                    despesa_id = table.Column<int>(nullable: false),
                    parceiro_id = table.Column<int>(nullable: false),
                    valor = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_responsavel", x => new { x.despesa_id, x.parceiro_id });
                    table.ForeignKey(
                        name: "fk_despesa_has_parceiro_despesa1",
                        column: x => x.despesa_id,
                        principalTable: "despesa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_despesa_has_parceiro_parceiro1",
                        column: x => x.parceiro_id,
                        principalTable: "parceiro",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "veiculo",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    placa = table.Column<string>(type: "varchar(10)", nullable: true),
                    chassis = table.Column<string>(type: "varchar(20)", nullable: true),
                    cor = table.Column<string>(type: "varchar(15)", nullable: true),
                    dut = table.Column<string>(type: "varchar(20)", nullable: true),
                    hodometro = table.Column<int>(nullable: true),
                    anofab = table.Column<int>(nullable: false),
                    anoModelo = table.Column<int>(nullable: false),
                    origem = table.Column<string>(type: "varchar(20)", nullable: true),
                    renavam = table.Column<int>(nullable: true),
                    valorfipe = table.Column<double>(nullable: true),
                    valorpago = table.Column<double>(nullable: true),
                    valorvenda = table.Column<double>(nullable: true),
                    despesa_id = table.Column<int>(nullable: true),
                    modeloCar_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_veiculo", x => x.id);
                    table.ForeignKey(
                        name: "fk_veiculo_despesa1",
                        column: x => x.despesa_id,
                        principalTable: "despesa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_veiculo_modeloCar1",
                        column: x => x.modeloCar_id,
                        principalTable: "modelocar",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "participacao",
                columns: table => new
                {
                    parceiro_id = table.Column<int>(nullable: false),
                    veiculo_id = table.Column<int>(nullable: false),
                    porcentagemCompra = table.Column<double>(nullable: false),
                    porcentagemLucro = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participacao", x => new { x.parceiro_id, x.veiculo_id });
                    table.ForeignKey(
                        name: "fk_parceiro_has_veiculo_parceiro",
                        column: x => x.parceiro_id,
                        principalTable: "parceiro",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_parceiro_has_veiculo_veiculo1",
                        column: x => x.veiculo_id,
                        principalTable: "veiculo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "fk_modeloCar_fabricante1_idx",
                table: "modelocar",
                column: "fabricante_id");

            migrationBuilder.CreateIndex(
                name: "fk_parceiro_has_veiculo_parceiro_idx",
                table: "participacao",
                column: "parceiro_id");

            migrationBuilder.CreateIndex(
                name: "fk_parceiro_has_veiculo_veiculo1_idx",
                table: "participacao",
                column: "veiculo_id");

            migrationBuilder.CreateIndex(
                name: "fk_despesa_has_parceiro_despesa1_idx",
                table: "responsavel",
                column: "despesa_id");

            migrationBuilder.CreateIndex(
                name: "fk_despesa_has_parceiro_parceiro1_idx",
                table: "responsavel",
                column: "parceiro_id");

            migrationBuilder.CreateIndex(
                name: "fk_veiculo_despesa1_idx",
                table: "veiculo",
                column: "despesa_id");

            migrationBuilder.CreateIndex(
                name: "fk_veiculo_modeloCar1_idx",
                table: "veiculo",
                column: "modeloCar_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "participacao");

            migrationBuilder.DropTable(
                name: "responsavel");

            migrationBuilder.DropTable(
                name: "veiculo");

            migrationBuilder.DropTable(
                name: "parceiro");

            migrationBuilder.DropTable(
                name: "despesa");

            migrationBuilder.DropTable(
                name: "modelocar");

            migrationBuilder.DropTable(
                name: "fabricante");
        }
    }
}
