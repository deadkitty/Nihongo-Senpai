﻿using SenpaiCreationKit.Controls;
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
    /// Interaktionslogik für EditVocabLessonPage.xaml
    /// </summary>
    public partial class EditVocabLessonPage : Page, IPageUpdater
    {
        #region Fields

        private CultureInfo defaultLanguage;
        private CultureInfo japaneseLanguage;

        private Lesson lesson = null;

        private List<Word> newWords = new List<Word>();
        private List<Word> wordsToDelete = new List<Word>();

        private DetailItem selectedItem = null;

        #endregion

        #region Constructor

        public EditVocabLessonPage(Lesson lesson)
        {
            InitializeComponent();

            defaultLanguage = CultureInfo.CurrentCulture;
            japaneseLanguage = CultureInfo.GetCultureInfo("ja-JP");

            selectTypeCtrl.pageUpdater = this;

            this.lesson = lesson;
        }

        #endregion

        #region Initialize

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            typeComboBox.Items.Add("Alle");

            for (int i = 0; i < (int)Word.EType.count; ++i)
            {
                typeComboBox.Items.Add(Word.GetTypeString((Word.EType)i, true));
            }

            //i add this here, so it is not called when i add all the types to it
            //to prevent that the words from the lesson to edit are added to early
            typeComboBox.SelectionChanged += typeComboBox_SelectionChanged;

            lessonNameTextbox.Text = lesson.Name;

            foreach (Word word in lesson.Words)
            {
                wordsListbox.Items.Add(new DetailItem(word));
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
                if (kanaTextbox.Text == "")
                {
                    MessageBox.Show(AppResources.VocabTextboxesCantBeEmpty);

                    kanaTextbox.Focus();
                }
                else if (translationTextbox.Text == "")
                {
                    MessageBox.Show(AppResources.VocabTextboxesCantBeEmpty);

                    translationTextbox.Focus();
                }
                else
                {
                    if (wordsListbox.SelectedItem == null)
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
                lesson.Size += newWords.Count - wordsToDelete.Count;

                DataManager.UpdateVocabLesson(lesson, newWords, wordsToDelete);

                NavigationService.GoBack();
            }
        }

        private void editCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(AppResources.CancelEdit, AppResources.Really, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
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
            if (MessageBox.Show(AppResources.Really, AppResources.DeleteWords, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                while (wordsListbox.SelectedItems.Count > 0)
                {
                    DetailItem item = wordsListbox.SelectedItems[0] as DetailItem;
                    
                    wordsListbox.Items.Remove(item);

                    //if the word was no new word than add it to the delete list to make sure to delete realy remove it from the database later
                    if (!newWords.Remove(item.sourceWord))
                    {
                        wordsToDelete.Add(item.sourceWord);
                        lesson.Words.Remove(item.sourceWord);
                    }
                }
            }
        }

        private void setType_Click(object sender, RoutedEventArgs e)
        {
            selectTypeCtrl.Visibility = System.Windows.Visibility.Visible;

            foreach (DetailItem item in wordsListbox.SelectedItems)
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
            if (typeComboBox.SelectedIndex == 0)
            {
                foreach (Word word in lesson.Words)
                {
                    wordsListbox.Items.Add(new DetailItem(word));
                }

                foreach (Word word in newWords)
                {
                    wordsListbox.Items.Add(new DetailItem(word));
                }
            }
            else //otherwise add just the correct type words
            {
                int selectedType = typeComboBox.SelectedIndex - 1;
                                
                foreach (Word word in lesson.Words)
                {
                    if (word.Type == selectedType)
                    {
                        wordsListbox.Items.Add(new DetailItem(word));
                    }
                }
                
                foreach (Word word in newWords)
                {
                    if (word.Type == selectedType)
                    {
                        wordsListbox.Items.Add(new DetailItem(word));
                    }
                }
            }
        }

        private void wordsListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (wordsListbox.SelectedItems.Count > 0)
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

        #region IPageUpdater

        public void UpdatePage()
        {
            TypeSelectionChanged();
        }

        #endregion

        #region Helper

        private void CreateWord()
        {
            Word word = new Word();

            word.Kana        = kanaTextbox       .Text;
            word.Kanji       = kanjiTextbox      .Text;
            word.Translation = translationTextbox.Text;
            word.Description = descriptionTextbox.Text;
            word.Type        = (int)Word.EType.other;

            newWords.Add(word);

            //if all is selected or other, than show word in the listbox
            if (typeComboBox.SelectedIndex     == 0 ||
                typeComboBox.SelectedIndex - 1 == (int)Word.EType.other)
            {
                DetailItem item = new DetailItem(word);

                wordsListbox.Items.Add(item);
            }
        }

        private void UpdateWord()
        {
            selectedItem.sourceWord.Kana        = kanaTextbox       .Text;
            selectedItem.sourceWord.Kanji       = kanjiTextbox      .Text;
            selectedItem.sourceWord.Translation = translationTextbox.Text;
            selectedItem.sourceWord.Description = descriptionTextbox.Text;

            selectedItem.Update();
        }

        private void FillTextboxes(Word word)
        {
            kanaTextbox       .Text = word.Kana;
            kanjiTextbox      .Text = word.Kanji;
            translationTextbox.Text = word.Translation;
            descriptionTextbox.Text = word.Description;
        }

        private void ClearTextboxes()
        {
            kanaTextbox       .Clear();
            kanjiTextbox      .Clear();
            translationTextbox.Clear();
            descriptionTextbox.Clear();
        }

        #endregion
    }
}
