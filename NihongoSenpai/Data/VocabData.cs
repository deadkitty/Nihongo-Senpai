using NihongoSenpai.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NihongoSenpai.Model
{
    public static class VocabData
    {
        #region Fields

        private static List<Word> words;

        private static List<IVocabItem> vocabItems;
        private static List<IVocabItem> activeItems;

        private static int itemIndex = 0;

        private static int itemsLeft;
        private static int itemsCorrect;
        private static int itemsWrong;

        #endregion

        #region Properties
        
        public static IVocabItem ActiveItem
        {
           get{ return activeItems[itemIndex]; }
        }

        public static List<Word> Words
        {
            get { return words; }
            set { words = value; }
        }

        public static List<IVocabItem> VocabItems
        {
            get { return vocabItems; }
            set { vocabItems = value; }
        }

        public static List<IVocabItem> ActiveItems
        {
            get { return activeItems; }
            set { activeItems = value; }
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
        
        public static int ItemIndex
        {
            get { return itemIndex; }
            set { itemIndex = value; }
        }

        #endregion
    }
}
