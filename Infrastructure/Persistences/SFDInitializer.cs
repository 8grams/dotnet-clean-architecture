using System;
using System.Collections.Generic;
using System.Linq;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Infrastructure.Persistences
{
    public class SFDInitializer
    {
        public static void Initialize(SFDDbContext context)
        {
            var initalizer = new SFDInitializer();
            initalizer.Seed(context);
        }

        public void Seed(SFDDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; // Db has been seeded
            }

            SeedMasterConfigs(context);
            SeedSalesmans(context);
            SeedUsers(context);
            SeedAccessTokens(context);
            SeedImageGalleries(context);
            SeedTrainingCategories(context);
            SeedTrainingMaterials(context);
            SeedGuideCategories(context);
            SeedGuideMaterials(context);
            SeedBulletins(context);
            SeedRecommendations(context);
            SeedAdditionalInfo(context);
            SeedFAQs(context);
            SeedNotifications(context);
            SeedStaticContent(context);
            SeedRoles(context);
            SeedAdmins(context);
        }

        private void SeedSalesmans(SFDDbContext context)
        {
            var salesmen = new[]
            {
                new Salesman
                {
                    Id = 1,
                    DealerCode = "MMK 212",
                    DealerName = "Srikandi Motor Yogyakarta",
                    DealerCity = "Yogyakarta",
                    DealerGroup = "Srikandi Group",
                    DealerArea = "DIY",
                    DealerBranchCode = "YKSG01",
                    DealerBranchName = "Srikandi Yogyakarta",
                    SalesmanCode = "MTSI 03402",
                    SalesmanName = "Steve Rogers",
                    SalesmanHireDate = DateTime.Now,
                    JobDescription = "Sales Director",
                    LevelDescription = "Supervisor",
                    SuperiorName = "Nick Fury",
                    SuperiorCode = "MTSI 0222",
                    SalesmanEmail = "fury@shield.com",
                    SalesmanHandphone = "08567463533",
                    SalesmanTeamCategory = "Avengers",
                    SalesmanStatus = "Active",
                    LastUpdateTime = DateTime.Now
                },
                new Salesman
                {
                    Id = 2,
                    DealerCode = "MMK 105",
                    DealerName = "Sumberjaya Motor Yogyakarta",
                    DealerCity = "Yogyakarta",
                    DealerGroup = "Sumberjaya Group",
                    DealerArea = "DIY",
                    DealerBranchCode = "YKSJ15",
                    DealerBranchName = "Sumberjaya Yogyakarta",
                    SalesmanCode = "MTSI 01650",
                    SalesmanName = "Tony Stark",
                    SalesmanHireDate = DateTime.Now,
                    JobDescription = "Sales Director",
                    LevelDescription = "Supervisor",
                    SuperiorName = "Nick Fury",
                    SuperiorCode = "MTSI 0222",
                    SalesmanEmail = "fury@shield.com",
                    SalesmanHandphone = "085659175203",
                    SalesmanTeamCategory = "Avengers",
                    SalesmanStatus = "Active",
                    LastUpdateTime = DateTime.Now
                },
                new Salesman
                {
                    Id = 3,
                    DealerCode = "MMK 212",
                    DealerName = "Srikandi Motor Yogyakarta",
                    DealerCity = "Yogyakarta",
                    DealerGroup = "Srikandi Group",
                    DealerArea = "DIY",
                    DealerBranchCode = "YKSG01",
                    DealerBranchName = "Srikandi Yogyakarta",
                    SalesmanCode = "MTSI 01110",
                    SalesmanName = "Clint Barton",
                    SalesmanHireDate = DateTime.Now,
                    JobDescription = "Team Leader",
                    LevelDescription = "Leader",
                    SuperiorName = "Prof. X",
                    SuperiorCode = "MTSI 0111",
                    SalesmanEmail = "x@xmen.com",
                    SalesmanHandphone = "081233434",
                    SalesmanTeamCategory = "X-Men",
                    SalesmanStatus = "Active",
                    LastUpdateTime = DateTime.Now
                }
            };

            context.Salesmen.AddRange(salesmen);
            context.SaveChanges();
        }

        private void SeedMasterConfigs(SFDDbContext context)
        {
            var masterConfig = new MasterConfig
            {
                Category = "SFD",
                ValueId = "SFD",
                ValueCode = "SFD",
                ValueDesc = "SFD",
                Sequence = 4
            };

            context.MasterConfigs.Add(masterConfig);
            context.SaveChanges();
        }

        private void SeedUsers(SFDDbContext context)
        {
            var user = new User
            {
                Email = "glend@refactory.id",
                Phone = "085648721439",
                UserName = context.Salesmen.First().SalesmanCode,
                Password = DBUtil.PasswordHash("password"),
                IsActive = true,
                DeviceId = "Samsung101",
                MasterConfigId = context.MasterConfigs.First().Id,
                LastLogin = DateTime.Now
            };

            context.Users.Add(user);
            context.SaveChanges();
        }

        private void SeedAccessTokens(SFDDbContext context)
        {
            var token = new AccessToken
            {
                AuthToken = "1qaz2wsx",
                Type = "Bearer",
                UserId = context.Users.First().Id,
                ExpiresAt = DateTime.Now.AddDays(1)
            };

            context.AccessTokens.Add(token);
            context.SaveChanges();
        }

        private void SeedImageGalleries(SFDDbContext context)
        {
            var img = new ImageGallery
            {
                Name = "Gambar apa saja",
                Category = "cover",
                Link = "https://i.imgur.com/Cw6g6hM.png"
            };

            context.ImageGalleries.Add(img);
            context.SaveChanges();
        }

        private void SeedTrainingCategories(SFDDbContext context)
        {
            var categories = new[]
            {
                new MasterCar
                {
                    Title = "Xpander",
                    Tag = "Passenger Car",
                    ImageThumbnailId = 1,
                    ImageCoverId = 1,
                    ImageLogoId = 1,
                    TrainingActive = true
                },
                new MasterCar
                {
                    Title = "Outlander",
                    Tag = "Passenger Car",
                    ImageThumbnailId = 1,
                    ImageCoverId = 1,
                    ImageLogoId = 1,
                    TrainingActive = true
                },
                new MasterCar
                {
                    Title = "Xpander Limited",
                    Tag = "Passenger Car",
                    ImageThumbnailId = 1,
                    ImageCoverId = 1,
                    ImageLogoId = 1,
                    TrainingActive = true
                },
                new MasterCar
                {
                    Title = "Eclipse Ross",
                    Tag = "Passenger Car",
                    ImageThumbnailId = 1,
                    ImageCoverId = 1,
                    ImageLogoId = 1,
                    TrainingActive = true
                },
                new MasterCar
                {
                    Title = "Pajero Sport",
                    Tag = "Passenger Car",
                    ImageThumbnailId = 1,
                    ImageCoverId = 1,
                    ImageLogoId = 1,
                    TrainingActive = true
                },
                new MasterCar
                {
                    Title = "Outlander Sport",
                    Tag = "Passenger Car",
                    ImageThumbnailId = 1,
                    ImageCoverId = 1,
                    ImageLogoId = 1,
                    TrainingActive = true
                },
                new MasterCar
                {
                    Title = "Triton",
                    Tag = "Light Commercial Vehicle",
                    ImageThumbnailId = 1,
                    ImageCoverId = 1,
                    ImageLogoId = 1,
                    TrainingActive = true
                },
                new MasterCar
                {
                    Title = "L300",
                    Tag = "Light Commercial Vehicle",
                    ImageThumbnailId = 1,
                    ImageCoverId = 1,
                    ImageLogoId = 1,
                    TrainingActive = true
                },
                new MasterCar
                {
                    Title = "T120SS",
                    Tag = "Light Commercial Vehicle",
                    ImageThumbnailId = 1,
                    ImageCoverId = 1,
                    ImageLogoId = 1,
                    TrainingActive = true
                }
            };

            context.MasterCars.AddRange(categories);
            context.SaveChanges();
        }

        private void SeedTrainingMaterials(SFDDbContext context)
        {
            var materials = new[]
            {
                new TrainingMaterial
                {
                    Title = "Training Material for Pajero Sport",
                    ImageThumbnailId = 1,
                    Link = "http://www.pdf995.com/samples/pdf.pdf",
                    FileType = "pdf",
                    TotalViews = 5,
                    MasterCarId = 1,
                    PublishedAt = DateTime.Now.AddDays(-7),
                    ExpiresAt = DateTime.Now.AddDays(-1),
                },
                new TrainingMaterial
                {
                    Title = "Training Material for Pajero",
                    ImageThumbnailId = 1,
                    Link = "http://www.pdf995.com/samples/pdf.pdf",
                    FileType = "pdf",
                    TotalViews = 5,
                    MasterCarId = 1,
                    PublishedAt = DateTime.Now.AddDays(-6),
                    ExpiresAt = DateTime.Now.AddDays(7),
                },
                new TrainingMaterial
                {
                    Title = "Training Material for Outlander",
                    ImageThumbnailId = 1,
                    Link = "https://i.imgur.com/Cw6g6hM.png",
                    FileType = "png",
                    TotalViews = 10,
                    MasterCarId = 1,
                    PublishedAt = DateTime.Now.AddDays(-5),
                    ExpiresAt = DateTime.Now.AddDays(9),
                },
                new TrainingMaterial
                {
                    Title = "Training Material for L300",
                    ImageThumbnailId = 1,
                    Link = "http://www.html5videoplayer.net/videos/toystory.mp4",
                    FileType = "mp4",
                    TotalViews = 15,
                    MasterCarId = 1,
                    PublishedAt = DateTime.Now.AddDays(-4),
                    ExpiresAt = DateTime.Now.AddDays(1),
                },
                new TrainingMaterial
                {
                    Title = "Training Material for Eclipse",
                    ImageThumbnailId = 1,
                    Link = "http://www.statvision.com/webinars/Countries%20of%20the%20world.xls",
                    FileType = "xls",
                    TotalViews = 18,
                    MasterCarId = 1,
                    PublishedAt = DateTime.Now.AddDays(-3),
                    ExpiresAt = DateTime.Now.AddDays(3),
                }
            };

            context.TrainingMaterials.AddRange(materials);
            context.SaveChanges();
        }

        private void SeedGuideCategories(SFDDbContext context)
        {
            var categories = new[]
            {
                new MasterCar
                {
                    Title = "Xpander",
                    Tag = "Passenger Car",
                    ImageThumbnailId = 1,
                    ImageCoverId = 1,
                    ImageLogoId = 1,
                    GuideActive = true
                },
                new MasterCar
                {
                    Title = "Outlander",
                    Tag = "Passenger Car",
                    ImageThumbnailId = 1,
                    ImageCoverId = 1,
                    ImageLogoId = 1,
                    GuideActive = true
                },
                new MasterCar
                {
                    Title = "Xpander Limited",
                    Tag = "Passenger Car",
                    ImageThumbnailId = 1,
                    ImageCoverId = 1,
                    ImageLogoId = 1,
                    GuideActive = true
                },
                new MasterCar
                {
                    Title = "Eclipse Ross",
                    Tag = "Passenger Car",
                    ImageThumbnailId = 1,
                    ImageCoverId = 1,
                    ImageLogoId = 1,
                    GuideActive = true
                },
                new MasterCar
                {
                    Title = "Pajero Sport",
                    Tag = "Passenger Car",
                    ImageThumbnailId = 1,
                    ImageCoverId = 1,
                    ImageLogoId = 1,
                    GuideActive = true
                },
                new MasterCar
                {
                    Title = "Outlander Sport",
                    Tag = "Passenger Car",
                    ImageThumbnailId = 1,
                    ImageCoverId = 1,
                    ImageLogoId = 1,
                    GuideActive = true
                },
                new MasterCar
                {
                    Title = "Triton",
                    Tag = "Light Commercial Vehicle",
                    ImageThumbnailId = 1,
                    ImageCoverId = 1,
                    ImageLogoId = 1,
                    GuideActive = true
                },
                new MasterCar
                {
                    Title = "L300",
                    Tag = "Light Commercial Vehicle",
                    ImageThumbnailId = 1,
                    ImageCoverId = 1,
                    ImageLogoId = 1,
                    GuideActive = true
                },
                new MasterCar
                {
                    Title = "T120SS",
                    Tag = "Light Commercial Vehicle",
                    ImageThumbnailId = 1,
                    ImageCoverId = 1,
                    ImageLogoId = 1,
                    GuideActive = true
                }
            };

            context.MasterCars.AddRange(categories);
            context.SaveChanges();
        }

        private void SeedGuideMaterials(SFDDbContext context)
        {
            var materials = new[]
            {
                new GuideMaterial
                {
                    Title = "Guide Material for Pajero Sport",
                    ImageThumbnailId = 1,
                    Link = "http://www.pdf995.com/samples/pdf.pdf",
                    FileType = "pdf",
                    FileCode = "001",
                    TotalViews = 5,
                    MasterCarId = 1,
                    PublishedAt = DateTime.Now.AddDays(-7),
                    ExpiresAt = DateTime.Now.AddDays(-1),
                },
                new GuideMaterial
                {
                    Title = "Guide Material for Pajero",
                    ImageThumbnailId = 1,
                    Link = "http://www.pdf995.com/samples/pdf.pdf",
                    FileType = "pdf",
                    FileCode = "002",
                    TotalViews = 5,
                    MasterCarId = 1,
                    PublishedAt = DateTime.Now.AddDays(-6),
                    ExpiresAt = DateTime.Now.AddDays(7),
                },
                new GuideMaterial
                {
                    Title = "Guide Material for Outlander",
                    ImageThumbnailId = 1,
                    Link = "https://i.imgur.com/Cw6g6hM.png",
                    FileType = "png",
                    FileCode = "003",
                    TotalViews = 10,
                    MasterCarId = 1,
                    PublishedAt = DateTime.Now.AddDays(-5),
                    ExpiresAt = DateTime.Now.AddDays(9),
                },
                new GuideMaterial
                {
                    Title = "Guide Material for L300",
                    ImageThumbnailId = 1,
                    Link = "http://www.html5videoplayer.net/videos/toystory.mp4",
                    FileType = "mp4",
                    FileCode = "004",
                    TotalViews = 15,
                    MasterCarId = 1,
                    PublishedAt = DateTime.Now.AddDays(-4),
                    ExpiresAt = DateTime.Now.AddDays(1),
                },
                new GuideMaterial
                {
                    Title = "Guide Material for Eclipse",
                    ImageThumbnailId = 1,
                    Link = "http://www.statvision.com/webinars/Countries%20of%20the%20world.xls",
                    FileType = "xls",
                    FileCode = "005",
                    TotalViews = 18,
                    MasterCarId  = 1,
                    PublishedAt = DateTime.Now.AddDays(-3),
                    ExpiresAt = DateTime.Now.AddDays(3),
                }
            };

            context.GuideMaterials.AddRange(materials);
            context.SaveChanges();
        }

        private void SeedBulletins(SFDDbContext context)
        {
            var bulletins = new[]
            {
                new Bulletin
                {
                    Title = "Bulletin for Pajero Sport",
                    ImageThumbnailId = 1,
                    Link = "http://www.pdf995.com/samples/pdf.pdf",
                    FileType = "pdf",
                    FileCode = "001",
                    TotalViews = 5,
                    PublishedAt = DateTime.Now.AddDays(-7),
                    ExpiresAt = DateTime.Now.AddDays(-1),
                },
                new Bulletin
                {
                    Title = "Bulletin for Pajero",
                    ImageThumbnailId = 1,
                    Link = "http://www.pdf995.com/samples/pdf.pdf",
                    FileType = "pdf",
                    FileCode = "002",
                    TotalViews = 5,
                    PublishedAt = DateTime.Now.AddDays(-6),
                    ExpiresAt = DateTime.Now.AddDays(7),
                },
                new Bulletin
                {
                    Title = "Bulletin for Outlander",
                    ImageThumbnailId = 1,
                    Link = "https://i.imgur.com/Cw6g6hM.png",
                    FileType = "png",
                    FileCode = "003",
                    TotalViews = 10,
                    PublishedAt = DateTime.Now.AddDays(-5),
                    ExpiresAt = DateTime.Now.AddDays(9),
                },
                new Bulletin
                {
                    Title = "Bulletin for L300",
                    ImageThumbnailId = 1,
                    Link = "http://www.html5videoplayer.net/videos/toystory.mp4",
                    FileType = "mp4",
                    FileCode = "004",
                    TotalViews = 15,
                    PublishedAt = DateTime.Now.AddDays(-4),
                    ExpiresAt = DateTime.Now.AddDays(1),
                },
                new Bulletin
                {
                    Title = "Bulletin for Eclipse",
                    ImageThumbnailId = 1,
                    Link = "http://www.statvision.com/webinars/Countries%20of%20the%20world.xls",
                    FileType = "xls",
                    FileCode = "005",
                    TotalViews = 18,
                    PublishedAt = DateTime.Now.AddDays(-3),
                    ExpiresAt = DateTime.Now.AddDays(3),
                }
            };

            context.Bulletins.AddRange(bulletins);
            context.SaveChanges();
        }

        private void SeedRecommendations(SFDDbContext context)
        {
            var recommendations = new List<Recommendation>();
            foreach (var bulletin in context.Bulletins.ToList())
            {
                recommendations.Add(new Recommendation
                {
                    ContentId = bulletin.Id,
                    ContentType = "bulletin",
                    PublishedAt = bulletin.PublishedAt,
                    ExpiresAt = bulletin.ExpiresAt
                });
            }

            foreach (var material in context.TrainingMaterials.ToList())
            {
                recommendations.Add(new Recommendation
                {
                    ContentId = material.Id,
                    ContentType = "training",
                    PublishedAt = material.PublishedAt,
                    ExpiresAt = material.ExpiresAt
                });
            }

            context.Recommendations.AddRange(recommendations);
            context.SaveChanges(); 
        }

        private void SeedAdditionalInfo(SFDDbContext context)
        {
            var info = new[]
            {
                new AdditionalInfo
                {
                    Title = "Info for Pajero Sport",
                    ImageThumbnailId = 1,
                    Link = "http://www.pdf995.com/samples/pdf.pdf",
                    FileType = "pdf",
                    FileCode = "001",
                    TotalViews = 5,
                    PublishedAt = DateTime.Now.AddDays(-7),
                    ExpiresAt = DateTime.Now.AddDays(-1),
                },
                new AdditionalInfo
                {
                    Title = "Info for Pajero",
                    ImageThumbnailId = 1,
                    Link = "http://www.pdf995.com/samples/pdf.pdf",
                    FileType = "pdf",
                    FileCode = "002",
                    TotalViews = 5,
                    PublishedAt = DateTime.Now.AddDays(-6),
                    ExpiresAt = DateTime.Now.AddDays(7),
                },
                new AdditionalInfo
                {
                    Title = "Info for Outlander",
                    ImageThumbnailId = 1,
                    Link = "https://i.imgur.com/Cw6g6hM.png",
                    FileType = "png",
                    FileCode = "003",
                    TotalViews = 10,
                    PublishedAt = DateTime.Now.AddDays(-5),
                    ExpiresAt = DateTime.Now.AddDays(9),
                },
                new AdditionalInfo
                {
                    Title = "Info for L300",
                    ImageThumbnailId = 1,
                    Link = "http://www.html5videoplayer.net/videos/toystory.mp4",
                    FileType = "mp4",
                    FileCode = "004",
                    TotalViews = 15,
                    PublishedAt = DateTime.Now.AddDays(-4),
                    ExpiresAt = DateTime.Now.AddDays(1),
                },
                new AdditionalInfo
                {
                    Title = "Info for Eclipse",
                    ImageThumbnailId = 1,
                    Link = "http://www.statvision.com/webinars/Countries%20of%20the%20world.xls",
                    FileType = "xls",
                    FileCode = "005",
                    TotalViews = 18,
                    PublishedAt = DateTime.Now.AddDays(-3),
                    ExpiresAt = DateTime.Now.AddDays(3),
                }
            };

            context.AdditionalInfos.AddRange(info);
            context.SaveChanges();
        }

        private void SeedFAQs(SFDDbContext context)
        {
            var faqs = new[]
            {
                new Faq
                {
                    Question = "Question 1",
                    Answer = "Answer 1",
                    IsActive = true
                },
                new Faq
                {
                    Question = "Question 2",
                    Answer = "Answer 2",
                    IsActive = true
                },
                new Faq
                {
                    Question = "Question 3",
                    Answer = "Answer 3",
                    IsActive = true
                },
                new Faq
                {
                    Question = "Question 4",
                    Answer = "Answer 4",
                    IsActive = true
                },
                new Faq
                {
                    Question = "Question 5",
                    Answer = "Answer 5",
                    IsActive = true
                },
                new Faq
                {
                    Question = "Question 6",
                    Answer = "Answer 6",
                    IsActive = true
                },
                new Faq
                {
                    Question = "Question 7",
                    Answer = "Answer 7",
                    IsActive = true
                },
                new Faq
                {
                    Question = "Question 8",
                    Answer = "Answer 8",
                    IsActive = true
                },
                new Faq
                {
                    Question = "Question 9",
                    Answer = "Answer 9",
                    IsActive = true
                },
                new Faq
                {
                    Question = "Question 10",
                    Answer = "Answer 10",
                    IsActive = true
                }
            };

            context.Faqs.AddRange(faqs);
            context.SaveChanges();
        }

        private void SeedNotifications(SFDDbContext context)
        {
            // var notifications = new[]
            // {
            //     new Notification
            //     {

            //     }
            // };

            // context.Notifications.AddRange(notifications);
            // context.SaveChanges();
        }

        private void SeedStaticContent(SFDDbContext context)
        {
            var content = "The search for existing technologies turned up a several worthwhile candidates. After much deliberation the list came to a consensus: the Open Inventor ASCII File Format from Silicon Graphics, Inc. The Inventor File Format supports complete descriptions of 3D scenes with polygonally rendered objects, lighting, materials, ambient properties and realism effects. A subset of the Inventor File Format, with extensions to support networking, forms the basis of VRML. Gavin Bell of Silicon Graphics has adapted the Inventor File Format for VRML, with design input from the mailing list. SGI has publicly stated that the file format is available for use in the open market, and have contributed.";

            var staticContent = new StaticContent
            {
                AppInfo = content,
                Disclaimer = content,
                PrivacyPolicy = content,
                TermCondition = content
            };

            context.StaticContent.Add(staticContent);
            context.SaveChanges();
        }

        private void SeedRoles(SFDDbContext context)
        {
            var role = new Role
            {
                Name = "Super Admin",
                Description = "Rule you all!",
                MasterConfigId = 1
            };

            context.Roles.Add(role);
            context.SaveChanges();
        }

        private void SeedAdmins(SFDDbContext context)
        {
            var admin = new Admin
            {
                Name = "Glend Maatita",
                Email = "glend@refactory.id",
                Password = DBUtil.PasswordHash("password"),
                RoleId = 1
            };

            context.Admins.Add(admin);
            context.SaveChanges();
        }   
    }
}
