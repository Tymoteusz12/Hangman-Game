using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
    abstract class HangmanLogic
    {
        protected int lives;
        protected string stateMessage;
        protected double elapsedTime;

        protected KeyValuePair<string, string> drawnPair;

        protected bool[] guessArray;

        protected List<char> missedLetters = new List<char>();
        protected List<char> hitLetters = new List<char>();
        protected List<string> missedCities = new List<string>();
        private FilesManager manageFiles;
        private ScoreWeights scoreWeights = new ScoreWeights();

        protected List<Score> highScores;

        protected HangmanLogic(string filePath)
        {
            manageFiles = new FilesManager(filePath);
            highScores = new List<Score>(manageFiles.scores);
            highScores = highScores.OrderByDescending(o => Int32.Parse(o.score)).ToList();
        }

        public void saveScoreToFile()
        {
            manageFiles.saveScoreToFile(highScores);
        }

        protected void drawPair()
        {
            drawnPair = manageFiles.drawCity();
        }

        protected void initGuessArray()
        {
            guessArray = new bool[drawnPair.Value.Length];
            for(int i = 0; i < drawnPair.Value.Length; i++)
            {
                if(drawnPair.Value[i] == ' ')
                    guessArray[i] = true;
                else 
                    guessArray[i] = false;  
            }
        }

        protected void restartAllStates()
        {
            lives = 5;
            stateMessage = "\tLet's play!";
            missedLetters.Clear();
            hitLetters.Clear();
            missedCities.Clear();
            drawPair();
            initGuessArray();
        }

        protected void guessSingleLetter() 
        {
            Console.WriteLine("\tPlease enter your single letter guess: ");
            string playerGuess = EnterInput();
            playerGuess = playerGuess.ToUpper();

            if (validateCharInput(playerGuess))
                checkIfPlayerHit(playerGuess[0]);
        }

        private bool validateCharInput(string playerGuess)
        {
            char playerInput = playerGuess[0];

            if (playerGuess.Length != 1 || (playerInput > 90 || playerInput < 65))
            {
                stateMessage = "\n\tYou entered invalid input! As a result you lost life :(\n"; 
                lives--;
                return false;
            }

            return true;
        }

        private void checkIfPlayerHit(char playerInput)
        {
            List<int> indexesList = new List<int>();
            if (hitLetters.Contains(playerInput)) 
                stateMessage = "\n\tYou already had used this letter and hit!\n";

            else if (missedLetters.Contains(playerInput))  
                stateMessage = "\n\tYou already had used this letter and missed!\n";

            else
            {
                int counter = 0;
                foreach (char letter in drawnPair.Value)
                {
                    if (letter == playerInput)
                        indexesList.Add(counter);

                    counter++;
                }

                if (indexesList.Count > 0) 
                {
                    stateMessage = "\n\tYou hit! Nice!\n";
                    replaceUnderscores(indexesList.ToArray());
                }
                else 
                {
                    stateMessage = "\n\tYou missed and lost life. Try again!\n";
                    lives--;
                    addToMissedLetters(playerInput);
                }
            }
        }

        private void replaceUnderscores(int[] indexesArray)
        {
            foreach (int index in indexesArray)
            {
                hitLetters.Add(drawnPair.Value[index]);
                guessArray[index] = true;
            }
        }

        private void addToMissedLetters(char playerInput)
        {
            if (!missedLetters.Contains(playerInput))
                missedLetters.Add(playerInput);
        }

        protected void guessCity()
        {
            Console.WriteLine("Please enter your city guess: ");
            string cityGuess = EnterInput();
            cityGuess = cityGuess.ToUpper();

            if (validateStringInput(cityGuess))
                checkIfPlayerHit(cityGuess);
        }

        private bool validateStringInput(string playerInput)
        {
            if (playerInput.Length != drawnPair.Value.Length)
            {
                stateMessage = "\n\tLooks like length of your input is invalid and doesn't match with hidden city. As a result you lost two lives!\n";
                lives -= 2;
            }

            return playerInput.Length == drawnPair.Value.Length;
        }

        private void checkIfPlayerHit(string playerInput)
        {
            if (missedCities.Contains(playerInput))
                stateMessage = "\n\tYou already had used this city and missed!\n";

            else
            {
                if (playerInput.Equals(drawnPair.Value))
                {
                    for (int i = 0; i < guessArray.Length; i++)
                        guessArray[i] = true;
                }
                else
                {
                    stateMessage = "\n\tYou entered wrong city! As a result you lost two lives!\n";
                    lives -= 2;
                    addToMissedCities(playerInput);
                }
            }
        }

        private void addToMissedCities(string playerInput)
        {
            if (!missedCities.Contains(playerInput))
                missedCities.Add(playerInput);
        }

        protected int countPlayerScore()
        {
            int score = scoreWeights.letterWeight * drawnPair.Value.Length + scoreWeights.liveWeight * lives - scoreWeights.timeWeight * (int)elapsedTime;
            if (score <= 0)
                score = 0;

            return score;
        }

        public string EnterInput()
        {
            Console.Write(">>>>>>> ");
            return Console.ReadLine();
        }

        protected void takeTopTen()
        {
            highScores = highScores.OrderByDescending(o => Int32.Parse(o.score)).ToList();
            if (highScores.Count > 10)
                highScores = highScores.Take(10).ToList();
        }
    }
}
