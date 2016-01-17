using SenpaiCreationKit.Resources;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiCreationKit.Data
{
    public class SenpaiDatabase : DataContext
    {
        #region Fields

        public Table<Lesson> Lessons;
        public Table<Word> Words;
        public Table<Cloze> Clozes;
        public Table<Kanji> Kanjis;

        #endregion

        #region Constructor

        public SenpaiDatabase() : base(AppResources.dbConnection)
        {

        }

        #endregion
    }
}
