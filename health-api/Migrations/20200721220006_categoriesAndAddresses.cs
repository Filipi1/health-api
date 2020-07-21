using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace health_api.Migrations
{
    public partial class categoriesAndAddresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Exams",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_UserId",
                table: "Exams");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "BornDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cellphone",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Idade",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Sexo",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "addressId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "Exams",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Exams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Exams",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exams",
                table: "Exams",
                columns: new[] { "UserId", "ColaboratorId" });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Street = table.Column<string>(nullable: true),
                    CEP = table.Column<int>(nullable: false),
                    Complement = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    UF = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_addressId",
                table: "Users",
                column: "addressId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CategoryId",
                table: "Exams",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Categories_CategoryId",
                table: "Exams",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Addresses_addressId",
                table: "Users",
                column: "addressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Categories_CategoryId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Addresses_addressId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Users_addressId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exams",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_CategoryId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BornDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Cellphone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Idade",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "addressId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Exams");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exams",
                table: "Exams",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_UserId",
                table: "Exams",
                column: "UserId");
        }
    }
}
