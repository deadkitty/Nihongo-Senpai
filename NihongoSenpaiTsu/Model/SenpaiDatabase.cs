using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NihongoSenpaiTsu.Model
{
    public class SenpaiDatabase // : DataContext
    {
        #region Fields

        private const String dbName = "Senpai.db";

        private SQLiteConnection connection;

        private List<Grammar> grammars;
        private List<Lesson> lessons;
        private List<Kanji> kanjis;
        private List<Word> words;

        private static SQLiteConnection currentConnection;

        public static SQLiteConnection CurrentConnection
        {
            get { return currentConnection; }
        }

        #endregion

        #region Properties

        public List<Lesson> Lessons
        {
            get
            {
                if (lessons == null)
                {
                    lessons = connection.Table<Lesson>().ToList();
                }
                return lessons;
            }
        }

        public List<Grammar> Grammars
        {
            get
            {
                if (grammars == null)
                {
                    grammars = connection.Table<Grammar>().ToList();
                }
                return grammars;
            }
        }

        public List<Kanji> Kanjis
        {
            get
            {
                if (kanjis == null)
                {
                    kanjis = connection.Table<Kanji>().ToList();
                }
                return kanjis;
            }
        }

        public List<Word> Words
        {
            get
            {
                if (words == null)
                {
                    words = connection.Table<Word>().ToList();
                }
                return words;
            }
        }
        
        #endregion

        #region Constructor

        public SenpaiDatabase()
        {

        }

        #endregion

        #region Database

        public void ConnectToDatabase()
        {
            connection = new SQLiteConnection(dbName);
            currentConnection = connection;
        }

        public void CloseConnection()
        {
            currentConnection = null;
            connection.Close();
            connection.Dispose();
        }

        public void CreateDatabase()
        {
            if (!DatabaseExists().Result)
            {
                connection.CreateTable<Lesson>();
                connection.CreateTable<Grammar>();
                connection.CreateTable<Kanji>();
                connection.CreateTable<Word>();
            }
        }

        public void DeleteDatabase()
        {
            if (DatabaseExists().Result)
            {
                connection.DropTable<Lesson>();
                connection.DropTable<Grammar>();
                connection.DropTable<Kanji>();
                connection.DropTable<Word>();
            }
        }

        #endregion

        public void Add<Type>(Type item)
        {
            connection.Insert(item);
        }

        public void AddRange<Type>(List<Type> items)
        {
            connection.InsertAll(items, typeof(Type));
        }

        public void Update<Type>(Type item)
        {
            connection.Update(item);
        }

        public void UpdateRange<Type>(List<Type> items)
        {
            connection.UpdateAll(items);
        }

        public void Delete(Lesson lesson)
        {
            connection.Delete<Lesson>(lesson.Id);
        }

        public void DeleteRange(List<Word> items)
        {
            foreach (Word item in items)
            {
                connection.Delete<Word>(item.Id);
            }
        }

        public void DeleteRange(List<Grammar> items)
        {
            foreach (Grammar item in items)
            {
                connection.Delete<Grammar>(item.Id);
            }
        }

        public void DeleteRange(List<Kanji> items)
        {
            foreach (Kanji item in items)
            {
                connection.Delete<Kanji>(item.Id);
            }
        }

        #region Helper

        private async Task<bool> DatabaseExists()
        {
            try
            {
                await ApplicationData.Current.LocalFolder.GetFileAsync(dbName);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
