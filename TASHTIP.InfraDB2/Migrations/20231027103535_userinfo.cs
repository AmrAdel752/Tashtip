using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TASHTIP.InfraDB.Migrations
{
    /// <inheritdoc />
    public partial class userinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityID",
                table: "BussinessGallary");

            migrationBuilder.DropColumn(
                name: "EngineerID",
                table: "BussinessGallary");

            migrationBuilder.DropColumn(
                name: "ServicesID",
                table: "BussinessGallary");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "BussinessGallary",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DateTimeAction",
                table: "BussinessGallary",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceNameAction",
                table: "BussinessGallary",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Engineer",
                table: "BussinessGallary",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Filter",
                table: "BussinessGallary",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServicesName",
                table: "BussinessGallary",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserAction",
                table: "BussinessGallary",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FilterGallary",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilterName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Section = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterGallary", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilterGallary");

            migrationBuilder.DropColumn(
                name: "City",
                table: "BussinessGallary");

            migrationBuilder.DropColumn(
                name: "DateTimeAction",
                table: "BussinessGallary");

            migrationBuilder.DropColumn(
                name: "DeviceNameAction",
                table: "BussinessGallary");

            migrationBuilder.DropColumn(
                name: "Engineer",
                table: "BussinessGallary");

            migrationBuilder.DropColumn(
                name: "Filter",
                table: "BussinessGallary");

            migrationBuilder.DropColumn(
                name: "ServicesName",
                table: "BussinessGallary");

            migrationBuilder.DropColumn(
                name: "UserAction",
                table: "BussinessGallary");

            migrationBuilder.AddColumn<int>(
                name: "CityID",
                table: "BussinessGallary",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EngineerID",
                table: "BussinessGallary",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServicesID",
                table: "BussinessGallary",
                type: "int",
                nullable: true);
        }
    }
}
