using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace NihongoSenpaiTsu.Model
{
    [Table("Lessons")]
    public class Lesson
    {
        #region EType

        public enum EType
        {
            vocab,
            kanji,
            grammar,
            count,
            undefined = -1,
        }
        
        #endregion
                
        #region Fields

        [PrimaryKey]
        public int Id { get; set; }
        public String Name { get; set; }
        public int Type { get; set; }
        public int Size { get; set; }

        private List<Word> words = null;
        private List<Kanji> kanjis = null;
        private List<Grammar> grammars = null;

        #endregion

        #region Properties
        
        public List<Grammar> Grammars
        {
            get
            {
                if (grammars == null)
                {
                    grammars = new List<Grammar>(SenpaiDatabase.CurrentConnection.Table<Grammar>().Where(x => x.LessonID == Id));
                }
                return grammars;
            }
        }

        public List<Kanji> Kanjis
        {
            get
            {
                if (kanjis == null)
                {
                    kanjis = new List<Kanji>(SenpaiDatabase.CurrentConnection.Table<Kanji>().Where(x => x.LessonID == Id));
                }
                return kanjis;
            }
        }

        public List<Word> Words
        {
            get
            {
                if (words == null)
                {
                    words = new List<Word>(SenpaiDatabase.CurrentConnection.Table<Word>().Where(x => x.LessonID == Id));
                }
                return words;
            }
        }
        
        #endregion

        #region Constructor

        public Lesson()
        {

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
        /// <para>Creates a String to use for export as txt file.</para>
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

        #region Helper

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

        #endregion
    }
}
