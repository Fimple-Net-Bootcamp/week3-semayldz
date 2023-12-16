using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareAPI.Migrations
{
    /// <inheritdoc />
    public partial class forNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HealthStatusId",
                table: "Pet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Pet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HealthStatus",
                columns: table => new
                {
                    HealthStatusId = table.Column<int>(type: "int", nullable: false),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthStatus", x => x.HealthStatusId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pet_HealthStatusId",
                table: "Pet",
                column: "HealthStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_UserId",
                table: "Pet",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_HealthStatus_HealthStatusId",
                table: "Pet",
                column: "HealthStatusId",
                principalTable: "HealthStatus",
                principalColumn: "HealthStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_User_UserId",
                table: "Pet",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pet_HealthStatus_HealthStatusId",
                table: "Pet");

            migrationBuilder.DropForeignKey(
                name: "FK_Pet_User_UserId",
                table: "Pet");

            migrationBuilder.DropTable(
                name: "HealthStatus");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Pet_HealthStatusId",
                table: "Pet");

            migrationBuilder.DropIndex(
                name: "IX_Pet_UserId",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "HealthStatusId",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pet");
        }
    }
}
