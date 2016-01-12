using NihongoSenpai.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NihongoSenpai.Data
{
    public static class FlashcardsData
    {
        #region Fields

        private static int itemsCorrect = 0;
        private static int itemsLeft = 0;
        private static int itemsWrong = 0;

        private static int itemIndex = 0;

        private static List<Kanji> kanjis;
        private static List<Kanji> activeKanjis;
        
        #endregion

        #region Properties

        public static Kanji ActiveKanji
        {
            get { return activeKanjis[itemIndex]; }
        }

        public static List<Kanji> Kanjis
        {
            get { return kanjis; }
            set { kanjis = value; }
        }
        
        public static List<Kanji> ActiveKanjis
        {
            get { return activeKanjis; }
            set { activeKanjis = value; }
        }
        
        public static int ItemIndex
        {
            get { return itemIndex; }
            set { itemIndex = value; }
        }

        public static int ItemsLeft
        {
            get { return itemsLeft; }
            set { itemsLeft = value; }
        }

        public static int ItemsCorrect
        {
            get { return itemsCorrect; }
            set { itemsCorrect = value; }
        }

        public static int ItemsWrong
        {
            get { return itemsWrong; }
            set { itemsWrong = value; }
        }

        #endregion
    }
}
