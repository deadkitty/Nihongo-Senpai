﻿using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NihongoSenpai.Model.Database
{
    [Table(Name="Lessons")]
    public class Lesson
    {
        #region EType

        public enum EType
        {
            vocab,
            insert,
            conjugation,
            kanji,
            grammar,
            count,
            undefined = -1,
        }
        
        #endregion
                
        #region Fields

        [Column(IsPrimaryKey = true, IsDbGenerated = false, CanBeNull = false, AutoSync = AutoSync.Never)]
        public int id;

        [Column]
        public String name;

        /// <summary>
        /// <para>Type of the Lesson</para>
        /// <para>0 - Vocab</para>
        /// <para>1 - Insert</para>
        /// <para>2 - conjugation Lesson (unused yet)</para>
        /// <para>3 - Kanji Flashcards</para>
        /// <para>4 - Grammar (unused yet)</para>
        /// </summary>
        [Column]
        internal int type;

        [Column]
        public int size;
        
        private EntitySet<Word>  words  = new EntitySet<Word> ();
        private EntitySet<Kanji> kanjis = new EntitySet<Kanji>();
        private EntitySet<Cloze> clozes = new EntitySet<Cloze>();
        
        #endregion

        #region Properties

        public EType Type
        {
            get { return (EType)type; }
            set { type = (int)value; }
        }

        [Association(Name = "lessonWordFK", Storage = "words", OtherKey = "lessonID", ThisKey = "id")]
        public ICollection<Word> Words
        {
            get { return words; }
            set { words.Assign(value); }
        }

        [Association(Name = "lessonKanjiFK", Storage = "kanjis", OtherKey = "lessonID", ThisKey = "id")]
        public ICollection<Kanji> Kanjis
        {
            get { return kanjis; }
            set { kanjis.Assign(value); }
        }

        [Association(Name = "lessonClozeFK", Storage = "clozes", OtherKey = "lessonID", ThisKey = "id")]
        public ICollection<Cloze> Clozes
        {
            get { return clozes; }
            set { clozes.Assign(value); }
        }
        
        #endregion

        #region Constructor

        public Lesson()
        {

        }
        
        public Lesson(String properties)
        {
            Fill(properties);
        }

        public Lesson(Lesson other)
        {
            Fill(other);
        }

        #endregion
        
        #region ToString

        public override string ToString()
        {
            return name;
        }

        /// <summary>
        /// <para>Creates a String to use for export as txt file.</para>
        /// <para>Export Pattern:</para>
        /// <para>id|name|type|size</para>
        /// </summary>
        public String ToExportString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(id);
            sb.Append("|");
            sb.Append(name);
            sb.Append("|");
            sb.Append(type);
            sb.Append("|");
            sb.Append(size);

            return sb.ToString();
        }
        
        #endregion

        #region Util

        public void Fill(String properties)
        {
            String[] parts = properties.Split('|');

            id   = Convert.ToInt32(parts[0]);
            name = parts[1];
            type = Convert.ToInt32(parts[2]);
            size = Convert.ToInt32(parts[3]);
        }

        public void Fill(Lesson other)
        {
            name = other.name;
            size = other.size;
            type = other.type;
        }

        #endregion
    }
}
