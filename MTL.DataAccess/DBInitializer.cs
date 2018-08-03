using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MTL.Library.Models.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace MTL.DataAccess
{
    public class DbInitializer
    {
        private readonly ILogger<DbInitializer> _logger;
        private readonly UserManager<AppUser> _userManager;

        public DbInitializer(ILogger<DbInitializer> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async void Initialize(MyAppContext context)
        {
            context.Database.EnsureCreated();

            string ownerId = "";

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
                await context.UserProfile.AddAsync(new UserProfile { IdentityId = seed.Id, Location = "MTL" });
                ownerId = seed.Id;
            }
            ///User END


            var TimeLines = new TimeLine[]
            {
            new TimeLine{  name = "TimeLine1", description = "A time line about ...", lastModified=DateTime.Now, owner = AppUsers[1]},
            new TimeLine{  name = "TimeLine2", description = "When I lived in ....", lastModified=DateTime.Now, owner = AppUsers[1]},
            new TimeLine{  name = "TimeLine3", description = "My time in Arkano....", lastModified=DateTime.Now, owner = AppUsers[1]},
            new TimeLine{  name = "TimeLine4", description = "WTF??", lastModified=DateTime.Now, owner = AppUsers[1]},
            };

            foreach (TimeLine seed in TimeLines)
            {
                context.TimeLines.Add(seed);
            }

            DateTime dateOne = new DateTime(2010, 4, 16);
            DateTime dateTwo = new DateTime(1984, 6, 7);
            DateTime dateThree = new DateTime(2010, 09, 16); ;
            DateTime dateFour = new DateTime(2017, 7, 1);

            var Memories = new Memory[]
            {
            new Memory{  name = "My first memory", description = "Really old", lastModified=DateTime.Now, date = dateOne, timeLine = TimeLines[0] },
            new Memory{  name = "Second", description = "Old", lastModified=DateTime.Now, date = dateTwo, timeLine = TimeLines[0]},
            new Memory{  name = "Third", description = "Not too bad", lastModified=DateTime.Now, date = dateThree, timeLine = TimeLines[0]},
            new Memory{  name = "Fourth", description = "Yesterday?", lastModified=DateTime.Now, date = dateFour, timeLine = TimeLines[0]},
            };

            foreach (Memory seed in Memories)
            {
                context.Memories.Add(seed);
            }

            //var computers = new Computer[]
            //{
            //new Computer{ ConfiguracionName = "The Basic", HardDrive = "512GB HDD", Memory = Memories[0], Processor = "AMD", LastModified=DateTime.Now},
            //new Computer{ ConfiguracionName = "The Internet", HardDrive = "128GB SDD", Memory = Memories[1], Processor = "Intel i3", LastModified=DateTime.Now},
            //new Computer{ ConfiguracionName = "The Gamer", HardDrive = "1TB HDD", Memory = Memories[2], Processor = "Intel i5", LastModified=DateTime.Now},
            //new Computer{ ConfiguracionName = "The Beast", HardDrive = "512GB SDD", Memory = Memories[3], Processor = "Intel i7", LastModified=DateTime.Now}
            //};
            //foreach (Computer c in computers)
            //{
            //    context.Computers.Add(c);
            //}

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("{0} : DataBase seeding error", System.Reflection.MethodBase.GetCurrentMethod()),ex);
            }

            _logger.LogInformation(string.Format("{0} : Seeded Database with {1} {2}", System.Reflection.MethodBase.GetCurrentMethod(), AppUsers.Count(), "AppUsers"));
            _logger.LogInformation(string.Format("{0} : Seeded Database with {1} {2}", System.Reflection.MethodBase.GetCurrentMethod(), AppUsers.Count(), "UserProfiles"));
            _logger.LogInformation(string.Format("{0} : Seeded Database with {1} {2}", System.Reflection.MethodBase.GetCurrentMethod(), Memories.Count(), "Memories"));
            _logger.LogInformation(string.Format("{0} : Seeded Database with {1} {2}", System.Reflection.MethodBase.GetCurrentMethod(), TimeLines.Count(), "TimeLines"));
            _logger.LogInformation(string.Format("{0} : DataBase Initializing Complete", System.Reflection.MethodBase.GetCurrentMethod()));
        }
    }
}

