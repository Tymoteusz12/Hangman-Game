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
                switch (Console.ReadLine()) {

                    case "1":
                        game.StartGame();
                        break;

                    case "0":
                        game.EndGame();
                        break;

                    default: 
                        Console.WriteLine("Your input seem to be invalid. Try again.");
                        break;
                }
            }
        }
    }
}
