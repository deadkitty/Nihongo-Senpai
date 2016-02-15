using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NihongoSenpaiTsu.Model
{
    [Table("Words")]
    public class Word
    {
        #region EType

        public enum EType
        {
            noun,
            verb1,
            verb2,
            verb3,
            iAdjective,
            naAdjective,
            adverb,
            particle,
            other,
            suffix,
            prefix,
            phrase,
            count,
            undefined = -1,
        }

        #endregion

        #region EShowFlags

        public enum EShowFlags
        {
            dontShow,
            justGerman,
            justJapanese,
            showBoth,
            count,
            undefined = -1,
        }

        #endregion

        #region Fields

        [PrimaryKey]
        public int Id { get; set; }        
        public int LessonID { get; set; }

        public String Kana { get; set; }
        public String Kanji { get; set; }
        public String Translation { get; set; }
        public String Description { get; set; }

        public int Type { get; set; } = (int)EType.other;
        
        public int TimeStampJap { get; set; } = 0;
        public int TimeStampGer { get; set; } = 0;

        public float EFactorJap { get; set; } = 2.5f;
        public int LastRoundJap { get; set; } = 0;
        public int NextRoundJap { get; set; } = 0;

        public float EFactorGer { get; set; } = 2.5f;
        public int LastRoundGer { get; set; } = 0;
        public int NextRoundGer { get; set; } = 0;

        /// <summary>
        /// <para>Bit 0 and 1</para>
        /// <para>--00 - Dont show Description</para>
        /// <para>--01 - just show Description for German Word</para>
        /// <para>--10 - just show Description for Japanese Word</para>
        /// <para>--11 - Always show Description</para>
        /// <para>Bit 2 and 3</para>
        /// <para>00-- - Dont show Word</para>
        /// <para>01-- - show just German Word</para>
        /// <para>10-- - show just Japanese Word</para>
        /// <para>11-- - show Both Words</para>
        /// </summary>
        private int ShowFlags { get; set; } = 15;

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

        /// <summary>
        /// <para>0 - Dont show Description</para>
        /// <para>1 - just show Description for German Word</para>
        /// <para>2 - just show Description for Japanese Word</para>
        /// <para>3 - Always show Description</para>
        /// </summary>
        public EShowFlags ShowDescription
        {
            get { return (EShowFlags)(ShowFlags & 3); }
            set { ShowFlags = (ShowFlags & 12) + (int)value; }
        }

        /// <summary>
        /// <para>0 - Dont show Word</para>
        /// <para>1 - show just German Word</para>
        /// <para>2 - show just Japanese Word</para>
        /// <para>3 - show Both Words</para>
        /// </summary>
        public EShowFlags ShowWord
        {
            get { return (EShowFlags)(ShowFlags >> 2); }
            set { ShowFlags = ((int)value << 2) + (ShowFlags & 3); }
        }

        #endregion

        #region Constructor

        public Word()
        {

        }

        public Word(Word other)
        {
            Fill(other);
        }

        public Word(String properties, Lesson lesson)
        {
            LessonID = lesson.Id;

            Fill(properties);
        }

        #endregion

        #region ToString

        /// <summary>
        /// kana|kanji|translation
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Id);
            sb.Append("|");
            sb.Append(Kana);
            sb.Append("|");
            sb.Append(Kanji);
            sb.Append("|");
            sb.Append(Translation);

            return sb.ToString();
        }

        /// <summary>
        /// <para>Creates a string containing kanji,kana and translation of the given word</para>
        /// <para>Patterns:</para>
        /// <para>1. kanji - kana - translation</para>
        /// <para>2. kana - translation</para>
        /// </summary>
        public String ToDetailString()
        {
            StringBuilder sb = new StringBuilder();
            
            if (Kanji != "")
            {
                sb.Append(Kanji);
                sb.Append(" - ");
            }

            sb.Append(Kana);
            sb.Append(" - ");
            sb.Append(Translation);
            
            return sb.ToString();
        }

        /// <summary>
        /// <para>Creates a string containing type and or description of the given word</para>
        /// <para>Patterns:</para>
        /// <para>1. type - description</para>
        /// <para>2. type</para>
        /// <para>3. description</para>
        /// </summary>
        public String ToDescriptionString()
        {
            if (Description != "")
            {
                if ((EType)Type == Word.EType.other)
                {
                    return Description;
                }
                return GetTypeString((EType)Type) + " - " + Description;
            }
            return GetTypeString((EType)Type);
        }

        /// <summary>
        /// checks if the kanji part is null or not and returns 
        /// either the kanji or the kana part
        /// </summary>
        public string ToJString()
        {
            return Kanji == "" ? Kana : Kanji;
        }

        /// <summary>
        /// <para>Creates a String to use for export as txt file.</para>
        /// <para>Export Pattern:</para>
        /// <para>Id|Kana|Kanji|Translation|Description|Type|TimeStampJap|TimeStampGer|EFactorJap|LastRoundJap|NextRoundJap|EFactorGer|LastRoundGer|NextRoundGer|ShowFlags</para>
        /// <para>Length = 15</para>
        /// </summary>
        public String ToExportString()
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
            sb.Append("|");
            sb.Append(TimeStampJap);
            sb.Append("|");
            sb.Append(TimeStampGer);
            sb.Append("|");
            sb.Append(EFactorJap);
            sb.Append("|");
            sb.Append(LastRoundJap);
            sb.Append("|");
            sb.Append(NextRoundJap);
            sb.Append("|");
            sb.Append(EFactorGer);
            sb.Append("|");
            sb.Append(LastRoundGer);
            sb.Append("|");
            sb.Append(NextRoundGer);
            sb.Append("|");
            sb.Append(ShowFlags);

            return sb.ToString();
        }

        /// <summary>
        /// returns a type string by the given type
        /// </summary>
        /// <param name="returnOther">if true, the type other returns "Sonstige" as string, otherwise it returns an empty string</param>
        public static String GetTypeString(EType type, bool returnOther = false)
        {
            switch (type)
            {
                case EType.noun       : return "Nomen";
                case EType.verb1      : return "う-Verb";
                case EType.verb2      : return "る-Verb";
                case EType.verb3      : return "する-Verb";
                case EType.iAdjective : return "い-Adj";
                case EType.naAdjective: return "な-Adj";
                case EType.adverb     : return "Adverb";
                case EType.particle   : return "Partikel";
                case EType.other      : return returnOther ? "Sonstige" : "";
                case EType.prefix     : return "Präfix";
                case EType.suffix     : return "Suffix";
                case EType.phrase     : return "Phrase";
                default               : return "";
            }
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
                    case  0: Id           = Convert.ToInt32 (parts[i]); break;
                    case  1: Kana         =                  parts[i];  break;
                    case  2: Kanji        =                  parts[i];  break;
                    case  3: Translation  =                  parts[i];  break;
                    case  4: Description  =                  parts[i];  break;
                    case  5: Type         = Convert.ToInt32 (parts[i]); break;

                    case  6: TimeStampJap = Convert.ToInt32 (parts[i]); break;
                    case  7: TimeStampGer = Convert.ToInt32 (parts[i]); break;

                    case  8: EFactorJap   = Convert.ToSingle(parts[i]); break;
                    case  9: LastRoundJap = Convert.ToInt32 (parts[i]); break;
                    case 10: NextRoundJap = Convert.ToInt32 (parts[i]); break;

                    case 11: EFactorGer   = Convert.ToSingle(parts[i]); break;
                    case 12: LastRoundGer = Convert.ToInt32 (parts[i]); break;
                    case 13: NextRoundGer = Convert.ToInt32 (parts[i]); break;

                    case 14: ShowFlags    = Convert.ToInt32 (parts[i]); break;
                }
            }
        }

        public void Fill(Word other)
        {
            Kana         = other.Kana;
            Kanji        = other.Kanji;
            Translation  = other.Translation;
            Description  = other.Description;
            Type         = other.Type;

            TimeStampJap = other.TimeStampJap;
            TimeStampGer = other.TimeStampGer;

            EFactorJap   = other.EFactorJap;
            LastRoundJap = other.LastRoundJap;
            NextRoundJap = other.NextRoundJap;

            EFactorGer   = other.EFactorGer;
            LastRoundGer = other.LastRoundGer;
            NextRoundGer = other.NextRoundGer;

            ShowFlags    = other.ShowFlags;
        }

        /// <summary>
        /// returns the bigger value of both timestamps
        /// </summary>
        public int Timestamp()
        {
            return Math.Max(TimeStampJap, TimeStampGer);
        }

        #endregion
    }
}
