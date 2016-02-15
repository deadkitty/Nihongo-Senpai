using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NihongoSenpaiTsu.Model
{
    [Table("Kanjis")]
    public class Kanji
    {
        #region Fields

        [PrimaryKey]
        public int Id { get; set; }
        public int LessonID { get; set; }

        public String Sign    { get; set; }
        public String Meaning { get; set; }
        public String Onyomi  { get; set; }
        public String Kunyomi { get; set; }
        public String Example { get; set; }
        
        public float EFactor { get; set; }
        public int LastRound { get; set; }
        public int NextRound { get; set; }

        public int Timestamp { get; set; }

        private Lesson lesson;

        #endregion

        #region Properties

        public Lesson Lesson
        {
            get
            {
                if (lesson == null)
                {
                    lesson = SenpaiDatabase.CurrentConnection.Get<Lesson>(LessonID);
                }
                return lesson;
            }
        }

        #endregion

        #region Constructor

        public Kanji()
        {

        }

        public Kanji(Kanji other)
        {
            Fill(other);
        }

        public Kanji(String properties, Lesson lesson)
        {
            LessonID = lesson.Id;

            Fill(properties);
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return Sign + " - " + Meaning + " - " + Onyomi + "、" + Kunyomi;
        }

        /// <summary>
        /// <para>Creates a String to use for export as txt file.</para>
        /// <para>Export Pattern:</para>
        /// <para>id|kanji|meaning|onyomi|kunyomi|example|efactor|lastRound|nextRound|timestamp</para>
        /// <para>Length = 10</para>
        /// </summary>
        public String ToExportString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Id);
            sb.Append("|");
            sb.Append(Sign);
            sb.Append("|");
            sb.Append(Meaning);
            sb.Append("|");
            sb.Append(Onyomi);
            sb.Append("|");
            sb.Append(Kunyomi);
            sb.Append("|");
            sb.Append(Example);
            sb.Append("|");
            sb.Append(EFactor);
            sb.Append("|");
            sb.Append(LastRound);
            sb.Append("|");
            sb.Append(NextRound);
            sb.Append("|");
            sb.Append(Timestamp);

            return sb.ToString();
        }

        #endregion

        #region Util

        public void Fill(String properties)
        {
            String[] parts = properties.Split('|');

            for (int i = 0; i < parts.Length; ++i)
            {
                switch (i)
                {
                    case 0: Id        = Convert.ToInt32 (parts[i]); break;

                    case 1: Sign      =                  parts[i];  break;
                    case 2: Meaning   =                  parts[i];  break;
                    case 3: Onyomi    =                  parts[i];  break;
                    case 4: Kunyomi   =                  parts[i];  break;
                    case 5: Example   =                  parts[i];  break;

                    case 6: EFactor   = Convert.ToSingle(parts[i]); break;
                    case 7: LastRound = Convert.ToInt32 (parts[i]); break;
                    case 8: NextRound = Convert.ToInt32 (parts[i]); break;

                    case 9: Timestamp = Convert.ToInt32 (parts[i]); break;
                }
            }
        }

        public void Fill(Kanji other)
        {
            Sign      = other.Sign;
            Meaning   = other.Meaning;
            Onyomi    = other.Onyomi;
            Kunyomi   = other.Kunyomi;
            Example   = other.Example;
            EFactor   = other.EFactor;
            LastRound = other.LastRound;
            NextRound = other.NextRound;
            Timestamp = other.Timestamp;
        }

        #endregion
    }
}
