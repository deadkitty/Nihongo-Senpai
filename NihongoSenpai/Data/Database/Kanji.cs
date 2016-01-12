using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable 0649

namespace NihongoSenpai.Data.Database
{
    [Table(Name="Kanjis")]
    public class Kanji
    {
        #region Fields

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int id;

        [Column]
        private int lessonID;
        private EntityRef<Lesson> lesson = new EntityRef<Lesson>();

        [Column]
        public String kanji;

        [Column]
        public String meaning;

        [Column]
        public String onyomi;

        [Column]
        public String kunyomi;

        [Column]
        public String example;

        /// <summary>
        /// not used yet
        /// </summary>
        [Column]
        public String strokeOrder;

        [Column]
        public float eFactor;

        [Column]
        public int lastRound;

        [Column]
        public int nextRound;

        [Column]
        public int timestamp;
        
        #endregion

        #region Properties
        
        [Association(Name="lessonKanjiFK", IsForeignKey=true, ThisKey="lessonID", Storage="lesson")]
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

        #endregion

        #region ToString

        public override string ToString()
        {
            return kanji + " - " + meaning + " - " + onyomi + "、" + kunyomi;
        }
        
        /// <summary>
        /// <para>Creates a String to use for export as txt file.</para>
        /// <para>Export Pattern:</para>
        /// <para>id|lessonId|kanji|meaning|onyomi|kunyomi|example|strokeOrder|efactor|lastRound|nextRound|timestamp</para>
        /// <para>Length = 12</para>
        /// </summary>
        public String ToExportString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(id);
            sb.Append("|");
            sb.Append(lessonID);
            sb.Append("|");
            sb.Append(kanji);
            sb.Append("|");
            sb.Append(meaning);
            sb.Append("|");
            sb.Append(onyomi);
            sb.Append("|");
            sb.Append(kunyomi);
            sb.Append("|");
            sb.Append(example);
            sb.Append("|");
            sb.Append(strokeOrder);
            sb.Append("|");
            sb.Append(eFactor.ToString(CultureInfo.InvariantCulture));
            sb.Append("|");
            sb.Append(lastRound);
            sb.Append("|");
            sb.Append(nextRound);
            sb.Append("|");
            sb.Append(timestamp);
            
            return sb.ToString();
        }
        
        #endregion
        
        #region Util

        public void Fill(String properties)
        {
            String[] parts = properties.Split('|');
            
            if(parts.Length == 12)
            {
                kanji       = parts[2];
                meaning     = parts[3];
                onyomi      = parts[4];
                kunyomi     = parts[5];
                example     = parts[6];
                strokeOrder = parts[7];
                eFactor     = Convert.ToSingle(parts[8], CultureInfo.InvariantCulture);
                lastRound   = Convert.ToInt32 (parts[9]);
                nextRound   = Convert.ToInt32 (parts[10]);
                timestamp   = Convert.ToInt32 (parts[11]);
            }
            else if(parts.Length == 6)
            {
                kanji       = parts[0];
                meaning     = parts[1];
                onyomi      = parts[2];
                kunyomi     = parts[3];
                example     = parts[4];
                strokeOrder = parts[5];
            }
        }

        public void Fill(Kanji other)
        {
            kanji       = other.kanji;
            meaning     = other.meaning;
            onyomi      = other.onyomi;
            kunyomi     = other.kunyomi;
            example     = other.example;
            strokeOrder = other.strokeOrder;
            eFactor     = other.eFactor;
            lastRound   = other.lastRound;
            nextRound   = other.nextRound;
            timestamp   = other.timestamp;
        }

        #endregion
    }
}