using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TASHTIP.InfraDB.Migrations
{
    /// <inheritdoc />
    public partial class PR_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Rating",
                table: "PurchaseRequest",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateTimeAction",
                table: "PurchaseRequest",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceNameAction",
                table: "PurchaseRequest",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAction",
                table: "PurchaseRequest",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeAction",
                table: "PurchaseRequest");

            migrationBuilder.DropColumn(
                name: "DeviceNameAction",
                table: "PurchaseRequest");

            migrationBuilder.DropColumn(
                name: "UserAction",
                table: "PurchaseRequest");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "PurchaseRequest",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
