using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mvcbookreader.helpers;

namespace mvcbookreader.Models
{
    public class BookAnalyzerResultModel
    {
        public WordModel MostFrqWord { get; set; }
        public WordModel MostFrq7CharWord { get; set; }

        public List<WordModel> ListOfHighestScoreWords = new List<WordModel>();
    }

    public class WordModel
    {
        public WordModel(string text, int freq, int scroe)
        {
            Text = text;
            Frequency = freq;
            Score = scroe;
        }
        public string Text {get;set;}
        public int Frequency {get;set;}
        public int Score {get;set;} 
        
    }
}