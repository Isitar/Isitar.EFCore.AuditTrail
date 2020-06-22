using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

namespace Isitar.AuditTrail.Lib.DataAccess.Migrations
{
    public partial class InitialMigraiton : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SwitchCabinets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SwitchCabinets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditTrailEntry<SwitchCabinet>",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SubjectId = table.Column<Guid>(nullable: false),
                    EntryType = table.Column<int>(nullable: false),
                    FromValue = table.Column<string>(nullable: true),
                    ToValue = table.Column<string>(nullable: true),
                    Timestamp = table.Column<Instant>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrailEntry<SwitchCabinet>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditTrailEntry<SwitchCabinet>_SwitchCabinets_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "SwitchCabinets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PLCs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SwitchCabinetId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLCs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PLCs_SwitchCabinets_SwitchCabinetId",
                        column: x => x.SwitchCabinetId,
                        principalTable: "SwitchCabinets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditTrailEntry<PLC>",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SubjectId = table.Column<Guid>(nullable: false),
                    EntryType = table.Column<int>(nullable: false),
                    FromValue = table.Column<string>(nullable: true),
                    ToValue = table.Column<string>(nullable: true),
                    Timestamp = table.Column<Instant>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrailEntry<PLC>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditTrailEntry<PLC>_PLCs_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "PLCs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditTrailEntry<PLC>_SubjectId",
                table: "AuditTrailEntry<PLC>",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditTrailEntry<SwitchCabinet>_SubjectId",
                table: "AuditTrailEntry<SwitchCabinet>",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PLCs_SwitchCabinetId",
                table: "PLCs",
                column: "SwitchCabinetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTrailEntry<PLC>");

            migrationBuilder.DropTable(
                name: "AuditTrailEntry<SwitchCabinet>");

            migrationBuilder.DropTable(
                name: "PLCs");

            migrationBuilder.DropTable(
                name: "SwitchCabinets");
        }
    }
}
