using NihongoSenpai.Data;
using NihongoSenpai.Data.Database;
using NihongoSenpai.Resources;
using System;
using System.Collections.Generic;

namespace NihongoSenpai.Utilities
{
    public static class Util
    {
        #region Fields

        private static Random rand = new Random();
        
        private static AppResources localizedStrings = new AppResources();

        #endregion

        #region Public Methods

        #region Sort Arrays
        
        public static void SortByTimestamp(List<IVocabItem> items)
        {
            //after this, sort these timestamp array and make every step for both
            //the timestamp array and the Words array
            for (int i = 0; i < items.Count - 1; ++i )
            {
                for(int j = i + 1; j < items.Count; ++j)
                {
                    if(items[i].TimeStamp > items[j].TimeStamp)
                    {
                        IVocabItem hv = items[i];
                        items[i] = items[j];
                        items[j] = hv;
                    }
                }
            }
        }

        public static void SortByTimestamp(List<Kanji> kanjis)
        {
            //after this, sort these timestamp array and make every step for both
            //the timestamp array and the Words array
            for (int i = 0; i < kanjis.Count - 1; ++i )
            {
                for(int j = i + 1; j < kanjis.Count; ++j)
                {
                    if(kanjis[i].timestamp > kanjis[j].timestamp)
                    {
                        Kanji hv = kanjis[i];
                        kanjis[i] = kanjis[j];
                        kanjis[j] = hv;
                    }
                }
            }
        }
        
        public static void SortByRandom<Type>(List<Type> values)
        {
            for (int i = 0; i < values.Count; ++i)
            {
                int newIndex = GetRandomNumber(values.Count);

                Type hv = values[i];
                values[i] = values[newIndex];
                values[newIndex] = hv;

                //swap with ref parameters doesn't work here, don't know why =(
                //Swap(ref values[i], ref values[newIndex]);
            }
        }

        /// <summary>
        /// Randomizes the list in within the given interval
        /// </summary>
        public static void SortByHalfRandom<Type>(List<Type> values, int intervall)
        {
            int index = 0;
            int round = 0;

            while(index < values.Count)
            {
                intervall = Math.Min(values.Count - round * intervall, intervall);

                for(int i = round * intervall; i < (round + 1) * intervall || index < values.Count; ++i)
                {


                    int newIndex = GetRandomNumber(intervall) + round * intervall;

                    Type hv = values[i];
                    values[i] = values[newIndex];
                    values[newIndex] = hv;

                    ++index;
                }

                ++round;
            }
        }

        public static void SortByRandom<Type>(Type[] values)
        {
            for (int i = 0; i < values.Length; ++i)
            {
                int newIndex = GetRandomNumber(values.Length);

                Swap(ref values[i], ref values[newIndex]);
            }
        }

        public static void SortByRandom(char[] values)
        {
            SortByRandom(values, values.Length);
        }

        /// <summary>
        /// Swaps items in the array randomly 
        /// </summary>
        /// <param name="values">items to randomize</param>
        /// <param name="lastIndex">last index until the method should swap the items</param>
        public static void SortByRandom(char[] values, int lastIndex)
        {
            for (int i = 0; i < lastIndex; ++i)
            {
                int newIndex = GetRandomNumber(values.Length);

                Swap(ref values[i], ref values[newIndex]);
            }
        }
        
        #endregion

        #region Swap Operations

        public static void Swap<Type>(Type a, Type b)
        {
            Type hv = a;
            a = b;
            b = hv;
        }

        public static void Swap<Type>(ref Type a, ref Type b)
        {
            Type hv = a;
            a = b;
            b = hv;
        }
        
        #endregion

        #region Generate Random Values

        public static int GetRandomNumber()
        {
            return rand.Next();
        }

        public static int GetRandomNumber(int max)
        {
            return rand.Next(max);
        }

        public static int GetRandomNumber(int min, int max)
        {
            return rand.Next(min, max);
        }
        
        #endregion

        #region Others

        public static Word.EType[] ExtractTypes(int loadOptions)
        {
            List<Word.EType> types = new List<Word.EType>();
            
            if (loadOptions % 2 == 1) types.Add(Word.EType.other);
            loadOptions >>= 1; 
            if (loadOptions % 2 == 1) types.Add(Word.EType.phrase);
            loadOptions >>= 1;            
            if (loadOptions % 2 == 1) types.Add(Word.EType.suffix);
            loadOptions >>= 1;
            if (loadOptions % 2 == 1) types.Add(Word.EType.prefix);
            loadOptions >>= 1;            
            if (loadOptions % 2 == 1) types.Add(Word.EType.particle);
            loadOptions >>= 1;
            if (loadOptions % 2 == 1) types.Add(Word.EType.noun);
            loadOptions >>= 1;
            if (loadOptions % 2 == 1) types.Add(Word.EType.adverb);
            loadOptions >>= 1;
            if (loadOptions % 2 == 1) types.Add(Word.EType.naAdjective);
            loadOptions >>= 1;
            if (loadOptions % 2 == 1) types.Add(Word.EType.iAdjective);
            loadOptions >>= 1;
            if (loadOptions % 2 == 1) types.Add(Word.EType.verb3);
            loadOptions >>= 1;
            if (loadOptions % 2 == 1) types.Add(Word.EType.verb2);
            loadOptions >>= 1;
            if (loadOptions % 2 == 1) types.Add(Word.EType.verb1);

            return types.ToArray();
        }
        
        #endregion

        #endregion
    }
}
