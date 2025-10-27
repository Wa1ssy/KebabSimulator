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
                new Kebab {ID = Guid.NewGuid(), KebabName = "Aslan",KebabLevel = 1 , KebabXP = 0, KebabXPNextLevel = 100, KebabBankAccount = 100, Checkout = 0},
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

            if (!context.KebabRecipes.Any())
            {
                var recipes = new KebabRecipe[]
                {
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.ErzanLegacy,
                        Name = "Erzan Legacy",
                        Description = "The original family recipe that started it all.",
                        Ingredients = "Beef, lamb, onion, garlic, black pepper, salt",
                        LevelRequired = 1,
                        Price = 50,
                        CreatedAt = DateTime.Now
                    },
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.AslansAmbition,
                        Name = "Aslan's Ambition",
                        Description = "A bold, spicy twist symbolizing Aslan’s drive.",
                        Ingredients = "Lamb, paprika, chili, cumin, garlic, onion",
                        LevelRequired = 2,
                        Price = 70,
                        CreatedAt = DateTime.Now
                    },
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.SteppeFlame,
                        Name = "Steppe Flame",
                        Description = "Grilled over open fire, capturing the spirit of the Kazakh steppe.",
                        Ingredients = "Beef, lamb, smoked paprika, thyme, chili flakes",
                        LevelRequired = 3,
                        Price = 90,
                        CreatedAt = DateTime.Now
                    },
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.NomadsFeast,
                        Name = "Nomad's Feast",
                        Description = "A hearty kebab for travelers and wanderers.",
                        Ingredients = "Mixed meat (beef & lamb), onion, bell pepper, garlic, yogurt marinade",
                        LevelRequired = 4,
                        Price = 110,
                        CreatedAt = DateTime.Now
                    },
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.GoldenYurt,
                        Name = "Golden Yurt",
                        Description = "Rich, golden-marinated meat fit for royalty.",
                        Ingredients = "Chicken, turmeric, saffron, garlic, honey glaze",
                        LevelRequired = 5,
                        Price = 130,
                        CreatedAt = DateTime.Now
                    },
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.SilkRoadSkewer,
                        Name = "Silk Road Skewer",
                        Description = "Exotic spices inspired by distant trade routes.",
                        Ingredients = "Beef, cumin, coriander, cinnamon, nutmeg, garlic",
                        LevelRequired = 6,
                        Price = 150,
                        CreatedAt = DateTime.Now
                    },
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.FathersPride,
                        Name = "Father's Pride",
                        Description = "The classic everyone remembers, in honor of Eržan.",
                        Ingredients = "Lamb, onion, salt, black pepper, coriander",
                        LevelRequired = 7,
                        Price = 170,
                        CreatedAt = DateTime.Now
                    },
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.EaglesBite,
                        Name = "Eagle's Bite",
                        Description = "Sharp, powerful flavors that leave a lasting impression.",
                        Ingredients = "Beef, chili, garlic, smoked paprika, cayenne",
                        LevelRequired = 8,
                        Price = 190,
                        CreatedAt = DateTime.Now
                    },
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.KazakhSun,
                        Name = "Kazakh Sun",
                        Description = "Bright, tangy marinade that radiates warmth.",
                        Ingredients = "Chicken, lemon, yogurt, paprika, garlic, turmeric",
                        LevelRequired = 9,
                        Price = 210,
                        CreatedAt = DateTime.Now
                    },
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.AslansRoar,
                        Name = "Aslan's Roar",
                        Description = "Extra spicy, bold flavors that demand attention.",
                        Ingredients = "Lamb, hot chili, black pepper, garlic, cumin",
                        LevelRequired = 10,
                        Price = 230,
                        CreatedAt = DateTime.Now
                    },
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.SteppeWhisper,
                        Name = "Steppe Whisper",
                        Description = "Subtle, delicate herbs that hint at hidden depth.",
                        Ingredients = "Chicken, dill, parsley, mint, garlic, yogurt",
                        LevelRequired = 11,
                        Price = 250,
                        CreatedAt = DateTime.Now
                    },
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.TravelersDelight,
                        Name = "Traveler's Delight",
                        Description = "Packed with flavor to satisfy hungry adventurers.",
                        Ingredients = "Mixed meat, bell pepper, onion, tomato, paprika, cumin",
                        LevelRequired = 12,
                        Price = 270,
                        CreatedAt = DateTime.Now
                    },
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.MountainSmoke,
                        Name = "Mountain Smoke",
                        Description = "Smoked to perfection, earthy and strong.",
                        Ingredients = "Beef, smoked paprika, rosemary, garlic, thyme",
                        LevelRequired = 13,
                        Price = 290,
                        CreatedAt = DateTime.Now
                    },
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.GoldenHorde,
                        Name = "Golden Horde",
                        Description = "Richly spiced, hearty enough for a feast.",
                        Ingredients = "Lamb, onion, garlic, cumin, coriander, paprika",
                        LevelRequired = 14,
                        Price = 310,
                        CreatedAt = DateTime.Now
                    },
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.MidnightCaravan,
                        Name = "Midnight Caravan",
                        Description = "Dark, mysterious flavors that intrigue the palate.",
                        Ingredients = "Beef, dark soy sauce, black pepper, garlic, chili",
                        LevelRequired = 15,
                        Price = 330,
                        CreatedAt = DateTime.Now
                    },
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.EternalFlame,
                        Name = "Eternal Flame",
                        Description = "A kebab that symbolizes family tradition and fire.",
                        Ingredients = "Lamb, paprika, chili, garlic, onion, smoked salt",
                        LevelRequired = 16,
                        Price = 350,
                        CreatedAt = DateTime.Now
                    },
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.AslansTriumph,
                        Name = "Aslan's Triumph",
                        Description = "A celebratory kebab for milestone victories.",
                        Ingredients = "Beef, lamb, garlic, rosemary, olive oil, paprika",
                        LevelRequired = 17,
                        Price = 370,
                        CreatedAt = DateTime.Now
                    },
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.SteppeBreeze,
                        Name = "Steppe Breeze",
                        Description = "Light, fresh, and refreshing for a quick bite.",
                        Ingredients = "Chicken, mint, cucumber, yogurt, lemon, garlic",
                        LevelRequired = 18,
                        Price = 390,
                        CreatedAt = DateTime.Now
                    },
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.LegendsSkewer,
                        Name = "Legend's Skewer",
                        Description = "The ultimate, unlockable kebab reserved for the master.",
                        Ingredients = "Beef, lamb, exotic spices, garlic, saffron, paprika",
                        LevelRequired = 19,
                        Price = 420,
                        CreatedAt = DateTime.Now
                    },
                    new KebabRecipe
                    {
                        ID = Guid.NewGuid(),
                        KebabType = KebabType.HeartOfTheStand,
                        Name = "Heart of the Stand",
                        Description = "The final kebab, representing everything Aslan built.",
                        Ingredients = "Mixed meat, secret family spices, garlic, onion, herbs",
                        LevelRequired = 20,
                        Price = 500,
                        CreatedAt = DateTime.Now
                    }
                };
                context.KebabRecipes.AddRange(recipes);
                context.SaveChanges();
            }

        }
    }
}


