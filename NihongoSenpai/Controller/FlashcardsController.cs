using Microsoft.Xna.Framework;
using NihongoSenpai.Data;
using NihongoSenpai.Data.Database;
using NihongoSenpai.Settings;
using NihongoSenpai.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NihongoSenpai.Controller
{
    public static class FlashcardsController
    {
        #region Fields

        public static IPageUpdater pageUpdater;
        
        private static int newKanjisAdded = 0;

        #endregion

        #region Initialize

        public static void LoadLessons(List<Lesson> lessons)
        {
            FlashcardsData.Kanjis = DataManager.GetKanjis(lessons);
            FlashcardsData.ActiveKanjis = new List<Kanji>();

            LoadLessons();
        }

        public static void LoadLessons()
        {
            FlashcardsData.ActiveKanjis.Clear();

            if(AppSettings.LoadAllKanji)
            {
                FlashcardsData.ActiveKanjis.AddRange(FlashcardsData.Kanjis);
            }
            else
            {
                PrepareKanjis();
            }
            
            switch(AppSettings.SortOrderFlash)
            {
                case ESortOrder.byLesson : /*         already sorted by lesson          */    break;
                case ESortOrder.timestamp: Util.SortByTimestamp(FlashcardsData.ActiveKanjis); break;
                case ESortOrder.random   : Util.SortByRandom   (FlashcardsData.ActiveKanjis); break;
            }

            FlashcardsData.ItemIndex    = 0;
            FlashcardsData.ItemsLeft    = FlashcardsData.ActiveKanjis.Count;
            FlashcardsData.ItemsCorrect = 0;
            FlashcardsData.ItemsWrong   = 0;
        }

        private static void PrepareKanjis()
        {
            newKanjisAdded = 0;

            foreach (Kanji kanji in FlashcardsData.Kanjis)
            {
                TryAddKanji(kanji);
            }
        }
        
        private static void TryAddKanji(Kanji kanji)
        {
            if(kanji.nextRound <= AppSettings.KanjiRound)
            {
                if(kanji.timestamp == 0)
                {
                    kanji.lastRound = AppSettings.KanjiRound;
                    kanji.nextRound = AppSettings.KanjiRound;

                    ++newKanjisAdded;

                    if(newKanjisAdded <= AppSettings.NewKanjiPerRound)
                    {
                        FlashcardsData.ActiveKanjis.Add(kanji);
                    }
                }
                else
                {
                    FlashcardsData.ActiveKanjis.Add(kanji);
                }
            }
        }
                   
        public static void Deinitialize()
        {
            FlashcardsData.Kanjis      .Clear();
            FlashcardsData.ActiveKanjis.Clear();
        }

        #endregion

        #region Practice
        
        public static void EvaluateKanji(int grade)
        {
            Kanji activeKanji = FlashcardsData.ActiveKanji;

            int lastRound = activeKanji.lastRound;
            int nextRound = activeKanji.nextRound;

            int repetition = nextRound - lastRound;

            activeKanji.timestamp = ++AppSettings.TimeStamp;

            if(grade > 2)
            {
                if(repetition == 0)
                {
                    ++nextRound;
                }
                else if(repetition == 1)
                {
                    nextRound += 3;
                }
                else
                {
                    nextRound = lastRound + (int)Math.Round(repetition * activeKanji.eFactor);
                }
                
                --FlashcardsData.ItemsLeft;
                ++FlashcardsData.ItemsCorrect;
            }
            else
            {
                nextRound = AppSettings.KanjiRound;
                
                ++FlashcardsData.ItemsWrong;

                FlashcardsData.ActiveKanjis.Add(activeKanji);
            }

            lastRound = AppSettings.KanjiRound;

            activeKanji.eFactor += -0.8f + 0.28f * grade - 0.02f * grade * grade;
            activeKanji.eFactor = Math.Max(1.3f, activeKanji.eFactor);
            activeKanji.nextRound = nextRound;
            activeKanji.lastRound = lastRound;
        }
                
        public static bool GetNext()
        {
            ++FlashcardsData.ItemIndex;

            if (FlashcardsData.ItemIndex == FlashcardsData.ActiveKanjis.Count)
            {
                ++AppSettings.KanjiRound;

                AppSettings.SaveSettings();
                DataManager.SaveChanges();
                
                pageUpdater.RoundFinished();
            }

            if(FlashcardsData.ActiveKanjis.Count > 0)
            {
                FlashcardsData.ItemIndex %= FlashcardsData.ActiveKanjis.Count;
            }
            else
            {
                pageUpdater.EndPractice();

                return false;
            }

            return true;
        }

        #endregion
    }
}
