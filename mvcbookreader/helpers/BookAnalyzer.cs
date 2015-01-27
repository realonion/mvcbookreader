using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mvcbookreader.helpers
{
    /// <summary>
    /// Analysis all text in a book and provide meta information about the book
    /// </summary>
    class BookAnalyzer
    {
        public static int MaxSizeInBytes = 1024 *1024 * 20; //20MB
        /// <summary>
        /// A dictionary that contains all the unique Word objects that is indexed by the Word's Text.
        /// </summary>
        private Dictionary<string, Word> allWordsInBook = new Dictionary<string, Word>();

        public BookAnalyzer(string text)
        {
            //Regular expression reference: https://msdn.microsoft.com/en-us/library/az24scfc%28v=vs.110%29.aspx
            text = Regex.Replace(text, "\\.|;|:|,|[0-9]|'", "");
            var words = Regex.Matches(text, @"[\w]+", RegexOptions.Multiline);

            allWordsInBook = new Dictionary<string, Word>();
            foreach (var word in words)
            {
                string key = word.ToString().ToLower();
                if (!allWordsInBook.ContainsKey(key))
                {
                    allWordsInBook.Add(key, new Word());
                    allWordsInBook[key].Frequency = 1;
                    allWordsInBook[key].Text = key;
                }
                else
                {
                    allWordsInBook[key].Frequency++;
                }
            }
        }

        /// <summary>
        /// Find most frequent x character word that has occured in the book.
        /// If wordLength is zero, it will find the most frequest word occured in the book.
        /// </summary>
        /// <param name="wordLength">The number of the characters of the word. </param>
        /// <returns>Returns a Word object</returns>
        public Word FindMostFrequentWord(int wordLength)
        {
            var mostFrequentWord = new Word();

            foreach (var word in allWordsInBook.Values)
            {
                //Ignore the words that does not have the length that is interested.
                if (wordLength>0 && word.Text.Length!=wordLength)
                {
                    continue;
                }

                if (word.Frequency >= mostFrequentWord.Frequency)
                {
                    mostFrequentWord.Frequency = word.Frequency;
                    mostFrequentWord.Text = word.Text;
                }
            }

            return mostFrequentWord;
        }

        /// <summary>
        /// Find most frequent word occured in the book.
        /// If there are more than two words that occured the same times, 
        /// it will take the last one that it has found. 
        /// </summary>
        /// <returns></returns>
        public Word FindMostFrequentWord()
        {
            return FindMostFrequentWord(0);
        }

        /// <summary>
        /// Find words in a book that has highest scrabble score
        /// </summary>
        /// <returns></returns>
        public List<Word> FindHighestScoringWords()
        {
            var orderedList = allWordsInBook.Values.OrderByDescending(x => x.ScrabbleScore).ToList();
            var highestScoreWord = orderedList[0];

            var highestScoreWords = new List<Word>();
            for (int i = 0; i < orderedList.ToList().Count(); i++)
            {
                if (orderedList[i].ScrabbleScore == highestScoreWord.ScrabbleScore)
                {
                    highestScoreWords.Add(orderedList[i]);
                }
                else
                {
                    break;
                }
            }
            return highestScoreWords;
        }

        public List<Word> GetWordsOccuredInBook()
        {
            var orderedList = allWordsInBook.Values.OrderByDescending(x => x.Text.Length).ToList();

            return orderedList;
        }

        public static BookAnalyzer CreateFromFileStream(Stream stream)
        {
            if (stream.Length > MaxSizeInBytes)
            {
                throw new Exception(String.Format("File size is greater than {0} bytes", MaxSizeInBytes));
            }

            StreamReader sr = new StreamReader(stream);
            
            string text = sr.ReadToEnd();

            sr.Close();

            return new BookAnalyzer(text);
        }


    }
}
