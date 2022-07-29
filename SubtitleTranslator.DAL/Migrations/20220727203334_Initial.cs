using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubtitleTranslator.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    TranslationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sourceLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    destinationLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Licence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.TranslationId);
                });

            migrationBuilder.CreateTable(
                name: "SrtLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lineNumber = table.Column<int>(type: "int", nullable: true),
                    timeStamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    translatedText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranslationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SrtLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SrtLines_Translations_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translations",
                        principalColumn: "TranslationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SrtLines_TranslationId",
                table: "SrtLines",
                column: "TranslationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SrtLines");

            migrationBuilder.DropTable(
                name: "Translations");
        }
    }
}
