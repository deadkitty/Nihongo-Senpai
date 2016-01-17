using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiCreationKit.Data
{
    public static class DataManager
    {
        #region Fields

        private static SenpaiDatabase database;

        public static SenpaiDatabase Database
        {
            get { return database; }
        }

        #endregion

        #region Database

        public static void ConnectToDatabase()
        {
            database = new SenpaiDatabase();
        }

        public static void CloseConnection()
        {
            database.Dispose();
            database = null;
        }

        public static void CreateDatabase()
        {
            if (!database.DatabaseExists())
            {
                database.CreateDatabase();
            }
        }

        public static void DeleteDatabase()
        {
            if (database.DatabaseExists())
            {
                database.DeleteDatabase();
            }
        }

        #endregion

        #region Load Tables

        public static Lesson GetLesson(int id)
        {
            return database.Lessons.Where(x => x.id == id).FirstOrDefault();
        }

        public static List<Lesson> GetLessons(bool sorted = true)
        {
            if (sorted)
            {
                return database.Lessons.OrderBy(x => x.Type).ToList();
            }
            else
            {
                return database.Lessons.ToList();
            }
        }

        public static List<Lesson> GetLessons(String searchText, bool sorted = true)
        {
            if (sorted)
            {
                return database.Lessons.OrderBy(x => x.Type).Where(x => x.Name.Contains(searchText)).ToList();
            }
            else
            {
                return database.Lessons.Where(x => x.Name.Contains(searchText)).ToList();
            }
        }

        #endregion

        #region Create Tables

        public static void CreateVocabLesson(String lessonName, List<Word> words)
        {
            Lesson lesson = new Lesson();
            lesson.Name = lessonName;
            lesson.Size = words.Count;
            lesson.Type = (int)Lesson.EType.vocab;

            foreach (Word word in words)
            {
                word.Lesson = lesson;
                lesson.Words.Add(word);
            }

            database.Words.InsertAllOnSubmit(lesson.Words);
            database.Lessons.InsertOnSubmit(lesson);

            database.SubmitChanges();
        }

        public static void CreateFlashLesson(String lessonName, List<Kanji> kanjis)
        {
            Lesson lesson = new Lesson();
            lesson.Name = lessonName;
            lesson.Size = kanjis.Count;
            lesson.Type = (int)Lesson.EType.kanji;

            foreach (Kanji kanji in kanjis)
            {
                kanji.Lesson = lesson;
                lesson.Kanjis.Add(kanji);
            }

            database.Kanjis.InsertAllOnSubmit(lesson.Kanjis);
            database.Lessons.InsertOnSubmit(lesson);

            database.SubmitChanges();
        }

        public static void CreateInsertLesson(String lessonName, List<Cloze> clozes)
        {
            Lesson lesson = new Lesson();
            lesson.Name = lessonName;
            lesson.Size = clozes.Count;
            lesson.Type = (int)Lesson.EType.insert;

            foreach (Cloze cloze in clozes)
            {
                cloze.Lesson = lesson;
                lesson.Clozes.Add(cloze);
            }

            database.Clozes.InsertAllOnSubmit(lesson.Clozes);
            database.Lessons.InsertOnSubmit(lesson);

            database.SubmitChanges();
        }

        #endregion

        #region Update Tables

        public static void UpdateVocabLesson(Lesson lesson, List<Word> newWords, List<Word> wordsToDelete)
        {
            foreach (Word word in newWords)
            {
                word.Lesson = lesson;
                lesson.Words.Add(word);
            }

            foreach (Word word in wordsToDelete)
            {
                lesson.Words.Remove(word);
            }

            database.Words.InsertAllOnSubmit(newWords);
            database.Words.DeleteAllOnSubmit(wordsToDelete);

            database.SubmitChanges();
        }

        public static void UpdateFlashLesson(Lesson lesson, List<Kanji> newKanjis, List<Kanji> kanjisToDelete)
        {
            foreach (Kanji kanji in newKanjis)
            {
                kanji.Lesson = lesson;
                lesson.Kanjis.Add(kanji);
            }

            foreach (Kanji kanji in kanjisToDelete)
            {
                lesson.Kanjis.Remove(kanji);
            }

            database.Kanjis.InsertAllOnSubmit(newKanjis);
            database.Kanjis.DeleteAllOnSubmit(kanjisToDelete);

            database.SubmitChanges();
        }

        public static void UpdateInsertLesson(Lesson lesson, List<Cloze> newClozes, List<Cloze> clozesToDelete)
        {
            foreach (Cloze cloze in newClozes)
            {
                cloze.Lesson = lesson;
                lesson.Clozes.Add(cloze);
            }

            foreach (Cloze cloze in clozesToDelete)
            {
                lesson.Clozes.Remove(cloze);
            }

            database.Clozes.InsertAllOnSubmit(newClozes);
            database.Clozes.DeleteAllOnSubmit(clozesToDelete);

            database.SubmitChanges();
        }

        #endregion

        #region Delete Tables

        /// <summary>
        /// Deletes the given lesson and all Words/Kanjis/Clozes assigned to this lesson
        /// </summary>
        public static void DeleteLesson(Lesson lesson)
        {
            switch ((Lesson.EType)lesson.Type)
            {
                case Lesson.EType.vocab : database.Words .DeleteAllOnSubmit(lesson.Words ); break;
                case Lesson.EType.kanji : database.Kanjis.DeleteAllOnSubmit(lesson.Kanjis); break;
                case Lesson.EType.insert: database.Clozes.DeleteAllOnSubmit(lesson.Clozes); break;
            }

            database.Lessons.DeleteOnSubmit(lesson);

            database.SubmitChanges();
        }

        #endregion

        #region Import

        public static void ImportLessons(StreamReader sr)
        {
            List<Lesson> lessons = new List<Lesson>();

            //read content from stream till end
            while (!sr.EndOfStream)
            {
                Lesson lesson = new Lesson(sr.ReadLine());

                for (int i = 0; i < lesson.Size; ++i)
                {
                    switch ((Lesson.EType)lesson.Type)
                    {
                        case Lesson.EType.vocab : lesson.Words .Add(new Word (sr.ReadLine(), lesson)); break;
                        case Lesson.EType.kanji : lesson.Kanjis.Add(new Kanji(sr.ReadLine(), lesson)); break;
                        case Lesson.EType.insert: lesson.Clozes.Add(new Cloze(sr.ReadLine(), lesson)); break;
                    }
                }

                lessons.Add(lesson);
            }
            
            //insert all into the database
            database.Lessons.InsertAllOnSubmit(lessons);

            foreach (Lesson lesson in lessons)
            {
                switch ((Lesson.EType)lesson.Type)
                {
                    case Lesson.EType.vocab : database.Words .InsertAllOnSubmit(lesson.Words ); break;
                    case Lesson.EType.kanji : database.Kanjis.InsertAllOnSubmit(lesson.Kanjis); break;
                    case Lesson.EType.insert: database.Clozes.InsertAllOnSubmit(lesson.Clozes); break;
                }
            }

            database.SubmitChanges();
        }

        #endregion

        #region Export

        public static void ExportLessons(List<Lesson> lessons, StreamWriter sw)
        {
            foreach (Lesson lesson in lessons)
            {
                sw.WriteLine(lesson.ToCreateNewString());

                foreach (Word word in lesson.Words)
                {
                    sw.WriteLine(word.ToCreateNewString());
                }

                foreach (Kanji kanji in lesson.Kanjis)
                {
                    sw.WriteLine(kanji.ToCreateNewString());
                }

                foreach (Cloze cloze in lesson.Clozes)
                {
                    sw.WriteLine(cloze.ToCreateNewString());
                }
            }
        }

        #endregion
    }
}