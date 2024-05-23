using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilkTeaBusinessObject.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Material_Tea_TeaID",
                table: "Material");

            migrationBuilder.DropIndex(
                name: "IX_Material_TeaID",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "TeaID",
                table: "Material");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeaID",
                table: "Material",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Material_TeaID",
                table: "Material",
                column: "TeaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Tea_TeaID",
                table: "Material",
                column: "TeaID",
                principalTable: "Tea",
                principalColumn: "TeaID");
        }
    }
}
