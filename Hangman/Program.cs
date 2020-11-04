using System;
using System.Collections.Generic;


namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            const string filePath = "./text/countries_and_capitals.txt";

            GameInterface game = new GameInterface(filePath);
           
            Console.WriteLine("\n\tWelcome to Hangman game! Would you like to proceed?\n\tEnter 1 to start the game, enter 2 to show highscores or 0 to exit application.");

            while (game.isRunning)
            {
                switch (Console.ReadLine()) {
                    
                    case "0":
                        game.EndGame();
                        game.SaveScoreToFile();
                        break;

                    case "1":
                        game.StartGame();
                        break;

                    case "2":
                        game.ShowHighScore();
                        Console.WriteLine("\n\tEnter 1 to start the game, enter 2 to show highscores or 0 to exit application.");
                        break;

                    default: 
                        Console.WriteLine("\tYour input seem to be invalid. Try again.");
                        break;
                }
            }
        }
    }
}
