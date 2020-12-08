using DungeonCrawler.Data.Models.Heroes;
using DungeonCrawler.Domain.Helpers;
using System;
using System.Collections.Generic;

namespace DungeonCrawler.Domain.Services
{
    public static class CharacterCreation
    {
        public static Hero CreateCharacter()
        {
            var heroList = new List<Hero>
            {
                new Warrior(),
                new Mage(),
                new Ranger()
            };

            var heroClass = 0;

            var stopParameter = false;
            while(!stopParameter)
            {
                Console.Clear();

                ColorPrints.ColorRed("\n >>>>>>>>>> CHARACTER CREATION <<<<<<<<<<\n");

                Console.WriteLine("\n Insert hero's name: \n");
                var heroName = Console.ReadLine();

                Console.Clear();

                var stopCondition = false;
                while (!stopCondition)
                {
                    ColorPrints.ColorRed("\n >>>>>>>>>> CHARACTER CREATION <<<<<<<<<<\n");

                    Console.WriteLine($" Name\t\t {heroName}");

                    Console.WriteLine("\n Choose your hero's class. Your choices are:\n" +
                    " 1\t Warrior\n" +
                    " 2\t Mage\n" +
                    " 3\t Ranger\n");

                    heroClass = IntegerInput.IntInputAndCheck(1, 3);

                    Console.WriteLine("\n Are you sure that's the class you want?\n" +
                        " 1\t Yes!\n" +
                        " 2\t On second thought... Let me choose again.\n");

                    var confirmClassChoice = IntegerInput.IntInputAndCheck(1, 2);

                    switch (confirmClassChoice)
                    {
                        case 1:
                            stopCondition = true;
                            break;
                        case 2:
                            break;
                    }

                    Console.Clear();
                }

                ColorPrints.ColorRed("\n >>>>>>>>>> CHARACTER CREATION <<<<<<<<<<\n");

                Console.WriteLine($" Name\t\t {heroName}");

                switch (heroClass)
                {
                    case 1:
                        Console.WriteLine(" Class\t\t Warrior\n\n" +
                            " The Warrior's base stats are:\n\n" +
                            "\t Health\t\t 100\n" +
                            "\t Damage\t\t 15\n");

                        heroList[heroClass - 1] = new Warrior
                        {
                            Health = 100,
                            CurrentHealth = 100,
                            Damage = 15,
                            Experience = 0,
                            HeroName = heroName,
                            Level = 1
                        };

                        break;

                    case 2:

                        Console.WriteLine(" Class\t\t Mage\n\n" +
                            " The Mage's base stats are:\n\n" +
                            "\t Health\t\t 50\n" +
                            "\t Damage\t\t 30\n" +
                            "\t Mana\t\t 50\n");

                        heroList[heroClass - 1] = new Mage
                        {
                            Health = 50,
                            CurrentHealth = 50,
                            Damage = 30,
                            Mana = 50,
                            Resurrection = true,
                            Experience = 0,
                            HeroName = heroName,
                            Level = 1
                        };

                        break;

                    case 3:

                        Console.WriteLine(" Class\t\t Ranger\n\n" +
                            " The Ranger's base stats are:\n\n" +
                            "\t Health\t\t 50\n" +
                            "\t Damage\t\t 30\n" +
                            "\t Critical Chance\t\t 5%\n" +
                            "\t Stun Chance\t\t 5%\n");

                        heroList[heroClass - 1] = new Ranger
                        {
                            Health = 75,
                            CurrentHealth = 75,
                            Damage = 25,
                            CriticalChance = 5,
                            StunChance = 5,
                            Experience = 0,
                            HeroName = heroName,
                            Level = 1
                        };

                        break;
                }

                Console.WriteLine(" Because you're super special, you get to allocate an additional 10 points however you'd like.\n" +
                            " The number you input next will be added to your Health stat.\n" +
                            " The remainder of the points (so 10 - what you input) " +
                            "will be added to your Damage.");

                Console.WriteLine(" Would you like to choose your extra stats yourself or have them randomly generated?\n\n" +
                    " 1\t Let me choose\n" +
                    " 2\t Generate for me\n" +
                    " 3\t I don't need extra stats!\n");

                var statInputChoice = IntegerInput.IntInputAndCheck(1, 3);

                var extraPoints = 0;

                switch (statInputChoice)
                {
                    case 1:
                        Console.WriteLine("\n Choose wisely!\n\n" +
                    " How many extra points are you adding to Health? (you can also choose 0)\n");

                        extraPoints = IntegerInput.IntInputAndCheck(0, 10);
                        break;

                    case 2:
                        var r = new Random();
                        extraPoints = r.Next(0, 10);

                        break;

                    case 3:
                        break;
                }

                heroList[heroClass - 1].Health += extraPoints;
                heroList[heroClass - 1].CurrentHealth += extraPoints;
                heroList[heroClass - 1].Damage += 10 - extraPoints;

                Console.Clear();

                ColorPrints.ColorRed("\n >>>>>>>>>> CHARACTER CREATION <<<<<<<<<<\n");

                Console.WriteLine(" This is your hero:\n");

                Console.WriteLine(heroList[heroClass - 1]);

                Console.WriteLine("\n Are you ready for the fight of your life?\n" +
                    " 1\t Let's do this!\n" +
                    " 2\t I don't like this character, make another one!\n" +
                    " 0\t I don't want to play this game.\n");

                var likeCharacterChoice = IntegerInput.IntInputAndCheck(0, 2);

                switch (likeCharacterChoice)
                {
                    case 1:
                        stopParameter = true;
                        break;

                    case 2:
                        break;

                    case 0:
                        stopParameter = true;
                        heroList[heroClass - 1] = null;
                        break;
                }
            }

            return heroList[heroClass - 1];
        }

    }
}
