using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NihongoSenpaiTsu.Model
{
    [Table("Grammars")]
    public class Grammar
    {
        #region Fields

        [PrimaryKey]
        public int Id { get; set; }
        public int LessonID { get; set; }

        public String ExpressionJap { get; set; }
        public String ExpressionGer { get; set; }
        public String ExampleJap { get; set; }
        public String ExampleGer { get; set; }
        public String Description { get; set; }
        
        public int TimeStampJap { get; set; } = 0;
        public int TimeStampGer { get; set; } = 0;

        public float EFactorJap { get; set; } = 2.5f;
        public int LastRoundJap { get; set; } = 0;
        public int NextRoundJap { get; set; } = 0;

        public float EFactorGer { get; set; } = 2.5f;
        public int LastRoundGer { get; set; } = 0;
        public int NextRoundGer { get; set; } = 0;
        
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

        public Grammar()
        {

        }

        public Grammar(Grammar other)
        {
            Fill(other);
        }

        public Grammar(String properties, Lesson lesson)
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
            return ExpressionJap;
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
            sb.Append(ExpressionJap);
            sb.Append("|");
            sb.Append(ExpressionGer);
            sb.Append("|");
            sb.Append(ExampleJap);
            sb.Append("|");
            sb.Append(ExampleGer);
            sb.Append("|");
            sb.Append(Description);
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
                    case  0: Id            = Convert.ToInt32 (parts[i]); break;

                    case  1: ExpressionJap =                  parts[i];  break;
                    case  2: ExpressionGer =                  parts[i];  break;
                    case  3: ExampleJap    =                  parts[i];  break;
                    case  4: ExampleJap    =                  parts[i];  break;
                    case  5: Description   =                  parts[i];  break;

                    case  6: TimeStampJap  = Convert.ToInt32 (parts[i]); break;
                    case  7: TimeStampGer  = Convert.ToInt32 (parts[i]); break;

                    case  8: EFactorJap    = Convert.ToSingle(parts[i]); break;
                    case  9: LastRoundJap  = Convert.ToInt32 (parts[i]); break;
                    case 10: NextRoundJap  = Convert.ToInt32 (parts[i]); break;

                    case 11: EFactorGer    = Convert.ToSingle(parts[i]); break;
                    case 12: LastRoundGer  = Convert.ToInt32 (parts[i]); break;
                    case 13: NextRoundGer  = Convert.ToInt32 (parts[i]); break;
                }
            }
        }

        public void Fill(Grammar other)
        {
            ExpressionJap = other.ExpressionJap;
            ExpressionGer = other.ExpressionGer;
            ExampleJap    = other.ExampleJap;
            ExampleGer    = other.ExampleGer;
            Description   = other.Description;
    
            TimeStampJap  = other.TimeStampJap;
            TimeStampGer  = other.TimeStampGer;

            EFactorJap    = other.EFactorJap; 
            LastRoundJap  = other.LastRoundJap;
            NextRoundJap  = other.NextRoundJap;

            EFactorGer    = other.EFactorGer;
            LastRoundGer  = other.LastRoundGer;
            NextRoundGer  = other.NextRoundGer; 
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
