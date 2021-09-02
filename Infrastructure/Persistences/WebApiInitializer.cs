using System;
using System.Linq;
using WebApi.Domain.Entities;

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
                new User
                {
                    Name = "Glend Maatita",
                    UserName = "glendmaatita",
                    Email = "glend.maatita@gmail.com",
                    Phone = "0856996665856",
                    Age = 40,
                    ProfilePicture = "https://secure.gravatar.com/avatar/3d2b652a26c2407232df0b412227710e",
                },
                new User
                {
                    Name = "Agung Laksono",
                    UserName = "agunglaksono",
                    Email = "agung.laksono@gmail.com",
                    Phone = "085936882228",
                    Age = 35,
                    ProfilePicture = "https://secure.gravatar.com/avatar/3d2b652a26c2407232df0b412227710e",
                },
                new User
                {
                    Name = "Ahmad Nadjib",
                    UserName = "ahmadnadjib",
                    Email = "ahmad.nadjib@gmail.com",
                    Phone = "0857452255896",
                    Age = 37,
                    ProfilePicture = "https://secure.gravatar.com/avatar/3d2b652a26c2407232df0b412227710e",
                },
                
            };

            context.Users.AddRange(info);
            context.SaveChanges();
        }
    }
}
