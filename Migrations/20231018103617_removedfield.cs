using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediPortal_Payments.Migrations
{
    /// <inheritdoc />
    public partial class removedfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrescriptionId",
                table: "Payments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PrescriptionId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
