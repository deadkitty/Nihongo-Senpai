using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SenpaiCreationKit.Data.WebObjects
{
    public class Kanji
    {
        public int Id { get; set; }

        public String Sign { get; set; }
        public String Meaning { get; set; }
        public String Onyomi { get; set; }
        public String Kunyomi { get; set; }
        public String Example { get; set; }
        public String StrokeOrder { get; set; }
        
        public int LessonID { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}