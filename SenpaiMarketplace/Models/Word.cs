using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace SenpaiMarketplace.Models
{
    public class Word : ILessonItem
    {
        #region Fields

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        /// <summary>
        /// <para>Creates a String to use for .nya-file export</para>
        /// <para>Export Pattern:</para>
        /// <para>id|kana|kanji|translation|description|type</para>
        /// <para>Length = 6</para>
        /// </summary>
        public String ToCreateNewString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Id);
            sb.Append("|");
            sb.Append(Kana);
            sb.Append("|");
            sb.Append(Kanji);
            sb.Append("|");
            sb.Append(Translation);
            sb.Append("|");
            sb.Append(Description);
            sb.Append("|");
            sb.Append(Type);

            return sb.ToString();
        }

        #endregion

        #region Util

        public void Fill(String properties)
        {
            String[] parts = properties.Split('|');

            Id          = Convert.ToInt32(parts[0]);
            Kana        = parts[1];
            Kanji       = parts[2];
            Translation = parts[3];
            Description = parts[4];
            Type        = Convert.ToInt32(parts[5]);
        }


        #endregion
    }
}