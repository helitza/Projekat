using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kompanije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kompanije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sifra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    admin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tribine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sektor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    datumPocetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    datumKraja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kotizacija = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tribine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mesta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrRedova = table.Column<int>(type: "int", nullable: false),
                    BrSedistaPoRedu = table.Column<int>(type: "int", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KompanijaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mesta_Kompanije_KompanijaId",
                        column: x => x.KompanijaId,
                        principalTable: "Kompanije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KopmanijeTribine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KompanijaId = table.Column<int>(type: "int", nullable: true),
                    TribinaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KopmanijeTribine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KopmanijeTribine_Kompanije_KompanijaId",
                        column: x => x.KompanijaId,
                        principalTable: "Kompanije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KopmanijeTribine_Tribine_TribinaId",
                        column: x => x.TribinaId,
                        principalTable: "Tribine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Odrzavanja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vreme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    mestoId = table.Column<int>(type: "int", nullable: true),
                    TribinaId = table.Column<int>(type: "int", nullable: true),
                    KompanijaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odrzavanja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Odrzavanja_Kompanije_KompanijaId",
                        column: x => x.KompanijaId,
                        principalTable: "Kompanije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Odrzavanja_Mesta_mestoId",
                        column: x => x.mestoId,
                        principalTable: "Mesta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Odrzavanja_Tribine_TribinaId",
                        column: x => x.TribinaId,
                        principalTable: "Tribine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sedista",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mestoId = table.Column<int>(type: "int", nullable: true),
                    BrReda = table.Column<int>(type: "int", nullable: false),
                    BrSedistaURedu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sedista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sedista_Mesta_mestoId",
                        column: x => x.mestoId,
                        principalTable: "Mesta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Akreditacije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sedisteId = table.Column<int>(type: "int", nullable: true),
                    korisnikId = table.Column<int>(type: "int", nullable: true),
                    OdrzavanjeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Akreditacije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Akreditacije_Korisnici_korisnikId",
                        column: x => x.korisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Akreditacije_Odrzavanja_OdrzavanjeId",
                        column: x => x.OdrzavanjeId,
                        principalTable: "Odrzavanja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Akreditacije_Sedista_sedisteId",
                        column: x => x.sedisteId,
                        principalTable: "Sedista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Akreditacije_korisnikId",
                table: "Akreditacije",
                column: "korisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Akreditacije_OdrzavanjeId",
                table: "Akreditacije",
                column: "OdrzavanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_Akreditacije_sedisteId",
                table: "Akreditacije",
                column: "sedisteId");

            migrationBuilder.CreateIndex(
                name: "IX_KopmanijeTribine_KompanijaId",
                table: "KopmanijeTribine",
                column: "KompanijaId");

            migrationBuilder.CreateIndex(
                name: "IX_KopmanijeTribine_TribinaId",
                table: "KopmanijeTribine",
                column: "TribinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesta_KompanijaId",
                table: "Mesta",
                column: "KompanijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Odrzavanja_KompanijaId",
                table: "Odrzavanja",
                column: "KompanijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Odrzavanja_mestoId",
                table: "Odrzavanja",
                column: "mestoId");

            migrationBuilder.CreateIndex(
                name: "IX_Odrzavanja_TribinaId",
                table: "Odrzavanja",
                column: "TribinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Sedista_mestoId",
                table: "Sedista",
                column: "mestoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Akreditacije");

            migrationBuilder.DropTable(
                name: "KopmanijeTribine");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Odrzavanja");

            migrationBuilder.DropTable(
                name: "Sedista");

            migrationBuilder.DropTable(
                name: "Tribine");

            migrationBuilder.DropTable(
                name: "Mesta");

            migrationBuilder.DropTable(
                name: "Kompanije");
        }
    }
}
