using Microsoft.Win32;
using SenpaiCreationKit.Controls;
using SenpaiCreationKit.Data;
using SenpaiCreationKit.Properties;
using SenpaiCreationKit.Resources;
using SenpaiCreationKit.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace SenpaiCreationKit.Pages
{
    /// <summary>
    /// Interaktionslogik für MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        #region Fields
        
        private List<Lesson> lessons = new List<Lesson>();

        #endregion

        #region Constructor

        public MainPage()
        {
            InitializeComponent();
        }
        
        #endregion

        #region Events

        #region Initialize/Deinitialize

        /// <summary>
        /// is called each time i navigate to this page
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (SenpaiDatabase database = new SenpaiDatabase())
            {
                lessons.AddRange(database.Lessons.OrderBy(x => x.Type));
            }

            foreach (Lesson lesson in lessons)
            {
                lessonsListbox.Items.Add(lesson);

                Debug.WriteLine("Lektion: " + lesson.ToString());
            }

            //foreach(ExportData data in DataManager.Database.exportData)
            //{
            //    Debug.WriteLine(data.ToString());
            //}

            typeComboBox.Items.Add(AppResources.ChooseAll);
            typeComboBox.Items.Add(AppResources.VocabLesson);
            typeComboBox.Items.Add(AppResources.FlashLesson);
            typeComboBox.Items.Add(AppResources.InsertLesson);
        }
        
        #endregion

        #region Button Click

        private void createVocabLesson_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateVocabLessonPage());
        }

        private void createFlashLesson_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateFlashLessonPage());
        }

        private void createInsertLesson_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateInsertLessonPage());
        }

        private void editLesson_Click(object sender, RoutedEventArgs e)
        {
            Lesson lesson = lessonsListbox.SelectedItem as Lesson;

            switch((Lesson.EType)lesson.Type)
            {
                case Lesson.EType.vocab : NavigationService.Navigate(new CreateVocabLessonPage (lesson)); break;
                case Lesson.EType.insert: NavigationService.Navigate(new CreateInsertLessonPage(lesson)); break;
                case Lesson.EType.kanji : NavigationService.Navigate(new CreateFlashLessonPage (lesson)); break;
            }
        }

        private void deleteLesson_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(AppResources.Really, AppResources.DeleteLessons, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                while(lessonsListbox.SelectedItems.Count > 0)
                {
                    Lesson lesson = lessonsListbox.SelectedItems[0] as Lesson;
                    
                    DataManager.DeleteLesson(lesson);
                    
                    lessonsListbox.Items.Remove(lesson);
                }

                DataManager.SaveChanges();
            }
        }

        private void exportLesson_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            
            fileDialog.FileName = "export";
            fileDialog.DefaultExt = ".nya";
            fileDialog.Filter = ".nya|*.nya";

            Nullable<bool> result = fileDialog.ShowDialog();

            if(result == true)
            {
                using(AppStreamWriter sw = new AppStreamWriter(fileDialog.FileName))
                {
                    List<Lesson> lessons = new List<Lesson>();
                    
                    foreach(Lesson lesson in lessonsListbox.SelectedItems)
                    {
                        lessons.Add(lesson);
                    }
                    
                    DataManager.ExportLessons(lessons, sw);
                }
            }
        }

        private void importLesson_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = ".poyo|*.poyo";

            bool? result = fileDialog.ShowDialog();

            if(result == true)
            {
                using (FileStream stream = new FileStream(fileDialog.FileName, FileMode.Open))
                {
                    using (AppStreamReader sr = new AppStreamReader(stream))
                    {
                        List<Lesson> lessons = new List<Lesson>();

                        sr.ReadLine();
                        sr.ReadLine();

                        while (!sr.EndOfStream)
                        {
                            String line = sr.ReadLine();

                            Lesson lesson = new Lesson(line);

                            for(int i = 0; i < lesson.Size; ++i)
                            {
                                switch((Lesson.EType)lesson.Type)
                                {
                                    case Lesson.EType.vocab : lesson.Words .Add(new Word (sr.ReadLine(), lesson)); break;
                                    case Lesson.EType.kanji : lesson.Kanjis.Add(new Kanji(sr.ReadLine(), lesson)); break;
                                    case Lesson.EType.insert: lesson.Clozes.Add(new Cloze(sr.ReadLine(), lesson)); break;
                                }
                            }

                            lessons.Add(lesson);
                        }

                        using (SenpaiDatabase database = new SenpaiDatabase())
                        {
                            database.Lessons.InsertAllOnSubmit(lessons);

                            foreach(Lesson lesson in lessons)
                            {
                                switch ((Lesson.EType)lesson.Type)
                                {
                                    case Lesson.EType.vocab : database.Words .InsertAllOnSubmit(lesson.Words ); break;
                                    case Lesson.EType.kanji : database.Kanjis.InsertAllOnSubmit(lesson.Kanjis); break;
                                    case Lesson.EType.insert: database.Clozes.InsertAllOnSubmit(lesson.Clozes); break;
                                }
                            }

                            database.SubmitChanges();
                        }
                    }
                }
            }

        }

        private void uploadLesson_Click(object sender, RoutedEventArgs e)
        {

        }

        private void marketplace_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MarketplacePage());
        }

        private void exitApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        #endregion

        #region Others
        
        private void searchTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(searchTextbox.Text == "")
                {
                    lessons.Clear();
                    using (SenpaiDatabase database = new SenpaiDatabase())
                    {
                        lessons.AddRange(database.Lessons);
                    }
                    typeComboBox_SelectionChanged(sender, null);
                }
                else
                {
                    lessons.Clear();
                    using (SenpaiDatabase database = new SenpaiDatabase())
                    {
                        lessons.AddRange(database.Lessons);
                    }
                    typeComboBox_SelectionChanged(sender, null);
                }
            }
        }
        
        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lessonsListbox.Items.Clear();
            
            //if all is selected add all lessons
            if(typeComboBox.SelectedIndex == 0)
            {
                foreach(Lesson lesson in lessons)
                {
                    lessonsListbox.Items.Add(lesson);
                }
            }
            else
            {
                Lesson.EType type = Lesson.EType.undefined;

                switch(typeComboBox.SelectedIndex)
                {
                    case 1: type = Lesson.EType.vocab; break;
                    case 2: type = Lesson.EType.kanji; break;
                    case 3: type = Lesson.EType.insert; break;
                }

                foreach(Lesson lesson in lessons)
                {
                    //otherwise check for the correct type and add it
                    if(lesson.Type == (int)type)
                    {
                        lessonsListbox.Items.Add(lesson);
                    }
                }
            }
        }

        private void lessonsListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SenpaiDatabase database = new SenpaiDatabase())
            {
                if (lessonsListbox.SelectedItems.Count > 0)
                {
                    Lesson lesson = database.Lessons.Where(x => x.id == (lessonsListbox.SelectedItem as Lesson).id).FirstOrDefault();

                    editLesson.IsEnabled = true;
                    deleteLesson.IsEnabled = true;
                    exportLesson.IsEnabled = true;
                    uploadLesson.IsEnabled = true;

                    LoadLessonPreview(lesson);
                }
                else
                {
                    editLesson.IsEnabled = false;
                    deleteLesson.IsEnabled = false;
                    exportLesson.IsEnabled = false;
                    uploadLesson.IsEnabled = false;

                    UnloadLessonPreview();
                }
            }
        }

        private void LoadLessonPreview(Lesson lesson)
        {
            detailsListbox.Items.Clear();

            switch((Lesson.EType)lesson.Type)
            {
                case Lesson.EType.vocab : LoadVocabPreview (lesson); break; 
                case Lesson.EType.kanji : LoadFlashPreview (lesson); break;
                case Lesson.EType.insert: LoadInsertPreview(lesson); break;
            }
        }
        
        private void LoadVocabPreview(Lesson lesson)
        {
            foreach(Word word in lesson.Words)
            {
                DetailItem item = new DetailItem(word);

                detailsListbox.Items.Add(item);
            }
        }

        private void LoadFlashPreview(Lesson lesson)
        {
            foreach(Kanji kanji in lesson.Kanjis)
            {
                DetailItem item = new DetailItem(kanji);

                detailsListbox.Items.Add(item);
            }
        }

        private void LoadInsertPreview(Lesson lesson)
        {
            foreach(Cloze cloze in lesson.Clozes)
            {
                DetailItem item = new DetailItem(cloze);

                detailsListbox.Items.Add(item);
            }
        }
        
        private void UnloadLessonPreview()
        {
            detailsListbox.Items.Clear();
        }

        #endregion

        #region Testing Stuff
        
        private void resetApplication_Click(object sender, RoutedEventArgs e)
        {
            DataManager.DeleteDatabase();
            DataManager.CreateDatabase();

            Settings.Default.Reset();

            //after delete and create database connection is closed, so i have to reconnect.
            //normaly i wouldn't need to reconnect because the app should already be connected to the db.
            //DataManager.ConnectToDatabase();

            ImportTestData();

            DataManager.SaveChanges();

            lessonsListbox.Items.Clear();

            foreach (Lesson lesson in DataManager.Database.Lessons)
            {
                lessonsListbox.Items.Add(lesson);
            }
        }

        private void ImportTestData()
        {
            using (StreamReader sr = new StreamReader("Resources\\SenpaiTestDatabase.txt"))
            {
                String line = sr.ReadLine();

                do
                {
                    Lesson lesson = new Lesson(line);

                    if (lesson.Type == (int)Lesson.EType.vocab)
                    {
                        List<Word> words = new List<Word>();

                        for (int j = 0; j < lesson.Size; ++j)
                        {
                            line = sr.ReadLine();

                            Word word = new Word(line);

                            words.Add(word);
                        }

                        DataManager.CreateVocabLesson(lesson, words);
                    }
                    else if (lesson.Type == (int)Lesson.EType.kanji)
                    {
                        List<Kanji> kanjis = new List<Kanji>();

                        for (int j = 0; j < lesson.Size; ++j)
                        {
                            line = sr.ReadLine();

                            Kanji kanji = new Kanji(line);

                            kanjis.Add(kanji);
                        }

                        DataManager.CreateFlashLesson(lesson, kanjis);
                    }
                    else if (lesson.Type == (int)Lesson.EType.insert)
                    {
                        List<Cloze> clozes = new List<Cloze>();

                        for (int j = 0; j < lesson.Size; ++j)
                        {
                            line = sr.ReadLine();

                            Cloze cloze = new Cloze(line);

                            clozes.Add(cloze);
                        }

                        DataManager.CreateInsertLesson(lesson, clozes);
                    }

                    line = sr.ReadLine();
                }
                while (line != null);
            }
        }

        #endregion

        #endregion
    }
}
