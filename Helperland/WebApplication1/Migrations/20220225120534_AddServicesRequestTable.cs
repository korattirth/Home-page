using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class AddServicesRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServicesRequests",
                columns: table => new
                {
                    ServiceRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    ServiceStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Zipcode = table.Column<int>(type: "int", nullable: false),
                    ServiceFrequency = table.Column<int>(type: "int", nullable: false),
                    ServiceHourlyrate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServicesHours = table.Column<float>(type: "real", maxLength: 5, nullable: false),
                    ExtraHours = table.Column<float>(type: "real", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", maxLength: 5, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", maxLength: 5, nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", maxLength: 5, nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PaymentTransactionrefNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentDue = table.Column<int>(type: "int", nullable: false),
                    JobStatus = table.Column<bool>(type: "bit", nullable: false),
                    ServicesProviderId = table.Column<int>(type: "int", nullable: false),
                    SPAcceptedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasPets = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CraetedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefundedAmount = table.Column<decimal>(type: "decimal(18,2)", maxLength: 5, nullable: false),
                    Haslssue = table.Column<int>(type: "int", nullable: false),
                    PaymentDone = table.Column<int>(type: "int", nullable: false),
                    uniqueidentifier = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesRequests", x => x.ServiceRequestId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServicesRequests");
        }
    }
}
