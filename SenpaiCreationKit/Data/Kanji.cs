using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiCreationKit.Data
{
    [Table(Name = "Kanjis")]
    public class Kanji
    {
        #region Fields

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int id { get; set; }

        [Column]
        public String Sign { get; set; }
        [Column]
        public String Meaning { get; set; }
        [Column]
        public String Onyomi { get; set; }
        [Column]
        public String Kunyomi { get; set; }
        [Column]
        public String Example { get; set; }
        [Column]
        public String StrokeOrder { get; set; }

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

        public Kanji(String properties)
        {
            Fill(properties);
        }

        public Kanji(String properties, Lesson lesson)
        {
            Lesson = lesson;
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
        /// <para>Creates a String to use for .nya-file export</para>
        /// <para>Export Pattern:</para>
        /// <para>Sign|meaning|onyomi|kunyomi|example|strokeOrder</para>
        /// <para>Length = 7</para>
        /// </summary>
        public String ToCreateNewString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(id);
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
            sb.Append(StrokeOrder);

            return sb.ToString();
        }

        #endregion

        #region Util

        public void Fill(String properties)
        {
            String[] parts = properties.Split('|');

            Sign        = parts[1];
            Meaning     = parts[2];
            Onyomi      = parts[3];
            Kunyomi     = parts[4];
            Example     = parts[5];
            StrokeOrder = parts[6];
        }

        #endregion
    }
}
