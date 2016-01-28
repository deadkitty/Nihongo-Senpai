using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            grammar,
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

        [Column]
        public String Kana { get; set; }
        [Column]
        public String Kanji { get; set; }
        [Column]
        public String Translation { get; set; }
        [Column]
        public String Description { get; set; }

        [Column]
        public int Type { get; set; }

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

        #endregion

        #region Constructor

        public Word()
        {

        }

        public Word(String properties)
        {
            Fill(properties);
        }

        public Word(String properties, Lesson lesson)
        {
            Lesson = lesson;
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
        /// <para>Creates a String to use for .nya-file export</para>
        /// <para>Export Pattern:</para>
        /// <para>id|kana|kanji|translation|description|type</para>
        /// <para>Length = 6</para>
        /// </summary>
        public String ToCreateNewString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(id);
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

        /// <summary>
        /// returns a type string by the given type
        /// </summary>
        /// <param name="returnOther">if true, the type other returns "Sonstige" as string, otherwise it returns an empty string</param>
        public static String GetTypeString(EType type, bool returnOther = false)
        {
            switch (type)
            {
                case EType.noun: return "Nomen";
                case EType.verb1: return "う-Verb";
                case EType.verb2: return "る-Verb";
                case EType.verb3: return "する-Verb";
                case EType.iAdjective: return "い-Adj";
                case EType.naAdjective: return "な-Adj";
                case EType.adverb: return "Adverb";
                case EType.particle: return "Partikel";
                case EType.other: return returnOther ? "Sonstige" : "";
                case EType.prefix: return "Präfix";
                case EType.suffix: return "Suffix";
                case EType.phrase: return "Phrase";
                case EType.grammar:return "Grammatik";
                default: return "";
            }
        }

        public void Fill(String properties)
        {
            String[] parts = properties.Split('|');

            Kana        = parts[1];
            Kanji       = parts[2];
            Translation = parts[3];
            Description = parts[4];
            Type        = Convert.ToInt32(parts[5]);
        }

        #endregion
    }
}
