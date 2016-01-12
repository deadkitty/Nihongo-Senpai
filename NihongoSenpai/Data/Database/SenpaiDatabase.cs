using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace NihongoSenpai.Data.Database
{
    public class SenpaiDatabase : DataContext
    {
        #region Fields
        
        public Table<Lesson> lessons;
        public Table<Word>  words;
        public Table<Cloze> clozes;
        public Table<Kanji> kanjis;

        #endregion

        #region Constructor

        public SenpaiDatabase(String connectionString)
            : base(connectionString)
        {
            
        }

        #endregion

        #region Select Tables

        #region Get Lessons

        public Lesson GetLesson(int id)
        {
            return lessons.SingleOrDefault(c => c.id == id);
        }

        public IQueryable<Lesson> GetLessons(Lesson.EType type)
        {
            return from c in lessons where c.type == (int)type orderby c.name select c;
        }

        #endregion

        #region Get Words

        public Word GetWord(int id)
        {
            return words.SingleOrDefault(c => c.id == id);
        }
        
        public List<Word> GetWords(List<Lesson> lessons, Word.EType[] types)
        {
            List<Word> words = new List<Word>();

            foreach(Lesson lesson in lessons)
            {
                IEnumerable<Word> eWords = from c in lesson.Words where types.Contains((Word.EType)c.type) select c;

                words.AddRange(eWords);
            }
    
            return words;
        }

        public IEnumerable<Word> FindWords(String substring)
        {
            return (from c in words where c.kana.Contains(substring) || c.kanji.Contains(substring) || c.translation.Contains(substring) orderby c.kanji select c);
        }

        #endregion

        #region Get Kanji

        public Kanji GetKanji(int id)
        {
            return kanjis.SingleOrDefault(c => c.id == id);
        }

        public List<Kanji> GetKanjis(List<Lesson> lessons)
        {
            List<Kanji> kanjis = new List<Kanji>();

            foreach(Lesson lesson in lessons)
            {
                kanjis.AddRange(lesson.Kanjis);
            }
    
            return kanjis;
        }
        #endregion

        #region Get Cloze

        public Cloze GetCloze(int id)
        {
            return clozes.SingleOrDefault(c => c.id == id);
        }

        #endregion
        
        #endregion
    }
}
