using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcbookreader.helpers;
using mvcbookreader.Models;

namespace mvcbookreader.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new FileModel();
            return View(model);
        }

        public ActionResult Upload(FileModel fileModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", fileModel);
            }

            if (Request.Files.Count < 0)
            {
                if (!String.IsNullOrEmpty(ViewBag.ErrorMessage))
                {
                    ViewBag.ErrorMessage = "Unable to fine the file, try to select a smaller file.";
                    return View("Index");
                }
            }

            var file = Request.Files[0];
            var model = new BookAnalyzerResultModel();
            var analyzer = BookAnalyzer.CreateFromFileStream(file.InputStream);

            //Find most frequent word occured in the book
            var mostFrqWord = analyzer.FindMostFrequentWord();
            model.MostFrqWord = new WordModel(mostFrqWord.Text, mostFrqWord.Frequency, mostFrqWord.ScrabbleScore);

            //Find most frequent 7 character word occured in the book
            mostFrqWord = analyzer.FindMostFrequentWord(7);
            model.MostFrq7CharWord = new WordModel(mostFrqWord.Text, mostFrqWord.Frequency, mostFrqWord.ScrabbleScore);

            //Get a list of highest scoring words
            var words = analyzer.FindHighestScoringWords();
            foreach (var word in words)
            {
                model.ListOfHighestScoreWords.Add(new WordModel(word.Text, word.Frequency, word.ScrabbleScore));
            }

            return View(model);
        }

    }
}