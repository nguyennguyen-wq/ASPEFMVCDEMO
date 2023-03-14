using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebMVCDemo.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kontingent",
                columns: table => new
                {
                    KontintId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontingent", x => x.KontintId);
                });

            migrationBuilder.CreateTable(
                name: "Medlem",
                columns: table => new
                {
                    Medlem_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Fornavn = table.Column<string>(type: "text", nullable: false),
                    Etternavn = table.Column<string>(type: "text", nullable: false),
                    Bosted = table.Column<string>(type: "text", nullable: true),
                    MobilTlf = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Fodselsdato = table.Column<DateTime>(type: "date", nullable: false),
                    CurrentKontintId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medlem", x => x.Medlem_Id);
                    table.ForeignKey(
                        name: "FK_Medlem_Kontingent_CurrentKontintId",
                        column: x => x.CurrentKontintId,
                        principalTable: "Kontingent",
                        principalColumn: "KontintId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medlem_CurrentKontintId",
                table: "Medlem",
                column: "CurrentKontintId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medlem");

            migrationBuilder.DropTable(
                name: "Kontingent");
        }
    }
}
