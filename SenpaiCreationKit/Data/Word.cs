using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;

namespace SenpaiCreationKit.Data
{
    [Table(Name = "Words")]
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

        #region EAnswerState

        public enum EAnswerState
        {
            none,
            jap,
            ger,
            both,
            count,
            undefined = -1,
        }

        #endregion

        #region Fields

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int id { get; set; }

        [Column] public String Kana { get; set; }
        [Column] public String Kanji { get; set; }
        [Column] public String Translation { get; set; }
        [Column] public String Description { get; set; }

        [Column] public int Type { get; set; }

        [Column]
        private int lessonID { get; set; }
        private EntityRef<Lesson> lesson = new EntityRef<Lesson>();

        #endregion

        #region Properties

        [Association(Name = "lessonWordFK", IsForeignKey = true, ThisKey = "lessonID", Storage = "lesson")]
        public Lesson Lesson
        {
            get { return lesson.Entity; }
            set { lesson.Entity = value; }
        }

        //public EType Type
        //{
        //    get { return (EType)type; }
        //    set { type = (int)value; }
        //}

        #endregion

        #region Constructor

        public Word()
        {

        }

        public Word(Word other)
        {
            Fill(other);
        }

        public Word(String properties)
        {
            Fill(properties);
        }

        public Word(String properties, Lesson lesson)
        {
            this.Lesson = lesson;
            Fill(properties);
        }

        #endregion

        #region ToString

        /// <summary>
        /// id|lessonID|kana|kanji|translation|Type
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(id);
            sb.Append("|");
            sb.Append(lessonID);
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
                if (Type == (int)Word.EType.other)
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
        /// <para>Creates a String to use for export</para>
        /// <para>Export Pattern:</para>
        /// <para>id|lessonId|kana|kanji|translation|description|type|eFactorTransl|repetitionTransl|nextIntervTransl|eFactorJap|repetitionJap|nextIntervJap|showFlags|timeStampTransl|timeStampJap</para>
        /// <para>Length = 16</para>
        /// </summary>
        //public String ToExportString()
        //{
        //    StringBuilder sb = new StringBuilder();
            
        //    sb.Append(id);
        //    sb.Append("|");
        //    sb.Append(lessonID);
        //    sb.Append("|");
        //    sb.Append(kana);
        //    sb.Append("|");
        //    sb.Append(kanji);
        //    sb.Append("|");
        //    sb.Append(translation);
        //    sb.Append("|");
        //    sb.Append(description);
        //    sb.Append("|");
        //    sb.Append(type);
        //    sb.Append("|");
        //    sb.Append(eFactorTranslation);
        //    sb.Append("|");
        //    sb.Append(repetitionTranslation);
        //    sb.Append("|");
        //    sb.Append(nextIntervalTranslation);
        //    sb.Append("|");
        //    sb.Append(eFactorJapanese);
        //    sb.Append("|");
        //    sb.Append(repetitionJapanese);
        //    sb.Append("|");
        //    sb.Append(nextIntervalJapanese);
        //    sb.Append("|");
        //    sb.Append(showFlags);
        //    sb.Append("|");
        //    sb.Append(timeStampTransl);
        //    sb.Append("|");
        //    sb.Append(timeStampJapanese);

        //    return sb.ToString();
        //}
        
        /// <summary>
        /// <para>Creates a String to use for export without id and supermemo related stuff</para>
        /// <para>Export Pattern:</para>
        /// <para>kana|kanji|translation|description|type</para>
        /// <para>Length = 5</para>
        /// </summary>
        public String ToCreateNewString()
        {
            StringBuilder sb = new StringBuilder();
            
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
        

        /// <summary>
        /// returns a type string by the given type
        /// </summary>
        /// <param name="returnOther">if true, the type other returns "Sonstige" as string, otherwise it returns an empty string</param>
        public static String GetTypeString(Word.EType type, bool returnOther = false)
        {
            switch (type)
            {
                case Word.EType.noun       : return "Nomen";
                case Word.EType.verb1      : return "う-Verb";
                case Word.EType.verb2      : return "る-Verb";
                case Word.EType.verb3      : return "Irreguläres Verb";
                case Word.EType.iAdjective : return "い-Adj";
                case Word.EType.naAdjective: return "な-Adj";
                case Word.EType.adverb     : return "Adverb";
                case Word.EType.particle   : return "Partikel";
                case Word.EType.other      : return returnOther ? "Sonstige" : "";
                case Word.EType.prefix     : return "Präfix";
                case Word.EType.suffix     : return "Suffix";
                case Word.EType.phrase     : return "Phrase";
                default                    : return "";
            }
        }

        #endregion

        #region Util

        public void Fill(String properties)
        {
            String[] parts = properties.Split('|');

            //from update word
            Kana = parts[2];
            Kanji = parts[3];
            Translation = parts[4];
            Description = parts[5];
            Type = Convert.ToInt32(parts[6]);
        }

        public void Fill(Word other)
        {
            id = other.id;
            lessonID = other.lessonID;
            Lesson = other.Lesson;

            Kana = other.Kana;
            Kanji = other.Kanji;
            Translation = other.Translation;
            Description = other.Description;
            Type = other.Type;
        }

        #endregion
    }
}
