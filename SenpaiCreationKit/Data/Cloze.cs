using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;

#pragma warning disable 0649

namespace SenpaiCreationKit.Data
{
    [Table(Name="Clozes")]
    public class Cloze
    {
        #region Fields

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int id;

        [Column]
        private int lessonID;
        private EntityRef<Lesson> lesson = new EntityRef<Lesson>();

        [Column]
        public String text;

        [Column]
        public String inserts;

        [Column]
        public String hints;
        
        #endregion

        #region Properties
        
        [Association(Name="lessonClozeFK", IsForeignKey=true, ThisKey="lessonID", Storage="lesson")]
        public Lesson Lesson
        {
            get { return lesson.Entity; }
            set { lesson.Entity = value; }
        }
        
        #endregion

        #region Constructor

        public Cloze()
        {

        }

        public Cloze(Cloze other)
        {
            Fill(other);
        }

        public Cloze(String properties)
        {
            Fill(properties);
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return text;
        }

        /// <summary>
        /// <para>Creates a String to use for export</para>
        /// <para>Export Pattern:</para>
        /// <para>id|lessonID|text|insertText|hintText</para>
        /// </summary>
        public String ToExportString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(id);
            sb.Append("|");
            sb.Append(lessonID);
            sb.Append("|");
            sb.Append(text);
            sb.Append("|");
            sb.Append(inserts);
            sb.Append("|");
            sb.Append(hints);

            return sb.ToString();
        }

        /// <summary>
        /// <para>Creates a String to use for export without id stuff</para>
        /// <para>Export Pattern:</para>
        /// <para>text|insertText|hintText</para>
        /// </summary>
        public String ToCreateNewString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(text);
            sb.Append("|");
            sb.Append(inserts);
            sb.Append("|");
            sb.Append(hints);

            return sb.ToString();
        }

        #endregion

        #region Util
        
        public void Fill(String properties)
        {
            String[] parts = properties.Split('|');
            
            //from export string
            if(parts.Length == 5)
            {
                text    = parts[2];
                inserts = parts[3];
                hints   = parts[4];
            }
            else //from add new string
            {
                text    = parts[0];
                inserts = parts[1];
                hints   = parts[2];
            }
        }

        public void Fill(Cloze other)
        {
            text    = other.text;
            inserts = other.inserts;
            hints   = other.hints;
        }

        #endregion
    }
}
