using Microsoft.EntityFrameworkCore.Migrations;

namespace testingGithub.Migrations
{
    public partial class databaset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGalery_Books_bookId",
                table: "BookGalery");

            migrationBuilder.RenameColumn(
                name: "bookId",
                table: "BookGalery",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BookGalery_bookId",
                table: "BookGalery",
                newName: "IX_BookGalery_BookId");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "BookGalery",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGalery_Books_BookId",
                table: "BookGalery",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGalery_Books_BookId",
                table: "BookGalery");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BookGalery",
                newName: "bookId");

            migrationBuilder.RenameIndex(
                name: "IX_BookGalery_BookId",
                table: "BookGalery",
                newName: "IX_BookGalery_bookId");

            migrationBuilder.AlterColumn<int>(
                name: "bookId",
                table: "BookGalery",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_BookGalery_Books_bookId",
                table: "BookGalery",
                column: "bookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
