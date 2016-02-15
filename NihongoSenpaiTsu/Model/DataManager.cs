using NihongoSenpai.Utilities;
using NihongoSenpaiTsu.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NihongoSenpaiTsu.Model
{
    public static class DataManager
    {
        #region Fields

        private static SenpaiDatabase database;

        #endregion

        #region Properties

        public static SenpaiDatabase Database
        {
            get { return database; }
        }

        #endregion

        #region Database

        public static void CreateDatabase()
        {
            database.CreateDatabase();
        }

        public static void DeleteDatabase()
        {
            database.DeleteDatabase();
        }

        public static void ConnectToDatabase()
        {
            if (database != null)
            {
                return;
            }

            database = new SenpaiDatabase();
        }

        public static void CloseConnection()
        {
            if (database == null)
            {
                return;
            }

            database.CloseConnection();
            database = null;
        }

//        public static void SaveChanges()
//        {
//#if DEBUG

//            database.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);

//            foreach (ObjectChangeConflict occ in database.ChangeConflicts)
//            {
//                Debug.WriteLine(occ.ToString());
//            }

//#else
            
//            database.SubmitChanges();

//#endif
//        }

        public static void ResetDatabase()
        {
            foreach (Lesson lesson in database.Lessons)
            {
                ResetLesson(lesson);
            }
        }

        /// <summary>
        /// resets all items in the selected lesson
        /// </summary>
        public static void ResetLesson(Lesson lesson)
        {
            switch ((Lesson.EType)lesson.Type)
            {
                case Lesson.EType.vocab:

                    foreach (Word w in lesson.Words)
                    {
                        ResetWord(w);
                    }

                    break;

                case Lesson.EType.kanji:

                    foreach (Kanji k in lesson.Kanjis)
                    {
                        ResetKanji(k);
                    }

                    break;
            }
        }

        private static void ResetWord(Word w)
        {
            //reset japanese members
            w.EFactorJap = 2.5f;
            w.LastRoundJap = 0;
            w.NextRoundJap = 0;
            w.TimeStampJap = 0;

            //reset german members
            w.EFactorGer = 2.5f;
            w.LastRoundGer = 0;
            w.NextRoundGer = 0;
            w.TimeStampGer = 0;

            w.ShowWord = Word.EShowFlags.showBoth;
        }

        private static void ResetKanji(Kanji k)
        {
            k.EFactor = 2.5f;
            k.LastRound = 0;
            k.NextRound = 0;
            k.Timestamp = 0;
        }

        #endregion

        #region Add Content/Import/Export

        #region Add Content

        public static String AddContentFromFile(Stream contentStream)
        {
            using (AppStreamReader sr = new AppStreamReader(contentStream))
            {
                StringBuilder sb = new StringBuilder();

                try
                {
                    List<Lesson> lessons = new List<Lesson>();

                    int importedLessons = 0;
                    int importedWords = 0;
                    int importedKanjis = 0;
                    int importedClozes = 0;

                    //read content from stream till end
                    while (!sr.EndOfStream)
                    {
                        Lesson lesson = new Lesson(sr.ReadLine());

                        for (int i = 0; i < lesson.Size; ++i)
                        {
                            switch ((Lesson.EType)lesson.Type)
                            {
                                case Lesson.EType.vocab  : lesson.Words.Add(new Word(sr.ReadLine(), lesson)); break;
                                case Lesson.EType.kanji  : lesson.Kanjis.Add(new Kanji(sr.ReadLine(), lesson)); break;
                                case Lesson.EType.grammar: lesson.Grammars.Add(new Grammar(sr.ReadLine(), lesson)); break;
                            }
                        }

                        importedWords  += lesson.Words.Count;
                        importedClozes += lesson.Grammars.Count;
                        importedKanjis += lesson.Kanjis.Count;

                        lessons.Add(lesson);
                    }

                    importedLessons = lessons.Count;

                    foreach (Lesson lesson in lessons)
                    {
                        switch ((Lesson.EType)lesson.Type)
                        {
                            case Lesson.EType.vocab  : ImportWords(lesson); break;
                            case Lesson.EType.kanji  : ImportKanjis(lesson); break;
                            case Lesson.EType.grammar: ImportGrammars(lesson); break;
                        }
                    }

                    sb.AppendLine("Import erfolreich (^ω^)/");
                    sb.AppendLine("Importierte Lektionen:\t" + importedLessons);
                    sb.AppendLine("Importierte Wörter:\t" + importedWords);
                    sb.AppendLine("Importierte Kanjis:\t" + importedKanjis);
                    sb.AppendLine("Importierte Lückentexte:\t" + importedClozes);
                }
                catch (Exception e)
                {
                    sb.Clear();
                    sb.AppendLine("Inhalt hinzufügen Fehlgeschlagen in Zeile: " + sr.CurrentLine);
                    sb.AppendLine("System: " + e.Message);
                }

                return sb.ToString();
            }
        }

        private static void ImportWords(Lesson newLesson)
        {
            List<Word> wordsToInsert = new List<Word>();
            List<Word> wordsToUpdate = new List<Word>();
            List<Word> wordsToDelete = new List<Word>();

            Lesson oldLesson = database.Lessons.FirstOrDefault(x => x.Id == newLesson.Id);

            //lesson doesnt exists, so insert it
            if(oldLesson == null)
            {
                wordsToInsert.AddRange(newLesson.Words);

                database.Add(newLesson);
            }
            else
            {
                //first find out wich words to delete and wich to update
                foreach (Word dbWord in oldLesson.Words)
                {
                    //to do so, find all words in the database that are no longer in the new imported lesson
                    Word newWord = newLesson.Words.FirstOrDefault(x => x.Id == dbWord.Id);
                    
                    //if the word can't be found in the importLesson, than delete it
                    if (newWord == null)
                    {
                        wordsToDelete.Add(dbWord);
                    }
                    else //if its still in the list, than update the word
                    {
                        dbWord.Fill(newWord);
                        wordsToUpdate.Add(dbWord);
                    }
                }

                //than go through all words from import lesson
                foreach (Word newWord in newLesson.Words)
                {
                    //and find all words that are not in the database
                    Word dbWord = oldLesson.Words.FirstOrDefault(x => x.Id == newWord.Id);

                    //if the word isn't in the database than insert it new
                    if (dbWord == null)
                    {
                        wordsToInsert.Add(newWord);
                    }
                }

                //update lesson
                oldLesson.Fill(newLesson);
                database.Update(oldLesson);
            }

            database.AddRange(wordsToInsert);
            database.UpdateRange(wordsToUpdate);
            database.DeleteRange(wordsToDelete);
        }

        private static void ImportGrammars(Lesson newLesson)
        {
            List<Grammar> grammarsToInsert = new List<Grammar>();
            List<Grammar> grammarsToUpdate = new List<Grammar>();
            List<Grammar> grammarsToDelete = new List<Grammar>();

            Lesson oldLesson = database.Lessons.FirstOrDefault(x => x.Id == newLesson.Id);

            //lesson doesnt exists, so insert it
            if (oldLesson == null)
            {
                grammarsToInsert.AddRange(newLesson.Grammars);

                database.Add(newLesson);
            }
            else
            {
                //first find out wich Grammars to delete and wich to update
                foreach (Grammar dbGrammar in oldLesson.Grammars)
                {
                    //to do so, find all Grammars in the database that are no longer in the new imported lesson
                    Grammar newGrammar = newLesson.Grammars.FirstOrDefault(x => x.Id == dbGrammar.Id);

                    //if the Grammar can't be found in the importLesson, than delete it
                    if (newGrammar == null)
                    {
                        grammarsToDelete.Add(dbGrammar);
                    }
                    else //if its still in the list, than update the Grammar
                    {
                        dbGrammar.Fill(newGrammar);
                        grammarsToUpdate.Add(dbGrammar);
                    }
                }

                //than go through all Grammars from import lesson
                foreach (Grammar newGrammar in newLesson.Grammars)
                {
                    //and find all Grammars that are not in the database
                    Grammar dbGrammar = oldLesson.Grammars.FirstOrDefault(x => x.Id == newGrammar.Id);

                    //if the Grammar isn't in the database than insert it new
                    if (dbGrammar == null)
                    {
                        grammarsToInsert.Add(newGrammar);
                    }
                }

                //update lesson
                oldLesson.Fill(newLesson);
                database.Update(oldLesson);
            }

            database.AddRange(grammarsToInsert);
            database.UpdateRange(grammarsToUpdate);
            database.DeleteRange(grammarsToDelete);
        }

        private static void ImportKanjis(Lesson newLesson)
        {
            List<Kanji> kanjisToInsert = new List<Kanji>();
            List<Kanji> kanjisToUpdate = new List<Kanji>();
            List<Kanji> kanjisToDelete = new List<Kanji>();

            Lesson oldLesson = database.Lessons.FirstOrDefault(x => x.Id == newLesson.Id);

            //lesson doesnt exists, so insert it
            if (oldLesson == null)
            {
                kanjisToInsert.AddRange(newLesson.Kanjis);

                database.Add(newLesson);
            }
            else
            {
                //first find out wich Kanjis to delete and wich to update
                foreach (Kanji dbKanji in oldLesson.Kanjis)
                {
                    //to do so, find all Kanjis in the database that are no longer in the new imported lesson
                    Kanji newKanji = newLesson.Kanjis.FirstOrDefault(x => x.Id == dbKanji.Id);

                    //if the Kanji can't be found in the importLesson, than delete it
                    if (newKanji == null)
                    {
                        kanjisToDelete.Add(dbKanji);
                    }
                    else //if its still in the list, than update the Kanji
                    {
                        dbKanji.Fill(newKanji);
                        kanjisToUpdate.Add(dbKanji);
                    }
                }

                //than go through all Kanjis from import lesson
                foreach (Kanji newKanji in newLesson.Kanjis)
                {
                    //and find all Kanjis that are not in the database
                    Kanji dbKanji = oldLesson.Kanjis.FirstOrDefault(x => x.Id == newKanji.Id);

                    //if the Kanji isn't in the database than insert it new
                    if (dbKanji == null)
                    {
                        kanjisToInsert.Add(newKanji);
                    }
                }

                //update lesson
                oldLesson.Fill(newLesson);
                database.Update(oldLesson);
            }

            database.AddRange(kanjisToInsert);
            database.UpdateRange(kanjisToUpdate);
            database.DeleteRange(kanjisToDelete);
        }

        #endregion

        #region Import

        public static String ImportFromFile(Stream importStream, out List<Lesson> lessons)
        {
            lessons = new List<Lesson>();

            StringBuilder sb = new StringBuilder();

            using (AppStreamReader sr = new AppStreamReader(importStream))
            {
                try
                {
                    int importedLessons = 0;
                    int importedWords  = 0;
                    int importedKanjis = 0;
                    int importedGrammars = 0;

                    while (!sr.EndOfStream)
                    {
                        Lesson lesson = new Lesson(sr.ReadLine());

                        for (int i = 0; i < lesson.Size; ++i)
                        {
                            switch ((Lesson.EType)lesson.Type)
                            {
                                case Lesson.EType.vocab  : lesson.Words   .Add(new Word   (sr.ReadLine(), lesson)); break;
                                case Lesson.EType.kanji  : lesson.Kanjis  .Add(new Kanji  (sr.ReadLine(), lesson)); break;
                                case Lesson.EType.grammar: lesson.Grammars.Add(new Grammar(sr.ReadLine(), lesson)); break;
                            }
                        }

                        lessons.Add(lesson);

                        importedWords += lesson.Words.Count;
                        importedGrammars += lesson.Grammars.Count;
                        importedKanjis += lesson.Kanjis.Count;
                    }

                    importedLessons = lessons.Count;

                    sb.AppendLine(importedLessons + " Lektionen,");
                    sb.AppendLine(importedWords + " Vokabeln,");
                    sb.AppendLine(importedKanjis + " Kanjis und");
                    sb.AppendLine(importedGrammars + " Grammatiken gefunden");
                    sb.AppendLine("Datenbank wirklich wiederherstellen?");
                    sb.AppendLine("Achtung! Dabei wird die derzeitige Datenbank überschrieben!");
                }
                catch (Exception e)
                {
                    sb.Clear();
                    sb.AppendLine("Import Fehlgeschlagen in Zeile: " + sr.CurrentLine);
                    sb.AppendLine("System: " + e.Message);
                }
            }

            return sb.ToString();
        }

        public static String UpdateDatabase(List<Lesson> lessons)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                DeleteDatabase();
                CreateDatabase();

                int importedLessons = lessons.Count;
                int importedWords = 0;
                int importedKanjis = 0;
                int importedGrammars = 0;

                foreach (Lesson lesson in lessons)
                {
                    database.AddRange(lesson.Words);
                    database.AddRange(lesson.Kanjis);
                    database.AddRange(lesson.Grammars);

                    importedWords += lesson.Words.Count;
                    importedGrammars += lesson.Grammars.Count;
                    importedKanjis += lesson.Kanjis.Count;
                }

                sb.AppendLine("Import erfolreich (^_^)/");
                sb.AppendLine("Importierte Lektionen:\t" + importedLessons);
                sb.AppendLine("Importierte Wörter:\t" + importedWords);
                sb.AppendLine("Importierte Kanjis:\t" + importedKanjis);
                sb.AppendLine("Importierte Grammatiken:\t" + importedGrammars);

                database.AddRange(lessons);
            }
            catch (Exception e)
            {
                sb.Clear();
                sb.AppendLine("Update Fehlgeschlagen =/");
                sb.AppendLine("System: " + e.Message);
            }

            return sb.ToString();
        }

        #endregion

        #region Export

        /// <summary>
        /// <para>ExportFormat:</para>
        /// 
        /// <para>VocabRound:int</para>
        /// <para>KanjiRound:int</para>
        /// 
        /// <para>Lesson:id|name|type|size</para>
        /// 
        /// <para>Word-1:id|kana|kanji|translation|description|type|eFactorTransl|lastRoundTransl|nextRoundTransl|eFactorJap|lastRoundJap|nextRoundJap|showFlags|timeStampTransl|timeStampJap</para>
        /// 
        /// <para>Word-size:id|kana|kanji|translation|description|type|eFactorTransl|lastRoundTransl|nextRoundTransl|eFactorJap|lastRoundJap|nextRoundJap|showFlags|timeStampTransl|timeStampJap</para>
        /// 
        /// <para>or</para>
        /// 
        /// <para>Kanji-1:id|kanji|meaning|onyomi|kunyomi|example|strokeOrder|efactor|lastRound|nextRound|timestamp</para>
        /// <para>...</para>
        /// <para>Kanji-size:id|kanji|meaning|onyomi|kunyomi|example|strokeOrder|efactor|lastRound|nextRound|timestamp</para>
        /// 
        /// <para>or</para>
        /// 
        /// <para>Cloze-1:id|text|insertText|hintText</para>
        /// <para>...</para>
        /// <para>Cloze-size:id|text|insertText|hintText</para>
        /// 
        /// <para>empty-line = end</para>
        /// </summary>
        public static String ExportToFile(Stream exportStream)
        {
            StringBuilder sb = new StringBuilder();

            using (StreamWriter sw = new StreamWriter(exportStream))
            {
                int exportedLessons = 0;
                int exportedWords = 0;
                int exportedGrammars = 0;
                int exportedKanjis = 0;

                foreach (Lesson lesson in database.Lessons)
                {
                    sw.WriteLine(lesson.ToExportString());

                    foreach (Word word in lesson.Words)
                    {
                        sw.WriteLine(word.ToExportString());
                    }

                    foreach (Kanji kanji in lesson.Kanjis)
                    {
                        sw.WriteLine(kanji.ToExportString());
                    }

                    foreach (Grammar grammars in lesson.Grammars)
                    {
                        sw.WriteLine(grammars.ToExportString());
                    }

                    exportedWords += lesson.Words.Count;
                    exportedGrammars += lesson.Grammars.Count;
                    exportedKanjis += lesson.Kanjis.Count;

                }
                exportedLessons = database.Lessons.Count();

                sb.AppendLine("Datenbank erfolgreich gesichert!");
                sb.AppendLine("Lektionen\t: " + exportedLessons);
                sb.AppendLine("Wörter\t\t: " + exportedWords);
                sb.AppendLine("Grammatiken\t: " + exportedGrammars);
                sb.AppendLine("Kanjis\t\t: " + exportedKanjis);
            }

            String exportResults = sb.ToString();

            sb.Clear();

            return exportResults;
        }

        #endregion

        #endregion

        #region Select Items

        //public static IQueryable<Lesson> GetLessons(Lesson.EType type)
        //{
        //    return database.GetLessons(type);
        //}

        //public static List<Word> GetWords(List<Lesson> lessons, Word.EType[] types)
        //{
        //    return database.GetWords(lessons, types);
        //}

        //public static List<Kanji> GetKanjis(List<Lesson> lessons)
        //{
        //    return database.GetKanjis(lessons);
        //}

        /// <summary>
        /// searches for words that contains the given substring
        /// the string can be part of the kana, kanji and translation members of Word
        /// </summary>
        //public static IEnumerable<Word> FindWords(String substring)
        //{
        //    return database.FindWords(substring);
        //}

        #endregion

        #region Delete Items

        public static void DeleteLesson(Lesson lesson)
        {
            switch ((Lesson.EType)lesson.Type)
            {
                case Lesson.EType.vocab  : database.DeleteRange(lesson.Words); break;
                case Lesson.EType.kanji  : database.DeleteRange(lesson.Kanjis); break;
                case Lesson.EType.grammar: database.DeleteRange(lesson.Grammars); break;
            }

            database.Delete(lesson);
        }

        #endregion

        #region Other

        //public static void InitializeAppData(Lesson selectedLesson)
        //{
        //    AppData.SelectedLesson = selectedLesson;

        //    AppData.Words = new List<Word>();
        //    AppData.Kanjis = new List<Kanji>();

        //    switch (selectedLesson.Type)
        //    {
        //        case Lesson.EType.vocab: AppData.Words.AddRange(AppData.SelectedLesson.Words); break;
        //        case Lesson.EType.kanji: AppData.Kanjis.AddRange(AppData.SelectedLesson.Kanjis); break;
        //    }
        //}

        //public static void DeinitializeAppData()
        //{
        //    if (AppData.Words != null)
        //    {
        //        AppData.Words.Clear();
        //        AppData.Kanjis.Clear();

        //        AppData.Words = null;
        //        AppData.Kanjis = null;
        //    }

        //    AppData.SelectedLesson = null;
        //}

        #endregion
    }
}
