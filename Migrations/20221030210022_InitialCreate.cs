using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebEFMVCDemo.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kontingents",
                columns: table => new
                {
                    KontintId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontingents", x => x.KontintId);
                });

            migrationBuilder.CreateTable(
                name: "Medlems",
                columns: table => new
                {
                    Medlem_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Fornavn = table.Column<string>(type: "text", nullable: true),
                    Etternavn = table.Column<string>(type: "text", nullable: true),
                    Bosted = table.Column<string>(type: "text", nullable: true),
                    MobilTlf = table.Column<int>(type: "integer", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Fodselsdato = table.Column<DateTime>(type: "date", nullable: false),
                    KontintId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medlems", x => x.Medlem_Id);
                    table.ForeignKey(
                        name: "FK_Medlems_Kontingents_KontintId",
                        column: x => x.KontintId,
                        principalTable: "Kontingents",
                        principalColumn: "KontintId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Kontingents",
                columns: new[] { "KontintId", "Name" },
                values: new object[,]
                {
                    { 1, "betalt" },
                    { 2, "ikke betalt" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medlems_KontintId",
                table: "Medlems",
                column: "KontintId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medlems");

            migrationBuilder.DropTable(
                name: "Kontingents");
        }
    }
}
