using Microsoft.Win32;
using SenpaiCreationKit.Controls;
using SenpaiCreationKit.Data;
using SenpaiCreationKit.Properties;
using SenpaiCreationKit.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private List<Lesson> lessons;

        #endregion

        #region Constructor

        public MainPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Initialize

        /// <summary>
        /// is called each time i navigate to this page
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
#if DEBUG

            resetApplication.Visibility = Visibility.Visible;

#endif

            lessons = DataManager.GetLessons();
            
            typeComboBox.Items.Add(AppResources.ChooseAll);
            typeComboBox.Items.Add(AppResources.VocabLesson);
            typeComboBox.Items.Add(AppResources.FlashLesson);
            typeComboBox.Items.Add(AppResources.InsertLesson);

            //marketplace_Click(sender, e);
            
            //NavigationService.Navigate(new StatisticsPage());
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

            switch ((Lesson.EType)lesson.Type)
            {
                case Lesson.EType.vocab : NavigationService.Navigate(new EditVocabLessonPage (lesson)); break;
                case Lesson.EType.insert: NavigationService.Navigate(new EditInsertLessonPage(lesson)); break;
                case Lesson.EType.kanji : NavigationService.Navigate(new EditFlashLessonPage (lesson)); break;
            }
        }

        private void deleteLesson_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(AppResources.Really, AppResources.DeleteLessons, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                while (lessonsListbox.SelectedItems.Count > 0)
                {
                    Lesson lesson = lessonsListbox.SelectedItems[0] as Lesson;

                    DataManager.DeleteLesson(lesson);

                    lessons.Remove(lesson);
                    lessonsListbox.Items.Remove(lesson);
                }
            }
        }

        private void exportLesson_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            fileDialog.FileName = "senpai_export";
            fileDialog.DefaultExt = ".nya";
            fileDialog.Filter = ".nya|*.nya";

            bool? result = fileDialog.ShowDialog();

            if (result == true)
            {
                using (StreamWriter sw = new StreamWriter(fileDialog.FileName))
                {
                    List<Lesson> lessons = new List<Lesson>();

                    foreach (Lesson lesson in lessonsListbox.SelectedItems)
                    {
                        lessons.Add(lesson);
                    }

                    lessons = lessons.OrderBy(x => x.id).ToList();

                    DataManager.ExportLessons(lessons, sw);
                }
            }
        }

        private void importLesson_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = ".nya|*.nya|.poyo|*.poyo";

            bool? result = fileDialog.ShowDialog();

            if (result == true)
            {
                using (FileStream stream = new FileStream(fileDialog.FileName, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        //poyo files save the currentRound Settings, so i overread them first
                        if (fileDialog.FileName.EndsWith(".poyo"))
                        {
                            sr.ReadLine();
                            sr.ReadLine();
                        }
                        
                        DataManager.ImportLessons(sr);
                    }
                }
                
                lessons = DataManager.GetLessons();
                typeComboBox_SelectionChanged(sender, null);
            }
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
            if (e.Key == Key.Enter)
            {
                if (searchTextbox.Text == "")
                {
                    lessons = DataManager.GetLessons();

                    typeComboBox_SelectionChanged(sender, null);
                }
                else
                {
                    lessons = DataManager.GetLessons(searchTextbox.Text);
                    typeComboBox_SelectionChanged(sender, null);
                }
            }
        }

        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lessonsListbox.Items.Clear();

            //if all is selected add all lessons
            if (typeComboBox.SelectedIndex == 0)
            {
                foreach (Lesson lesson in lessons)
                {
                    lessonsListbox.Items.Add(lesson);
                }
            }
            else
            {
                Lesson.EType type = Lesson.EType.undefined;

                switch (typeComboBox.SelectedIndex)
                {
                    case 1: type = Lesson.EType.vocab; break;
                    case 2: type = Lesson.EType.kanji; break;
                    case 3: type = Lesson.EType.insert; break;
                }

                foreach (Lesson lesson in lessons)
                {
                    //otherwise check for the correct type and add it
                    if (lesson.Type == (int)type)
                    {
                        lessonsListbox.Items.Add(lesson);
                    }
                }
            }
        }

        private void lessonsListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lessonsListbox.SelectedItems.Count > 0)
            {
                editLesson  .IsEnabled = true;
                deleteLesson.IsEnabled = true;
                exportLesson.IsEnabled = true;

                detailsListbox.Items.Clear();

                Lesson lesson = lessonsListbox.SelectedItem as Lesson;

                foreach (Word word in lesson.Words)
                {
                    DetailItem item = new DetailItem(word);

                    detailsListbox.Items.Add(item);
                }

                foreach (Kanji kanji in lesson.Kanjis)
                {
                    DetailItem item = new DetailItem(kanji);

                    detailsListbox.Items.Add(item);
                }

                foreach (Cloze cloze in lesson.Clozes)
                {
                    DetailItem item = new DetailItem(cloze);

                    detailsListbox.Items.Add(item);
                }
            }
            else
            {
                editLesson.IsEnabled = false;
                deleteLesson.IsEnabled = false;
                exportLesson.IsEnabled = false;

                UnloadLessonPreview();
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
            Settings.Default.Save();

            lessonsListbox.Items.Clear();
        }
        
        #endregion
    }
}
