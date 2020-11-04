using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman
{ 
    public class Country
    {
        public string name;
        public string capital;

        public Country(string name, string capital)
        {
            this.name = name;
            this.capital = capital;
        }
    }

    public class Score
    {
        public string name;
        public string date;
        public string time;
        public string city;
        public string score;

        public Score(string name, string date, string time, string city, string score)
        {
            this.name = name;
            this.date = date;
            this.time = time;
            this.city = city;
            this.score = score;
        }

    }
    class ScoreWeights
    {
        public ScoreWeights(int LiveWeight = 16, int TimeWeight = 2, int LetterWeight = 36)
        {
            this.LiveWeight = LiveWeight;
            this.TimeWeight = TimeWeight;
            this.LetterWeight = LetterWeight;
        }

        public int LiveWeight;
        public int TimeWeight;
        public int LetterWeight;
    }
}
