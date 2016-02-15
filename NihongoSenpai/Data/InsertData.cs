using NihongoSenpai.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NihongoSenpai.Model
{
    public static class InsertData
    {
        #region Fields

        private static int itemIndex;
        
        private static List<Cloze> clozes = new List<Cloze>();

        private static String sentenceText;
        private static String sentenceAnswer;
        
        #endregion

        #region Properties

        public static int ItemIndex
        {
            get { return itemIndex; }
            set { itemIndex = value; }
        }

        public static List<Cloze> Clozes
        {
            get { return clozes; }
            set { clozes = value; }
        }

        public static Cloze ActiveCloze
        {
            get { return clozes[ItemIndex]; }
        }

        public static String SentenceText
        {
            get { return sentenceText; }
            set { sentenceText = value; }
        }

        public static String SentenceAnswer
        {
            get { return sentenceAnswer; }
            set { sentenceAnswer = value; }
        }
        
        #endregion
    }
}
