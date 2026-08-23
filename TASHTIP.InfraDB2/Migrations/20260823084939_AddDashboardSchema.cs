using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TASHTIP.InfraDB.Migrations
{
    /// <inheritdoc />
    public partial class AddDashboardSchema : Migration
    {
        /// <inheritdoc />
        // NOTE: this project's tracked migrations drifted from the live database over
        // time (several schema changes - PurchaseRequest.Email/Status, dropping
        // PersonalID - were applied directly against the DB, outside any committed
        // migration). Those columns already exist live, so the operations EF scaffolded
        // for them here were removed; this migration only contains what's actually new.
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PurchaseRequest",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model3DPath",
                table: "BussinessGallary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Panorama360Path",
                table: "BussinessGallary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    EngineerName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PreferredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeSlot = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BussinessGallaryImage",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BussinessGallaryID = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BussinessGallaryImage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BussinessGallaryImage_BussinessGallary_BussinessGallaryID",
                        column: x => x.BussinessGallaryID,
                        principalTable: "BussinessGallary",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RequestStatusHistory",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseRequestId = table.Column<int>(type: "int", nullable: false),
                    OldStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NewStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ChangedByUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ChangedByName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatusHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RequestStatusHistory_PurchaseRequest_PurchaseRequestId",
                        column: x => x.PurchaseRequestId,
                        principalTable: "PurchaseRequest",
                        principalColumn: "ID_PR",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BussinessGallaryID = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Review_BussinessGallary_BussinessGallaryID",
                        column: x => x.BussinessGallaryID,
                        principalTable: "BussinessGallary",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BussinessGallaryImage_BussinessGallaryID",
                table: "BussinessGallaryImage",
                column: "BussinessGallaryID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusHistory_PurchaseRequestId",
                table: "RequestStatusHistory",
                column: "PurchaseRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_BussinessGallaryID",
                table: "Review",
                column: "BussinessGallaryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "BussinessGallaryImage");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "RequestStatusHistory");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PurchaseRequest");

            migrationBuilder.DropColumn(
                name: "Model3DPath",
                table: "BussinessGallary");

            migrationBuilder.DropColumn(
                name: "Panorama360Path",
                table: "BussinessGallary");
        }
    }
}
