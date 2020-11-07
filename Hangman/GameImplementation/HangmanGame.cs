using System;

namespace Hangman
{
    class HangmanGame : HangmanInterface
    {
        public bool isRunning = true;
        public HangmanGame(string filePath) : base(filePath) { }
        public void endGame()
        {
            isRunning = false;
        }

        public void startGame()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            restartAllStates();
            mainGameLoop();
            getElapsedTime(watch);
            displayFinalResult(checkIfPlayerWon());
            askIfPlayAgain();
        }

        private void mainGameLoop()
        {
            while (gameShouldRun())
            {
                Console.Clear();
                showRoundHeader();
                displayLivesLeft();
                drawHangman();
                displayCurrentResult();
                displayMissedLetters();
                displayMissedCities();
                showHint();
                letUserChooseTypeOfGuess();
            }
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
        protected void displayFinalResult(bool playerWon)
        {
            Console.Clear();
            showHighScore();
            if (playerWon)
            {
                showWinInterface();
                saveScore();
                takeTopTen();
            }

            else
                showLoseInterface();
        }
        protected void saveScore()
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

            if (name.Length > 9)
                name = name.Substring(0, 8) + " ...";

            highScores.Add(new Score(name, DateTime.Now.ToString(), ((int)elapsedTime).ToString(), capitalize(drawnPair.Value), countPlayerScore().ToString()));
        }
        private void getElapsedTime(System.Diagnostics.Stopwatch watch)
        {
            watch.Stop();
            elapsedTime = watch.ElapsedMilliseconds / 1000;
        }

        private bool gameShouldRun()
        {
            return lives > 0 && !checkIfPlayerWon();
        }
        protected bool checkIfPlayerWon()
        {
            foreach (bool hitResult in guessArray)
            {
                if (!hitResult)
                    return false;
            }
            return true;
        }
    }
}
