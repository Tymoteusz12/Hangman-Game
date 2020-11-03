using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hangman
{
    class Country
    {
        public string name;
        public string capitol;

        public Country(string name, string capitol)
        {
            this.name = name;
            this.capitol = capitol;
        }
    }

    class GameLogic
    {
        protected int lives = 5;
        private char[] letterArray;
        private string filePath;
        protected string[] fileContents;
        protected List<Country> countries;
        public bool isRunning = true;
        protected KeyValuePair<string, string> drawnPair;
        protected bool[] guessArray;

        public void EndGame()
        {
            isRunning = false;
        }

        protected GameLogic(string filePath)
        {
            this.filePath = filePath;
            LoadFileData();
            SplitFileData();
        }

        protected void SplitFileData()
        {
            countries = new List<Country>();
           
            foreach (string word in fileContents)
            {
                countries.Add(new Country(word.Split(" | ")[0], word.Split(" | ")[1]));
            }
        }

        protected void DrawPair()
        {
            Random rnd = new Random();
            int randomInteger = rnd.Next(0, fileContents.Length);
            string country = countries[randomInteger].name;
            string capitol = countries[randomInteger].capitol;
            drawnPair = new KeyValuePair<string, string>(country, capitol);
        }

        protected void InitGuessArray()
        {
            guessArray = new bool[drawnPair.Value.Length];
            for(int i = 0; i < drawnPair.Value.Length; i++)
            {
                guessArray[i] = false;
            }
        }
        protected void GuessSingleLetter() { }

        protected void GuessCity() { }

        private void LoadFileData()
        {
            fileContents = File.ReadAllLines(this.filePath);
        }
    }
}
