using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable 0649

namespace NihongoSenpai.Model.Database
{
    [Table(Name="Words")]
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
            grammar,
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
        
        [Column(IsPrimaryKey = true, IsDbGenerated = false, CanBeNull = false, AutoSync = AutoSync.Never)]
        public int id;
        
        [Column]
        private int lessonID;
        private EntityRef<Lesson> lesson = new EntityRef<Lesson>();

        [Column]
        public String kana;
        
        [Column]
        public String kanji;

        [Column]
        public String translation;

        [Column]
        public String description;
        
        /// <summary>
        /// ATTENTION!!!!
        /// if you create new types, make sure to place them under the others, and do not change
        /// the order in any of the type lists otherwise all types will be propably inconsistent =(
        /// </summary>
        [Column]
        internal int type = (int)EType.other;


        [Column]
        public float eFactorTranslation;

        [Column]
        public int lastRoundTranslation;

        [Column]
        public int nextRoundTranslation;
        

        [Column]
        public float eFactorJapanese;

        [Column]
        public int lastRoundJapanese;

        [Column]
        public int nextRoundJapanese;

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
        [Column]
        private int showFlags = 15;

        [Column]
        public int timeStampJapanese;

        [Column]
        public int timeStampTranslation;

        #endregion

        #region Properties
        
        [Association(Name="lessonWordFK", IsForeignKey=true, ThisKey="lessonID", Storage="lesson")]
        public Lesson Lesson
        {
            get { return lesson.Entity; }
            set { lesson.Entity = value; }
        }

        public EType Type
        {
            get { return (EType)type; }
            set { type = (int)value; }
        }

        /// <summary>
        /// <para>0 - Dont show Description</para>
        /// <para>1 - just show Description for German Word</para>
        /// <para>2 - just show Description for Japanese Word</para>
        /// <para>3 - Always show Description</para>
        /// </summary>
        public EShowFlags ShowDescription
        {
            get { return (EShowFlags)(showFlags & 3); }
            set { showFlags = (showFlags & 12) + (int)value; }
        }

        /// <summary>
        /// <para>0 - Dont show Word</para>
        /// <para>1 - show just German Word</para>
        /// <para>2 - show just Japanese Word</para>
        /// <para>3 - show Both Words</para>
        /// </summary>
        public EShowFlags ShowWord
        {
            get { return (EShowFlags)(showFlags >> 2); }
            set { showFlags = ((int)value << 2) + (showFlags & 3); }
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
            Lesson = lesson;
            lessonID = lesson.id;

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

            sb.Append(id);
            sb.Append("|");
            sb.Append(kana);
            sb.Append("|");
            sb.Append(kanji);
            sb.Append("|");
            sb.Append(translation);

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

            if (Type == EType.grammar)
            {
                sb.AppendLine("Japanisch: " + kana);
                sb.AppendLine("Deutsch: " + translation);
                sb.Append("Beispiel: " + kanji);
            }
            else
            {
                if (kanji != "")
                {
                    sb.Append(kanji);
                    sb.Append(" - ");
                }

                sb.Append(kana);
                sb.Append(" - ");
                sb.Append(translation);
            }

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
            if (description != "")
            {
                if (Type == Word.EType.other)
                {
                    return description;
                }
                return GetTypeString(Type) + " - " + description;
            }
            return GetTypeString(Type);
        }

        /// <summary>
        /// checks if the kanji part is null or not and returns 
        /// either the kanji or the kana part
        /// </summary>
        public string ToJString()
        {
            return kanji == "" ? kana : kanji;
        }

        /// <summary>
        /// <para>Creates a String to use for export as txt file.</para>
        /// <para>Export Pattern:</para>
        /// <para>id|kana|kanji|translation|description|type|eFactorTransl|lastRoundTransl|nextRoundTransl|eFactorJap|lastRoundJap|nextRoundJap|showFlags|timeStampTransl|timeStampJap</para>
        /// <para>Length = 15</para>
        /// </summary>
        public String ToExportString()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.Append(id);
            sb.Append("|");
            sb.Append(kana);
            sb.Append("|");
            sb.Append(kanji);
            sb.Append("|");
            sb.Append(translation);
            sb.Append("|");
            sb.Append(description);
            sb.Append("|");
            sb.Append(type);
            sb.Append("|");
            sb.Append(eFactorTranslation);
            sb.Append("|");
            sb.Append(lastRoundTranslation);
            sb.Append("|");
            sb.Append(nextRoundTranslation);
            sb.Append("|");
            sb.Append(eFactorJapanese);
            sb.Append("|");
            sb.Append(lastRoundJapanese);
            sb.Append("|");
            sb.Append(nextRoundJapanese);
            sb.Append("|");
            sb.Append(showFlags);
            sb.Append("|");
            sb.Append(timeStampTranslation);
            sb.Append("|");
            sb.Append(timeStampJapanese);

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
                switch(i)
                {
                    case  0: id                   = Convert.ToInt32 (parts[i]); break;
                    case  1: kana                 =                  parts[i] ; break;
                    case  2: kanji                =                  parts[i] ; break;
                    case  3: translation          =                  parts[i] ; break;
                    case  4: description          =                  parts[i] ; break;
                    case  5: type                 = Convert.ToInt32 (parts[i]); break;
                    case  6: eFactorTranslation   = Convert.ToSingle(parts[i]); break;
                    case  7: lastRoundTranslation = Convert.ToInt32 (parts[i]); break;
                    case  8: nextRoundTranslation = Convert.ToInt32 (parts[i]); break;
                    case  9: eFactorJapanese      = Convert.ToSingle(parts[i]); break;
                    case 10: lastRoundJapanese    = Convert.ToInt32 (parts[i]); break;
                    case 11: nextRoundJapanese    = Convert.ToInt32 (parts[i]); break;
                    case 12: showFlags            = Convert.ToInt32 (parts[i]); break;
                    case 13: timeStampTranslation = Convert.ToInt32 (parts[i]); break;
                    case 14: timeStampJapanese    = Convert.ToInt32 (parts[i]); break;
                }
            }
        }

        public void Fill(Word other)
        {
            kana                 = other.kana;
            kanji                = other.kanji;
            translation          = other.translation;
            description          = other.description;
            type                 = other.type;
            eFactorTranslation   = other.eFactorTranslation;
            lastRoundTranslation = other.lastRoundTranslation;
            nextRoundTranslation = other.nextRoundTranslation;
            eFactorJapanese      = other.eFactorJapanese;
            lastRoundJapanese    = other.lastRoundJapanese;
            nextRoundJapanese    = other.nextRoundJapanese;
            showFlags            = other.showFlags;
            timeStampTranslation = other.timeStampTranslation;
            timeStampJapanese    = other.timeStampJapanese;
        }

        /// <summary>
        /// returns the bigger value of both timestamps
        /// </summary>
        public int Timestamp()
        {
            return Math.Max(timeStampJapanese, timeStampTranslation);
        }

        #endregion
    }
}