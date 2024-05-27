using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace net_il_mio_fotoalbum.Migrations
{
    public partial class UpdatePhotoCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foto_Category_CategoryId",
                table: "Foto");

            migrationBuilder.DropIndex(
                name: "IX_Foto_CategoryId",
                table: "Foto");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Foto");

            migrationBuilder.CreateTable(
                name: "CategoryPhoto",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    PhotosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPhoto", x => new { x.CategoriesId, x.PhotosId });
                    table.ForeignKey(
                        name: "FK_CategoryPhoto_Category_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryPhoto_Foto_PhotosId",
                        column: x => x.PhotosId,
                        principalTable: "Foto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPhoto_PhotosId",
                table: "CategoryPhoto",
                column: "PhotosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryPhoto");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Foto",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foto_CategoryId",
                table: "Foto",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foto_Category_CategoryId",
                table: "Foto",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
