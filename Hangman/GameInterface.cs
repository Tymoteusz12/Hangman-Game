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
            var watch = System.Diagnostics.Stopwatch.StartNew();
            RestartAllStates();
            StartGameLoop();
            watch.Stop();
            elapsedTime = watch.ElapsedMilliseconds / 1000;
        }

        private void DisplayFinalResult(bool playerWon)
        {
            Console.Clear();
            if (playerWon)
            {
                Console.WriteLine("\n\tYou won! The city was capital of: " + drawnPair.Key + ", " + drawnPair.Value + ".\n");
                Console.WriteLine("You managed to guess city in : " + elapsedTime + " s");
                Console.WriteLine("You guessed after " + hitLetters.ToArray().Length + " letters!");
                DrawHangman();
            }
            else
            {
                Console.WriteLine("\n\tYou lost :(. The city was: " + drawnPair.Value + ". This city is capital of " + drawnPair.Key + ".\n");
                DisplayMissedLetters();
                DisplayMissedCities();
                DrawHangman();
            }
        }

        private void StartGameLoop()
        {
            bool playerWon = false;
            Console.WriteLine("\nGame has started.");
            while (lives > 0 && !playerWon)
            {
                Console.Clear();
                Console.WriteLine("\tThe topic of game is: Country capitals on Earth.\n");

                if (lives >= 1)
                {
                    Console.WriteLine(stateMessage);
                    DisplayLivesLeft();
                    DisplayMissedLetters();
                    DisplayMissedCities();
                    DrawHangman();
                    DisplayCurrentResult();
                    LetUserGuess();
                }

                if (lives == 1) ShowHint();

                playerWon = CheckIfPlayerWon();
            }

            DisplayFinalResult(playerWon);
            AskIfPlayAgain();
        }

        private void DisplayLivesLeft()
        {
            if (lives == 1)     
                Console.WriteLine("\n\tYou still have " + lives + " life left.\n");
            else    
                Console.WriteLine("\n\tYou still have " + lives + " lives left.\n");
        }

        private void DisplayCurrentResult()
        {
            Console.WriteLine("\tCurrent result is: \n");
            Console.Write("\t");
            for (int i = 0; i < drawnPair.Value.Length; i++)
            {
                if (!guessArray[i])
                    Console.Write("_ ");
                else
                    Console.Write(drawnPair.Value[i]);
            }
            Console.WriteLine("\n");
        }

        private void DisplayMissedLetters()
        {
            Console.Write("\tYou used and missed letters: ");
            Console.Write("\t[");
            foreach(char letter in missedLetters)
                Console.Write(" '" + letter + "', ");

            Console.Write("]\n");
        }

        private void DisplayMissedCities()
        {
            Console.Write("\tYou used and missed cities: ");
            Console.Write("\t[");
            foreach (string city in missedCities)
                Console.Write(" '" + city + "', ");

            Console.Write("]\n\n");
        }

        private void DrawHangman()
        {

        }

        private void LetUserGuess()
        {
            Console.WriteLine("\tWould you like to enter single letter or whole city name? ");
            Console.WriteLine("\tEnter 1 for single letter or\n\tEnter 0 for whole city name: ");
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

        private void ShowHint()
        {
            Console.WriteLine("\tI would like to give you a hint. This city is a capital of " + drawnPair.Key + "\n");
        }

        private void AskIfPlayAgain()
        {
            Console.WriteLine("\tThe game has ended. Would you like to play again?");
            Console.WriteLine("\tEnter 1 to play again or enter 0 to exit game.");
        }
    }
}
