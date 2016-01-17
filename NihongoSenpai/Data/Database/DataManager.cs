using NihongoSenpai.Resources;
using NihongoSenpai.Settings;
using NihongoSenpai.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NihongoSenpai.Data.Database
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

        public static void ConnectToDatabase()
        {
            if (database != null)
            {
                return;
            }

            database = new SenpaiDatabase(AppResources.DatabaseConnectionString);
        }

        public static void CloseConnection()
        {
            if (database == null)
            {
                return;
            }

            database.Dispose();
            database = null;
        }

        public static void SaveChanges()
        {
#if DEBUG

            database.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);

            foreach (ObjectChangeConflict occ in database.ChangeConflicts)
            {
                Debug.WriteLine(occ.ToString());
            }
            
#else
            
            database.SubmitChanges();

#endif
        }

        public static void ResetDatabase()
        {
            foreach(Lesson lesson in database.lessons)
            {
                ResetLesson(lesson);
            }
            
            database.SubmitChanges();
            
            AppSettings.TimeStamp = 0;
            AppSettings.SaveSettings();
        }
        
        /// <summary>
        /// resets all items in the selected lesson
        /// </summary>
        public static void ResetLesson(Lesson lesson)
        {
            switch(lesson.Type)
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
            //reset translation members
            w.eFactorJapanese   = 2.5f;
            w.lastRoundJapanese = 0;
            w.nextRoundJapanese = 0;
            w.timeStampJapanese = 0;

            //w.ToggleJWord();
            
            //reset japanese members
            w.eFactorTranslation   = 2.5f;
            w.lastRoundTranslation = 0;
            w.nextRoundTranslation = 0;
            w.timeStampTranslation = 0;
            
            w.ShowWord = Word.EShowFlags.showBoth;
        }

        private static void ResetKanji(Kanji k)
        {
            k.eFactor = 2.5f;
            k.lastRound = 0;
            k.nextRound = 0;

            k.timestamp = 0;
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
                    int importedWords   = 0;
                    int importedKanjis  = 0;
                    int importedClozes  = 0;

                    //read content from stream till end
                    while (!sr.EndOfStream)
                    {
                        Lesson lesson = new Lesson(sr.ReadLine());

                        for (int i = 0; i < lesson.size; ++i)
                        {
                            switch ((Lesson.EType)lesson.Type)
                            {
                                case Lesson.EType.vocab : lesson.Words .Add(new Word (sr.ReadLine(), lesson)); break;
                                case Lesson.EType.kanji : lesson.Kanjis.Add(new Kanji(sr.ReadLine(), lesson)); break;
                                case Lesson.EType.insert: lesson.Clozes.Add(new Cloze(sr.ReadLine(), lesson)); break;
                            }
                        }
                        
                        importedWords  += lesson.Words .Count;
                        importedClozes += lesson.Clozes.Count;
                        importedKanjis += lesson.Kanjis.Count;

                        lessons.Add(lesson);
                    }

                    importedLessons = lessons.Count;

                    foreach (Lesson lesson in lessons)
                    {
                        switch(lesson.Type)
                        {
                            case Lesson.EType.vocab : ImportWords (lesson); break;
                            case Lesson.EType.insert: ImportClozes(lesson); break;
                            case Lesson.EType.kanji : ImportKanjis(lesson); break;
                        }
                    }
                    
                    sb.AppendLine("Import erfolreich (^ω^)/");
                    sb.AppendLine("Importierte Lektionen:\t"   + importedLessons);
                    sb.AppendLine("Importierte Wörter:\t"      + importedWords);
                    sb.AppendLine("Importierte Kanjis:\t"      + importedKanjis);
                    sb.AppendLine("Importierte Lückentexte:\t" + importedClozes);

                    database.SubmitChanges();
                }
                catch(Exception e)
                {
                    sb.Clear();
                    sb.AppendLine("Inhalt hinzufügen Fehlgeschlagen in Zeile: " + sr.CurrentLine);
                    sb.AppendLine("System: " + e.Message);
                }

                return sb.ToString();
            }
        }

        private static void ImportWords(Lesson importLesson)
        {
            Lesson dbLesson = database.lessons.FirstOrDefault(x => x.id == importLesson.id);

            //lesson exists, so update it
            if (dbLesson != null)
            {
                //update lesson properties
                dbLesson.Fill(importLesson);

                //first find out wich words to delete
                List<Word> wordsToDelete = new List<Word>();

                foreach (Word dbWord in dbLesson.Words)
                {
                    //to do so, find all words in the database that are no longer in the new imported lesson
                    Word word = importLesson.Words.FirstOrDefault(x => x.id == dbWord.id);

                    //if the word can't be found in the importLesson, than delete it
                    if (word == null)
                    {
                        wordsToDelete.Add(dbWord);
                    }
                    else //if its still in the list, than update the word
                    {
                        dbWord.Fill(word);
                    }
                }

                //than go through all words from import lesson
                foreach (Word word in importLesson.Words)
                {
                    //and find all words that are not in the database
                    Word dbWord = dbLesson.Words.FirstOrDefault(x => x.id == word.id);

                    //if the word isn't in the database than insert it new
                    if (dbWord == null)
                    {
                        //before insert set last and nextRound Properties to the correct values
                        word.lastRoundJapanese = AppSettings.VocabRound;
                        word.nextRoundJapanese = AppSettings.VocabRound;
                        word.lastRoundTranslation = AppSettings.VocabRound;
                        word.nextRoundTranslation = AppSettings.VocabRound;

                        word.Lesson = dbLesson;
                        dbLesson.Words.Add(word);

                        database.words.InsertOnSubmit(word);
                    }
                }

                database.words.DeleteAllOnSubmit(wordsToDelete);

            }
            else //lesson does not exists, so just insert it
            {
                foreach(Word word in importLesson.Words)
                {
                    word.lastRoundJapanese = AppSettings.VocabRound;
                    word.nextRoundJapanese = AppSettings.VocabRound;
                    word.lastRoundTranslation = AppSettings.VocabRound;
                    word.nextRoundTranslation = AppSettings.VocabRound;
                }

                database.words.InsertAllOnSubmit(importLesson.Words);
                database.lessons.InsertOnSubmit(importLesson);
            }
        }

        private static void ImportClozes(Lesson importLesson)
        {
            Lesson dbLesson = database.lessons.FirstOrDefault(x => x.id == importLesson.id);

            //lesson exists, so update it
            if (dbLesson != null)
            {
                //update lesson properties
                dbLesson.Fill(importLesson);

                //first find out wich clozes to delete
                List<Cloze> clozesToDelete = new List<Cloze>();

                foreach (Cloze dbCloze in dbLesson.Clozes)
                {
                    //to do so, find all clozes in the database that are no longer in the new imported lesson
                    Cloze cloze = importLesson.Clozes.FirstOrDefault(x => x.id == dbCloze.id);

                    //if the cloze can't be found in the importLesson, than delete it
                    if (cloze == null)
                    {
                        clozesToDelete.Add(dbCloze);
                    }
                    else //if its still in the list, than update the cloze
                    {
                        dbCloze.Fill(cloze);
                    }
                }

                //than go through all clozes from import lesson
                foreach (Cloze cloze in importLesson.Clozes)
                {
                    //and find all clozes that are not in the database
                    Cloze dbCloze = dbLesson.Clozes.FirstOrDefault(x => x.id == cloze.id);

                    //if the cloze isn't in the database than insert it new
                    if (dbCloze == null)
                    {
                        cloze.Lesson = dbLesson;
                        dbLesson.Clozes.Add(cloze);

                        database.clozes.InsertOnSubmit(cloze);
                    }
                }

                database.clozes.DeleteAllOnSubmit(clozesToDelete);

            }
            else //lesson does not exists, so just insert it
            {
                database.clozes.InsertAllOnSubmit(importLesson.Clozes);
                database.lessons.InsertOnSubmit(importLesson);
            }
        }

        private static void ImportKanjis(Lesson importLesson)
        {
            Lesson dbLesson = database.lessons.FirstOrDefault(x => x.id == importLesson.id);

            //lesson exists, so update it
            if (dbLesson != null)
            {
                //update lesson properties
                dbLesson.Fill(importLesson);

                //first find out wich kanjis to delete
                List<Kanji> kanjisToDelete = new List<Kanji>();

                foreach (Kanji dbKanji in dbLesson.Kanjis)
                {
                    //to do so, find all kanjis in the database that are no longer in the new imported lesson
                    Kanji kanji = importLesson.Kanjis.FirstOrDefault(x => x.id == dbKanji.id);

                    //if the kanji can't be found in the importLesson, than delete it
                    if (kanji == null)
                    {
                        kanjisToDelete.Add(dbKanji);
                    }
                    else //if its still in the list, than update the kanji
                    {
                        dbKanji.Fill(kanji);
                    }
                }

                //than go through all kanjis from import lesson
                foreach (Kanji kanji in importLesson.Kanjis)
                {
                    //and find all kanjis that are not in the database
                    Kanji dbKanji = dbLesson.Kanjis.FirstOrDefault(x => x.id == kanji.id);

                    //if the kanji isn't in the database than insert it new
                    if (dbKanji == null)
                    {
                        kanji.lastRound = AppSettings.KanjiRound;
                        kanji.nextRound = AppSettings.KanjiRound;

                        kanji.Lesson = dbLesson;
                        dbLesson.Kanjis.Add(kanji);

                        database.kanjis.InsertOnSubmit(kanji);
                    }
                }

                database.kanjis.DeleteAllOnSubmit(kanjisToDelete);
            }
            else //lesson does not exists, so just insert it
            {
                foreach(Kanji kanji in importLesson.Kanjis)
                {
                    kanji.lastRound = AppSettings.KanjiRound;
                    kanji.nextRound = AppSettings.KanjiRound;
                }
                database.kanjis.InsertAllOnSubmit(importLesson.Kanjis);
                database.lessons.InsertOnSubmit(importLesson);
            }
        }

        #endregion

        #region Import

        public static String ImportFromFile(Stream importStream, out List<Lesson> lessons, out int vocabRound, out int kanjiRound)
        {
            kanjiRound = 0;
            vocabRound = 0;

            lessons = new List<Lesson>();

            StringBuilder sb = new StringBuilder();

            using (AppStreamReader sr = new AppStreamReader(importStream))
            {
                try
                {
                    vocabRound = Convert.ToInt32(sr.ReadLine());
                    kanjiRound = Convert.ToInt32(sr.ReadLine());
                    
                    int importedLessons = 0;
                    int importedWords   = 0;
                    int importedKanjis  = 0;
                    int importedClozes  = 0;
                    
                    while(!sr.EndOfStream)
                    {
                        Lesson lesson = new Lesson(sr.ReadLine());

                        for(int i = 0; i < lesson.size; ++i)
                        {
                            switch (lesson.Type)
                            {
                                case Lesson.EType.vocab : lesson.Words .Add(new Word (sr.ReadLine(), lesson)); break;
                                case Lesson.EType.insert: lesson.Clozes.Add(new Cloze(sr.ReadLine(), lesson)); break;
                                case Lesson.EType.kanji : lesson.Kanjis.Add(new Kanji(sr.ReadLine(), lesson)); break;
                            }
                        }

                        lessons.Add(lesson);
                        
                        importedWords  += lesson.Words.Count;
                        importedClozes += lesson.Clozes.Count;
                        importedKanjis += lesson.Kanjis.Count;
                    }

                    importedLessons = lessons.Count;
                    
                    sb.AppendLine(importedLessons + " Lektionen,");
                    sb.AppendLine(importedWords   + " Vokabeln,");
                    sb.AppendLine(importedKanjis  + " Kanjis und");
                    sb.AppendLine(importedClozes  + " Lückentexte gefunden");
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
                int importedWords   = 0;
                int importedKanjis  = 0;
                int importedClozes  = 0;

                foreach (Lesson lesson in lessons)
                {
                    database.words .InsertAllOnSubmit(lesson.Words);
                    database.clozes.InsertAllOnSubmit(lesson.Clozes);
                    database.kanjis.InsertAllOnSubmit(lesson.Kanjis);

                    importedWords  += lesson.Words .Count;
                    importedClozes += lesson.Clozes.Count;
                    importedKanjis += lesson.Kanjis.Count;
                }

                sb.AppendLine("Import erfolreich (^_^)/");
                sb.AppendLine("Importierte Lektionen:\t" + importedLessons);
                sb.AppendLine("Importierte Wörter:\t" + importedWords);
                sb.AppendLine("Importierte Kanjis:\t" + importedKanjis);
                sb.AppendLine("Importierte Lückentexte:\t" + importedClozes);

                database.lessons.InsertAllOnSubmit(lessons);
                database.SubmitChanges();
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

            using (AppStreamWriter sw = new AppStreamWriter(exportStream))
            {
                sw.WriteLine(AppSettings.VocabRound);
                sw.WriteLine(AppSettings.KanjiRound);

                int exportedLessons = 0;
                int exportedWords   = 0;
                int exportedClozes  = 0;
                int exportedKanjis  = 0;

                foreach (Lesson lesson in database.lessons)
                {
                    sw.WriteLine(lesson.ToExportString());

                    foreach(Word word in lesson.Words)
                    {
                        sw.WriteLine(word.ToExportString());
                    }

                    foreach (Cloze cloze in lesson.Clozes)
                    {
                        sw.WriteLine(cloze.ToExportString());
                    }

                    foreach (Kanji kanji in lesson.Kanjis)
                    {
                        sw.WriteLine(kanji.ToExportString());
                    }

                    exportedWords  += lesson.Words .Count;
                    exportedClozes += lesson.Clozes.Count;
                    exportedKanjis += lesson.Kanjis.Count;

                }
                exportedLessons = database.lessons.Count();

                sb.AppendLine("Datenbank erfolgreich gesichert!" );
                sb.AppendLine("Lektionen\t: "   + exportedLessons);
                sb.AppendLine("Wörter\t\t: "    + exportedWords  );
                sb.AppendLine("Lückentexte\t: " + exportedClozes );
                sb.AppendLine("Kanjis\t\t: "    + exportedKanjis );
            }

            String exportResults = sb.ToString();

            sb.Clear();

            return exportResults;
        }
        
        #endregion

        #endregion

        #region Select Items
        
        public static IQueryable<Lesson> GetLessons(Lesson.EType type)
        {
            return database.GetLessons(type);
        }

        public static List<Word> GetWords(List<Lesson> lessons, Word.EType[] types)
        {
            return database.GetWords(lessons, types);
        }

        public static List<Kanji> GetKanjis(List<Lesson> lessons)
        {
            return database.GetKanjis(lessons);
        }
        
        /// <summary>
        /// searches for words that contains the given substring
        /// the string can be part of the kana, kanji and translation members of Word
        /// </summary>
        public static IEnumerable<Word> FindWords(String substring)
        {
            return database.FindWords(substring);
        }

        #endregion

        #region Delete Items

        public static void DeleteLesson(Lesson lesson)
        {
            switch(lesson.Type)
            {
                case Lesson.EType.vocab : database.words .DeleteAllOnSubmit(lesson.Words ); break;
                case Lesson.EType.insert: database.clozes.DeleteAllOnSubmit(lesson.Clozes); break;
                case Lesson.EType.kanji : database.kanjis.DeleteAllOnSubmit(lesson.Kanjis); break;
            }

            database.lessons.DeleteOnSubmit(lesson);
        }

        #endregion

        #region Other

        public static void InitializeAppData(Lesson selectedLesson)
        {
            AppData.SelectedLesson = selectedLesson;

            AppData.Words = new List<Word>();
            AppData.Kanjis = new List<Kanji>();

            switch(selectedLesson.Type)
            {
                case Lesson.EType.vocab: AppData.Words.AddRange(AppData.SelectedLesson.Words); break;
                case Lesson.EType.kanji: AppData.Kanjis.AddRange(AppData.SelectedLesson.Kanjis); break;
            }
        }

        public static void DeinitializeAppData()
        {
            if(AppData.Words != null)
            {
                AppData.Words.Clear();
                AppData.Kanjis.Clear();

                AppData.Words = null;
                AppData.Kanjis = null;
            }

            AppData.SelectedLesson = null;
        }

        #endregion
    }
}