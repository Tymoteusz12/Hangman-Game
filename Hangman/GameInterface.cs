using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hangman
{
    class GameInterface : GameLogic
    {
        double elapsedTime;
        public GameInterface(string filePath) : base(filePath){ }
        
        public void StartGame()
        {
            DrawPair();
            Console.WriteLine("Game has started. The topic of game is: Country capitols on Earth.");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            InitGuessArray();
            StartGameLoop();
            watch.Stop();
            elapsedTime = watch.ElapsedMilliseconds / 1000;
            DisplayFinalResult();
        }

        private void DisplayFinalResult()
        {
            Console.WriteLine(elapsedTime);
        }

        private void StartGameLoop()
        {

            for(int i = 0; i < drawnPair.Value.Length; i++)
            {
                if (!guessArray[i])
                {
                    Console.Write(" _ ");
                }
                else
                {
                    Console.WriteLine(drawnPair.Value[i]);
                }
            }
        }

        
    }
}
