using Microsoft.EntityFrameworkCore.Migrations;

namespace health_api.Migrations
{
    public partial class nomesd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "url",
                table: "Nomes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "url",
                table: "Nomes");
        }
    }
}
