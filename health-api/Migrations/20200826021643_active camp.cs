using Microsoft.EntityFrameworkCore.Migrations;

namespace health_api.Migrations
{
    public partial class activecamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Users_UserId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_UserId",
                table: "Exams");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Exams",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_ColaboratorId",
                table: "Exams",
                column: "ColaboratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Users_ColaboratorId",
                table: "Exams",
                column: "ColaboratorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Users_ColaboratorId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_ColaboratorId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Exams");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Exams_UserId",
                table: "Exams",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Users_UserId",
                table: "Exams",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
