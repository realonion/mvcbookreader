using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcbookreader.helpers
{
    /// <summary>
    /// Word contains meta information about a word that has occured in a book.
    /// </summary>
    class Word
    {
#region Srabble Score Table
        public static Dictionary<char, int> ScrabbleScoreTable = new Dictionary<char, int>() 
        {
            {'a', 1},
            {'b', 3},
            {'c', 3},
            {'d', 2},
            {'e', 1},
            {'f', 4},
            {'g', 2},
            {'h', 4},
            {'i', 1},
            {'j', 8},
            {'k', 5},
            {'l', 1},
            {'m', 3},
            {'n', 1},
            {'o', 1},
            {'p', 3},
            {'q', 10},
            {'r', 1},
            {'s', 1},
            {'t', 1},
            {'u', 1},
            {'v', 4},
            {'w', 4},
            {'x', 8},
            {'y', 4},
            {'z', 10}
        };
#endregion

        private int scrabbleScore;
        private string text;

        /// <summary>
        /// The actual text of the word
        /// </summary>
        public string Text 
        { 
            get { return text; } 
            set 
            {
                scrabbleScore = 0; //reset the score so it will be recalculated.
                text = value; 
            } 
        }
        /// <summary>
        /// The number of the times that the word has occured in the book
        /// </summary>
        public int Frequency { get; set; }
        /// <summary>
        /// Get the scrabble score of a word.  The score will only be calculated once.  
        /// </summary>
        public int ScrabbleScore { 
            get 
            {
                if (scrabbleScore <= 0)
                {
                    scrabbleScore = CalculateScrabbleScore();
                }
                return scrabbleScore;
            }
        }

        private int CalculateScrabbleScore() {

            var text = Text.ToLower();
            var score = 0;
            for (int i = 0; i < Text.Length; i++)
            {
                if (ScrabbleScoreTable.ContainsKey(text[i]))
                {
                    score += ScrabbleScoreTable[text[i]];
                }
            }

            return score;
        }
    }
}
