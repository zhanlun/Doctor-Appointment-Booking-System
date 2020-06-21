using Microsoft.EntityFrameworkCore.Migrations;

namespace NgDoctorBookingSystem.Migrations
{
    public partial class AddUniqueIC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ICNo",
                table: "Patients",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ICNo",
                table: "Doctors",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ICNo",
                table: "Patients",
                column: "ICNo",
                unique: true,
                filter: "[ICNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_ICNo",
                table: "Doctors",
                column: "ICNo",
                unique: true,
                filter: "[ICNo] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_ICNo",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_ICNo",
                table: "Doctors");

            migrationBuilder.AlterColumn<string>(
                name: "ICNo",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ICNo",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
