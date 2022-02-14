using Microsoft.EntityFrameworkCore.Migrations;

namespace testingGithub.Migrations
{
    public partial class addtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImageUrl",
                table: "BookModel",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookGalery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    booksId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGalery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookGalery_Books_booksId",
                        column: x => x.booksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GalleryModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    BookModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GalleryModel_BookModel_BookModelId",
                        column: x => x.BookModelId,
                        principalTable: "BookModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookGalery_booksId",
                table: "BookGalery",
                column: "booksId");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryModel_BookModelId",
                table: "GalleryModel",
                column: "BookModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookGalery");

            migrationBuilder.DropTable(
                name: "GalleryModel");

            migrationBuilder.DropColumn(
                name: "CoverImageUrl",
                table: "BookModel");
        }
    }
}
