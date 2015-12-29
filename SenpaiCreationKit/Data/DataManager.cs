using SenpaiCreationKit.Resources;
using SenpaiCreationKit.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SenpaiCreationKit.Data
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

        #region Public Methods

        #region Database

        public static void CreateDatabase()
        {
            using (database = new SenpaiDatabase(AppResources.dbConnection))
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
            using (database = new SenpaiDatabase(AppResources.dbConnection))
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

            database = new SenpaiDatabase(AppResources.dbConnection);
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
        
        public static void RefreshConnection()
        {
            CloseConnection();
            ConnectToDatabase();
        }

        public static void SaveChanges(bool trackChanges = true)
        {
#if DEBUG

            database.SubmitChanges(ConflictMode.ContinueOnConflict, trackChanges);

            foreach (ObjectChangeConflict occ in database.ChangeConflicts)
            {
                Debug.WriteLine(occ.ToString());
            }
            
#else
            
            database.SubmitChanges(ConflictMode.FailOnFirstConflict);

#endif
        }

        #endregion
        
        #region Load Tables

        #region Load Lessons
        
        public static IQueryable<Lesson> LoadLessons()
        {
            return database.GetLessons();
        }

        public static IQueryable<Lesson> LoadLessons(String searchText)
        {
            return database.GetLessons(searchText);
        }

        ///// <summary>
        ///// Loads all lessons from the database into AppData.Lessons
        ///// </summary>
        //public static void LoadAllLessons()
        //{
        //    AppData.Lessons = database.lessons.;
        //}

        ///// <summary>
        ///// Loads all lessons with the given type into AppData.Lessons
        ///// </summary>
        //public static void LoadLessons(Lesson.EType type)
        //{
        //    AppData.Lessons = database.GetLessons(type);
        //}

        ///// <summary>
        ///// Loads the given lesson into AppData.SelectedLesson and all Words/Kanjis/Clozes assigned to the given lesson into Appdata.Words/Clozes/Kanjis
        ///// </summary>
        //public static void LoadLesson(Lesson lesson)
        //{
        //    AppData.SelectedLesson = lesson;

        //    switch (lesson.Type)
        //    {
        //        case Lesson.EType.vocab: AppData.Words = lesson.Words.GetEnumerator(); break;
        //        case Lesson.EType.kanji: AppData.Kanjis = database.GetKanjis(lesson); break;
        //        case Lesson.EType.insert: AppData.Clozes = database.GetClozes(lesson); break;
        //    }
        //}

        ///// <summary>
        ///// Loads the lesson identified by the given lessonID into AppData.SelectedLesson, and the 
        /////  Words/Kanjis/Clozes assigned by this lesson into  Appdata.Words/Clozes/Kanjis
        ///// </summary>
        //public static void LoadLesson(int lessonID)
        //{
        //    Lesson lesson = database.GetLesson(lessonID);

        //    AppData.SelectedLesson = lesson;

        //    switch (lesson.Type)
        //    {
        //        case Lesson.EType.vocab: AppData.Words = database.GetWords(lesson); break;
        //        case Lesson.EType.kanji: AppData.Kanjis = database.GetKanjis(lesson); break;
        //        case Lesson.EType.insert: AppData.Clozes = database.GetClozes(lesson); break;
        //    }
        //}
        
        #endregion

        #region Load Words/Kanjis/Clozes

        ///// <summary>
        ///// Loads all Words from the database into Appdata.Words
        ///// </summary>
        //public static void LoadAllWords()
        //{
        //    AppData.Words = database.GetWords();
        //}
        
        ///// <summary>
        ///// Loads all Kanjis from the database into Appdata.Kanjis
        ///// </summary>
        //public static void LoadAllKanjis()
        //{
        //    AppData.Kanjis = database.GetKanjis();
        //}
        
        ///// <summary>
        ///// Loads all Clozes from the database into Appdata.Clozes
        ///// </summary>
        //public static void LoadAllClozes()
        //{
        //    AppData.Clozes = database.GetClozes();
        //}

        #endregion

        #endregion

        #region Create Tables

        public static void CreateExportData()
        {
            using(database = new SenpaiDatabase(AppResources.dbConnection))
            {
                //database.CreateExportData();
            }
            database = null;
        }

        public static void CreateVocabLesson(Lesson lesson, List<Word> words)
        {
            foreach(Word word in words)
            {
                word.Lesson = lesson;
                lesson.Words.Add(word);
            }
            
            database.words.InsertAllOnSubmit(lesson.Words);
            database.lessons.InsertOnSubmit(lesson);
        }

        public static void CreateFlashLesson(Lesson lesson, List<Kanji> kanjis)
        {
            foreach(Kanji kanji in kanjis)
            {
                kanji.Lesson = lesson;
                lesson.Kanjis.Add(kanji);
            }

            database.kanjis.InsertAllOnSubmit(lesson.Kanjis);
            database.lessons.InsertOnSubmit(lesson);
        }
        
        public static void CreateInsertLesson(Lesson lesson, List<Cloze> clozes)
        {
            foreach(Cloze cloze in clozes)
            {
                cloze.Lesson = lesson;
                lesson.Clozes.Add(cloze);
            }

            database.clozes.InsertAllOnSubmit(lesson.Clozes);
            database.lessons.InsertOnSubmit(lesson);
        }

        #endregion

        #region Update Tables
        
        public static void UpdateVocabLesson(Lesson lesson, List<Word> newWords, List<Word> wordsToDelete)
        {
            foreach(Word word in newWords)
            {
                word.Lesson = lesson;
                lesson.Words.Add(word);
            }

            foreach(Word word in wordsToDelete)
            {
                lesson.Words.Remove(word);
            }

            database.words.InsertAllOnSubmit(newWords);
            database.words.DeleteAllOnSubmit(wordsToDelete);
        }

        public static void UpdateFlashLesson(Lesson lesson, List<Kanji> newKanjis, List<Kanji> kanjisToDelete)
        {
            foreach(Kanji kanji in newKanjis)
            {
                kanji.Lesson = lesson;
                lesson.Kanjis.Add(kanji);
            }

            foreach(Kanji kanji in kanjisToDelete)
            {
                lesson.Kanjis.Remove(kanji);
            }

            database.kanjis.InsertAllOnSubmit(newKanjis);
            database.kanjis.DeleteAllOnSubmit(kanjisToDelete);
        }

        public static void UpdateInsertLesson(Lesson lesson, List<Cloze> newClozes, List<Cloze> clozesToDelete)
        {
            foreach(Cloze cloze in newClozes)
            {
                cloze.Lesson = lesson;
                lesson.Clozes.Add(cloze);
            }

            foreach(Cloze cloze in clozesToDelete)
            {
                lesson.Clozes.Remove(cloze);
            }

            database.clozes.InsertAllOnSubmit(newClozes);
            database.clozes.DeleteAllOnSubmit(clozesToDelete);
        }


        #endregion

        #region Delete Tables
        
        /// <summary>
        /// Deletes the given lesson and all Words/Kanjis/Clozes assigned to this lesson
        /// </summary>
        public static void DeleteLesson(Lesson lesson)
        {
            switch(lesson.Type)
            {
                case Lesson.EType.vocab : database.words .DeleteAllOnSubmit(lesson.Words ); break;
                case Lesson.EType.kanji : database.kanjis.DeleteAllOnSubmit(lesson.Kanjis); break;
                case Lesson.EType.insert: database.clozes.DeleteAllOnSubmit(lesson.Clozes); break;
            }

            database.lessons.DeleteOnSubmit(lesson);
        }

        ///// <summary>
        ///// Deletes all given lessons and the assigned Words/Kanjis/Clozes
        ///// </summary>
        //public static void DeleteLessons(IEnumerable<Lesson> lessons)
        //{
        //    foreach(Lesson lesson in lessons)
        //    {
        //        DeleteLesson(lesson);
        //    }
        //}   
             
        ///// <summary>
        ///// Deletes the given Word
        ///// </summary>
        //public static void DeleteWord(Word word)
        //{
        //    database.words.DeleteOnSubmit(word);
        //}

        ///// <summary>
        ///// Deletes all words given by the words param
        ///// </summary>
        //public static void DeleteWords(IEnumerable<Word> words)
        //{
        //    database.words.DeleteAllOnSubmit(words);
        //}

        ///// <summary>
        ///// Deletes the given kanji
        ///// </summary>
        //public static void DeleteKanji(Kanji kanji)
        //{
        //    database.kanjis.DeleteOnSubmit(kanji);
        //}

        ///// <summary>
        ///// Deletes all kanjis given by the kanjis param
        ///// </summary>
        //public static void DeleteKanjis(IEnumerable<Kanji> kanjis)
        //{
        //    database.kanjis.DeleteAllOnSubmit(kanjis);
        //}
        
        ///// <summary>
        ///// Deletes the given Cloze
        ///// </summary>
        //public static void DeleteCloze(Cloze Cloze)
        //{
        //    database.Clozes.DeleteOnSubmit(Cloze);
        //}

        ///// <summary>
        ///// Deletes all Clozes given by the Clozes param
        ///// </summary>
        //public static void DeleteClozes(IEnumerable<Cloze> Clozes)
        //{
        //    database.Clozes.DeleteAllOnSubmit(Clozes);
        //}

        #endregion

        #region Import/Export Tables

        #region Import
        
        #endregion

        #region Export

        public static void ExportLessons(List<Lesson> lessons, AppStreamWriter sw)
        {
            foreach(Lesson lesson in lessons)
            {
                sw.WriteLine(lesson.ToCreateNewString());

                switch(lesson.Type)
                {
                    case Lesson.EType.vocab : WriteWords (lesson, sw); break;
                    case Lesson.EType.insert: WriteClozes(lesson, sw); break;
                    case Lesson.EType.kanji : WriteKanjis(lesson, sw); break;
                }
            }

            foreach(ExportData data in database.exportData)
            {

            }
        }

        private static void WriteWords(Lesson lesson, AppStreamWriter sw)
        {
            foreach(Word word in lesson.Words)
            {
                sw.WriteLine(word.ToCreateNewString());
            }
        }

        private static void WriteKanjis(Lesson lesson, AppStreamWriter sw)
        {
            foreach(Kanji kanji in lesson.Kanjis)
            {
                sw.WriteLine(kanji.ToCreateNewString());
            }
        }

        private static void WriteClozes(Lesson lesson, AppStreamWriter sw)
        {
            foreach(Cloze cloze in lesson.Clozes)
            {
                sw.WriteLine(cloze.ToCreateNewString());
            }
        }

        #endregion

        #endregion

        //#region Load Tables
        
        //public static void LoadLessons()
        //{
        //    AppData.Lessons = database.GetLessons();
        //}

        //public static void LoadLessons(Lesson.EType type)
        //{
        //    AppData.Lessons = database.GetLessons(type);
        //}

        //public static void LoadLessonsUnsorted()
        //{
        //    AppData.Lessons = database.GetLessonsUnsorted();
        //}

        //public static void LoadWords(int lessonID)
        //{
        //    AppData.SelectedLesson = database.GetLesson(lessonID);
        //    AppData.Words = database.GetWords(AppData.SelectedLesson);
        //}

        //public static void LoadWords(Lesson lesson)
        //{
        //    AppData.Words = database.GetWords(lesson);
        //    AppData.SelectedLesson = lesson;
        //}

        //public static void LoadWords(Lesson[] lessons)
        //{
        //    AppData.Words = database.GetWords(lessons);
        //}

        //public static void LoadWords(Lesson[] lessons, Word.EType[] types)
        //{
        //    AppData.Words = database.GetWords(lessons, types);
        //}
        
        //public static void LoadClozes()
        //{
        //    AppData.Clozes = database.GetClozes();
        //}

        //public static void LoadClozes(Lesson lesson)
        //{
        //    AppData.Clozes = database.GetClozes(lesson);
        //    AppData.SelectedLesson = lesson;
        //}

        //public static void LoadClozes(Lesson[] lessons)
        //{
        //    AppData.Clozes = database.GetClozes(lessons);
        //}

        //public static void LoadKanjis()
        //{
        //    AppData.Kanjis = database.GetKanjis();
        //}
        
        //public static void LoadKanjis(Lesson lesson)
        //{
        //    AppData.Kanjis = database.GetKanjis(lesson);
        //    AppData.SelectedLesson = lesson;
        //}
        
        //public static void LoadKanjis(Lesson[] lessons)
        //{
        //    AppData.Kanjis = database.GetKanjis(lessons);
        //}
        
        ///// <summary>
        ///// searches for words that contains the given substring
        ///// the string can be part of the kana, kanji and translation members of Word
        ///// </summary>
        //public static void FindWords(String substring)
        //{
        //    Word[] words = database.GetWords(substring);

        //    if (words.Length > 0)
        //    {
        //        AppData.Words = words;
        //    }
        //}

        //#endregion

        //#region Update Tables


        ////#region Import

        ////public static String ImportFromFile(Stream importStream)
        ////{
        ////    StringBuilder importResults = new StringBuilder();

        ////    using (AppStreamReader sr = new AppStreamReader(importStream))
        ////    {
        ////        try
        ////        {
        ////            String line = sr.ReadLine();

        ////            do
        ////            {
        ////                String[] parts = line.Split('|');
        ////                int operation = Convert.ToInt32(parts[0]);
        ////                int itemCount = Convert.ToInt32(parts[1]);

        ////                String results = "";

        ////                switch (operation)
        ////                {
        ////                    case 0: results = AddLessons(sr, itemCount); break;
        ////                    case 1: results = UpdateLessons(sr, itemCount); break;
        ////                    case 2: results = UpdateWords(sr, itemCount); break;
        ////                    case 3: results = UpdateKanjis(sr, itemCount); break;
        ////                    case 4: results = AddWords(sr, itemCount); break;
        ////                    case 5: results = AddKanjis(sr, itemCount); break;
        ////                }

        ////                importResults.Append(results);

        ////                line = sr.ReadLine();
        ////            }
        ////            while (line != null);

        ////            database.SubmitChanges();
        ////        }
        ////        catch (Exception e)
        ////        {
        ////            importResults.Clear();
        ////            importResults.AppendLine("Import Fehlgeschlagen in Zeile: " + sr.CurrentLine);
        ////            importResults.AppendLine("System: " + e.Message);
        ////        }
        ////    }

        ////    return importResults.ToString();
        ////}

        ////private static String AddLessons(AppStreamReader sr, int itemCount)
        ////{
        ////    int lessonID = database.GetLastLessonID();

        ////    int importedLessons = 0;
        ////    int importedWords = 0;
        ////    int importedClozes = 0;
        ////    int importedKanjis = 0;

        ////    for (int i = 0; i < itemCount; ++i)
        ////    {
        ////        String line = sr.ReadLine();

        ////        Lesson lesson = new Lesson(line);
        ////        lesson.id = ++lessonID;

        ////        switch (lesson.type)
        ////        {
        ////            case 0: importedWords += ReadVocabLesson(sr, lesson); break;
        ////            case 1: importedClozes += ReadInsertLesson(sr, lesson); break;
        ////            case 2: break;
        ////            case 3: importedKanjis += ReadKanjiLesson(sr, lesson); break;
        ////            case 4: break;
        ////        }
        ////        database.lessons.InsertOnSubmit(lesson);

        ////        ++importedLessons;
        ////    }

        ////    StringBuilder sb = new StringBuilder();

        ////    sb.AppendLine("Hinzugefügte Lektionen\t: " + importedLessons);
        ////    sb.AppendLine("Hinzugefügte Wörter\t: " + importedWords);
        ////    sb.AppendLine("Hinzugefügte Lückentexte\t: " + importedClozes);
        ////    sb.AppendLine("Hinzugefügte Kanjis\t: " + importedKanjis);

        ////    return sb.ToString();
        ////}

        ////private static int ReadVocabLesson(AppStreamReader sr, Lesson lesson)
        ////{
        ////    Word[] words = new Word[lesson.size];

        ////    for (int i = 0; i < words.Length; ++i)
        ////    {
        ////        String line = sr.ReadLine();

        ////        words[i] = new Word(line, lesson.id);
        ////    }

        ////    database.words.InsertAllOnSubmit(words);

        ////    return words.Length;
        ////}

        ////private static int ReadInsertLesson(AppStreamReader sr, Lesson lesson)
        ////{
        ////    Cloze[] Clozes = new Cloze[lesson.size];

        ////    for (int i = 0; i < Clozes.Length; ++i)
        ////    {
        ////        String line = sr.ReadLine();

        ////        Clozes[i] = new Cloze(line, lesson.id);
        ////    }

        ////    database.Clozes.InsertAllOnSubmit(Clozes);

        ////    return Clozes.Length;
        ////}

        ////private static int ReadKanjiLesson(AppStreamReader sr, Lesson lesson)
        ////{
        ////    Kanji[] kanjis = new Kanji[lesson.size];

        ////    for (int i = 0; i < kanjis.Length; ++i)
        ////    {
        ////        String line = sr.ReadLine();

        ////        kanjis[i] = new Kanji(line, lesson.id);
        ////    }

        ////    database.kanjis.InsertAllOnSubmit(kanjis);

        ////    return kanjis.Length;
        ////}

        ////private static String UpdateLessons(AppStreamReader sr, int itemCount)
        ////{
        ////    for (int i = 0; i < itemCount; ++i)
        ////    {
        ////        String line = sr.ReadLine();

        ////        Lesson newLesson = new Lesson(line);
        ////        Lesson oldLesson = database.GetLesson(newLesson.id);

        ////        oldLesson.Update(newLesson);
        ////    }
        ////    StringBuilder sb = new StringBuilder();
        ////    sb.AppendLine("Aktualisierte Lektionen\t: " + itemCount);
        ////    return sb.ToString();
        ////}

        ////private static String UpdateWords(AppStreamReader sr, int itemCount)
        ////{
        ////    for (int i = 0; i < itemCount; ++i)
        ////    {
        ////        String line = sr.ReadLine();

        ////        String idStr = line.Substring(0, line.IndexOf('|'));
        ////        Word w = database.GetWord(Convert.ToInt32(idStr));

        ////        w.Update(line);
        ////    }
        ////    StringBuilder sb = new StringBuilder();
        ////    sb.AppendLine("Aktualisierte Wörter\t: " + itemCount);
        ////    return sb.ToString();
        ////}

        ////private static String UpdateKanjis(AppStreamReader sr, int itemCount)
        ////{
        ////    for (int i = 0; i < itemCount; ++i)
        ////    {
        ////        String line = sr.ReadLine();

        ////        String idStr = line.Substring(0, line.IndexOf('|'));
        ////        Kanji k = database.GetKanji(Convert.ToInt32(idStr));

        ////        k.Update(line);
        ////    }
        ////    StringBuilder sb = new StringBuilder();
        ////    sb.AppendLine("Aktualisierte Kanjis\t: " + itemCount);
        ////    return sb.ToString();
        ////}

        ////private static String AddWords(AppStreamReader sr, int itemCount)
        ////{
        ////    Dictionary<int, int> lessonsDict = new Dictionary<int, int>();

        ////    for (int i = 0; i < itemCount; ++i)
        ////    {
        ////        String line = sr.ReadLine();
        ////        String[] parts = line.Split('|');

        ////        int lessonID = Convert.ToInt32(parts[0]);

        ////        if (lessonsDict.ContainsKey(lessonID))
        ////        {
        ////            lessonsDict[lessonID] += 1;
        ////        }
        ////        else
        ////        {
        ////            lessonsDict.Add(lessonID, 1);
        ////        }

        ////        Word w = new Word(line, lessonID);
        ////        database.words.InsertOnSubmit(w);
        ////    }

        ////    foreach (KeyValuePair<int, int> pair in lessonsDict)
        ////    {
        ////        Lesson l = database.GetLesson(pair.Key);
        ////        l.size += pair.Value;
        ////    }

        ////    StringBuilder sb = new StringBuilder();
        ////    sb.AppendLine("Wörter zu bestehenden Lektionen hinzugefügt: " + itemCount);
        ////    return sb.ToString();
        ////}

        ////private static String AddKanjis(AppStreamReader sr, int itemCount)
        ////{
        ////    Dictionary<int, int> lessonsDict = new Dictionary<int, int>();

        ////    for (int i = 0; i < itemCount; ++i)
        ////    {
        ////        String line = sr.ReadLine();
        ////        String[] parts = line.Split('|');

        ////        int lessonID = Convert.ToInt32(parts[1]);

        ////        if (lessonsDict.ContainsKey(lessonID))
        ////        {
        ////            lessonsDict[lessonID] += 1;
        ////        }
        ////        else
        ////        {
        ////            lessonsDict.Add(lessonID, 1);
        ////        }

        ////        Kanji k = new Kanji(line, lessonID);
        ////        database.kanjis.InsertOnSubmit(k);
        ////    }

        ////    foreach (KeyValuePair<int, int> pair in lessonsDict)
        ////    {
        ////        Lesson l = database.GetLesson(pair.Key);
        ////        l.size += pair.Value;
        ////    }

        ////    StringBuilder sb = new StringBuilder();
        ////    sb.AppendLine("Kanjis zu bestehenden Lektionen hinzugefügt: " + itemCount);
        ////    return sb.ToString();
        ////}
        
        ////#endregion

        ////#region Export

        ////public static String ExportToFile(Stream exportStream)
        ////{            
        ////    StringBuilder exportResults = new StringBuilder();

        ////    using (AppStreamWriter sw = new AppStreamWriter(exportStream))
        ////    {
        ////        LoadLessonsUnsorted();

        ////        //write Number of Lessons
        ////        sw.WriteLine("0|" + AppData.Lessons.Length.ToString());

        ////        int exportedLessons = 0;
        ////        int exportedWords = 0;
        ////        int exportedClozes = 0;
        ////        int exportedKanjis = 0;

        ////        foreach (Lesson lesson in AppData.Lessons)
        ////        {
        ////            sw.WriteLine(lesson.ToExportString());

        ////            switch (lesson.type)
        ////            {
        ////                case 0: exportedWords += WriteVocabLesson(sw, lesson); break;
        ////                case 1: exportedClozes += WriteInsertLesson(sw, lesson); break;
        ////                case 2: break;
        ////                case 3: exportedKanjis += WriteKanjiLesson(sw, lesson); break;
        ////                case 4: break;
        ////            }

        ////            ++exportedLessons;
        ////        }

        ////        exportResults.AppendLine("Datenbank erfolgreich exportiert!");
        ////        exportResults.AppendLine("Exportierte Lektionen\t: " + exportedLessons);
        ////        exportResults.AppendLine("Exportierte Wörter\t: " + exportedWords);
        ////        exportResults.AppendLine("Exportierte Lückentexte\t: " + exportedClozes);
        ////        exportResults.AppendLine("Exportierte Kanjis\t\t: " + exportedKanjis);
        ////    }

        ////    AppData.Lessons = null;
        ////    AppData.Words = null;
        ////    AppData.Clozes = null;
        ////    AppData.Kanjis = null;

        ////    return exportResults.ToString();
        ////}

        ////private static int WriteVocabLesson(AppStreamWriter sw, Lesson lesson)
        ////{
        ////    LoadWords(lesson);

        ////    foreach (Word word in AppData.Words)
        ////    {
        ////        sw.WriteLine(word.ToExportString());
        ////    }

        ////    return AppData.Words.Length;
        ////}

        ////private static int WriteInsertLesson(AppStreamWriter sw, Lesson lesson)
        ////{
        ////    LoadClozes(lesson);

        ////    foreach (Cloze Cloze in AppData.Clozes)
        ////    {
        ////        sw.WriteLine(Cloze.ToExportString());
        ////    }

        ////    return AppData.Clozes.Length;
        ////}

        ////private static int WriteKanjiLesson(AppStreamWriter sw, Lesson lesson)
        ////{
        ////    LoadKanjis(lesson);

        ////    foreach (Kanji kanji in AppData.Kanjis)
        ////    {
        ////        sw.WriteLine(kanji.ToExportString());
        ////    }

        ////    return AppData.Kanjis.Length;
        ////}
        
        ////#endregion

        //#endregion

        #region Other

        //public static void SortWords(bool accending, bool sortByTranslation)
        //{
        //    Comparer comparer = new Comparer();
            
        //    Array.Sort(AppData.Words, comparer);
        //}

        //#region Class Comparer

        //private class Comparer : IComparer
        //{
        //    public delegate int Comp(Word x, Word y);

        //    public Comp compFunction;
            
        //    public int Compare(object x, object y)
        //    {
        //        return compFunction(x as Word, y as Word);
        //    }
        //}
        
        //#endregion

        #endregion

        #endregion
    }
}