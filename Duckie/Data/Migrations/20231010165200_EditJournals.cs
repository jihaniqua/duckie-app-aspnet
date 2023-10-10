using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Duckie.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditJournals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Journal",
                columns: table => new
                {
                    JournalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JournalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JournalBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChildProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journal", x => x.JournalId);
                    table.ForeignKey(
                        name: "FK_Journal_ChildProfile_ChildProfileId",
                        column: x => x.ChildProfileId,
                        principalTable: "ChildProfile",
                        principalColumn: "ChildProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Journal_ChildProfileId",
                table: "Journal",
                column: "ChildProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Journal");
        }
    }
}
