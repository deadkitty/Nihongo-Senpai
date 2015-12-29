using SenpaiCreationKit.Controls;
using SenpaiCreationKit.Data;
using SenpaiCreationKit.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaktionslogik für CreateFlashLessonPage.xaml
    /// </summary>
    public partial class CreateFlashLessonPage : Page
    {
        #region Fields

        CultureInfo defaultLanguage;
        CultureInfo japaneseLanguage;
        
        private Lesson lesson = null;
        private List<Kanji> newKanjis = new List<Kanji>();
        private List<Kanji> kanjisToDelete = new List<Kanji>();

        private DetailItem selectedItem = null;

        private String createButtonContent = null;
        private String cancelButtonContent = null;

        #endregion

        #region Constructor

        public CreateFlashLessonPage()
        {
            InitializeComponent();
            
            defaultLanguage = CultureInfo.CurrentCulture;
            japaneseLanguage = CultureInfo.GetCultureInfo("ja-JP");
            
            createButtonContent = AppResources.CreateLesson;
            cancelButtonContent = AppResources.CancelCreation;
        }
        
        public CreateFlashLessonPage(Lesson lesson)
        {
            InitializeComponent();
            
            defaultLanguage = CultureInfo.CurrentCulture;
            japaneseLanguage = CultureInfo.GetCultureInfo("ja-JP");
            
            createButtonContent = AppResources.EditLesson;
            cancelButtonContent = AppResources.CancelEdit;

            this.lesson = lesson;
        }

        #endregion
        
        #region Page Initialize/Deinitialize

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //AddTestLesson();

            if(lesson != null)
            {
                AddLesson(lesson);
                AddTestKanjis();
            }
            else
            {
                //AddTestLesson();
            }
        }
        
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }
        
        private void AddLesson(Lesson lesson)
        {
            lessonNameTextbox.Text = lesson.name;

            foreach(Kanji kanji in lesson.Kanjis)
            {
                kanjisListbox.Items.Add(new DetailItem(kanji));
            }
        }

        private void AddTestKanjis()
        {
            Kanji[] kanjis = new Kanji[5];
            
            kanjis[0] = new Kanji("母|Mutter|ぼ|はは|祖母 - そぼ - Großmutter|-");
            kanjis[1] = new Kanji("父|Vater|ふ|ちち|祖父 - そふ - Großvater|-");
            kanjis[2] = new Kanji("女|Frau, weiblich|じょ|おんあ、め|女性 - じょせい - Weiblich|-");
            kanjis[3] = new Kanji("男|Mann, männlich|だん、なん|おとこ|男性 - だんせい - Männlich|-");
            kanjis[4] = new Kanji("子|Kind|し、す|こ|子供 - こども - Kind|-");
            
            foreach(Kanji kanji in kanjis)
            {
                DetailItem item = new DetailItem(kanji);

                kanjisListbox.Items.Add(item);
                newKanjis.Add(kanji);
            }
        }

        private void AddTestLesson()
        {
            StreamReader sr = new StreamReader("Resources\\TestLessonFlash.txt");
            String lessonLine = sr.ReadLine();

            lessonNameTextbox.Text = lessonLine.Split('|')[1];

            String[] lines = sr.ReadToEnd().Split('\n');
            
            foreach (String line in lines)
            {
                Kanji k = new Kanji(line);

                kanjisListbox.Items.Add(new DetailItem(k));
                newKanjis.Add(k);
            }

            sr.Close();
        }

        #endregion

        #region Textbox Focus

        private void kanjiTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            InputLanguageManager.Current.CurrentInputLanguage = japaneseLanguage;
        }

        private void meaningTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            InputLanguageManager.Current.CurrentInputLanguage = defaultLanguage;
        }

        private void onyomiTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            InputLanguageManager.Current.CurrentInputLanguage = japaneseLanguage;
        }

        private void kunyomiTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            InputLanguageManager.Current.CurrentInputLanguage = japaneseLanguage;
        }

        private void exampleTextbox1_GotFocus(object sender, RoutedEventArgs e)
        {
            InputLanguageManager.Current.CurrentInputLanguage = japaneseLanguage;
        }

        private void exampleTextbox2_GotFocus(object sender, RoutedEventArgs e)
        {
            InputLanguageManager.Current.CurrentInputLanguage = japaneseLanguage;
        }

        private void exampleTextbox3_GotFocus(object sender, RoutedEventArgs e)
        {
            InputLanguageManager.Current.CurrentInputLanguage = defaultLanguage;
        }

        private void kanjiTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                meaningTextbox.Focus();
            }
        }

        private void meaningTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                onyomiTextbox.Focus();
            }
        }

        private void onyomiTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                kunyomiTextbox.Focus();
            }
        }

        private void kunyomiTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                exampleTextbox1.Focus();
            }
        }

        private void exampleTextbox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                exampleTextbox2.Focus();
            }
        }

        private void exampleTextbox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                exampleTextbox3.Focus();
            }
        }

        private void exampleTextbox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if(kanjisListbox.SelectedItem == null)
                {
                    CreateKanji();
                }
                else
                {
                    UpdateKanji();

                    selectedItem = null;
                    kanjisListbox.SelectedItems.Clear();
                }

                ClearTextboxes();

                kanjiTextbox.Focus();
            }
        }
        
        #endregion

        #region Button Click

        private void createLesson_Click(object sender, RoutedEventArgs e)
        {
            if(lessonNameTextbox.Text == "")
            {
                MessageBox.Show(AppResources.LessonNameEmpty);
            }
            else
            {
                if(lesson == null)
                {
                    CreateLesson();
                }
                else
                {
                    UpdateLesson();
                }

                DataManager.SaveChanges();

                NavigationService.GoBack();
            }
        }
        
        private void CreateLesson()
        {
            Lesson newLesson = new Lesson();

            newLesson.name = lessonNameTextbox.Text;
            newLesson.size = newKanjis.Count;
            newLesson.Type = Lesson.EType.kanji;

            DataManager.CreateFlashLesson(newLesson, newKanjis);
        }

        private void UpdateLesson()
        {
            lesson.name = lessonNameTextbox.Text;
            lesson.size += newKanjis.Count - kanjisToDelete.Count;

            DataManager.UpdateFlashLesson(lesson, newKanjis, kanjisToDelete);
        }

        private void cancelCreation_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show(cancelButtonContent, AppResources.Really, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                if(lesson != null)
                {
                    //refresh to undo changes
                    DataManager.RefreshConnection();
                }

                NavigationService.GoBack();
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show(AppResources.Really, AppResources.DeleteKanjis, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                while(kanjisListbox.SelectedItems.Count > 0)
                {
                    DetailItem item = kanjisListbox.SelectedItems[0] as DetailItem;
                    
                    kanjisListbox.Items.Remove(item);
                    
                    if(!newKanjis.Remove(item.sourceKanji))
                    {
                        kanjisToDelete.Add(item.sourceKanji);
                    }
                }
            }
        }

        private void chooseAll_Click(object sender, RoutedEventArgs e)
        {
            kanjisListbox.SelectAll();
        }
        
        #endregion

        #region Others
        
        private void kanjisListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(kanjisListbox.SelectedItems.Count > 0)
            {
                selectedItem = kanjisListbox.SelectedItems[0] as DetailItem;
                FillTextboxes(selectedItem.sourceKanji);
            }
            else
            {
                ClearTextboxes();
            }
        }

        #endregion

        #region Helper

        private void CreateKanji()
        {
            Kanji kanji = new Kanji();

            kanji.kanji   = kanjiTextbox  .Text;
            kanji.meaning = meaningTextbox.Text;
            kanji.onyomi  = onyomiTextbox .Text;
            kanji.kunyomi = kunyomiTextbox.Text;
            
            StringBuilder sb = new StringBuilder();
            
            sb.Append(exampleTextbox1.Text);
            sb.Append(" - ");
            sb.Append(exampleTextbox2.Text);
            sb.Append(" - ");
            sb.Append(exampleTextbox3.Text);
            sb.Append(" - ");

            kanji.example = sb.ToString();

            newKanjis.Add(kanji);

            DetailItem item = new DetailItem(kanji);
            kanjisListbox.Items.Add(item);
        }
        
        private void UpdateKanji()
        {
            selectedItem.sourceKanji.kanji   = kanjiTextbox  .Text;
            selectedItem.sourceKanji.meaning = meaningTextbox.Text;
            selectedItem.sourceKanji.onyomi  = onyomiTextbox .Text;
            selectedItem.sourceKanji.kunyomi = kunyomiTextbox.Text;

            StringBuilder sb = new StringBuilder();
            
            sb.Append(exampleTextbox1.Text);
            sb.Append(" - ");
            sb.Append(exampleTextbox2.Text);
            sb.Append(" - ");
            sb.Append(exampleTextbox3.Text);
            sb.Append(" - ");

            selectedItem.sourceKanji.example = sb.ToString();

            selectedItem.Update();
        }

        private void FillTextboxes(Kanji kanji)
        {
            kanjiTextbox  .Text = kanji.kanji;
            meaningTextbox.Text = kanji.meaning;
            onyomiTextbox .Text = kanji.onyomi;
            kunyomiTextbox.Text = kanji.kunyomi;
            
            String[] examples = kanji.example.Split('-');
            
            exampleTextbox1.Text = examples[0].Trim();
            exampleTextbox2.Text = examples[1].Trim();
            exampleTextbox3.Text = examples[2].Trim();
        }

        private void ClearTextboxes()
        {
            kanjiTextbox.Clear();
            meaningTextbox.Clear();
            onyomiTextbox.Clear();
            kunyomiTextbox.Clear();
            exampleTextbox1.Clear();
            exampleTextbox2.Clear();
            exampleTextbox3.Clear();
        }

        #endregion
    }
}
