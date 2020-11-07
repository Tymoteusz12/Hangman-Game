namespace Hangman
{ 
    class Score
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
        public ScoreWeights(int liveWeight = 48, int timeWeight = 2, int letterWeight = 96)
        {
            this.liveWeight = liveWeight;
            this.timeWeight = timeWeight;
            this.letterWeight = letterWeight;
        }
        public int liveWeight;
        public int timeWeight;
        public int letterWeight;
    }
}
