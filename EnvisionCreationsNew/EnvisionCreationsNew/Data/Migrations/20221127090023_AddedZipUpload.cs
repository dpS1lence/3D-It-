using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnvisionCreationsNew.Data.Migrations
{
    public partial class AddedZipUpload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Content",
                newName: "PhotosZip");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotosZip",
                table: "Content",
                newName: "Photo");
        }
    }
}
