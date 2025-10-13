
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Kebab_Simulator.Data;
using Kebab_Simulator.Core.Domain;

namespace Kebab_Simulator.Data
{
    public static class DbInitialize
    {
        public static void Initialize(KebabSimulatorContext context)
        {
            // Ensures that the database is created or already exists
            context.Database.EnsureCreated();

            // If there are any students in the database, return immediately
            if (context.Kebabs.Any())
            {
                return; // Database has already been seeded
            }

            // Create an array of students to be added if the table is empty
            var kebabs = new Kebab[]
            {
                new Kebab {ID = Guid.NewGuid(),KebabName = "Ahmet",KebabLevel = 1 , KebabXP = 0, KebabXPNextLevel = 100, KebabStatus = KebabStatus.Raw, KebabBankAccount = 2000, Checkout = 0},
                new Kebab {ID = Guid.NewGuid(), KebabName = "Aslan",KebabLevel = 1 , KebabXP = 0, KebabXPNextLevel = 100, KebabStatus = KebabStatus.Raw, KebabBankAccount = 2000, Checkout = 0},
                new Kebab {ID = Guid.NewGuid(), KebabName = "Baki",KebabLevel = 1 , KebabXP = 0, KebabXPNextLevel = 100, KebabStatus = KebabStatus.Raw, KebabBankAccount = 2000, Checkout = 0},

            };

            // Add students to the context and save the changes
            context.Kebabs.AddRange(kebabs);
            context.SaveChanges();  

            // Create an array of courses
            var countries = new Country[]
            {
                new Country {ID = Guid.NewGuid(), CountryType = CountryType.Indigenous, Name = "Japan",},
                new Country {ID = Guid.NewGuid(), CountryType = CountryType.African, Name = "Nigeria", },
                new Country {ID = Guid.NewGuid(), CountryType = CountryType.LatinAmerican, Name = "Brazil", },
                new Country {ID = Guid.NewGuid(), CountryType = CountryType.Indigenous, Name = "Indonesia", },
                new Country {ID = Guid.NewGuid(), CountryType = CountryType.NorthAmerican, Name = "Usa", },
                new Country {ID = Guid.NewGuid(), CountryType = CountryType.MiddleEastern, Name = "Pakistan", },
                new Country {ID = Guid.NewGuid(), CountryType = CountryType.European, Name = "Russia", },


            };

            // Add courses to the context and save the changes
            context.Countries.AddRange(countries);
            context.SaveChanges();

           
        }
    }
}
