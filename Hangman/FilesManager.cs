using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Hangman
{
    class Country
    {
        public string name;
        public string capital;

        public Country(string name, string capital)
        {
            this.name = name;
            this.capital = capital;
        }
    }
    class FilesManager
    {
        private string filePath;
        private string[] fileContents;
        private List<Country> countries;

        public FilesManager(string filePath)
        {
            this.filePath = filePath;
            LoadFileData();
            SplitFileData();
        }

        private void LoadFileData()
        {
            fileContents = File.ReadAllLines(this.filePath);
        }

        protected void SplitFileData()
        {
            countries = new List<Country>();

            foreach (string word in fileContents)
                countries.Add(new Country(word.Split(" | ")[0], word.Split(" | ")[1]));
        }

        public KeyValuePair<string, string> DrawCity()
        {
            Random rnd = new Random();
            int randomInteger = rnd.Next(0, fileContents.Length);

            string country = countries[randomInteger].name;
            string capital = countries[randomInteger].capital;

            return new KeyValuePair<string, string>(country, capital);
        }
    }
}
