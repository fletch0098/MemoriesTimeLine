using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MTL.DataAccess.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace MTL.DataAccess
{
    public class RepositoryInitializer
    {
        private readonly ILogger<RepositoryInitializer> _logger;
        private readonly UserManager<AppUser> _userManager;

        public RepositoryInitializer(ILogger<RepositoryInitializer> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async void Initialize(RepositoryContext context)
        {
            context.Database.EnsureCreated();

            // Check for Data
            if (context.Users.Any())
            {
                _logger.LogWarning(string.Format("{0} : DataBase already seeded", System.Reflection.MethodBase.GetCurrentMethod()));
                return;   // DB has been seeded
            }

            _logger.LogInformation(string.Format("{0} : Preparing to seed database", System.Reflection.MethodBase.GetCurrentMethod()));

            //User
            var AppUsers = new AppUser[]
            {
            new AppUser{ UserName = "Admin@mtl.com", FirstName = "Admin", LastName= "Bob", Email="Admin@mtl.com" },
            new AppUser{ UserName = "User@mtl.com", FirstName = "User", LastName= "Bob", Email="User@mtl.com" }
            };

            foreach (AppUser seed in AppUsers)
            {
                var result = await _userManager.CreateAsync(seed, "mtlmtl");
                await context.UserProfiles.AddAsync(new UserProfile { IdentityId  = seed.Id, Location = "MTL", Gender= "Male", Locale="en-US", LastModified = DateTime.Now });
            }
            ///User END


            var TimeLines = new TimeLine[]
            {
            new TimeLine{  Name = "TimeLine1", Description = "A time line about ...", LastModified=DateTime.Now, IdentityId = AppUsers[1].Id},
            new TimeLine{  Name = "TimeLine2", Description = "A time line about ...", LastModified=DateTime.Now, IdentityId = AppUsers[1].Id},
            new TimeLine{  Name = "TimeLine3", Description = "A time line about ...", LastModified=DateTime.Now, IdentityId = AppUsers[1].Id},
            new TimeLine{  Name = "TimeLine4", Description = "A time line about ...", LastModified=DateTime.Now, IdentityId = AppUsers[1].Id},
            };

            foreach (TimeLine seed in TimeLines)
            {
                context.TimeLines.Add(seed);
            }

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0} : TimeLine seeding error", System.Reflection.MethodBase.GetCurrentMethod()), ex);
            }

            DateTime dateOne = new DateTime(2010, 4, 16);
            DateTime dateTwo = new DateTime(1984, 6, 7);
            DateTime dateThree = new DateTime(2010, 09, 16); ;
            DateTime dateFour = new DateTime(2017, 7, 1);

            var Memories = new Memory[]
            {
            new Memory{  Name = "My first memory", Description = "Really old", LastModified=DateTime.Now, Date = dateOne, TimeLineId = TimeLines[0].Id },
            new Memory{  Name = "My 2 memory", Description = "Really old", LastModified=DateTime.Now, Date = dateOne, TimeLineId = TimeLines[0].Id },
            new Memory{  Name = "My 3 memory", Description = "Really old", LastModified=DateTime.Now, Date = dateOne, TimeLineId = TimeLines[0].Id },
            new Memory{  Name = "My 4 memory", Description = "Really old", LastModified=DateTime.Now, Date = dateOne, TimeLineId = TimeLines[0].Id },
            };

            foreach (Memory seed in Memories)
            {
                context.Memories.Add(seed);
            }

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0} : Memory seeding error", System.Reflection.MethodBase.GetCurrentMethod()),ex);
            }

            _logger.LogInformation(string.Format("{0} : Seeded Database with {1} {2}", System.Reflection.MethodBase.GetCurrentMethod(), AppUsers.Count(), "AppUsers"));
            _logger.LogInformation(string.Format("{0} : Seeded Database with {1} {2}", System.Reflection.MethodBase.GetCurrentMethod(), AppUsers.Count(), "UserProfiles"));
            _logger.LogInformation(string.Format("{0} : Seeded Database with {1} {2}", System.Reflection.MethodBase.GetCurrentMethod(), Memories.Count(), "Memories"));
            _logger.LogInformation(string.Format("{0} : Seeded Database with {1} {2}", System.Reflection.MethodBase.GetCurrentMethod(), TimeLines.Count(), "TimeLines"));
            _logger.LogInformation(string.Format("{0} : DataBase Initializing Complete", System.Reflection.MethodBase.GetCurrentMethod()));
        }
    }
}

