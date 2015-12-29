using System;
using System.IO.IsolatedStorage;

namespace NihongoSenpai.Settings
{
    #region ESortOrder
        
    public enum ESortOrder
    {
        byLesson,
        timestamp,
        random,
        count,
        undefined = -1,
    }

    #endregion

    #region EPracticeDirection
        
    public enum EPracticeDirection
    {
        japGer,
        ger,
        jap,
        count,
        undefined = -1,
    }

    #endregion

    public static class AppSettings
    {
        #region Fields

        private static IsolatedStorageSettings settings;

        #endregion

        #region Properties

        /// <summary>
        /// Checks if the app starts the first time
        /// </summary>
        public static bool FirstStart
        {
            get { return (bool)settings["firstStart"]; }
            set { settings["firstStart"] = value; }
        }

        /// <summary>
        /// if true, all words will be loaded on PracticeVocabPage and ignores word.showFlags 
        /// </summary>
        public static bool LoadAllWords
        {
            get { return (bool)settings["loadAllWords"]; }
            set { settings["loadAllWords"] = value; }
        }
        
        /// <summary>
        /// if true, all Kanjis will be loaded and ignores Supermemo NextInterval and Repetition
        /// </summary>
        public static bool LoadAllKanji
        {
            get { return (bool)settings["loadAllKanji"]; }
            set { settings["loadAllKanji"] = value; }
        }

        /// <summary>
        /// if true, Description in PracticeVocabPage will be shown
        /// </summary>
        public static bool ShowDescription
        {
            get { return (bool)settings["showDescription"]; }
            set { settings["showDescription"] = value; }
        }
        
        /// <summary>
        /// Path to background Image on MainPage
        /// </summary>
        public static String BackgroundImage
        {
            get { return settings["backgroundImage"] as string; }
            set { settings["backgroundImage"] = value; }
        }

        /// <summary>
        /// Says how Many new Words will be added to the new round
        /// </summary>
        public static int NewWordsPerRound
        {
            get { return (int)settings["newWordsPerRound"]; }
            set { settings["newWordsPerRound"] = value; }
        }
        
        /// <summary>
        /// Says how Many new Kanji will be added to the new round
        /// </summary>
        public static int NewKanjiPerRound
        {
            get { return (int)settings["newKanjiPerRound"]; }
            set { settings["newKanjiPerRound"] = value; }
        }
        
        /// <summary>
        /// is used to lessons for word practice in differend ways
        /// <para>0 - shows translation and jWord</para>
        /// <para>1 - shows just translation</para>
        /// <para>2 - shows just jWord</para>
        /// </summary>
        public static EPracticeDirection VocabPracticeDirection
        {
            get { return (EPracticeDirection)settings["vocabPracticeDirection"]; }
            set { settings["vocabPracticeDirection"] = value; }
        }
        
        /// <summary>
        /// is incremented for every word by one
        /// </summary>
        public static int TimeStamp
        {
            get { return (int)settings["timeStamp"]; }
            set { settings["timeStamp"] = value; }
        }

        /// <summary>
        /// decides the order in wich the loaded words are displayed
        /// <para>0 - longest time not learned first</para>
        /// <para>1 - by Lesson</para>
        /// <para>2 - Random</para>
        /// </summary>
        public static ESortOrder SortOrder
        {
            get { return (ESortOrder)settings["sortOrder"]; }
            set { settings["sortOrder"] = value; }
        }

        /// <summary>
        /// decides the order in wich the loaded words are displayed
        /// <para>0 - longest time not learned first</para>
        /// <para>1 - by Lesson</para>
        /// <para>2 - Random</para>
        /// </summary>
        public static ESortOrder SortOrderFlash
        {
            get { return (ESortOrder)settings["sortOrderFlash"]; }
            set { settings["sortOrderFlash"] = value; }
        }

        /// <summary>
        /// stores word types that should be loaded bitwise in an integer value
        /// </summary>
        public static int LoadOptions
        {
            get { return (int)settings["loadOptions"]; }
            set { settings["loadOptions"] = value; }
        }

        #endregion

        #region Public Methods

        public static void Initialize()
        {
            settings = IsolatedStorageSettings.ApplicationSettings;

            if (!settings.Contains("firstStart"))
            {
                FirstStart = true;
                VocabPracticeDirection = EPracticeDirection.japGer;
                LoadAllWords = false;
                LoadAllKanji = false;
                ShowDescription = true;
                BackgroundImage = null;
                NewWordsPerRound = 20;
                NewKanjiPerRound = 5;
                TimeStamp = 0;
                LoadOptions = 4095; //binary : 1111 1111 1111 (at first start load all types)
                SortOrder = ESortOrder.random;
                SortOrderFlash = ESortOrder.random;
            }
            else
            {
                FirstStart = false;
            }
        }

        public static void SaveSettings()
        {
            settings.Save();
        }

        #endregion
    }
}