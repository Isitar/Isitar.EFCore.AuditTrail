using Microsoft.EntityFrameworkCore.Migrations;

namespace Isitar.AuditTrail.Lib.DataAccess.Migrations
{
    public partial class RelationDel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PLCs_SwitchCabinets_SwitchCabinetId",
                table: "PLCs");

            migrationBuilder.AddForeignKey(
                name: "FK_PLCs_SwitchCabinets_SwitchCabinetId",
                table: "PLCs",
                column: "SwitchCabinetId",
                principalTable: "SwitchCabinets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PLCs_SwitchCabinets_SwitchCabinetId",
                table: "PLCs");

            migrationBuilder.AddForeignKey(
                name: "FK_PLCs_SwitchCabinets_SwitchCabinetId",
                table: "PLCs",
                column: "SwitchCabinetId",
                principalTable: "SwitchCabinets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
