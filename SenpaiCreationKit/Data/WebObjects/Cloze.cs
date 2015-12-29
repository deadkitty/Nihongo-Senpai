using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SenpaiCreationKit.Data.WebObjects
{
    public class Cloze
    {
        public int Id { get; set; }

        public String Text { get; set; }
        public String Inserts { get; set; }
        public String Hints { get; set; }
        
        public int LessonID { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}