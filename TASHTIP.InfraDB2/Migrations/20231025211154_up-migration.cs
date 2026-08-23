using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TASHTIP.InfraDB.Migrations
{
    /// <inheritdoc />
    public partial class upmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BussinessGallary",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServicesID = table.Column<int>(type: "int", nullable: true),
                    CityID = table.Column<int>(type: "int", nullable: true),
                    EngineerID = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vendor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BussinessDate = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DetailsUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InteriorDesign = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinishingQuality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkVideo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BussinessGallary", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BussinessGallary");
        }
    }
}
