using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Infrastructure.Persistences.Migrations
{
    public partial class InitialMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "SFDAdditionalInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowStatus = table.Column<int>(nullable: true, defaultValue: 0),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Link = table.Column<string>(maxLength: 500, nullable: false),
                    FileCode = table.Column<string>(maxLength: 50, nullable: false),
                    FileType = table.Column<string>(maxLength: 50, nullable: false),
                    ImageThumbnailId = table.Column<int>(nullable: false),
                    ImageCoverId = table.Column<int>(nullable: false),
                    TotalViews = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDAdditionalInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SFDAdditionalInfo_SFDImageGallery_ImageCoverId",
                        column: x => x.ImageCoverId,
                        principalTable: "SFDImageGallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SFDAdditionalInfo_SFDImageGallery_ImageThumbnailId",
                        column: x => x.ImageThumbnailId,
                        principalTable: "SFDImageGallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SFDAdditionalInfo");
        }
    }
}
