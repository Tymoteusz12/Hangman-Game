using System;
using System.Collections.Generic;
using System.IO;

namespace Hangman
{
    class FilesManager
    {
        private string filePath;
        private string scoreFilePath;
        private string[] fileContents;
        private string[] scoreFileContents;
        private List<Country> countries;
        public List<Score> scores;

        public FilesManager(string filePath, string scoreFilePath = "./text/HighScores.txt")
        {
            this.filePath = filePath;
            this.scoreFilePath = scoreFilePath;
            LoadFilesData();
            SplitCountryData();
            SplitScoresData();
        }

        public void SaveScoreToFile(List<Score> scoresToSave)
        {
            List<string> linesToWrite = new List<string>();
            foreach (Score scoreInstance in scoresToSave) 
                linesToWrite.Add(scoreInstance.name + " | " + scoreInstance.date + " | " + scoreInstance.time + " | " + scoreInstance.city + " | " + scoreInstance.score);

            File.WriteAllLines(scoreFilePath, linesToWrite.ToArray());
        }

        private void LoadFilesData()
        {
            fileContents = File.ReadAllLines(filePath);
            scoreFileContents = File.ReadAllLines(scoreFilePath);
        }

        private void SplitCountryData()
        {
            countries = new List<Country>();

            foreach (string line in fileContents)
                countries.Add(new Country(line.Split(" | ")[0], line.Split(" | ")[1]));
        }

        private void SplitScoresData()
        {
            scores = new List<Score>();

            foreach (string line in scoreFileContents)
                scores.Add(new Score(line.Split(" | ")[0], line.Split(" | ")[1], line.Split(" | ")[2], line.Split(" | ")[3], line.Split(" | ")[4]));
        }

        public KeyValuePair<string, string> DrawCity()
        {
            Random rnd = new Random();
            int randomInteger = rnd.Next(0, fileContents.Length);

            string country = countries[randomInteger].name.ToUpper();
            string capital = countries[randomInteger].capital.ToUpper();

            return new KeyValuePair<string, string>(country, capital);
        }
    }
}
