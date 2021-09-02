using System;
using System.Linq;

namespace WebApi.Infrastructure.Persistences
{
    public class WebApiInitializer
    {
        public static void Initialize(WebApiDbContext context)
        {
            var initalizer = new WebApiInitializer();
            initalizer.Seed(context);
        }

        public void Seed(WebApiDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; // Db has been seeded
            }

            SeedAdditionalInfo(context);
        }

        private void SeedAdditionalInfo(WebApiDbContext context)
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
                
            };

            context.Users.AddRange(info);
            context.SaveChanges();
        }
    }
}
