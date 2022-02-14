using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace testingGithub.Migrations
{
    public partial class userbook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "SignUpUserModel",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Useridbook",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ForgetPasswordddModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: false),
                    EmailSent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForgetPasswordddModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForgetPasswordModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentPassword = table.Column<string>(nullable: false),
                    NewPassword = table.Column<string>(nullable: false),
                    ConfirmNewPassword = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForgetPasswordModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    RememberMe = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginModel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ResetPasswordModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Userid = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    NewPassword = table.Column<string>(nullable: false),
                    ConfirmNewPassword = table.Column<string>(nullable: false),
                    IsSuccess = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetPasswordModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForgetPasswordddModel");

            migrationBuilder.DropTable(
                name: "ForgetPasswordModel");

            migrationBuilder.DropTable(
                name: "LoginModel");

            migrationBuilder.DropTable(
                name: "ResetPasswordModel");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "SignUpUserModel");

            migrationBuilder.DropColumn(
                name: "Useridbook",
                table: "Books");
        }
    }
}
