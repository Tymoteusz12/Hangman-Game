using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hangman
{

    class GameInterface : GameLogic
    {
        string[] hangman = new string[] {
        "\t\t + ------ + \n \t\t |\t  |\n\t\t\t  | <=== This is gibbet!\n\t\t\t  |\n\t\t\t  |\n\t\t\t  |\n\t\t==========\n",
        "\t\t + ------ + \n \t\t |\t  |\n\t\t O\t  | <=== This is Hangman! Help him!\n\t\t\t  |\n\t\t\t  |\n\t\t\t  |\n\t\t==========\n",
        "\t\t + ------ + \n \t\t |\t  |\n\t\t O\t  | <=== This is Hangman! Help him!\n\t\t |\\\t  |\n\t\t\t  |\n\t\t\t  |\n\t\t==========\n",
        "\t\t + ------ + \n \t\t |\t  |\n\t\t O\t  | <=== This is Hangman! Help him!\n\t\t/|\\\t  |\n\t\t |\t  |\n\t\t\t  |\n\t\t==========\n",
        "\t\t + ------ + \n \t\t |\t  |\n\t\t O\t  | <=== This is Hangman! Help him!\n\t\t/|\\\t  |\n\t\t |\t  |\n\t\t/ \t  |\n\t\t==========\n",
        "\t\t + ------ + \n \t\t |\t  |\n\t\t O\t  | <=== Luckily this is only puppet!\n\t\t/|\\\t  |\n\t\t |\t  |\n\t\t/ \\\t  |\n\t\t==========\n",
        };

        public bool isRunning = true;
        public GameInterface(string filePath) : base(filePath) { }

        public void StartGame()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            RestartAllStates();
            StartGameLoop();
            watch.Stop();
            elapsedTime = watch.ElapsedMilliseconds / 1000;
            DisplayFinalResult(CheckIfPlayerWon());
            AskIfPlayAgain();
        }
        public void EndGame()
        {
            isRunning = false;
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
                    DrawHangman();
                    DisplayCurrentResult();
                    DisplayMissedLetters();
                    DisplayMissedCities();
                    if (lives == 1) ShowHint();
                    LetUserGuess();
                }
                playerWon = CheckIfPlayerWon();
            }
        }

        public void ShowHighScore()
        {
            Console.WriteLine("\n\t------------------------------------------ High score ---------------------------------------------------- ");
            Console.WriteLine("\t| Name\t\t|\tDate\t\t|\tElapsed time\t|\tGuessed city\t|\tScore\t |\n");

            foreach (Score scoreInstance in highScores)
                Console.WriteLine( "\t| " + scoreInstance.name + " \t| " + scoreInstance.date + " \t|\t " + scoreInstance.time + " \t\t|\t " + scoreInstance.city + "      \t|\t" + scoreInstance.score + "\t |");
        }

        private void DisplayFinalResult(bool playerWon)
        {
            Console.Clear();

            if (playerWon)
            {
                ShowWinInterface();
                ShowHighScore();
                SaveScore();
                highScores = highScores.Take(10).ToList();
            }

            else
            {
                ShowHighScore();
                ShowLoseInterface();
            }
        }

        private void ShowWinInterface()
        {
            Console.WriteLine("\n\tYou won! The city was capital of: " + drawnPair.Key + ", " + drawnPair.Value + ".\n");
            Console.WriteLine("\tYou managed to guess city in : " + elapsedTime + " s");
            Console.WriteLine("\tYou guessed after " + hitLetters.ToArray().Length + " letters!");
            Console.WriteLine("\tYou scored: " + CountPlayerScore() + " score and saved Hangman!\n");
        }

        private void SaveScore()
        {
            Console.WriteLine("\n\tWould you like to save your score? Enter 1 for yes, enter 0 for no: ");
            switch (Console.ReadLine())
            {
                case "1":
                    EnterData();
                    break;
                case "0":
                    break;
                default:
                    break;
            }
        }

        private void EnterData()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();

            highScores.Add(new Score(name, DateTime.Now.ToString(), ((int)elapsedTime).ToString(), drawnPair.Value, CountPlayerScore().ToString()));
        }

        private void ShowLoseInterface()
        {
            Console.WriteLine("\n\tYou lost :(. The city was: " + drawnPair.Value + ". This city is capital of " + drawnPair.Key + ".\n");
            DisplayMissedLetters();
            DisplayMissedCities();
            DrawHangman();
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
            int position;
            if (lives >= 0)
                position = 5 - lives;
            else
                position = 5;
            Console.WriteLine(hangman[position]);
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
