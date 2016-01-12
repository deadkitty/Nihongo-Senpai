using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SenpaiCreationKit.Data.WebObjects
{
    public class Lesson
    {
        #region Fields

        public int Id { get; set; }
        public String Name { get; set; }
        public int Type { get; set; }
        public int Size { get; set; }
        
        public virtual List<Word> Words { get; set; }
        public virtual List<Kanji> Kanjis { get; set; }
        public virtual List<Cloze> Clozes { get; set; }

        #endregion

        #region Constructor

        public Lesson()
        {
            Words = new List<Word>();
            Kanjis = new List<Kanji>();
            Clozes = new List<Cloze>();
        }
        
        public Lesson(String properties)
        {
            Fill(properties);
        }

        public Lesson(Lesson other)
        {
            Fill(other);
        }

        #endregion
        
        #region ToString

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// <para>Creates a String to use for export</para>
        /// <para>Export Pattern:</para>
        /// <para>id|name|type|size</para>
        /// </summary>
        public String ToExportString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Id);
            sb.Append("|");
            sb.Append(Name);
            sb.Append("|");
            sb.Append(Type);
            sb.Append("|");
            sb.Append(Size);
            
            return sb.ToString();
        }

        #endregion

        #region Util

        public void Fill(String properties)
        {
            String[] parts = properties.Split('|');
            
            if(parts.Length == 4)
            {
                Name = parts[1];
                Type = Convert.ToInt32(parts[2]);
                Size = Convert.ToInt32(parts[3]);
            }
            else
            {
                Name = parts[0];
                Type = Convert.ToInt32(parts[1]);
                Size = Convert.ToInt32(parts[2]);
            }
        }

        public void Fill(Lesson other)
        {
            Name = other.Name;
            Type = other.Type;
            Size = other.Size;
        }

        #endregion
    }
}