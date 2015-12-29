﻿using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;

#pragma warning disable 0649

namespace SenpaiCreationKit.Data
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
        private float eFactorTranslation;

        [Column]
        private int repetitionTranslation;

        [Column]
        private int nextIntervalTranslation;
        

        [Column]
        private float eFactorJapanese;

        [Column]
        private int repetitionJapanese;

        [Column]
        private int nextIntervalJapanese;

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
        private int timeStampJapanese;

        [Column]
        private int timeStampTransl;

        
        public EAnswerState answerState = EAnswerState.none;

        public bool showJWord = false;

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
        public int ShowDescription
        {
            get { return showFlags & 3; }
            set { showFlags = (showFlags & 12) + value; }
        }

        /// <summary>
        /// <para>0 - Dont show Word</para>
        /// <para>1 - show just German Word</para>
        /// <para>2 - show just Japanese Word</para>
        /// <para>3 - show Both Words</para>
        /// </summary>
        public int ShowWord
        {
            get { return showFlags >> 2; }
            set { showFlags = (value << 2) + (showFlags & 3); }
        }

        public float EFactor
        {
            get { return showJWord ? eFactorJapanese : eFactorTranslation; }
            set 
            { 
                if(showJWord)
                {
                    eFactorJapanese = value;
                }
                else
                {
                    eFactorTranslation = value;
                }
            }
        }

        public int Repetition
        {
            get { return showJWord ? repetitionJapanese : repetitionTranslation; }
            set 
            { 
                if(showJWord)
                {
                    repetitionJapanese = value;
                }
                else
                {
                    repetitionTranslation = value;
                }
            }
        }

        public int NextInterval
        {
            get { return showJWord ? nextIntervalJapanese : nextIntervalTranslation; }
            set 
            { 
                if(showJWord)
                {
                    nextIntervalJapanese = value;
                }
                else
                {
                    nextIntervalTranslation = value;
                }
            }
        }

        public int TimeStamp
        {
            get { return showJWord ? timeStampJapanese : timeStampTransl; }
            set
            {
                if(showJWord)
                {
                    timeStampJapanese = value;
                }
                else
                {
                    timeStampTransl = value;
                }
            }
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

        public Word(String properties)
        {
            Fill(properties);
        }

        #endregion

        #region ToString

        /// <summary>
        /// kana|kanji|translation|showJWord|answerState
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(kana);

            sb.Append("|");
            sb.Append(kanji);
            sb.Append("|");
            sb.Append(translation);
            sb.Append("|");
            //sb.Append(showJWord);
            //sb.Append("|");
            //sb.Append(answerState);

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

            if (kanji != "")
            {
                sb.Append(kanji);
                sb.Append(" - ");
            }

            sb.Append(kana);
            sb.Append(" - ");
            sb.Append(translation);
            
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
        /// <para>Creates a String to use for export</para>
        /// <para>Export Pattern:</para>
        /// <para>id|lessonId|kana|kanji|translation|description|type|eFactorTransl|repetitionTransl|nextIntervTransl|eFactorJap|repetitionJap|nextIntervJap|showFlags|timeStampTransl|timeStampJap</para>
        /// <para>Length = 16</para>
        /// </summary>
        public String ToExportString()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.Append(id);
            sb.Append("|");
            sb.Append(lessonID);
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
            sb.Append(repetitionTranslation);
            sb.Append("|");
            sb.Append(nextIntervalTranslation);
            sb.Append("|");
            sb.Append(eFactorJapanese);
            sb.Append("|");
            sb.Append(repetitionJapanese);
            sb.Append("|");
            sb.Append(nextIntervalJapanese);
            sb.Append("|");
            sb.Append(showFlags);
            sb.Append("|");
            sb.Append(timeStampTransl);
            sb.Append("|");
            sb.Append(timeStampJapanese);

            return sb.ToString();
        }
        
        /// <summary>
        /// <para>Creates a String to use for export without id and supermemo related stuff</para>
        /// <para>Export Pattern:</para>
        /// <para>kana|kanji|translation|description|type</para>
        /// <para>Length = 5</para>
        /// </summary>
        public String ToCreateNewString()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.Append(kana);
            sb.Append("|");
            sb.Append(kanji);
            sb.Append("|");
            sb.Append(translation);
            sb.Append("|");
            sb.Append(description);
            sb.Append("|");
            sb.Append(type);
            
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
            
            //from export string
            if(parts.Length == 16)
            {
                kana                    = parts[2];
                kanji                   = parts[3];
                translation             = parts[4];
                description             = parts[5];
                type                    = Convert.ToInt32 (parts[6]);
                eFactorTranslation      = Convert.ToSingle(parts[7]);
                repetitionTranslation   = Convert.ToInt32 (parts[8]);
                nextIntervalTranslation = Convert.ToInt32 (parts[9]);
                eFactorJapanese         = Convert.ToSingle(parts[10]);
                repetitionJapanese      = Convert.ToInt32 (parts[11]);
                nextIntervalJapanese    = Convert.ToInt32 (parts[12]);
                showFlags               = Convert.ToInt32 (parts[13]);
                timeStampJapanese       = Convert.ToInt32 (parts[14]);
                timeStampTransl         = Convert.ToInt32 (parts[15]);
            }
            //from update word
            else if(parts.Length == 6)
            {
                kana        = parts[0];
                kanji       = parts[1];
                translation = parts[2];
                description = parts[3];
                type        = Convert.ToInt32 (parts[4]);
            }
            //from add new word
            else if(parts.Length == 5)
            {
                kana        = parts[0];
                kanji       = parts[1];
                translation = parts[2];
                description = parts[3];
                type        = Convert.ToInt32(parts[4]);
            }

        }

        public void Fill(Word other)
        {
            kana                    = other.kana;
            kanji                   = other.kanji;
            translation             = other.translation;
            description             = other.description;
            type                    = other.type;
            eFactorTranslation      = other.eFactorTranslation;
            repetitionTranslation   = other.repetitionTranslation;
            nextIntervalTranslation = other.nextIntervalTranslation;
            eFactorJapanese         = other.eFactorJapanese;
            repetitionJapanese      = other.repetitionJapanese;
            nextIntervalJapanese    = other.nextIntervalJapanese;
            showFlags               = other.showFlags;
            timeStampJapanese       = other.timeStampJapanese;
            timeStampTransl         = other.timeStampTransl;
        }
        
        public void ToggleJWord()
        {
            showJWord = !showJWord;
        }

        /// <summary>
        /// returns the bigger value of both timestamps
        /// </summary>
        public int Timestamp()
        {
            if(timeStampJapanese > timeStampTransl)
            {
                return timeStampJapanese;
            }
            else
            {
                return timeStampTransl;
            }
        }

        public void CorrectAnswered()
        {
            switch(answerState)
            {
                case Word.EAnswerState.none: answerState = showJWord ? Word.EAnswerState.ger : Word.EAnswerState.jap; break;
                case Word.EAnswerState.ger : answerState = Word.EAnswerState.both; break;
                case Word.EAnswerState.jap : answerState = Word.EAnswerState.both; break;
            }
       }

        #endregion
    }
}
