using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NihongoSenpai.Controller;
using NihongoSenpai.Data.Database;
using NihongoSenpai.Controls;
using System.Windows.Input;
using NihongoSenpai.Resources;

namespace NihongoSenpai.Pages
{
    public partial class SearchWordsPage : PhoneApplicationPage, IPageUpdater
    {
        #region Constructor

        public SearchWordsPage()
        {
            InitializeComponent();

            editWordsControl.pageUpdater = this;
        }
        
        #endregion

        #region OnNavigatedTo/From

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                DataManager.ConnectToDatabase();
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (e.NavigationMode == NavigationMode.Back)
            {
                DataManager.CloseConnection();
            }
        }
        
        #endregion

        #region Events

        private void searchTextbox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                matchedItemsListbox.Items.Clear();
                
                if (searchTextbox.Text != "")
                {
                    IEnumerable<Word> words = DataManager.FindWords(searchTextbox.Text);
                    
                    foreach (Word w in words)
                    {
                        matchedItemsListbox.Items.Add(new DetailWordItem(w));
                    }
                }

                //hide keyboard
                Focus();
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (matchedItemsListbox.SelectedItem == null)
            {
                MessageBox.Show(AppResources.NoWordSelected);
            }
            else
            {
                DetailWordItem wordItem = matchedItemsListbox.SelectedItem as DetailWordItem;

                editWordsControl.Visibility = System.Windows.Visibility.Visible;
                editWordsControl.FillControl(wordItem.value);
            }
        }

        private void showLessonButton_Click(object sender, RoutedEventArgs e)
        {
            if (matchedItemsListbox.SelectedItem == null)
            {
                MessageBox.Show(AppResources.NoWordSelected);
            }
            else
            {
                DetailWordItem wordItem = matchedItemsListbox.SelectedItem as DetailWordItem;

                DataManager.InitializeAppData(wordItem.value.Lesson);

                NavigationService.Navigate(new Uri("/Pages/ShowVocabPage.xaml", UriKind.Relative));

                matchedItemsListbox.SelectedItem = null;
            }
        }

        private void matchedItemsListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (DetailWordItem item in e.AddedItems)
            {
                item.Selected();
            }

            foreach (DetailWordItem item in e.RemovedItems)
            {
                item.Deselected();
            }
        }
        
        #endregion

        #region IPageUpdater

        public void UpdatePage()
        {
            DetailWordItem wordItem = matchedItemsListbox.SelectedItem as DetailWordItem;

            wordItem.Update();
        }

        public void RoundFinished()
        {

        }

        public void EndPractice()
        {

        }
        
        #endregion
    }
}