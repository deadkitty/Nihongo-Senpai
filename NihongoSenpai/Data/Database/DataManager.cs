using NihongoSenpai.Resources;
using NihongoSenpai.Settings;
using NihongoSenpai.Utilities;
using System;
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
        
        private static StringBuilder sb;

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
            using (database = new SenpaiDatabase(AppResources.DatabaseConnectionString))
            {
                if (!database.DatabaseExists())
                {
                    database.CreateDatabase();
                }
            }
            database = null;
        }

        public static void DeleteDatabase()
        {
            using (database = new SenpaiDatabase(AppResources.DatabaseConnectionString))
            {
                if (database.DatabaseExists())
                {
                    database.DeleteDatabase();
                }
            }
            database = null;
        }

        public static void ConnectToDatabase()
        {
            if (database != null)
            {
                return;
            }

            sb = new StringBuilder();
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
            sb = null;
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
            throw new NotImplementedException();

            //using(AppStreamReader sr = new AppStreamReader(contentStream))
            //{
            //    try
            //    {
            //        String line = sr.ReadLine();
                    
            //        do
            //        {
            //            String[] parts = line.Split('|');

            //            int operation = Convert.ToInt32(parts[0]);
            //            int opCount = Convert.ToInt32(parts[1]);
                        
            //            //switch(operation)
            //            //{
            //            //    case 0: AddNewLessons(sr, opCount, results); break;
            //            //    case 1: AddItemsToLessons(sr, opCount, results); break;
            //            //    case 2: results.updatedItems += UpdateItems  (sr, opCount); break;
            //            //    case 3: results.updatedItems += UpdateLessons(sr, opCount); break;
            //            //    case 4: results.deletedItems += DeleteItems  (sr, opCount); break;
            //            //}
                        
            //            line = sr.ReadLine();
            //        }
            //        while (line != null);
                    
            //        database.SubmitChanges();
            //    }
            //    catch (Exception e)
            //    {
            //        sb.Clear();
            //        sb.AppendLine("Inhalt hinzufügen Fehlgeschlagen in Zeile: " + sr.CurrentLine);
            //        sb.AppendLine("System: " + e.Message);

            //        String failureMessage = sb.ToString();

            //        sb.Clear();

            //        return failureMessage;
            //    }
            //}

            //return null;
        }

        private static int AddNewLessons(AppStreamReader sr, int opCount)
        {
            for(int i = 0; i < opCount; ++i)
            {
                String line = sr.ReadLine();
                String[] parts = line.Split('|');
                
                Lesson lesson = new Lesson();

                lesson.name = parts[0];
                lesson.size = Convert.ToInt32(parts[1]);
                lesson.Type = (Lesson.EType)Convert.ToInt32(parts[2]);

                //for(int j = 0; j < lesson.size; ++j)
                //{
                //    switch(lesson.Type)
                //    {
                //        case Lesson.EType.vocab : results.addedWords  += AddWords (sr, lesson); break;
                //        case Lesson.EType.kanji : results.addedKanjis += AddKanjis(sr, lesson); break;
                //        case Lesson.EType.insert: results.addedClozes += AddClozes(sr, lesson); break;
                //    }
                //}

                database.lessons.InsertOnSubmit(lesson);
            }

            return 0;
        }

        private static int AddWords(AppStreamReader sr, Lesson lesson)
        {
            for(int i = 0; i < lesson.size; ++i)
            {
                String line = sr.ReadLine();
                String[] parts = line.Split('|');

                Word word = new Word();
                
                word.kana        = parts[0];
                word.kanji       = parts[1];
                word.translation = parts[2];
                word.description = parts[3];
                word.Type        = (Word.EType)Convert.ToInt32(parts[4]);
             
                word.Lesson = lesson;                
                lesson.Words.Add(word);

                database.words.InsertOnSubmit(word);
            }

            return lesson.size;
        }
        
        private static int AddKanjis(AppStreamReader sr, Lesson lesson)
        {
            for(int i = 0; i < lesson.size; ++i)
            {
                String line = sr.ReadLine();
                String[] parts = line.Split('|');

                Kanji kanji = new Kanji();
                
                kanji.kanji       = parts[0];
                kanji.meaning     = parts[1];
                kanji.onyomi      = parts[2];
                kanji.kunyomi     = parts[3];
                kanji.example     = parts[4];
                kanji.strokeOrder = parts[5];
             
                kanji.Lesson = lesson;                
                lesson.Kanjis.Add(kanji);

                database.kanjis.InsertOnSubmit(kanji);
            }
            return lesson.size;
        }
        
        private static int AddClozes(AppStreamReader sr, Lesson lesson)
        {
            for(int i = 0; i < lesson.size; ++i)
            {
                String line = sr.ReadLine();
                String[] parts = line.Split('|');

                Cloze cloze = new Cloze();
                
                cloze.text    = parts[0];
                cloze.inserts = parts[1];
                cloze.hints   = parts[2];
             
                cloze.Lesson = lesson;                
                lesson.Clozes.Add(cloze);

                database.clozes.InsertOnSubmit(cloze);
            }
            return lesson.size;
        }
        
        private static int AddItemsToLessons(AppStreamReader sr, int opCount)
        {
            for(int i = 0; i < opCount; ++i)
            {
                String line = sr.ReadLine();
                String[] parts = line.Split('|');

            }
            return 0;
        }

        private static int UpdateItems(AppStreamReader sr, int opCount)
        {
            return 0;
        }

        private static int UpdateLessons(AppStreamReader sr, int opCount)
        {
            return 0;
        }

        private static int DeleteItems(AppStreamReader sr, int opCount)
        {
            return 0;
        }

        #endregion

        #region Import

        public static String ImportFromFile(Stream importStream)
        {
            List<Lesson> oldLessons = new List<Lesson>();

            oldLessons.AddRange(database.lessons);

            using (AppStreamReader sr = new AppStreamReader(importStream))
            {
                try
                {
                    AppSettings.VocabRound = Convert.ToInt32(sr.ReadLine());
                    AppSettings.KanjiRound = Convert.ToInt32(sr.ReadLine());

                    String line = sr.ReadLine();
                    
                    int importedLessons = 0;
                    int importedWords   = 0;
                    int importedKanjis  = 0;
                    int importedClozes  = 0;

                    do
                    {
                        Lesson lesson = ImportLesson(line);

                        ++importedLessons;

                        switch (lesson.Type)
                        {
                            case Lesson.EType.vocab : importedWords  += ImportWords (lesson, sr); break;
                            case Lesson.EType.insert: importedClozes += ImportClozes(lesson, sr); break;
                            case Lesson.EType.kanji : importedKanjis += ImportKanjis(lesson, sr); break;
                        }

                        line = sr.ReadLine();
                    }
                    while (line != null);

                    sb.AppendLine("Importierte Lektionen:\t"   + importedLessons);
                    sb.AppendLine("Importierte Wörter:\t"      + importedWords);
                    sb.AppendLine("Importierte Kanjis:\t"      + importedKanjis);
                    sb.AppendLine("Importierte Lückentexte:\t" + importedClozes);
                    
                    foreach(Lesson oldLesson in oldLessons)
                    {
                        DeleteLesson(oldLesson);
                    }

                    AppSettings.SaveSettings();
                    database.SubmitChanges();
                }
                catch (Exception e)
                {
                    sb.Clear();
                    sb.AppendLine("Import Fehlgeschlagen in Zeile: " + sr.CurrentLine);
                    sb.AppendLine("System: " + e.Message);
                }
            }

            String importResults = sb.ToString();

            sb.Clear();

            return importResults;
        }

        private static Lesson ImportLesson(String lessonString)
        {
            Lesson lesson = new Lesson(lessonString);
    
            database.lessons.InsertOnSubmit(lesson);

            return lesson;
        }

        private static int ImportWords(Lesson lesson, AppStreamReader sr)
        {
            for (int i = 0; i < lesson.size; ++i)
            {
                String line = sr.ReadLine();
                
                Word word = new Word(line);
                
                word.Lesson = lesson;
                lesson.Words.Add(word);

                AppSettings.TimeStamp = Math.Max(AppSettings.TimeStamp, word.Timestamp());
            }
            
            database.words.InsertAllOnSubmit(lesson.Words);

            return lesson.size;
        }
        
        private static int ImportClozes(Lesson lesson, AppStreamReader sr)
        {
            for (int i = 0; i < lesson.size; ++i)
            {
                String line = sr.ReadLine();
                
                Cloze cloze = new Cloze(line);

                cloze.Lesson = lesson;
                lesson.Clozes.Add(cloze);
            }

            database.clozes.InsertAllOnSubmit(lesson.Clozes);

            return lesson.size;
        }

        private static int ImportKanjis(Lesson lesson, AppStreamReader sr)
        {
            for (int i = 0; i < lesson.size; ++i)
            {
                String line = sr.ReadLine();
                
                Kanji kanji = new Kanji(line);
        
                kanji.Lesson = lesson;
                lesson.Kanjis.Add(kanji);

                AppSettings.TimeStamp = Math.Max(AppSettings.TimeStamp, kanji.timestamp);
            }

            database.kanjis.InsertAllOnSubmit(lesson.Kanjis);

            return lesson.size;
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
        /// <para>Word-1:id|lessonId|kana|kanji|translation|description|type|eFactorTransl|lastRoundTransl|nextRoundTransl|eFactorJap|lastRoundJap|nextRoundJap|showFlags|timeStampTransl|timeStampJap</para>
        /// 
        /// <para>Word-size:id|lessonId|kana|kanji|translation|description|type|eFactorTransl|lastRoundTransl|nextRoundTransl|eFactorJap|lastRoundJap|nextRoundJap|showFlags|timeStampTransl|timeStampJap</para>
        /// 
        /// <para>or</para>
        /// 
        /// <para>Kanji-1:id|lessonId|kanji|meaning|onyomi|kunyomi|example|strokeOrder|efactor|lastRound|nextRound|timestamp</para>
        /// <para>...</para>
        /// <para>Kanji-size:id|lessonId|kanji|meaning|onyomi|kunyomi|example|strokeOrder|efactor|lastRound|nextRound|timestamp</para>
        /// 
        /// <para>or</para>
        /// 
        /// <para>Cloze-1:id|lessonID|text|insertText|hintText</para>
        /// <para>...</para>
        /// <para>Cloze-size:id|lessonID|text|insertText|hintText</para>
        /// 
        /// <para>empty-line = end</para>
        /// </summary>
        public static String ExportToFile(Stream exportStream)
        {
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

                    switch (lesson.Type)
                    {
                        case Lesson.EType.vocab : exportedWords  += WriteVocabLesson (sw, lesson); break;
                        case Lesson.EType.insert: exportedClozes += WriteInsertLesson(sw, lesson); break;
                        case Lesson.EType.kanji : exportedKanjis += WriteKanjiLesson (sw, lesson); break;
                    }

                    ++exportedLessons;
                }

                sb.AppendLine("Datenbank erfolgreich gesichert!" );
                sb.AppendLine("Lektionen\t: "   + exportedLessons);
                sb.AppendLine("Wörter\t: "      + exportedWords  );
                sb.AppendLine("Lückentexte\t: " + exportedClozes );
                sb.AppendLine("Kanjis\t\t: "    + exportedKanjis );
            }

            String exportResults = sb.ToString();

            sb.Clear();

            return exportResults;
        }

        private static int WriteVocabLesson(AppStreamWriter sw, Lesson lesson)
        {
            foreach (Word word in lesson.Words)
            {
                sw.WriteLine(word.ToExportString());
            }

            return lesson.Words.Count;
        }

        private static int WriteInsertLesson(AppStreamWriter sw, Lesson lesson)
        {
            foreach (Cloze cloze in lesson.Clozes)
            {
                sw.WriteLine(cloze.ToExportString());
            }

            return lesson.Clozes.Count;
        }

        private static int WriteKanjiLesson(AppStreamWriter sw, Lesson lesson)
        {
            foreach (Kanji kanji in lesson.Kanjis)
            {
                sw.WriteLine(kanji.ToExportString());
            }

            return lesson.Kanjis.Count;
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

    //struct SUpdateResults
    //{
    //    public int addedLessons;
    //    public int addedWords;
    //    public int addedKanjis;
    //    public int addedClozes;
    //    public int updatedItems;
    //    public int deletedItems;

    //    public override string ToString()
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        if(addedLessons > 0)
    //        {
    //            sb.AppendLine("Hinzugefügte Lektionen:\t" + addedLessons);
    //        }
    //        if(addedWords > 0)
    //        {
    //            sb.AppendLine("Hinzugefügte Wörter:\t" + addedWords);
    //        }
    //        if(addedKanjis > 0)
    //        {
    //            sb.AppendLine("Hinzugefügte Kanjis:\t" + addedKanjis);
    //        }
    //        if(addedClozes > 0)
    //        {
    //            sb.AppendLine("Hinzugefügte Lückentexte:\t" + addedClozes);
    //        }
    //        if(updatedItems > 0)
    //        {
    //            sb.AppendLine("Geupdatete Objekte:\t" + updatedItems);
    //        }
    //        if(deletedItems > 0)
    //        {
    //            sb.AppendLine("Gelöschte Objekte:\t" + deletedItems);
    //        }

    //        return sb.ToString();
    //    }
    //}
}
