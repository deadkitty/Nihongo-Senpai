using System;
using System.Text;
using System.Globalization;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using Newtonsoft.Json;

#pragma warning disable 0649

namespace SenpaiCreationKit.Data
{
    [Table(Name = "Kanjis")]
    public class Kanji
    {
        #region Fields

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int id { get; set; }
        
        [Column] public String Sign { get; set; }
        [Column] public String Meaning { get; set; }
        [Column] public String Onyomi { get; set; }
        [Column] public String Kunyomi { get; set; }
        [Column] public String Example { get; set; }
        [Column] public String StrokeOrder { get; set; }
        
        [Column]
        private int lessonID { get; set; }
        private EntityRef<Lesson> lesson = new EntityRef<Lesson>();

        #endregion

        #region Properties

        [Association(Name = "lessonKanjiFK", IsForeignKey = true, ThisKey = "lessonID", Storage = "lesson")]
        public Lesson Lesson
        {
            get { return lesson.Entity; }
            set { lesson.Entity = value; }
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

        public Kanji(String properties)
        {
            Fill(properties);
        }

        public Kanji(String properties, Lesson lesson)
        {
            this.Lesson = lesson;
            Fill(properties);
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return Sign + " - " + Meaning + " - " + Onyomi + "、" + Kunyomi;
        }
        
        public String ToDetailString()
        {
            return Sign + " - " + Meaning + " - " + Onyomi + "、" + Kunyomi;
        }

        public String ToExampleString()
        {
            return Example;
        }

        /// <summary>
        /// <para>Creates a String to use for export</para>
        /// <para>Export Pattern:</para>
        /// <para>id|lessonId|Sign|meaning|onyomi|kunyomi|example|strokeOrder|efactor|repetition|nextInterval|timestamp</para>
        /// <para>Length = 12</para>
        /// </summary>
        //public String ToExportString()
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append(id);
        //    sb.Append("|");
        //    sb.Append(lessonID);
        //    sb.Append("|");
        //    sb.Append(Sign);
        //    sb.Append("|");
        //    sb.Append(meaning);
        //    sb.Append("|");
        //    sb.Append(onyomi);
        //    sb.Append("|");
        //    sb.Append(kunyomi);
        //    sb.Append("|");
        //    sb.Append(example);
        //    sb.Append("|");
        //    sb.Append(strokeOrder);
        //    sb.Append("|");
        //    sb.Append(eFactor.ToString(CultureInfo.InvariantCulture));
        //    sb.Append("|");
        //    sb.Append(repetition);
        //    sb.Append("|");
        //    sb.Append(nextInterval);
        //    sb.Append("|");
        //    sb.Append(timestamp);
            
        //    return sb.ToString();
        //}
        
        /// <summary>
        /// <para>Creates a String to use for export without id and supermemo stuff</para>
        /// <para>Export Pattern:</para>
        /// <para>Sign|meaning|onyomi|kunyomi|example|strokeOrder</para>
        /// <para>Length = 6</para>
        /// </summary>
        public String ToCreateNewString()
        {
            StringBuilder sb = new StringBuilder();

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
            sb.Append(StrokeOrder);
            
            return sb.ToString();
        }
        #endregion
        
        #region Util

        public void Fill(String properties)
        {
            String[] parts = properties.Split('|');
            
            if(parts.Length == 12)
            {
                Sign         = parts[2];
                Meaning      = parts[3];
                Onyomi       = parts[4];
                Kunyomi      = parts[5];
                Example      = parts[6];
                StrokeOrder  = parts[7];
            }
            else if(parts.Length == 6)
            {
                Sign        = parts[0];
                Meaning     = parts[1];
                Onyomi      = parts[2];
                Kunyomi     = parts[3];
                Example     = parts[4];
                StrokeOrder = parts[5];
            }
        }

        public void Fill(Kanji other)
        {
            Sign        = other.Sign;
            Meaning     = other.Meaning;
            Onyomi      = other.Onyomi;
            Kunyomi     = other.Kunyomi;
            Example     = other.Example;
            StrokeOrder = other.StrokeOrder;
        }

        #endregion
    }
}