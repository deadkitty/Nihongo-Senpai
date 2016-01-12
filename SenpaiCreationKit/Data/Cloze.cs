using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;

#pragma warning disable 0649

namespace SenpaiCreationKit.Data
{
    [Table(Name = "Clozes")]
    public class Cloze
    {
        #region Fields

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int id { get; set; }

        [Column] public String Text { get; set; }
        [Column] public String Inserts { get; set; }
        [Column] public String Hints { get; set; }

        [Column]
        private int lessonID { get; set; }
        private EntityRef<Lesson> lesson = new EntityRef<Lesson>();

        #endregion

        #region Properties

        [Association(Name = "lessonClozeFK", IsForeignKey = true, ThisKey = "lessonID", Storage = "lesson")]
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

        public Cloze(String properties, Lesson lesson)
        {
            this.Lesson = lesson;
            Fill(properties);
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return Text;
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
            sb.Append(Text);
            sb.Append("|");
            sb.Append(Inserts);
            sb.Append("|");
            sb.Append(Hints);

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

            sb.Append(Text);
            sb.Append("|");
            sb.Append(Inserts);
            sb.Append("|");
            sb.Append(Hints);

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
                Text    = parts[2];
                Inserts = parts[3];
                Hints   = parts[4];
            }
            else //from add new string
            {
                Text    = parts[0];
                Inserts = parts[1];
                Hints   = parts[2];
            }
        }

        public void Fill(Cloze other)
        {
            Text    = other.Text;
            Inserts = other.Inserts;
            Hints   = other.Hints;
        }

        #endregion
    }
}
