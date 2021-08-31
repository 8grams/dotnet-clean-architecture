using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SFIDWebAPI.Infrastructure.Persistences.Migrations
{
    public partial class InitialMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SFDFaq",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowStatus = table.Column<int>(nullable: true, defaultValue: 0),
                    Question = table.Column<string>(nullable: false),
                    Answer = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDFaq", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SFDImageGallery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowStatus = table.Column<int>(nullable: true, defaultValue: 0),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Link = table.Column<string>(maxLength: 500, nullable: false),
                    Category = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDImageGallery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SFDMasterConfig",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowStatus = table.Column<int>(nullable: true, defaultValue: 0),
                    Category = table.Column<string>(maxLength: 100, nullable: false),
                    ValueId = table.Column<string>(nullable: false),
                    ValueCode = table.Column<string>(nullable: false),
                    ValueDesc = table.Column<string>(nullable: false),
                    Sequence = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDMasterConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SFDMenuType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowStatus = table.Column<int>(nullable: true, defaultValue: 0),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDMenuType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SFDNotification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowStatus = table.Column<int>(nullable: true, defaultValue: 0),
                    OwnerId = table.Column<string>(maxLength: 100, nullable: false),
                    OwnerType = table.Column<string>(maxLength: 100, nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Content = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(maxLength: 500, nullable: true),
                    IsDeletable = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false, defaultValue: "unread")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDNotification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SFDPKTHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateBy = table.Column<string>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    RowStatus = table.Column<int>(nullable: true),
                    Vin = table.Column<string>(maxLength: 50, nullable: true),
                    Image = table.Column<string>(maxLength: 100, nullable: true),
                    Status = table.Column<int>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDPKTHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SFDRecommendation",
                columns: table => new
                {
                    ContentType = table.Column<string>(maxLength: 100, nullable: false),
                    ContentId = table.Column<int>(nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowStatus = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDRecommendation", x => new { x.ContentId, x.ContentType });
                });

            migrationBuilder.CreateTable(
                name: "SFDStaticContent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateBy = table.Column<string>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    RowStatus = table.Column<int>(nullable: true),
                    AppInfo = table.Column<string>(nullable: true),
                    Disclaimer = table.Column<string>(nullable: true),
                    TermCondition = table.Column<string>(nullable: true),
                    PrivacyPolicy = table.Column<string>(nullable: true),
                    Tutorial = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDStaticContent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VWI_SFDMobileSalesman",
                columns: table => new
                {
                    SalesmanCode = table.Column<string>(maxLength: 50, nullable: false),
                    Id = table.Column<short>(nullable: false),
                    DealerCode = table.Column<string>(maxLength: 50, nullable: true),
                    DealerName = table.Column<string>(maxLength: 50, nullable: true),
                    DealerCity = table.Column<string>(maxLength: 50, nullable: true),
                    DealerGroup = table.Column<string>(maxLength: 50, nullable: true),
                    DealerArea = table.Column<string>(maxLength: 50, nullable: true),
                    DealerBranchCode = table.Column<string>(maxLength: 50, nullable: true),
                    DealerBranchName = table.Column<string>(maxLength: 50, nullable: true),
                    SalesmanName = table.Column<string>(maxLength: 50, nullable: true),
                    SalesmanHireDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    JobDescription = table.Column<string>(maxLength: 50, nullable: true),
                    LevelDescription = table.Column<string>(maxLength: 100, nullable: true),
                    SuperiorName = table.Column<string>(maxLength: 50, nullable: true),
                    SuperiorCode = table.Column<string>(maxLength: 50, nullable: true),
                    SalesmanEmail = table.Column<string>(maxLength: 50, nullable: true),
                    SalesmanHandphone = table.Column<string>(maxLength: 50, nullable: true),
                    SalesmanTeamCategory = table.Column<string>(maxLength: 50, nullable: true),
                    SalesmanStatus = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VWI_SFDMobileSalesman", x => x.SalesmanCode);
                });

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

            migrationBuilder.CreateTable(
                name: "SFDBulletin",
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
                    table.PrimaryKey("PK_SFDBulletin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SFDBulletin_SFDImageGallery_ImageCoverId",
                        column: x => x.ImageCoverId,
                        principalTable: "SFDImageGallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SFDBulletin_SFDImageGallery_ImageThumbnailId",
                        column: x => x.ImageThumbnailId,
                        principalTable: "SFDImageGallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SFDGuideCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowStatus = table.Column<int>(nullable: true, defaultValue: 0),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Tag = table.Column<string>(maxLength: 100, nullable: false),
                    ImageThumbnailId = table.Column<int>(nullable: false),
                    ImageCoverId = table.Column<int>(nullable: false),
                    ImageLogoId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDGuideCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SFDGuideCategory_SFDImageGallery_ImageCoverId",
                        column: x => x.ImageCoverId,
                        principalTable: "SFDImageGallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SFDGuideCategory_SFDImageGallery_ImageLogoId",
                        column: x => x.ImageLogoId,
                        principalTable: "SFDImageGallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SFDGuideCategory_SFDImageGallery_ImageThumbnailId",
                        column: x => x.ImageThumbnailId,
                        principalTable: "SFDImageGallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SFDHomeBanner",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowStatus = table.Column<int>(nullable: true, defaultValue: 0),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDHomeBanner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SFDHomeBanner_SFDImageGallery_ImageId",
                        column: x => x.ImageId,
                        principalTable: "SFDImageGallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SFDTrainingCategory",
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
                    Tag = table.Column<string>(maxLength: 100, nullable: false),
                    ImageThumbnailId = table.Column<int>(nullable: false),
                    ImageCoverId = table.Column<int>(nullable: false),
                    ImageLogoId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDTrainingCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SFDTrainingCategory_SFDImageGallery_ImageCoverId",
                        column: x => x.ImageCoverId,
                        principalTable: "SFDImageGallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SFDTrainingCategory_SFDImageGallery_ImageLogoId",
                        column: x => x.ImageLogoId,
                        principalTable: "SFDImageGallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SFDTrainingCategory_SFDImageGallery_ImageThumbnailId",
                        column: x => x.ImageThumbnailId,
                        principalTable: "SFDImageGallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SFDRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowStatus = table.Column<int>(nullable: true, defaultValue: 0),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    SFDMCID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SFDRole_SFDMasterConfig_SFDMCID",
                        column: x => x.SFDMCID,
                        principalTable: "SFDMasterConfig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SFDMenu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowStatus = table.Column<int>(nullable: true, defaultValue: 0),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    SFDMenuTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SFDMenu_SFDMenuType_SFDMenuTypeID",
                        column: x => x.SFDMenuTypeID,
                        principalTable: "SFDMenuType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SFDNotificationStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowStatus = table.Column<int>(nullable: true, defaultValue: 0),
                    NotificationId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    HasRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDNotificationStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SFDNotificationStatus_SFDNotification_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "SFDNotification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SFDUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowStatus = table.Column<int>(nullable: true, defaultValue: 0),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: false),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeviceID = table.Column<string>(nullable: false),
                    MasterConfigId = table.Column<int>(nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime", nullable: false),
                    ProfilePhoto = table.Column<string>(maxLength: 500, nullable: true),
                    LoginThrottle = table.Column<int>(nullable: false, defaultValue: 0),
                    FirebaseToken = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SFDUser_SFDMasterConfig_MasterConfigId",
                        column: x => x.MasterConfigId,
                        principalTable: "SFDMasterConfig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SFDUser_VWI_SFDMobileSalesman_Username",
                        column: x => x.Username,
                        principalTable: "VWI_SFDMobileSalesman",
                        principalColumn: "SalesmanCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SFDGuideMaterial",
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
                    ImageThumbnailId = table.Column<int>(nullable: false),
                    ImageCoverId = table.Column<int>(nullable: false),
                    Link = table.Column<string>(maxLength: 500, nullable: false),
                    FileType = table.Column<string>(maxLength: 50, nullable: true),
                    FileCode = table.Column<string>(maxLength: 50, nullable: true),
                    TotalViews = table.Column<int>(nullable: false),
                    SFDGuideCategoryId = table.Column<int>(nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDGuideMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SFDGuideMaterial_SFDGuideCategory_SFDGuideCategoryId",
                        column: x => x.SFDGuideCategoryId,
                        principalTable: "SFDGuideCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SFDGuideMaterial_SFDImageGallery_ImageCoverId",
                        column: x => x.ImageCoverId,
                        principalTable: "SFDImageGallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SFDGuideMaterial_SFDImageGallery_ImageThumbnailId",
                        column: x => x.ImageThumbnailId,
                        principalTable: "SFDImageGallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SFDTrainingMaterial",
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
                    ImageThumbnailId = table.Column<int>(nullable: false),
                    ImageCoverId = table.Column<int>(nullable: false),
                    Link = table.Column<string>(maxLength: 500, nullable: false),
                    FileType = table.Column<string>(maxLength: 50, nullable: true),
                    FileCode = table.Column<string>(maxLength: 50, nullable: true),
                    TotalViews = table.Column<int>(nullable: false),
                    SFDTrainingCategoryId = table.Column<int>(nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDTrainingMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SFDTrainingMaterial_SFDImageGallery_ImageCoverId",
                        column: x => x.ImageCoverId,
                        principalTable: "SFDImageGallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SFDTrainingMaterial_SFDImageGallery_ImageThumbnailId",
                        column: x => x.ImageThumbnailId,
                        principalTable: "SFDImageGallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SFDTrainingMaterial_SFDTrainingCategory_SFDTrainingCategoryId",
                        column: x => x.SFDTrainingCategoryId,
                        principalTable: "SFDTrainingCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SFDAdmin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowStatus = table.Column<int>(nullable: true, defaultValue: 0),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDAdmin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SFDAdmin_SFDRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SFDRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SFDPermission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowStatus = table.Column<int>(nullable: true, defaultValue: 0),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    SFDRoleID = table.Column<int>(nullable: true),
                    SFDMenuID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SFDPermission_SFDMenu_SFDMenuID",
                        column: x => x.SFDMenuID,
                        principalTable: "SFDMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SFDPermission_SFDRole_SFDRoleID",
                        column: x => x.SFDRoleID,
                        principalTable: "SFDRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SFDAccessToken",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowStatus = table.Column<int>(nullable: true, defaultValue: 0),
                    AuthToken = table.Column<string>(maxLength: 100, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDAccessToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SFDAccessToken_SFDUser_UserId",
                        column: x => x.UserId,
                        principalTable: "SFDUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SFDOTP",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateBy = table.Column<string>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    RowStatus = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    PIN = table.Column<string>(maxLength: 10, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDOTP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SFDOTP_SFDUser_UserId",
                        column: x => x.UserId,
                        principalTable: "SFDUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SFDAdminToken",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RowStatus = table.Column<int>(nullable: true, defaultValue: 0),
                    AuthToken = table.Column<string>(maxLength: 200, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    AdminId = table.Column<int>(nullable: false),
                    ExpiresAt = table.Column<DateTime>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SFDAdminToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SFDAdminToken_SFDAdmin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "SFDAdmin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SFDAccessToken_UserId",
                table: "SFDAccessToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDAdditionalInfo_ImageCoverId",
                table: "SFDAdditionalInfo",
                column: "ImageCoverId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDAdditionalInfo_ImageThumbnailId",
                table: "SFDAdditionalInfo",
                column: "ImageThumbnailId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDAdmin_RoleId",
                table: "SFDAdmin",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDAdminToken_AdminId",
                table: "SFDAdminToken",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDBulletin_ImageCoverId",
                table: "SFDBulletin",
                column: "ImageCoverId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDBulletin_ImageThumbnailId",
                table: "SFDBulletin",
                column: "ImageThumbnailId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDGuideCategory_ImageCoverId",
                table: "SFDGuideCategory",
                column: "ImageCoverId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDGuideCategory_ImageLogoId",
                table: "SFDGuideCategory",
                column: "ImageLogoId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDGuideCategory_ImageThumbnailId",
                table: "SFDGuideCategory",
                column: "ImageThumbnailId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDGuideMaterial_SFDGuideCategoryId",
                table: "SFDGuideMaterial",
                column: "SFDGuideCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDGuideMaterial_ImageCoverId",
                table: "SFDGuideMaterial",
                column: "ImageCoverId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDGuideMaterial_ImageThumbnailId",
                table: "SFDGuideMaterial",
                column: "ImageThumbnailId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDHomeBanner_ImageId",
                table: "SFDHomeBanner",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDMenu_SFDMenuTypeID",
                table: "SFDMenu",
                column: "SFDMenuTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_SFDNotificationStatus_NotificationId",
                table: "SFDNotificationStatus",
                column: "NotificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SFDOTP_UserId",
                table: "SFDOTP",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDPermission_SFDMenuID",
                table: "SFDPermission",
                column: "SFDMenuID");

            migrationBuilder.CreateIndex(
                name: "IX_SFDPermission_SFDRoleID",
                table: "SFDPermission",
                column: "SFDRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_SFDRole_SFDMCID",
                table: "SFDRole",
                column: "SFDMCID");

            migrationBuilder.CreateIndex(
                name: "IX_SFDTrainingCategory_ImageCoverId",
                table: "SFDTrainingCategory",
                column: "ImageCoverId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDTrainingCategory_ImageLogoId",
                table: "SFDTrainingCategory",
                column: "ImageLogoId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDTrainingCategory_ImageThumbnailId",
                table: "SFDTrainingCategory",
                column: "ImageThumbnailId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDTrainingMaterial_ImageCoverId",
                table: "SFDTrainingMaterial",
                column: "ImageCoverId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDTrainingMaterial_ImageThumbnailId",
                table: "SFDTrainingMaterial",
                column: "ImageThumbnailId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDTrainingMaterial_SFDTrainingCategoryId",
                table: "SFDTrainingMaterial",
                column: "SFDTrainingCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDUser_MasterConfigId",
                table: "SFDUser",
                column: "MasterConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_SFDUser_Username",
                table: "SFDUser",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SFDAccessToken");

            migrationBuilder.DropTable(
                name: "SFDAdditionalInfo");

            migrationBuilder.DropTable(
                name: "SFDAdminToken");

            migrationBuilder.DropTable(
                name: "SFDBulletin");

            migrationBuilder.DropTable(
                name: "SFDFaq");

            migrationBuilder.DropTable(
                name: "SFDGuideMaterial");

            migrationBuilder.DropTable(
                name: "SFDHomeBanner");

            migrationBuilder.DropTable(
                name: "SFDNotificationStatus");

            migrationBuilder.DropTable(
                name: "SFDOTP");

            migrationBuilder.DropTable(
                name: "SFDPermission");

            migrationBuilder.DropTable(
                name: "SFDPKTHistory");

            migrationBuilder.DropTable(
                name: "SFDRecommendation");

            migrationBuilder.DropTable(
                name: "SFDStaticContent");

            migrationBuilder.DropTable(
                name: "SFDTrainingMaterial");

            migrationBuilder.DropTable(
                name: "SFDAdmin");

            migrationBuilder.DropTable(
                name: "SFDGuideCategory");

            migrationBuilder.DropTable(
                name: "SFDNotification");

            migrationBuilder.DropTable(
                name: "SFDUser");

            migrationBuilder.DropTable(
                name: "SFDMenu");

            migrationBuilder.DropTable(
                name: "SFDTrainingCategory");

            migrationBuilder.DropTable(
                name: "SFDRole");

            migrationBuilder.DropTable(
                name: "VWI_SFDMobileSalesman");

            migrationBuilder.DropTable(
                name: "SFDMenuType");

            migrationBuilder.DropTable(
                name: "SFDImageGallery");

            migrationBuilder.DropTable(
                name: "SFDMasterConfig");
        }
    }
}
