using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdemyCarBook.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveShadowProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentACars_Cars_CarID1",
                table: "RentACars");

            migrationBuilder.DropForeignKey(
                name: "FK_RentACars_Cars_CarID2",
                table: "RentACars");

            migrationBuilder.DropIndex(
                name: "IX_RentACars_CarID1",
                table: "RentACars");

            migrationBuilder.DropIndex(
                name: "IX_RentACars_CarID2",
                table: "RentACars");

            migrationBuilder.DropColumn(
                name: "CarID1",
                table: "RentACars");

            migrationBuilder.DropColumn(
                name: "CarID2",
                table: "RentACars");

            migrationBuilder.RenameColumn(
                name: "KM",
                table: "Cars",
                newName: "Km");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerMail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "RentACarProcess",
                columns: table => new
                {
                    RentACarProcessID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarID = table.Column<int>(type: "int", nullable: false),
                    PickUpLocation = table.Column<int>(type: "int", nullable: false),
                    DropOffLocation = table.Column<int>(type: "int", nullable: false),
                    PickUpDate = table.Column<DateTime>(type: "Date", nullable: false),
                    DropOffDate = table.Column<DateTime>(type: "Date", nullable: false),
                    PickUpTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DropOffTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    PickUpDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DropOffDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentACarProcess", x => x.RentACarProcessID);
                    table.ForeignKey(
                        name: "FK_RentACarProcess_Cars_CarID",
                        column: x => x.CarID,
                        principalTable: "Cars",
                        principalColumn: "CarID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentACarProcess_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentACarProcess_CarID",
                table: "RentACarProcess",
                column: "CarID");

            migrationBuilder.CreateIndex(
                name: "IX_RentACarProcess_CustomerID",
                table: "RentACarProcess",
                column: "CustomerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentACarProcess");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.RenameColumn(
                name: "Km",
                table: "Cars",
                newName: "KM");

            migrationBuilder.AddColumn<int>(
                name: "CarID1",
                table: "RentACars",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarID2",
                table: "RentACars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentACars_CarID1",
                table: "RentACars",
                column: "CarID1");

            migrationBuilder.CreateIndex(
                name: "IX_RentACars_CarID2",
                table: "RentACars",
                column: "CarID2");

            migrationBuilder.AddForeignKey(
                name: "FK_RentACars_Cars_CarID1",
                table: "RentACars",
                column: "CarID1",
                principalTable: "Cars",
                principalColumn: "CarID");

            migrationBuilder.AddForeignKey(
                name: "FK_RentACars_Cars_CarID2",
                table: "RentACars",
                column: "CarID2",
                principalTable: "Cars",
                principalColumn: "CarID");
        }
    }
}
