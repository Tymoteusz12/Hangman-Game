using System;
using System.Collections.Generic;


namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            const string filePath = "./FilesManagment/text/countries_and_capitals.txt";

            GameInterface game = new GameInterface(filePath);
           
            Console.WriteLine("\n\tWelcome to Hangman game! Would you like to proceed?\n\tEnter 1 to start the game, enter 2 to show highscores or 0 to exit application.");

            while (game.isRunning)
            {
                switch (game.EnterInput()) {
                    
                    case "0":
                        game.endGame();
                        game.saveScoreToFile();
                        Console.WriteLine("\n\tThank you for playing!");
                        break;

                    case "1":
                        game.startGame();
                        break;

                    case "2":
                        game.showHighScore();
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
