using System;

namespace Hangman
{
    abstract class HangmanInterface : HangmanLogic
    {
        string[] hangman = new string[] {
        "\t\t + ------ + \n \t\t |\t  |\n\t\t\t  | <=== This is gibbet!\n\t\t\t  |\n\t\t\t  |\n\t\t\t  |\n\t\t==========\n",
        "\t\t + ------ + \n \t\t |\t  |\n\t\t O\t  |\n\t\t\t  |\n\t\t\t  |\n\t\t\t  |\n\t\t==========\n",
        "\t\t + ------ + \n \t\t |\t  |\n\t\t O\t  |\n\t\t |\\\t  |\n\t\t\t  |\n\t\t\t  |\n\t\t==========\n",
        "\t\t + ------ + \n \t\t |\t  |\n\t\t O\t  |\n\t\t/|\\\t  |\n\t\t |\t  |\n\t\t\t  |\n\t\t==========\n",
        "\t\t + ------ + \n \t\t |\t  |\n\t\t O\t  |\n\t\t/|\\\t  |\n\t\t |\t  |\n\t\t/ \t  |\n\t\t==========\n",
        "\t\t + ------ + \n \t\t |\t  |\n\t\t O\t  |\n\t\t/|\\\t  |\n\t\t |\t  |\n\t\t/ \\\t  |\n\t\t==========\n",
        };

        public HangmanInterface(string filePath) : base(filePath) { }

        public void showHighScore()
        {
            Console.WriteLine("\n\n\t------------------------------------------ High score --------------------------------------------------- ");
            Console.WriteLine("\t| Name\t\t|\tDate\t\t|Elapsed time\t|Guessed city\t\t\t|\tScore\t|\n");     

            foreach (Score scoreInstance in highScores) {
                if (scoreInstance.city.Length < 6) 
                    scoreInstance.city = scoreInstance.city + "\t";

                Console.WriteLine("\t| " + scoreInstance.name + " \t| " + scoreInstance.date + " \t|\t " + scoreInstance.time + "\t| " + scoreInstance.city + "\t\t\t|\t" + scoreInstance.score + "\t|");
            }

            Console.WriteLine("\n\n");
        }
        protected void showWinInterface()
        {
            Console.WriteLine("\n\tYou won! The city was capital of: " + drawnPair.Key + ", " + capitalize(drawnPair.Value) + ".\n");
            Console.WriteLine("\tYou managed to guess city in : " + elapsedTime + " s");
            Console.WriteLine("\tYou guessed after " + hitLetters.ToArray().Length + " letters!");
            Console.WriteLine("\tYou scored: " + countPlayerScore() + " score and saved Hangman!\n");
        }
        protected void showLoseInterface()
        {
            Console.WriteLine("\n\tYou lost :(. The city was: " + capitalize(drawnPair.Value) + ". This city is capital of " + drawnPair.Key + ".\n");
            displayMissedLetters();
            displayMissedCities();
            drawHangman();
        }

        protected void displayLivesLeft()
        {
            if (lives == 1)     
                Console.WriteLine("\n\tYou still have " + lives + " life left.\n");
            else    
                Console.WriteLine("\n\tYou still have " + lives + " lives left.\n");
        }

        protected void displayCurrentResult()
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

        protected void displayMissedLetters()
        {
            Console.Write("\tYour (used && missed) letters: ");
            Console.Write("\t[");
            foreach(char letter in missedLetters)
                Console.Write(" '" + letter + "', ");

            Console.Write("]\n");
        }

        protected void displayMissedCities()
        {
            Console.Write("\tYour (used && missed) cities: ");
            Console.Write("\t[");
            foreach (string city in missedCities)
                Console.Write(" '" + city + "', ");

            Console.Write("]\n\n");
        }

        protected void drawHangman()
        {  
            Console.WriteLine(hangman[getCurrStage()]);
        }

        private int getCurrStage()
        {
            if (lives >= 0)
                return 5 - lives;
            else
                return 5;
        }

        protected void showHint()
        {
            if (lives == 1)
                Console.WriteLine("\tI would like to give you a hint. This city is a capital of " + drawnPair.Key + "\n");
        }

        protected void askIfPlayAgain()
        {
            Console.WriteLine("\tThe game has ended. Would you like to play again?");
            Console.WriteLine("\tEnter 1 to play again or enter 0 to exit game.");
        }

        protected void showRoundHeader()
        {
            Console.WriteLine("\tThe topic of game is: Country capitals on Earth.\n");
            Console.WriteLine(stateMessage);
        }

        protected string capitalize(string city)
        {
            return char.ToUpper(city[0]) + city.ToLower().Substring(1);
        }
    }
}
