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
    /// Interaktionslogik für CreateVocabLessonPage.xaml
    /// </summary>
    public partial class CreateVocabLessonPage : Page, IPageUpdater
    {
        #region Fields

        private CultureInfo defaultLanguage;
        private CultureInfo japaneseLanguage;
        
        private Lesson lesson = null;
        private List<Word> newWords = new List<Word>();
        private List<Word> wordsToDelete = new List<Word>();

        private DetailItem selectedItem = null;

        private String createButtonContent = null;
        private String cancelButtonContent = null;

        #endregion

        #region Constructor

        public CreateVocabLessonPage()
        {
            InitializeComponent();
            
            defaultLanguage = CultureInfo.CurrentCulture;
            japaneseLanguage = CultureInfo.GetCultureInfo("ja-JP");

            selectTypeCtrl.pageUpdater = this;
            
            createButtonContent = AppResources.CreateLesson;
            cancelButtonContent = AppResources.CancelCreation;
        }
        
        public CreateVocabLessonPage(Lesson lesson)
        {
            InitializeComponent();
            
            defaultLanguage = CultureInfo.CurrentCulture;
            japaneseLanguage = CultureInfo.GetCultureInfo("ja-JP");

            selectTypeCtrl.pageUpdater = this;

            this.lesson = lesson;
            createButtonContent = AppResources.EditLesson;
            cancelButtonContent = AppResources.CancelEdit;

            createLesson.Content = createButtonContent;
            cancelCreation.Content = cancelButtonContent;
        }

        #endregion

        #region Page Initialize/Deinitialize
        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            typeComboBox.Items.Add("Alle");

            for(int i = 0; i < (int)Word.EType.count; ++i)
            {
                typeComboBox.Items.Add(Word.GetTypeString((Word.EType)i, true));
            }

            if(lesson != null)
            {
                AddLesson(lesson);
                //AddTestWords();
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
            lessonNameTextbox.Text = lesson.Name;

            foreach(Word word in lesson.Words)
            {
                wordsListbox.Items.Add(new DetailItem(word));
            }
        }

        private void AddTestLesson()
        {
            StreamReader sr = new StreamReader("Resources\\TestLesson.txt");
            String lessonLine = sr.ReadLine();

            lessonNameTextbox.Text = lessonLine.Split('|')[1];

            String[] lines = sr.ReadToEnd().Split('\n');
            
            foreach (String line in lines)
            {
                Word w = new Word(line);

                //w.Type = Word.EType.other;

                wordsListbox.Items.Add(new DetailItem(w));
                newWords.Add(w);
            }

            sr.Close();
        }

        private void AddTestWords()
        {
            Word[] words = new Word[5];
            
            words[0] = new Word("かわいい|可愛い|süüß :3||0");
            words[1] = new Word("かっこいい||cool||0");
            words[2] = new Word("あたらしい|新しい|neu||0");
            words[3] = new Word("はい||ja||0");
            words[4] = new Word("いいえ||nöö||0");
            
            foreach(Word word in words)
            {
                DetailItem item = new DetailItem(word);

                wordsListbox.Items.Add(item);
                newWords.Add(word);
            }

        }

        #endregion

        #region Textbox Focus

        private void kanaTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            InputLanguageManager.Current.CurrentInputLanguage = japaneseLanguage;
        }

        private void kanjiTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            InputLanguageManager.Current.CurrentInputLanguage = japaneseLanguage;
        }

        private void translationTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            InputLanguageManager.Current.CurrentInputLanguage = defaultLanguage;
        }

        private void descriptionTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            InputLanguageManager.Current.CurrentInputLanguage = defaultLanguage;
        }

        private void kanaTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                kanjiTextbox.Focus();
            }
        }

        private void kanjiTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                translationTextbox.Focus();
            }

        }

        private void translationTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                descriptionTextbox.Focus();
            }
        }

        private void descriptionTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if(wordsListbox.SelectedItem == null)
                {
                    CreateWord();
                }
                else
                {
                    UpdateWord();

                    selectedItem = null;
                    wordsListbox.SelectedItems.Clear();
                }

                ClearTextboxes();
                
                kanaTextbox.Focus();
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

            newLesson.Name = lessonNameTextbox.Text;
            newLesson.Size = newWords.Count;
            newLesson.Type = (int)Lesson.EType.vocab;

            DataManager.CreateVocabLesson(newLesson, newWords);
        }

        private void UpdateLesson()
        {
            lesson.Name = lessonNameTextbox.Text;
            lesson.Size += newWords.Count - wordsToDelete.Count;

            DataManager.UpdateVocabLesson(lesson, newWords, wordsToDelete);
        }

        private void cancelCreation_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show(cancelButtonContent, AppResources.Really, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                NavigationService.GoBack();
            }
        }
        
        private void chooseAll_Click(object sender, RoutedEventArgs e)
        {
            wordsListbox.SelectAll();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show(AppResources.Really, AppResources.DeleteWords, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                while(wordsListbox.SelectedItems.Count > 0)
                {
                    DetailItem item = wordsListbox.SelectedItems[0] as DetailItem;
                    
                    wordsListbox.Items.Remove(item);

                    if(!newWords.Remove(item.sourceWord))
                    {
                        wordsToDelete.Add(item.sourceWord);
                    }
                }

                //TypeSelectionChanged();
            }
        }

        private void setType_Click(object sender, RoutedEventArgs e)
        {
            selectTypeCtrl.Visibility = System.Windows.Visibility.Visible;

            foreach(DetailItem item in wordsListbox.SelectedItems)
            {
                selectTypeCtrl.selectedWords.Add(item.sourceWord);
            }

            wordsListbox.SelectedItems.Clear();
        }

        #endregion

        #region Others

        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TypeSelectionChanged();
        }

        private void TypeSelectionChanged()
        {
            wordsListbox.Items.Clear();
            
            //if all is selected, add all
            if(typeComboBox.SelectedIndex == 0)
            {
                if(lesson != null)
                {
                    foreach(Word word in lesson.Words)
                    {
                        wordsListbox.Items.Add(new DetailItem(word));
                    }
                }

                foreach(Word word in newWords)
                {
                    wordsListbox.Items.Add(new DetailItem(word));
                }
            }
            else //otherwise add just the correct type words
            {
                int selectedType = typeComboBox.SelectedIndex - 1;

                if(lesson != null)
                {
                    foreach(Word word in lesson.Words)
                    {
                        if(word.Type == selectedType)
                        {
                            wordsListbox.Items.Add(new DetailItem(word));
                        }
                    }
                }

                foreach(Word word in newWords)
                {
                    if(word.Type == selectedType)
                    {
                        wordsListbox.Items.Add(new DetailItem(word));
                    }
                }
            }
        }
        
        private void wordsListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(wordsListbox.SelectedItems.Count > 0)
            {
                selectedItem = wordsListbox.SelectedItems[0] as DetailItem;
                FillTextboxes(selectedItem.sourceWord);
            }
            else
            {
                ClearTextboxes();
            }
        }

        #endregion

        #region Helper
        
        public void UpdatePage()
        {
            TypeSelectionChanged();
        }

        private void CreateWord()
        {
            Word word = new Word();

            word.Kana = kanaTextbox.Text;
            word.Kanji = kanjiTextbox.Text;
            word.Translation = translationTextbox.Text;
            word.Description = descriptionTextbox.Text;

            newWords.Add(word);

            //if all is selected or other, than show word in the listbox
            if(typeComboBox.SelectedIndex == 0 ||
               typeComboBox.SelectedIndex - 1 == (int)Word.EType.other)
            {
                DetailItem item = new DetailItem(word);
                
                wordsListbox.Items.Add(item);
            }
        }

        private void UpdateWord()
        {
            selectedItem.sourceWord.Kana = kanaTextbox.Text;
            selectedItem.sourceWord.Kanji = kanjiTextbox.Text;
            selectedItem.sourceWord.Translation = translationTextbox.Text;
            selectedItem.sourceWord.Description = descriptionTextbox.Text;

            selectedItem.Update();
        }

        private void FillTextboxes(Word word)
        {
            kanaTextbox.Text = word.Kana;
            kanjiTextbox.Text = word.Kanji;
            translationTextbox.Text = word.Translation;
            descriptionTextbox.Text = word.Description;
        }

        private void ClearTextboxes()
        {
            kanaTextbox.Clear();
            kanjiTextbox.Clear();
            translationTextbox.Clear();
            descriptionTextbox.Clear();
        }

        #endregion
    }
}
