using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
    class GameLogic
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
        private ScoreWeights scoreWeights;

        protected List<Score> highScores;

        protected GameLogic(string filePath)
        {
            missedLetters = new List<char>();
            hitLetters = new List<char>();
            missedCities = new List<string>();
            manageFiles = new FilesManager(filePath);
            scoreWeights = new ScoreWeights();
            highScores = new List<Score>(manageFiles.scores).OrderByDescending(o => o.score).ToList();
        }

        public void SaveScoreToFile()
        {
            manageFiles.SaveScoreToFile(highScores);
        }

        protected void DrawPair()
        {
            drawnPair = manageFiles.DrawCity();
        }

        protected void InitGuessArray()
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

        protected void RestartAllStates()
        {
            lives = 5;
            stateMessage = "\tLet's play!";
            missedLetters.Clear();
            hitLetters.Clear();
            missedCities.Clear();
            DrawPair();
            InitGuessArray();
        }

        protected void GuessSingleLetter() 
        {
            Console.WriteLine("\tPlease enter your single letter guess: ");
            string playerGuess = Console.ReadLine();
            playerGuess = playerGuess.ToUpper();
            if (ValidateCharInput(playerGuess))
                CheckIfPlayerHit(playerGuess[0]);
        }

        private bool ValidateCharInput(string playerGuess)
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

        private void CheckIfPlayerHit(char playerInput)
        {
            List<int> indexesList = new List<int>();
            int counter = 0;
            if (hitLetters.Contains(playerInput)) 
                stateMessage = "\n\tYou already had used this letter and hit!\n";

            else if (missedLetters.Contains(playerInput))  
                stateMessage = "\n\tYou already had used this letter and missed!\n";

            else
            {
                foreach (char letter in drawnPair.Value)
                {
                    if (letter == playerInput)
                        indexesList.Add(counter);

                    counter++;
                }

                if (indexesList.Count > 0) 
                {
                    stateMessage = "\n\tYou hit! Nice!\n";
                    ReplaceUnderscores(indexesList.ToArray());
                }
                else 
                {
                    stateMessage = "\n\tYou missed and lost life. Try again!\n";
                    lives--;
                    AddToMissedLetters(playerInput);
                }
            }
        }

        private void ReplaceUnderscores(int[] indexesArray)
        {
            foreach (int index in indexesArray)
            {
                hitLetters.Add(drawnPair.Value[index]);
                guessArray[index] = true;
            }
        }

        private void AddToMissedLetters(char playerInput)
        {
            if (!missedLetters.Contains(playerInput))
                missedLetters.Add(playerInput);
        }

        protected void GuessCity()
        {
            Console.WriteLine("Please enter your city guess: ");
            string cityGuess = Console.ReadLine();
            cityGuess = cityGuess.ToUpper();
            if (ValidateStringInput(cityGuess))
                CheckIfPlayerHit(cityGuess);
        }

        private bool ValidateStringInput(string playerInput)
        {
            if (playerInput.Length != drawnPair.Value.Length)
            {
                stateMessage = "\n\tLooks like length of your input is invalid and doesn't match with hidden city. As a result you lost two lives!\n";
                lives -= 2;
            }
            return playerInput.Length == drawnPair.Value.Length;
        }

        private void CheckIfPlayerHit(string playerInput)
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
                    AddToMissedCities(playerInput);
                }
            }
        }

        private void AddToMissedCities(string playerInput)
        {
            if (!missedCities.Contains(playerInput))
                missedCities.Add(playerInput);
        }

        protected bool CheckIfPlayerWon()
        {
            foreach(bool hitResult in guessArray) 
            {
                if (!hitResult)
                    return false;
            }
            return true; 
        }

        protected int CountPlayerScore()
        {
            int score = scoreWeights.LetterWeight * drawnPair.Value.Length + scoreWeights.LiveWeight * lives - scoreWeights.TimeWeight * (int)elapsedTime;
            return score;
        }
    }
}
