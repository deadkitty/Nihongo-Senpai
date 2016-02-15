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
using NihongoSenpai.Controls;
using NihongoSenpai.Model;

namespace NihongoSenpai.Pages
{
    public partial class EditLessonPage : PhoneApplicationPage
    {
        private DetailWordItem[] detailItems;

        public EditLessonPage()
        {
            InitializeComponent();

            showTypeControl.parent = this;
            showDescControl.parent = this;
            showWordsControl.parent = this;

            detailItems = new DetailWordItem[AppData.Words.Count];

            foreach (Word w in AppData.Words)
            {
                itemsListbox.Items.Add(new DetailWordItem(w));
                detailItems[itemsListbox.Items.Count - 1] = itemsListbox.Items[itemsListbox.Items.Count - 1] as DetailWordItem;
            }
        }
        
        private void itemsListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach(DetailWordItem item in e.AddedItems)
            {
                item.Selected();
            }

            foreach(DetailWordItem item in e.RemovedItems)
            {
                item.Deselected();
            }
        }

        private void executeButton_Click(object sender, RoutedEventArgs e)
        {
            switch(editOperationPicker.SelectedIndex)
            {
                case 0: showTypeControl.Visibility = System.Windows.Visibility.Visible; break;
                case 1: showDescControl.Visibility = System.Windows.Visibility.Visible; break;
                case 2: showWordsControl.Visibility = System.Windows.Visibility.Visible; break;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if(e.NavigationMode == NavigationMode.Back)
            {
                DataManager.SaveChanges();
            }
        }

        public void EditSelectedItems()
        {           
            switch(editOperationPicker.SelectedIndex)
            {
                case 0: EditType(); break;
                case 1: EditDesc(); break;
                case 2: EditWord(); break;
            }

            itemsListbox.SelectedItems.Clear();
        }

        private void EditType()
        {
            foreach(DetailWordItem item in itemsListbox.SelectedItems)
            {
                item.value.Type = (Word.EType)showTypeControl.SelectedIndex;
                item.Update();
            }
        }

        private void EditDesc()
        {
            foreach(DetailWordItem item in itemsListbox.SelectedItems)
            {
                item.value.ShowDescription = (Word.EShowFlags)showDescControl.SelectedIndex;
                item.Update();
            }
        }

        private void EditWord()
        {
            int wordsFlag = showTypeControl.SelectedIndex << 2;

            foreach(DetailWordItem item in itemsListbox.SelectedItems)
            {
                item.value.ShowWord = (Word.EShowFlags)showWordsControl.SelectedIndex;
                item.Update();
            }
        }
    }
}