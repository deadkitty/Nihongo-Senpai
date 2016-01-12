using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace SenpaiMarketplace.Models
{
    public class Word
    {
        #region Fields

        public int Id { get; set; }

        public String Kana { get; set; }
        public String Kanji { get; set; }
        public String Translation { get; set; }
        public String Description { get; set; }

        public int Type { get; set; }

        public int LessonID { get; set; }
        public virtual Lesson Lesson { get; set; }

        #endregion
        
        #region Constructor

        public Word()
        {

        }

        public Word(String properties)
        {
            Fill(properties);
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Id);
            sb.Append("|");
            sb.Append(LessonID);
            sb.Append("|");
            sb.Append(Kana);
            sb.Append("|");
            sb.Append(Kanji);
            sb.Append("|");
            sb.Append(Translation);
            sb.Append("|");
            sb.Append(Type);

            return sb.ToString();
        }
        
        #endregion
        
        #region Util

        public void Fill(String properties)
        {
            String[] parts = properties.Split('|');
            
            //from update word
            Kana        = parts[0];
            Kanji       = parts[1];
            Translation = parts[2];
            Description = parts[3];
            Type        = Convert.ToInt32(parts[4]);
        }

        #endregion
    }
}