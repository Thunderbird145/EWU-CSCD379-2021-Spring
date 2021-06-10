using Microsoft.EntityFrameworkCore.Migrations;

namespace SecretSanta.Data.Migrations
{
    public partial class refresh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_Users_OwnerId",
                table: "Gifts");

            migrationBuilder.DropIndex(
                name: "IX_Gifts_OwnerId",
                table: "Gifts");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Gifts",
                newName: "OwnerID");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerID",
                table: "Gifts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Gifts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_UserId",
                table: "Gifts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_Users_UserId",
                table: "Gifts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_Users_UserId",
                table: "Gifts");

            migrationBuilder.DropIndex(
                name: "IX_Gifts_UserId",
                table: "Gifts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Gifts");

            migrationBuilder.RenameColumn(
                name: "OwnerID",
                table: "Gifts",
                newName: "OwnerId");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Gifts",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_OwnerId",
                table: "Gifts",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_Users_OwnerId",
                table: "Gifts",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
