using SenpaiCreationKit.Controls;
using SenpaiCreationKit.Data;
using SenpaiCreationKit.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaktionslogik für EditFlashLessonPage.xaml
    /// </summary>
    public partial class EditFlashLessonPage : Page
    {
        #region Fields

        private CultureInfo defaultLanguage;
        private CultureInfo japaneseLanguage;

        private Lesson lesson;

        private List<Kanji> newKanjis = new List<Kanji>();
        private List<Kanji> kanjisToDelete = new List<Kanji>();

        private DetailItem selectedItem = null;

        #endregion

        #region Constructor

        public EditFlashLessonPage(Lesson lesson)
        {
            InitializeComponent();

            this.lesson = lesson;

            defaultLanguage = CultureInfo.CurrentCulture;
            japaneseLanguage = CultureInfo.GetCultureInfo("ja-JP");
        }

        #endregion

        #region Initialize
        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lessonNameTextbox.Text = lesson.Name;

            foreach(Kanji kanji in lesson.Kanjis)
            {
                kanjisListbox.Items.Add(new DetailItem(kanji));
            }
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
                if (kanjiTextbox.Text == "")
                {
                    MessageBox.Show(AppResources.KanjiTextboxesCantBeEmpty);

                    kanjiTextbox.Focus();
                }
                else if (meaningTextbox.Text == "")
                {
                    MessageBox.Show(AppResources.KanjiTextboxesCantBeEmpty);

                    meaningTextbox.Focus();
                }
                else if (onyomiTextbox.Text == "")
                {
                    MessageBox.Show(AppResources.KanjiTextboxesCantBeEmpty);

                    onyomiTextbox.Focus();
                }
                else
                {
                    if (kanjisListbox.SelectedItem == null)
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
        }

        #endregion

        #region Button Click
        
        private void editLesson_Click(object sender, RoutedEventArgs e)
        {
            if (lessonNameTextbox.Text == "")
            {
                MessageBox.Show(AppResources.LessonNameEmpty);
            }
            else
            {
                lesson.Name = lessonNameTextbox.Text;
                lesson.Size += newKanjis.Count - kanjisToDelete.Count;

                DataManager.UpdateFlashLesson(lesson, newKanjis, kanjisToDelete);

                NavigationService.GoBack();
            }
        }

        private void cancelEdit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(AppResources.CancelEdit, AppResources.Really, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                NavigationService.GoBack();
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(AppResources.Really, AppResources.DeleteKanjis, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                while (kanjisListbox.SelectedItems.Count > 0)
                {
                    DetailItem item = kanjisListbox.SelectedItems[0] as DetailItem;

                    kanjisListbox.Items.Remove(item);
                    
                    if (!newKanjis.Remove(item.sourceKanji))
                    {
                        kanjisToDelete.Add(item.sourceKanji);
                        lesson.Kanjis.Remove(item.sourceKanji);
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
            if (kanjisListbox.SelectedItems.Count > 0)
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

            kanji.Sign    = kanjiTextbox  .Text;
            kanji.Meaning = meaningTextbox.Text;
            kanji.Onyomi  = onyomiTextbox .Text;
            kanji.Kunyomi = kunyomiTextbox.Text;

            StringBuilder sb = new StringBuilder();

            sb.Append(exampleTextbox1.Text);
            sb.Append(" - ");
            sb.Append(exampleTextbox2.Text);
            sb.Append(" - ");
            sb.Append(exampleTextbox3.Text);

            kanji.Example = sb.ToString();

            newKanjis.Add(kanji);

            DetailItem item = new DetailItem(kanji);
            kanjisListbox.Items.Add(item);
        }

        private void UpdateKanji()
        {
            selectedItem.sourceKanji.Sign    = kanjiTextbox  .Text;
            selectedItem.sourceKanji.Meaning = meaningTextbox.Text;
            selectedItem.sourceKanji.Onyomi  = onyomiTextbox .Text;
            selectedItem.sourceKanji.Kunyomi = kunyomiTextbox.Text;

            StringBuilder sb = new StringBuilder();

            sb.Append(exampleTextbox1.Text);
            sb.Append(" - ");
            sb.Append(exampleTextbox2.Text);
            sb.Append(" - ");
            sb.Append(exampleTextbox3.Text);

            selectedItem.sourceKanji.Example = sb.ToString();

            selectedItem.Update();
        }

        private void FillTextboxes(Kanji kanji)
        {
            kanjiTextbox  .Text = kanji.Sign;
            meaningTextbox.Text = kanji.Meaning;
            onyomiTextbox .Text = kanji.Onyomi;
            kunyomiTextbox.Text = kanji.Kunyomi;

            String[] examples = kanji.Example.Split('-');

            exampleTextbox1.Text = examples[0].Trim();
            exampleTextbox2.Text = examples[1].Trim();
            exampleTextbox3.Text = examples[2].Trim();
        }

        private void ClearTextboxes()
        {
            kanjiTextbox   .Clear();
            meaningTextbox .Clear();
            onyomiTextbox  .Clear();
            kunyomiTextbox .Clear();
            exampleTextbox1.Clear();
            exampleTextbox2.Clear();
            exampleTextbox3.Clear();
        }

        #endregion
    }
}
