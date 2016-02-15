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
    [Table(Name="Clozes")]
    public class Cloze
    {
        #region Fields

        [Column(IsPrimaryKey = true, IsDbGenerated = false, CanBeNull = false, AutoSync = AutoSync.Never)]
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

        public Cloze(String properties, Lesson lesson)
        {
            Lesson = lesson;
            lessonID = lesson.id;

            Fill(properties);
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return text;
        }

        /// <summary>
        /// <para>Creates a String to use for export as txt file.</para>
        /// <para>Export Pattern:</para>
        /// <para>id|text|insertText|hintText</para>
        /// <para>Lenght = 4</para>
        /// </summary>
        public String ToExportString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(id);
            sb.Append("|");
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

            id      = Convert.ToInt32(parts[0]);
            text    = parts[1];
            inserts = parts[2];
            hints   = parts[3];
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