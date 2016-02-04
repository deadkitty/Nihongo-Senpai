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
using NihongoSenpai.Data;
using NihongoSenpai.Resources;
using NihongoSenpai.Data.Database;
using NihongoSenpai.Controls;

namespace NihongoSenpai.Pages
{
    public partial class PracticeFlashcardsPage : PhoneApplicationPage, IPageUpdater
    {
        #region Constructor
                
        public PracticeFlashcardsPage()
        {         
            InitializeComponent();

            DetailKanjiItem.canHideText = true;

            FlashcardsController.pageUpdater = this;
            
            UpdateView();
        }
        
        #endregion

        #region Events

        #region OnNavigatedTo/From

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if(MessageBox.Show(AppResources.Really, AppResources.CancelExercise, MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                DataManager.SaveChanges();
            }
            
            base.OnBackKeyPress(e);
        }

        private void PhoneApplicationPage_Unloaded(object sender, RoutedEventArgs e)
        {
            FlashcardsController.Deinitialize();
        }

        #endregion

        #region Buttons

        private void correct1Button_Click(object sender, RoutedEventArgs e)
        {
            FlashcardsController.EvaluateKanji(5);

            if (FlashcardsController.GetNext())
            {
                UpdateView();
            }
        }

        private void correct2Button_Click(object sender, RoutedEventArgs e)
        {
            FlashcardsController.EvaluateKanji(4);

            if (FlashcardsController.GetNext())
            {
                UpdateView();
            }
        }

        private void correct3Button_Click(object sender, RoutedEventArgs e)
        {
            FlashcardsController.EvaluateKanji(3);

            if (FlashcardsController.GetNext())
            {
                UpdateView();
            }
        }

        private void wrong1Button_Click(object sender, RoutedEventArgs e)
        {
            FlashcardsController.EvaluateKanji(2);

            if (FlashcardsController.GetNext())
            {
                UpdateView();
            }
        }

        private void wrong2Button_Click(object sender, RoutedEventArgs e)
        {
            FlashcardsController.EvaluateKanji(1);

            if (FlashcardsController.GetNext())
            {
                UpdateView();
            }
        }

        private void wrong3Button_Click(object sender, RoutedEventArgs e)
        {
            FlashcardsController.EvaluateKanji(0);

            if(FlashcardsController.GetNext())
            {
                UpdateView();
            }
        }
        
        #endregion

        private void UpdateView()
        {
            wordsLeftValueTextblock   .Text = FlashcardsData.ItemsLeft.ToString();
            wordsCorrectValueTextblock.Text = FlashcardsData.ItemsCorrect.ToString();
            wordsWrongValueTextblock  .Text = FlashcardsData.ItemsWrong.ToString();

            detailKanjiItem.FillKanjiItem(FlashcardsData.ActiveKanji, true);
        }

        #endregion

        #region IPageUpdater

        public void UpdatePage()
        {
            throw new NotImplementedException();
        }

        public void EndPractice()
        {
            MessageBox.Show(AppResources.NoKanjisLeft);

            NavigationService.GoBack();
        }

        public void RoundFinished()
        {
            if(MessageBox.Show(AppResources.Again, AppResources.RoundFinished, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                FlashcardsController.LoadLessons();
            }
            else
            {
                NavigationService.Navigate(new Uri("/Pages/SelectFlashcardsPage.xaml", UriKind.Relative));

                NavigationService.RemoveBackEntry();
                NavigationService.RemoveBackEntry();
            }
        }

        #endregion
    }
}