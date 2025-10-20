
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
                new Kebab {ID = Guid.NewGuid(), KebabName = "Aslan",KebabLevel = 1 , KebabXP = 0, KebabXPNextLevel = 100, KebabStatus = KebabStatus.Raw, KebabBankAccount = 100, Checkout = 0},
            };

            // Add students to the context and save the changes
            context.Kebabs.AddRange(kebabs);
            context.SaveChanges();

            // Create an array of courses
            var countries = new Country[]
            {
                new Country {ID = Guid.NewGuid(), CountryType = CountryType.Indigenous, Name = "Kazakhstan",},

            };


            // Add courses to the context and save the changes
            context.Countries.AddRange(countries);
            context.SaveChanges();

            var kebab = new Kebab[]
            {
            new Kebab { KebabType = KebabType.ErzanLegacy, Description = "The original family recipe that started it all.", Ingredients = "Beef, lamb, onion, garlic, black pepper, salt" },
            new Kebab { KebabType = KebabType.AslansAmbition, Description = "A bold, spicy twist symbolizing Aslan’s drive.", Ingredients = "Lamb, paprika, chili, cumin, garlic, onion" },
            new Kebab { KebabType = KebabType.SteppeFlame, Description = "Grilled over open fire, capturing the spirit of the Kazakh steppe.", Ingredients = "Beef, lamb, smoked paprika, thyme, chili flakes" },
            new Kebab { KebabType = KebabType.NomadsFeast, Description = "A hearty kebab for travelers and wanderers.", Ingredients = "Mixed meat (beef & lamb), onion, bell pepper, garlic, yogurt marinade" },
            new Kebab { KebabType = KebabType.GoldenYurt, Description = "Rich, golden-marinated meat fit for royalty.", Ingredients = "Chicken, turmeric, saffron, garlic, honey glaze" },
            new Kebab { KebabType = KebabType.SilkRoadSkewer, Description = "Exotic spices inspired by distant trade routes.", Ingredients = "Beef, cumin, coriander, cinnamon, nutmeg, garlic" },
            new Kebab { KebabType = KebabType.FathersPride, Description = "The classic everyone remembers, in honor of Eržan.", Ingredients = "Lamb, onion, salt, black pepper, coriander" },
            new Kebab { KebabType = KebabType.EaglesBite, Description = "Sharp, powerful flavors that leave a lasting impression.", Ingredients = "Beef, chili, garlic, smoked paprika, cayenne" },
            new Kebab { KebabType = KebabType.KazakhSun, Description = "Bright, tangy marinade that radiates warmth.", Ingredients = "Chicken, lemon, yogurt, paprika, garlic, turmeric" },
            new Kebab { KebabType = KebabType.AslansRoar, Description = "Extra spicy, bold flavors that demand attention.", Ingredients = "Lamb, hot chili, black pepper, garlic, cumin" },
            new Kebab { KebabType = KebabType.SteppeWhisper, Description = "Subtle, delicate herbs that hint at hidden depth.", Ingredients = "Chicken, dill, parsley, mint, garlic, yogurt" },
            new Kebab { KebabType = KebabType.TravelersDelight, Description = "Packed with flavor to satisfy hungry adventurers.", Ingredients = "Mixed meat, bell pepper, onion, tomato, paprika, cumin" },
            new Kebab { KebabType = KebabType.MountainSmoke, Description = "Smoked to perfection, earthy and strong.", Ingredients = "Beef, smoked paprika, rosemary, garlic, thyme" },
            new Kebab { KebabType = KebabType.GoldenHorde, Description = "Richly spiced, hearty enough for a feast.", Ingredients = "Lamb, onion, garlic, cumin, coriander, paprika" },
            new Kebab { KebabType = KebabType.MidnightCaravan, Description = "Dark, mysterious flavors that intrigue the palate.", Ingredients = "Beef, dark soy sauce, black pepper, garlic, chili" },
            new Kebab { KebabType = KebabType.EternalFlame, Description = "A kebab that symbolizes family tradition and fire.", Ingredients = "Lamb, paprika, chili, garlic, onion, smoked salt" },
            new Kebab { KebabType = KebabType.AslansTriumph, Description = "A celebratory kebab for milestone victories.", Ingredients = "Beef, lamb, garlic, rosemary, olive oil, paprika" },
            new Kebab { KebabType = KebabType.SteppeBreeze, Description = "Light, fresh, and refreshing for a quick bite.", Ingredients = "Chicken, mint, cucumber, yogurt, lemon, garlic" },
            new Kebab { KebabType = KebabType.LegendsSkewer, Description = "The ultimate, unlockable kebab reserved for the master.", Ingredients = "Beef, lamb, exotic spices, garlic, saffron, paprika" },
            new Kebab { KebabType = KebabType.HeartOfTheStand, Description = "The final kebab, representing everything Aslan built.", Ingredients = "Mixed meat, secret family spices, garlic, onion, herbs" }
            };

            context.Kebabs.AddRange(kebab);
            context.SaveChanges();

        }
    }
}
