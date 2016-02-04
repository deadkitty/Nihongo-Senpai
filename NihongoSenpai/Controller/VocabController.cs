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
    public static class VocabController
    {
        #region Fields

        public static IPageUpdater pageUpdater;
        
        #endregion

        #region Public Methods

        public static void LoadLessons(List<Lesson> lessons)
        {
            VocabData.Words = DataManager.GetWords(lessons, Util.ExtractTypes(AppSettings.LoadOptions));

            VocabData.VocabItems = new List<IVocabItem>();
            VocabData.ActiveItems = new List<IVocabItem>();

            if (AppSettings.LoadAllWords)
            {
                LoadAllWords();
            }
            else
            {
                if (AppSettings.PracticeDirection == EPracticeDirection.japGer)
                {
                    foreach (Word w in VocabData.Words)
                    {
                        if (w.ShowWord == Word.EShowFlags.showBoth ||
                            w.ShowWord == Word.EShowFlags.justJapanese)
                        {
                            VocabData.VocabItems.Add(new JapGerItem(w));
                        }
                    }

                    foreach (Word w in VocabData.Words)
                    {
                        if (w.ShowWord == Word.EShowFlags.showBoth ||
                            w.ShowWord == Word.EShowFlags.justGerman)
                        {
                            VocabData.VocabItems.Add(new GerJapItem(w));
                        }
                    }
                }
                else if (AppSettings.PracticeDirection == EPracticeDirection.jap)
                {
                    foreach (Word w in VocabData.Words)
                    {
                        if (w.ShowWord == Word.EShowFlags.showBoth ||
                            w.ShowWord == Word.EShowFlags.justJapanese)
                        {
                            VocabData.VocabItems.Add(new JapGerItem(w));
                        }
                        VocabData.VocabItems.Add(new JapGerItem(w));
                    }
                }
                else if (AppSettings.PracticeDirection == EPracticeDirection.ger)
                {
                    foreach (Word w in VocabData.Words)
                    {
                        if (w.ShowWord == Word.EShowFlags.showBoth ||
                            w.ShowWord == Word.EShowFlags.justGerman)
                        {
                            VocabData.VocabItems.Add(new GerJapItem(w));
                        }
                    }
                }
            }

            LoadLessons();
        }

        public static void LoadLessons()
        {
            if(!AppSettings.LoadAllWords)
            {
                PrepareWords();
            }
            
            switch(AppSettings.SortOrder)
            {
                case ESortOrder.byLesson : /*       already sorted by lesson         */ break;
                case ESortOrder.timestamp: Util.SortByTimestamp(VocabData.ActiveItems); break;
                case ESortOrder.random   : Util.SortByRandom   (VocabData.ActiveItems); break;
            }

            VocabData.ItemIndex    = 0;
            VocabData.ItemsLeft    = VocabData.ActiveItems.Count;
            VocabData.ItemsCorrect = 0;
            VocabData.ItemsWrong   = 0;
        }

        private static void LoadAllWords()
        {
            if (AppSettings.PracticeDirection == EPracticeDirection.jap ||
                AppSettings.PracticeDirection == EPracticeDirection.japGer)
            {
                foreach(Word w in VocabData.Words)
                {
                    IVocabItem item = new JapGerItem(w);
                    item.InitText();

                    VocabData.VocabItems.Add(item);
                    VocabData.ActiveItems.Add(item);
                }
            }
            
            if (AppSettings.PracticeDirection == EPracticeDirection.ger ||
                AppSettings.PracticeDirection == EPracticeDirection.japGer)
            {
                foreach(Word w in VocabData.Words)
                {
                    IVocabItem item = new GerJapItem(w);
                    item.InitText();

                    VocabData.VocabItems.Add(item);
                    VocabData.ActiveItems.Add(item);
                }
            }
        }

        private static void PrepareWords()
        {
            VocabData.ActiveItems.Clear();

            int newWords = 0;
            int maxNewWords = 0;

            switch(AppSettings.PracticeDirection)
            {
                case EPracticeDirection.ger   : maxNewWords = AppSettings.NewWordsPerRound; break;
                case EPracticeDirection.jap   : maxNewWords = AppSettings.NewWordsPerRound; break;
                case EPracticeDirection.japGer: maxNewWords = AppSettings.NewWordsPerRound / 2; break;
            }

            int currentItemIndex = 0;

            foreach(IVocabItem item in VocabData.VocabItems)
            {
                if(item.NextRound <= AppSettings.VocabRound)
                {
                    if(item.TimeStamp == 0)
                    {
                        item.LastRound = AppSettings.VocabRound;
                        item.NextRound = AppSettings.VocabRound;

                        if(newWords < maxNewWords)
                        {
                            ++newWords;
                            
                            item.InitText();
                            VocabData.ActiveItems.Add(item);
                        }
                    }
                    else
                    {
                        item.InitText();
                        VocabData.ActiveItems.Add(item);
                    }
                }

                ++currentItemIndex;
                if(currentItemIndex == VocabData.Words.Count)
                {
                    newWords = 0;
                }
            }
        }

        public static void Deinitialize()
        {
            VocabData.Words      .Clear();
            VocabData.VocabItems .Clear();
            VocabData.ActiveItems.Clear();
        }

        public static void EvaluateWord(int grade)
        {
            IVocabItem activeItem = VocabData.ActiveItem;

            int lastRound = activeItem.LastRound;
            int nextRound = activeItem.NextRound;

            int repetition = nextRound - lastRound;

            activeItem.TimeStamp = ++AppSettings.TimeStamp;
            
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
                    nextRound = lastRound + (int)Math.Round(repetition * activeItem.EFactor);
                }

                ++VocabData.ItemsCorrect;
                --VocabData.ItemsLeft;
            }
            else
            {
                nextRound = AppSettings.VocabRound;

                ++VocabData.ItemsWrong;
                VocabData.ActiveItems.Add(activeItem);
            }

            lastRound = AppSettings.VocabRound;

            activeItem.EFactor += -0.8f + 0.28f * grade - 0.02f * grade * grade;
            activeItem.EFactor = Math.Max(1.3f, activeItem.EFactor);
            activeItem.NextRound = nextRound;
            activeItem.LastRound = lastRound;
        }

        public static bool GetNext()
        {
            ++ VocabData.ItemIndex;

            if(VocabData.ItemIndex == VocabData.ActiveItems.Count)
            {
                ++AppSettings.VocabRound;

                pageUpdater.RoundFinished();
            }

            if (VocabData.ActiveItems.Count > 0)
            {
                VocabData.ItemIndex %= VocabData.ActiveItems.Count;
            }
            else
            {
                pageUpdater.EndPractice();

                return false;
            }

            return true;
        }

        public static void EndPractice()
        {
            foreach(IVocabItem item in VocabData.VocabItems)
            {
                item.WriteBack();
            }

            AppSettings.SaveSettings();
            DataManager.SaveChanges();
        }

        #endregion
    }
}