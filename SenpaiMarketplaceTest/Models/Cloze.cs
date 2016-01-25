using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace SenpaiMarketplaceTest.Models
{
    public class Cloze : ILessonItem
    {
        #region Fields

        public int Id { get; set; }

        public String Text { get; set; }
        public String Inserts { get; set; }
        public String Hints { get; set; }

        public int LessonID { get; set; }
        public virtual Lesson Lesson { get; set; }

        #endregion

        #region Constructor

        public Cloze()
        {

        }

        public Cloze(String properties)
        {
            Fill(properties);
        }

        public Cloze(String properties, Lesson lesson)
        {
            Lesson = lesson;
            Fill(properties);
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return Text;
        }

        /// <summary>
        /// <para>Creates a String to use for .nya-file export</para>
        /// <para>Export Pattern:</para>
        /// <para>text|insertText|hintText</para>
        /// </summary>
        public String ToCreateNewString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Id);
            sb.Append("|");
            sb.Append(Text);
            sb.Append("|");
            sb.Append(Inserts);
            sb.Append("|");
            sb.Append(Hints);

            return sb.ToString();
        }

        #endregion

        #region Util

        public void Fill(String properties)
        {
            String[] parts = properties.Split('|');

            Id      = Convert.ToInt32(parts[0]);
            Text    = parts[1];
            Inserts = parts[2];
            Hints   = parts[3];
        }

        #endregion
    }
}