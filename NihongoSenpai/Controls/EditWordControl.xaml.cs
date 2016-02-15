using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NihongoSenpai.Model.Database;
using NihongoSenpai.Pages;
using NihongoSenpai.Controller;

namespace NihongoSenpai.Controls
{
    public partial class EditWordControl : UserControl
    {
        private Word word;

        public IPageUpdater pageUpdater = null;

        public EditWordControl()
        {
            InitializeComponent();
        }

        public void FillControl(Word word)
        {
            this.word = word;

            kanaTextbox.Text = word.kana;
            kanjiTextbox.Text = word.kanji;
            translationTextbox.Text = word.translation;
            descriptionTextbox.Text = word.description;
            showDescFlagListbox.SelectedIndex = (int)word.ShowDescription;
            showWordFlagListbox.SelectedIndex = (int)word.ShowWord;
            
            typeListbox.SelectedIndex = (int)word.Type;
        }

        private void cancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Visibility = System.Windows.Visibility.Collapsed;
        }

        private void editButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            word.kana = kanaTextbox.Text;
            word.kanji = kanjiTextbox.Text;
            word.translation = translationTextbox.Text;
            word.description = descriptionTextbox.Text;
            word.Type = (Word.EType)typeListbox.SelectedIndex;
            word.ShowDescription = (Word.EShowFlags)showDescFlagListbox.SelectedIndex;
            word.ShowWord = (Word.EShowFlags)showWordFlagListbox.SelectedIndex;

            if(pageUpdater != null)
            {
                pageUpdater.UpdatePage();
            }

            DataManager.SaveChanges();
            Visibility = System.Windows.Visibility.Collapsed;
        }

        private void editOperationPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            typeListbox        .Visibility = System.Windows.Visibility.Collapsed;
            showDescFlagListbox.Visibility = System.Windows.Visibility.Collapsed;
            showWordFlagListbox.Visibility = System.Windows.Visibility.Collapsed;

            switch(editOperationPicker.SelectedIndex)
            {
                case 0: typeListbox        .Visibility = System.Windows.Visibility.Visible; break;
                case 1: showDescFlagListbox.Visibility = System.Windows.Visibility.Visible; break;
                case 2: showWordFlagListbox.Visibility = System.Windows.Visibility.Visible; break;
            }
        }
    }
}
