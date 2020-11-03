using System;
using System.Collections.Generic;


namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            const string filePath = "countries_and_capitals.txt";

            GameInterface game = new GameInterface(filePath);
           
            Console.WriteLine("Welcome to Hangman game! Would you like to proceed?\nPress 1 to start the game, and 0 to exit application.");

            while (game.isRunning)
            {
                string selection = Console.ReadLine();

                if (selection == "1")
                {
                    game.StartGame();
                }
                else if (selection == "0")
                {
                    game.EndGame();
                }
                else
                {
                    Console.WriteLine("Your input seem to be invalid. Try again.");
                }
            }
        }
    }
}
