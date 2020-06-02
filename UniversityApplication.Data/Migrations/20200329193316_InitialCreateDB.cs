using Microsoft.EntityFrameworkCore.Migrations;

namespace BankApplication.Data.Migrations
{
    public partial class InitialCreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(maxLength: 400, nullable: false),
                    City = table.Column<string>(maxLength: 400, nullable: false),
                    Country = table.Column<string>(maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 400, nullable: true),
                    ClientTypeId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 400, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    AccountTypeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "Street" },
                values: new object[,]
                {
                    { 1, "Stockholm", "Sweden", "Killdeer Pass" },
                    { 2, "London", "United Kingdom", "Ridgeway Parkway" },
                    { 3, "New York", "United States", "Southridge Hill" },
                    { 4, "Tokyo", "Japan", "Forest Park" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "AddressId", "Email", "Name", "PhoneNumber", "ClientTypeId" },
                values: new object[,]
                {
                    { 1, 1, "NicolineAbspoel@gmail.com", "Nicoline Abspoel", "077-999-999", 1 },
                    { 2, 2, "akennard@firm.com", "Andrew Kennard", "+38976999999", 1 },
                    { 3, 3, "info@google.com", "Google", "1111111111111", 2 },
                    { 4, 4, "info@microsoft.com", "Microsoft", "32-3231-354", 2 }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Balance", "ClientId", "IsActive", "Name", "AccountTypeId" },
                values: new object[,]
                {
                    { 1, 7900m, 1, true, "Personal Account", 2 },
                    { 2, 1m, 2, false, "MasterCard", 3 },
                    { 3, 5688.40m, 2, true, "MasterCard", 3 },
                    { 4, -55000.40m, 2, true, "Housing Load", 4 },
                    { 5, 240000.00m, 3, true, "Salary Account", 1 },
                    { 6, 500000.70m, 3, true, "Cash Management", 2 },
                    { 7, 200500.50m, 4, true, "Salary Account", 1 },
                    { 8, 433833.23m, 4, true, "Cash Management", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ClientId",
                table: "Accounts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_AddressId",
                table: "Clients",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
