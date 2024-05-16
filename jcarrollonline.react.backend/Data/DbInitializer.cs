using jcarrollonline.react.backend.Models.Entities;

namespace jcarrollonline.react.backend.Data
{
    internal class DbInitializer
    {
        internal static void Initialize(ApplicationDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();
            if (dbContext.Forum.Any()) return;

            Forum[] fora = new Forum[]
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

            foreach (Forum forum in fora)
                dbContext.Forum.Add(forum);

            ForumThread[] forumThreads = new ForumThread[]
            {
                new ForumThread{ Title="C# First Thread", Content="Contents of first C# thread",
                Locked=false, CreatedAt=DateTime.UtcNow, UpdatedAt=DateTime.UtcNow,
                PostNumber=1, ParentId=null, RootId=null, ForumId=1 },
                new ForumThread{Title="C# Second Thread", Content="Contents of second C# thread",
                Locked=false, CreatedAt=DateTime.UtcNow, UpdatedAt=DateTime.UtcNow,
                PostNumber=1, ParentId=null, RootId=null, ForumId=1 },
                new ForumThread{ Title="Drones First Thread", Content="Contents of first Drones thread",
                Locked=false, CreatedAt=DateTime.UtcNow, UpdatedAt=DateTime.UtcNow,
                PostNumber=1, ParentId=null, RootId=null, ForumId=3},
                new ForumThread{ Title="News First Thread", Content="Contents of first News thread",
                Locked=false, CreatedAt=DateTime.UtcNow, UpdatedAt=DateTime.UtcNow,
                PostNumber=1, ParentId=null, RootId=null, ForumId=8},
            };

            foreach(ForumThread forumThread in forumThreads)
                dbContext.ForumThread.Add(forumThread);

            dbContext.SaveChanges();
        }
    }
}
