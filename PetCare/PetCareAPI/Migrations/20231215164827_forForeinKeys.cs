using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCareAPI.Migrations
{
    /// <inheritdoc />
    public partial class forForeinKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Pet_PetId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_Pet_PetId",
                table: "Food");

            migrationBuilder.AlterColumn<int>(
                name: "PetId",
                table: "Food",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PetId",
                table: "Activity",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Pet_PetId",
                table: "Activity",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "PetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Pet_PetId",
                table: "Food",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "PetId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Pet_PetId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_Pet_PetId",
                table: "Food");

            migrationBuilder.AlterColumn<int>(
                name: "PetId",
                table: "Food",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PetId",
                table: "Activity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Pet_PetId",
                table: "Activity",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Pet_PetId",
                table: "Food",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "PetId");
        }
    }
}
