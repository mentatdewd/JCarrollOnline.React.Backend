using JCarrollOnlineV2.Entities;

namespace jcarrollonline.react.backend.Data
{
    internal class DbInitializer
    {
        internal static void Initialize(ApplicationDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();
            if (dbContext.Forum.Any()) return;

            var fora = new Forum[]
            {
            new Forum{ Title = "C#", Description = "Discussions about the c# programming language.", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Forum{ Title = "Mariners", Description = "Discussions about the Mariners.", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Forum{ Title = "Drones", Description = "Discussions regarding drones, especially home made drones.", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Forum{ Title = "Cooking", Description = "Discussions about cooking and recipes.", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Forum{ Title = "3D Printing", Description = "Discussions regarding 3D printing.", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Forum{ Title = "Bugs", Description = "A fora to keep track of broken features and the status of their fixes.", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Forum{ Title = "Feature Requests", Description = "If anybody has ideas on how to improve the site or wants some new feature here is the place to request it.", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Forum{ Title = "News", Description = "Discussing current events.", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Forum{ Title = "Angular", Description = "Discussion regarding Angular, and asp.net core.", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            //add other users
            };

            foreach (var forum in fora)
                dbContext.Forum.Add(forum);

            dbContext.SaveChanges();
        }
    }
}
