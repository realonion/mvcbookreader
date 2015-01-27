using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using mvcbookreader.helpers;

namespace mvcbookreader.Models
{
    public class ValidateFileAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return false;
            }

            if (file.ContentLength > BookAnalyzer.MaxSizeInBytes)
            {
                return false;
            }

            return true;
        }
    }
    public class FileModel
    {
        [ValidateFile(ErrorMessage = "Please select a text file smaller than 20MB")]
        public HttpPostedFileBase File { get; set; }
    }
}