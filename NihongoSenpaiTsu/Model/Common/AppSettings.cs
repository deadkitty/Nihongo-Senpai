using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace NihongoSenpaiTsu.Common
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

        private static IPropertySet settings = ApplicationData.Current.LocalSettings.Values;

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
        public static EPracticeDirection PracticeDirection
        {
            get { return (EPracticeDirection)settings["vocabPracticeDirection"]; }
            set { settings["vocabPracticeDirection"] = (int)value; }
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
        /// <para>0 - by Lesson</para>
        /// <para>1 - longest time not learned first</para>
        /// <para>2 - Random</para>
        /// </summary>
        public static ESortOrder VocabSortOrder
        {
            get { return (ESortOrder)settings["vocabSortOrder"]; }
            set { settings["vocabSortOrder"] = (int)value; }
        }

        /// <summary>
        /// decides the order in wich the loaded words are displayed
        /// <para>0 - by Lesson</para>
        /// <para>1 - longest time not learned first</para>
        /// <para>2 - Random</para>
        /// </summary>
        public static ESortOrder FlashSortOrder
        {
            get { return (ESortOrder)settings["flashSortOrder"]; }
            set { settings["flashSortOrder"] = (int)value; }
        }

        /// <summary>
        /// stores word types that should be loaded bitwise in an integer value
        /// </summary>
        public static int LoadOptions
        {
            get { return (int)settings["loadOptions"]; }
            set { settings["loadOptions"] = value; }
        }

        ///// <summary>
        ///// Current Round on Vocab Exercises
        ///// </summary>
        //public static int VocabRound
        //{
        //    get { return (int)settings["vocabRound"]; }
        //    set { settings["vocabRound"] = value; }
        //}

        ///// <summary>
        ///// Current Round on Flashcard Exercises
        ///// </summary>
        //public static int KanjiRound
        //{
        //    get { return (int)settings["kanjiRound"]; }
        //    set { settings["kanjiRound"] = value; }
        //}

        #endregion

        #region Initialization

        public static void Initialize()
        {
            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey("firstStart"))
            {
                FirstStart        = true;
                PracticeDirection = EPracticeDirection.japGer;
                LoadAllWords      = false;
                LoadAllKanji      = false;
                ShowDescription   = true;
                BackgroundImage   = null;
                NewWordsPerRound  = 20;
                NewKanjiPerRound  = 5;
                TimeStamp         = 0;
                LoadOptions       = 4095; //binary : 1111 1111 1111 (at first start load all types)
                VocabSortOrder    = ESortOrder.random;
                FlashSortOrder    = ESortOrder.random;
                //VocabRound        = 0;
                //KanjiRound        = 0;
            }
            else
            {
                FirstStart = false;
            }
        }
        
        #endregion
    }
}
