using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hangman
{
    class GameInterface : GameLogic
    {
        double elapsedTime;
        public GameInterface(string filePath) : base(filePath) { }

        public void StartGame()
        {
            DrawPair();
            Console.WriteLine("Game has started. The topic of game is: Country capitols on Earth.");

            var watch = System.Diagnostics.Stopwatch.StartNew();
            InitGuessArray();
            StartGameLoop();
            watch.Stop();

            elapsedTime = watch.ElapsedMilliseconds / 1000;
            DisplayFinalResult();
        }

        private void DisplayFinalResult()
        {
            Console.WriteLine(elapsedTime);
        }

        private void StartGameLoop()
        {
            bool playerCanGuess = true;

            while (playerCanGuess)
            {
                DisplayCurrentResult();
                LetUserGuess();

                if (lives <= 0)
                {
                    PlayerHasLost();
                    playerCanGuess = false;
                }
            }
        }

        private void DisplayCurrentResult()
        {
            Console.WriteLine("Current result is: ");
            for (int i = 0; i < drawnPair.Value.Length; i++)
            {
                if (!guessArray[i])
                {
                    Console.Write(" _ ");
                }
                else
                {
                    Console.Write(drawnPair.Value[i]);
                }
            }
        }

        private void LetUserGuess()
        {
            Console.WriteLine("Would you like to enter single letter or whole city name? ");
            Console.WriteLine("Enter 1 for single letter or\nEnter 0 for whole city name: ");

            switch (Console.ReadLine())
            {
                case "1":
                    GuessSingleLetter();
                    break;
                case "0":
                    GuessCity();
                    break;
            }
        }

        private void PlayerHasLost()
        {

        }

        
    }
}
