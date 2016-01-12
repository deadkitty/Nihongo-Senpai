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

            //refactoring repetion/nextInterval for new design pattern
            //foreach (Kanji kanji in FlashcardsData.Kanjis)
            //{
            //    kanji.nextRound -= kanji.lastRound;

            //    kanji.nextRound = (int)MathHelper.Clamp(kanji.nextRound, 0, 100);

            //    kanji.lastRound = 0;
            //}
            //DataManager.SaveChanges();
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

            foreach (Kanji k in FlashcardsData.Kanjis)
            {
                TryAddKanji(k);
            }
        }
        
        private static void TryAddKanji(Kanji k)
        {
            if(k.nextRound <= AppSettings.KanjiRound)
            {
                if(k.timestamp == 0)
                {
                    ++newKanjisAdded;

                    if(newKanjisAdded <= AppSettings.NewKanjiPerRound)
                    {
                        FlashcardsData.ActiveKanjis.Add(k);
                    }
                }
                else
                {
                    FlashcardsData.ActiveKanjis.Add(k);
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
                    nextRound += 1;
                    //interval = 1;
                    //repetition = 1;
                }
                else if(repetition == 1)
                {
                    //on supermemo the new intervall here is 6 but with 20 items per interval the next time i see this item again would be 80 items away o.0 too much i think^^
                    //but maybe later i'll give the user an option to adjust the interval size, like with partlessons count
                    nextRound += 3;
                    //repetition = 2;
                }
                else
                {
                    nextRound = lastRound + (int)Math.Round(repetition * activeKanji.eFactor);
                    //interval = (int)Math.Round(interval * activeKanji.eFactor);
                    //++repetition;
                }
                
                --FlashcardsData.ItemsLeft;
                ++FlashcardsData.ItemsCorrect;
            }
            else
            {
                nextRound = AppSettings.KanjiRound;
                //interval = 0;
                //repetition = 0;

                ++FlashcardsData.ItemsWrong;

                FlashcardsData.ActiveKanjis.Add(activeKanji);
            }

            lastRound = AppSettings.KanjiRound;

            activeKanji.eFactor += -0.8f + 0.28f * grade - 0.02f * grade * grade;
            activeKanji.eFactor = Math.Max(1.3f, activeKanji.eFactor);
            activeKanji.nextRound = nextRound;
            activeKanji.lastRound = lastRound;
        }
                
        public static void GetNext()
        {
            ++FlashcardsData.ItemIndex;

            if (FlashcardsData.ItemIndex == FlashcardsData.ActiveKanjis.Count)
            {
                ++AppSettings.KanjiRound;

                AppSettings.SaveSettings();
                DataManager.SaveChanges();
                
                pageUpdater.RoundFinished();
            }

            FlashcardsData.ItemIndex %= FlashcardsData.ActiveKanjis.Count;
        }

        #endregion
    }
}
