using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Duckie.Data.Migrations
{
    /// <inheritdoc />
    public partial class StarterTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChildProfile",
                columns: table => new
                {
                    ChildProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildProfile", x => x.ChildProfileId);
                });

            migrationBuilder.CreateTable(
                name: "Milestone",
                columns: table => new
                {
                    MilestoneID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MilestoneName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MilestoneDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChildProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milestone", x => x.MilestoneID);
                    table.ForeignKey(
                        name: "FK_Milestone_ChildProfile_ChildProfileId",
                        column: x => x.ChildProfileId,
                        principalTable: "ChildProfile",
                        principalColumn: "ChildProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Milestone_ChildProfileId",
                table: "Milestone",
                column: "ChildProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Milestone");

            migrationBuilder.DropTable(
                name: "ChildProfile");
        }
    }
}
