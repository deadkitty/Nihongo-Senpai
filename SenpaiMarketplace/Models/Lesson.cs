using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace SenpaiMarketplace.Models
{
    public class Lesson
    {
        #region EType

        public enum EType
        {
            vocab,
            insert,
            conjugation,
            kanji,
            grammar,
            count,
            undefined = -1,
        }

        #endregion

        #region Fields
        
        public int Id { get; set; }
        public String Name { get; set; }
        public int Type { get; set; }
        public int Size { get; set; }

        public List<Word>  Words { get; set; }
        public List<Kanji> Kanjis { get; set; }
        public List<Cloze> Clozes { get; set; }

        #endregion

        #region Constructor

        public Lesson()
        {
            Words  = new List<Word>();
            Kanjis = new List<Kanji>();
            Clozes = new List<Cloze>();
        }

        public Lesson(String properties)
        {
            Fill(properties);
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// <para>Creates a String to use for export without id</para>
        /// <para>Export Pattern:</para>
        /// <para>name|type|size</para>
        /// </summary>
        public String ToCreateNewString()
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

            Id   = Convert.ToInt32(parts[0]);
            Name = parts[1];
            Type = Convert.ToInt32(parts[2]);
            Size = Convert.ToInt32(parts[3]);
        }

        public void Fill(Lesson other)
        {
            Name = other.Name;
            Size = other.Size;
            Type = other.Type;
        }

        public void AddItem(String properties)
        {
            switch ((Lesson.EType)Type)
            {
                case Lesson.EType.vocab : Words .Add(new Word (properties)); break;
                case Lesson.EType.kanji : Kanjis.Add(new Kanji(properties)); break;
                case Lesson.EType.insert: Clozes.Add(new Cloze(properties)); break;
            }
        }

        #endregion
    }
}