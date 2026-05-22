using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdemyCarBook.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class campaing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CampaignExpiryDate",
                table: "CarPricings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CampaignPrice",
                table: "CarPricings",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCampaign",
                table: "CarPricings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CampaignExpiryDate",
                table: "CarPricings");

            migrationBuilder.DropColumn(
                name: "CampaignPrice",
                table: "CarPricings");

            migrationBuilder.DropColumn(
                name: "IsCampaign",
                table: "CarPricings");
        }
    }
}
