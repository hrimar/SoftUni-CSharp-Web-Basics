using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleMvc.Data.Migrations
{
    public partial class Baseredact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kittens_Users_OwnerId",
                table: "Kittens");

            migrationBuilder.DropIndex(
                name: "IX_Kittens_OwnerId",
                table: "Kittens");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Kittens");

            migrationBuilder.DropColumn(
                name: "AmericanShorthair",
                table: "Breeds");

            migrationBuilder.DropColumn(
                name: "Munchkin",
                table: "Breeds");

            migrationBuilder.DropColumn(
                name: "Siamese",
                table: "Breeds");

            migrationBuilder.DropColumn(
                name: "StreetTranscended",
                table: "Breeds");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Kittens",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Breeds",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Breeds");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Kittens",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Kittens",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AmericanShorthair",
                table: "Breeds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Munchkin",
                table: "Breeds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Siamese",
                table: "Breeds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetTranscended",
                table: "Breeds",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kittens_OwnerId",
                table: "Kittens",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kittens_Users_OwnerId",
                table: "Kittens",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
