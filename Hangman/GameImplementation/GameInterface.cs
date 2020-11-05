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
        "\t\t + ------ + \n \t\t |\t  |\n\t\t O\t  |\n\t\t\t  |\n\t\t\t  |\n\t\t\t  |\n\t\t==========\n",
        "\t\t + ------ + \n \t\t |\t  |\n\t\t O\t  |\n\t\t |\\\t  |\n\t\t\t  |\n\t\t\t  |\n\t\t==========\n",
        "\t\t + ------ + \n \t\t |\t  |\n\t\t O\t  |\n\t\t/|\\\t  |\n\t\t |\t  |\n\t\t\t  |\n\t\t==========\n",
        "\t\t + ------ + \n \t\t |\t  |\n\t\t O\t  |\n\t\t/|\\\t  |\n\t\t |\t  |\n\t\t/ \t  |\n\t\t==========\n",
        "\t\t + ------ + \n \t\t |\t  |\n\t\t O\t  |\n\t\t/|\\\t  |\n\t\t |\t  |\n\t\t/ \\\t  |\n\t\t==========\n",
        };

        public bool isRunning = true;
        public GameInterface(string filePath) : base(filePath) { }

        public void startGame()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            restartAllStates();
            startGameLoop();
            watch.Stop();
            elapsedTime = watch.ElapsedMilliseconds / 1000;
            displayFinalResult(checkIfPlayerWon());
            askIfPlayAgain();
        }
        public void endGame()
        {
            isRunning = false;
        }

        private void startGameLoop()
        {
            bool playerWon = false;
            while (lives > 0 && !playerWon)
            {
                Console.Clear();
                Console.WriteLine("\tThe topic of game is: Country capitals on Earth.\n");
                Console.WriteLine(stateMessage);

                displayLivesLeft();
                drawHangman();
                displayCurrentResult();
                displayMissedLetters();
                displayMissedCities();
                if (lives == 1) showHint();
                letUserChooseTypeOfGuess();

                playerWon = checkIfPlayerWon();
            }
        }

        public void showHighScore()
        {
            Console.WriteLine("\n\n\t------------------------------------------ High score --------------------------------------------------- ");
            Console.WriteLine("\t| Name\t\t|\tDate\t\t|Elapsed time\t|Guessed city\t\t\t|\tScore\t|\n");
            
            string city;
            foreach (Score scoreInstance in highScores) {
                if (scoreInstance.city.Length < 6) scoreInstance.city = scoreInstance.city + "\t";
                Console.WriteLine("\t| " + scoreInstance.name + " \t| " + scoreInstance.date + " \t|\t " + scoreInstance.time + "\t| " + scoreInstance.city + "\t\t\t|\t" + scoreInstance.score + "\t|");
            }

            Console.WriteLine("\n\n");
        }

        private void displayFinalResult(bool playerWon)
        {
            Console.Clear();
            showHighScore();
            if (playerWon)
            {
                showWinInterface();
                saveScore();
                if(highScores.Count > 10)
                    highScores = highScores.Take(10).ToList();
            }

            else
                showLoseInterface();

        }

        private void showWinInterface()
        {

            Console.WriteLine("\n\tYou won! The city was capital of: " + drawnPair.Key + ", " + capitalize(drawnPair.Value) + ".\n");
            Console.WriteLine("\tYou managed to guess city in : " + elapsedTime + " s");
            Console.WriteLine("\tYou guessed after " + hitLetters.ToArray().Length + " letters!");
            Console.WriteLine("\tYou scored: " + countPlayerScore() + " score and saved Hangman!\n");
        }

        private void saveScore()
        {
            Console.WriteLine("\n\tWould you like to save your score? Enter 1 for yes, enter 0 for no: ");
            switch (EnterInput())
            {
                case "1":
                    enterData();
                    Console.WriteLine("\tI saved your score to the table of glory!\n");
                    break;
                case "0":
                    break;
                default:
                    break;
            }
        }

        private void enterData()
        {
            Console.WriteLine("\tEnter your name: ");
            string name = EnterInput();

            if(name.Length >= 9)
                name = name.Substring(0, 9) + "...";

            highScores.Add(new Score(name, DateTime.Now.ToString(), ((int)elapsedTime).ToString(), capitalize(drawnPair.Value), countPlayerScore().ToString()));
        }

        private void showLoseInterface()
        {
            Console.WriteLine("\n\tYou lost :(. The city was: " + capitalize(drawnPair.Value) + ". This city is capital of " + drawnPair.Key + ".\n");
            displayMissedLetters();
            displayMissedCities();
            drawHangman();
        }

        private void displayLivesLeft()
        {
            if (lives == 1)     
                Console.WriteLine("\n\tYou still have " + lives + " life left.\n");
            else    
                Console.WriteLine("\n\tYou still have " + lives + " lives left.\n");
        }

        private void displayCurrentResult()
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

        private void displayMissedLetters()
        {
            Console.Write("\tYour (used && missed) letters: ");
            Console.Write("\t[");
            foreach(char letter in missedLetters)
                Console.Write(" '" + letter + "', ");

            Console.Write("]\n");
        }

        private void displayMissedCities()
        {
            Console.Write("\tYour (used && missed) cities: ");
            Console.Write("\t[");
            foreach (string city in missedCities)
                Console.Write(" '" + city + "', ");

            Console.Write("]\n\n");
        }

        private void drawHangman()
        {
            int stage;
            if (lives >= 0)
                stage = 5 - lives;
            else
                stage = 5;
            Console.WriteLine(hangman[stage]);
        }

        private void letUserChooseTypeOfGuess()
        {
            Console.WriteLine("\tWould you like to enter single letter or whole city name? ");
            Console.WriteLine("\tEnter 1 for single letter or\n\tEnter 0 for whole city name: ");
            switch (EnterInput())
            {
                case "1":
                    guessSingleLetter();
                    break;
                case "0":
                    guessCity();
                    break;
            }
        }

        private void showHint()
        {
            Console.WriteLine("\tI would like to give you a hint. This city is a capital of " + drawnPair.Key + "\n");
        }

        private void askIfPlayAgain()
        {
            Console.WriteLine("\tThe game has ended. Would you like to play again?");
            Console.WriteLine("\tEnter 1 to play again or enter 0 to exit game.");
        }

        private string capitalize(string city)
        {
            return char.ToUpper(city[0]) + city.ToLower().Substring(1);
        }
    }
}
