using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transcend.PL.Migrations
{
    public partial class UpdateOrderModelToIncludeStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserPlaceId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserPlaceId",
                table: "Orders",
                column: "UserPlaceId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserPlaceId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserPlaceId",
                table: "Orders",
                column: "UserPlaceId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
