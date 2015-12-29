using NihongoSenpai.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NihongoSenpai.Data
{
    public static class AppData
    {
        #region Fields
        
        private static Lesson selectedLesson;

        private static List<Word> words;
        private static List<Kanji> kanjis;

        private static Kanji selectedKanji;

        #endregion

        #region Properties

        public static Lesson SelectedLesson
        {
            get { return selectedLesson; }
            set { selectedLesson = value; }
        }
        
        public static List<Word> Words
        {
            get { return words; }
            set { words = value; }
        }

        public static List<Kanji> Kanjis
        {
            get { return kanjis; }
            set { kanjis = value; }
        }
        
        public static Kanji SelectedKanji
        {
            get { return selectedKanji; }
            set { selectedKanji = value; }
        }

        #endregion
    }
}
