using DungeonCrawler.Data;
using DungeonCrawler.Data.Enums;
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
            var newHero = new Hero();

            var isDoneWithCharacterCreation = false;

            while(!isDoneWithCharacterCreation)
            {
                Console.Clear();

                var heroName = ChooseHeroName();

                Console.Clear();
                
                newHero = ChooseClass(heroName);

                ChooseExtraStats(newHero);

                Console.Clear();

                ColorPrints.ColorRed("\n >>>>>>>>>> CHARACTER CREATION <<<<<<<<<<\n");

                Console.WriteLine(" This is your final hero:\n");

                Console.WriteLine(newHero);

                Console.WriteLine("\n Are you ready for the fight of your life?\n" +
                                    " 1\t Let's do this!\n" +
                                    " 2\t I don't like this character, make a new one!\n" +
                                    " 0\t I don't want to play this game.\n");

                var likeCharacterChoice = Input.IntInputAndCheck(0, 2);

                switch (likeCharacterChoice)
                {
                    case 1:

                        isDoneWithCharacterCreation = true;
                        break;

                    case 2:

                        Console.Clear();

                        Console.WriteLine("\n You will be returned to the Character Creation screen.\n" +
                            " Press any key to continue\n");
                        Console.ReadKey();

                        Console.Clear();
                        break;

                    case 0:

                        isDoneWithCharacterCreation = true;
                        newHero = null;

                        Console.Clear();

                        Console.WriteLine("\n Sorry to see you leave. :(\n" +
                                            " Grab a cup of coffee and come again.\n" +
                                            " I'll be waiting here...\n");
                        Console.ReadKey();

                        Console.Clear();

                        Console.WriteLine("\n\n\n\tForever.\n");
                        Console.ReadKey();

                        Console.Clear();

                        break;
                }
            }

            return newHero;
        }

        public static Hero ChooseClass(string heroName)
        {
            var heroClass = new HeroClass();

            var isHeroHappyWithClass = false;

            do
            {
                ColorPrints.ColorRed("\n >>>>>>>>>> CHARACTER CREATION <<<<<<<<<<\n");

                Console.WriteLine($" Name\t\t {heroName}");

                Console.WriteLine("\n Choose your hero's class. Your choices are:\n" +
                                " 1\t Warrior\n" +
                                " 2\t Mage\n" +
                                " 3\t Ranger\n");

                var heroClassChoice = Input.IntInputAndCheck(1, 3);

                Console.WriteLine("\n Are you sure that's the class you want?\n" +
                                    " 1\t Yes!\n" +
                                    " 2\t On second thought... Let me choose again.\n");

                var confirmClassChoice = Input.IntInputAndCheck(1, 2);

                switch (confirmClassChoice)
                {
                    case 1:
                        heroClass = (HeroClass) heroClassChoice;
                        isHeroHappyWithClass = true;
                        break;

                    case 2:
                        break;
                }

                Console.Clear();

            } while (!isHeroHappyWithClass);

            var newHero = new Hero();

            switch (heroClass)
            {
                case HeroClass.Warrior:

                    newHero = new Warrior(heroName)
                    {
                        HeroClass = HeroClass.Warrior
                    };

                    break;

                case HeroClass.Mage:

                    newHero = new Mage(heroName)
                    {
                        HeroClass = HeroClass.Mage
                    };

                    break;

                case HeroClass.Ranger:

                    newHero = new Ranger(heroName)
                    {
                        HeroClass = HeroClass.Ranger
                    };

                    break;
            }

            return newHero;
        }



        public static string ChooseHeroName()
        {
            var heroName = "";
            var isHappyWithName = false;

            do
            {
                ColorPrints.ColorRed("\n >>>>>>>>>> CHARACTER CREATION <<<<<<<<<<\n");

                Console.WriteLine("\n Insert the hero's name: \n");

                heroName = Console.ReadLine();

                Console.WriteLine($"\n Are you sure you want to name your hero {heroName} ?\n" +
                    $" 1\t Yeah!\n" +
                    $" 2\t No, let me choose again.\n");

                var changeNameChoice = Input.IntInputAndCheck(1, 2);

                switch(changeNameChoice)
                {
                    case 1:
                        isHappyWithName = true;
                        break;

                    case 2:
                        Console.Clear();
                        break;

                }

            } while (!isHappyWithName);

            return heroName;
        }



        public static void ChooseExtraStats(Hero hero)
        {
            ColorPrints.ColorRed("\n >>>>>>>>>> CHARACTER CREATION <<<<<<<<<<\n");

            Console.WriteLine(" Your hero's current stats:\n");
            Console.WriteLine(hero);

            Console.WriteLine("\n Because you're super special, you get to allocate\n" +
                                " an additional 15 points to Health or 5 points to Damage.\n");

            Console.WriteLine(" Would you like to make this choice yourself, or have it randomly assigned?\n\n" +
                                " 1\t I'm responsible enough (let me choose)\n" +
                                " 2\t Help me please (choose for me)\n" +
                                " 3\t I'm not special, let me out! (you get nothing)\n");

            var statInputChoice = Input.IntInputAndCheck(1, 3);

            var extraStatsChoice = 0;

            switch (statInputChoice)
            {
                case 1:
                    Console.WriteLine("\n Choose wisely!\n\n" +
                                        " 1\t Add 15 points to Health\n" +
                                        " 2\t Add 5 points to Damage\n");

                    extraStatsChoice = Input.IntInputAndCheck(1, 2);

                    break;

                case 2:
                    extraStatsChoice = StaticRandom.GetRandom(1, 2);
                    break;

                case 3:
                    break;
            }

            switch(extraStatsChoice)
            {
                case 1:
                    hero.Health += 15;
                    hero.CurrentHealth += 15;
                    break;
                case 2:
                    hero.Damage += 5;
                    break;
            }

        }
    }
}
