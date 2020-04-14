using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Syngenta.Sintegra.Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Street = table.Column<string>(type: "varchar(100)", nullable: true),
                    HouseNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    District = table.Column<string>(type: "varchar(100)", nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(100)", nullable: true),
                    City = table.Column<string>(type: "varchar(100)", nullable: true),
                    Country = table.Column<string>(type: "varchar(100)", nullable: true),
                    Region = table.Column<string>(type: "varchar(100)", nullable: true),
                    CNPJ = table.Column<string>(type: "varchar(100)", nullable: true),
                    CPF = table.Column<string>(type: "varchar(100)", nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestVerification",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    FileName = table.Column<string>(type: "varchar(100)", nullable: true),
                    RequestStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestVerification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RequestId = table.Column<Guid>(nullable: true),
                    CustomerId = table.Column<string>(type: "varchar(100)", nullable: true),
                    CustomerName = table.Column<string>(type: "varchar(100)", nullable: true),
                    CustomerStreet = table.Column<string>(type: "varchar(100)", nullable: true),
                    CustomerHouseNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    CustomerDistrict = table.Column<string>(type: "varchar(100)", nullable: true),
                    CustomerPostalCode = table.Column<string>(type: "varchar(100)", nullable: true),
                    CustomerCity = table.Column<string>(type: "varchar(100)", nullable: true),
                    CustomerCountry = table.Column<string>(type: "varchar(100)", nullable: true),
                    CustomerRegion = table.Column<string>(type: "varchar(100)", nullable: true),
                    CustomerCNPJ = table.Column<string>(type: "varchar(14)", nullable: true),
                    CustomerCPF = table.Column<string>(type: "varchar(11)", nullable: true),
                    CustomerInscricaoEstadual = table.Column<string>(type: "varchar(100)", nullable: true),
                    RequestItemStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestItems_RequestVerification_RequestId",
                        column: x => x.RequestId,
                        principalTable: "RequestVerification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChangeLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RequestItemId = table.Column<Guid>(nullable: false),
                    FieldName = table.Column<string>(type: "varchar(50)", nullable: false),
                    NewValue = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChangeLogs_RequestItems_RequestItemId",
                        column: x => x.RequestItemId,
                        principalTable: "RequestItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLogs_RequestItemId",
                table: "ChangeLogs",
                column: "RequestItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestItems_RequestId",
                table: "RequestItems",
                column: "RequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeLogs");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "RequestItems");

            migrationBuilder.DropTable(
                name: "RequestVerification");
        }
    }
}
