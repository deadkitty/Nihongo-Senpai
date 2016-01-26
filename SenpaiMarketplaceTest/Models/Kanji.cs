using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace SenpaiMarketplaceTest.Models
{
    public class Kanji : ILessonItem
    {
        #region Fields

        public int Id { get; set; }

        public String kanji { get; set; }
        public String Meaning { get; set; }
        public String Onyomi { get; set; }
        public String Kunyomi { get; set; }
        public String Example { get; set; }
        public String StrokeOrder { get; set; }

        public int LessonID { get; set; }
        public virtual Lesson Lesson { get; set; }
        
        #endregion
        
        #region Constructor

        public Kanji()
        {

        }

        public Kanji(String properties)
        {
            Fill(properties);
        }
        
        #endregion

        #region ToString

        public override string ToString()
        {
            return kanji + " - " + Meaning + " - " + Onyomi + "、" + Kunyomi;
        }

        /// <summary>
        /// <para>Creates a String to use for .nya-file export</para>
        /// <para>Export Pattern:</para>
        /// <para>Sign|meaning|onyomi|kunyomi|example|strokeOrder</para>
        /// <para>Length = 7</para>
        /// </summary>
        public String ToCreateNewString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Id);
            sb.Append("|");
            sb.Append(kanji);
            sb.Append("|");
            sb.Append(Meaning);
            sb.Append("|");
            sb.Append(Onyomi);
            sb.Append("|");
            sb.Append(Kunyomi);
            sb.Append("|");
            sb.Append(Example);
            sb.Append("|");
            sb.Append(StrokeOrder);

            return sb.ToString();
        }

        #endregion

        #region Util

        public void Fill(String properties)
        {
            String[] parts = properties.Split('|');

            Id          = Convert.ToInt32(parts[0]);
            kanji        = parts[1];
            Meaning     = parts[2];
            Onyomi      = parts[3];
            Kunyomi     = parts[4];
            Example     = parts[5];
            StrokeOrder = parts[6];
        }

        #endregion
    }
}