using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MTL.Library.Models.Entities;
using Microsoft.Extensions.Logging;

namespace MTL.DataAccess
{
    public class DbInitializer
    {
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(ILogger<DbInitializer> logger)
        {
            _logger = logger;
        }

        public void Initialize(MyAppContext context)
        {
            context.Database.EnsureCreated();

            // Check for Data
            if (context.Memories.Any())
            {
                _logger.LogWarning(string.Format("{0} : DataBase already seeded", System.Reflection.MethodBase.GetCurrentMethod()));
                return;   // DB has been seeded
            }

            _logger.LogInformation(string.Format("{0} : Preparing to seed database", System.Reflection.MethodBase.GetCurrentMethod()));

            var Memories = new Memory[]
            {
            new Memory{  name = "My first memory", description = "Really old", lastModified=DateTime.Now},
            new Memory{  name = "Second", description = "Old", lastModified=DateTime.Now},
            new Memory{  name = "Third", description = "Not too bad", lastModified=DateTime.Now},
            new Memory{  name = "Fourth", description = "Yesterday?", lastModified=DateTime.Now},
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

            _logger.LogInformation(string.Format("{0} : Seeded Database with {1} {2}", System.Reflection.MethodBase.GetCurrentMethod(), Memories.Count(), "Memories"));
            _logger.LogInformation(string.Format("{0} : DataBase Initializing Complete", System.Reflection.MethodBase.GetCurrentMethod()));
        }
    }
}

