using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NihongoSenpai.Resources;
using NihongoSenpai.Data;
using NihongoSenpai.Controller;
using NihongoSenpai.Data.Database;
using System.Windows.Media.Imaging;
using NihongoSenpai.Utilities;
using System.Windows.Media;
using System.Text;

namespace NihongoSenpai.Pages
{
    public partial class PracticeVocabPage : PhoneApplicationPage, IPageUpdater
    {
        #region Fields

        private bool showAnswerButtons = false;
                
        #endregion

        #region Constructor

        public PracticeVocabPage()
        {
            InitializeComponent();
            
            VocabController.pageUpdater = this;

            UpdateView();

            SetBackgroundImage();
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
            
            base.OnBackKeyPress(e);
        }

        private void PhoneApplicationPage_Unloaded(object sender, RoutedEventArgs e)
        {
            VocabController.EndPractice();
            VocabController.Deinitialize();
        }

        #endregion

        #region Button Click

        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleVisibility();

            descriptionTextblock.Text = VocabData.ActiveItem.SourceWord.ToDescriptionString();
        }

        private void correct1Button_Click(object sender, RoutedEventArgs e)
        {
            ToggleVisibility();

            VocabController.EvaluateWord(5);

            if(VocabController.GetNext())
            {
                UpdateView();
            }
        }

        private void correct2Button_Click(object sender, RoutedEventArgs e)
        {
            ToggleVisibility();

            VocabController.EvaluateWord(4);

            if (VocabController.GetNext())
            {
                UpdateView();
            }
        }

        private void correct3Button_Click(object sender, RoutedEventArgs e)
        {
            ToggleVisibility();

            VocabController.EvaluateWord(3);

            if (VocabController.GetNext())
            {
                UpdateView();
            }
        }

        private void wrong1Button_Click(object sender, RoutedEventArgs e)
        {
            ToggleVisibility();

            VocabController.EvaluateWord(2);

            if (VocabController.GetNext())
            {
                UpdateView();
            }
        }

        private void wrong2Button_Click(object sender, RoutedEventArgs e)
        {
            ToggleVisibility();

            VocabController.EvaluateWord(1);

            if (VocabController.GetNext())
            {
                UpdateView();
            }
        }

        private void wrong3Button_Click(object sender, RoutedEventArgs e)
        {
            ToggleVisibility();

            VocabController.EvaluateWord(0);

            if (VocabController.GetNext())
            {
                UpdateView();
            }
        }
        
        #endregion

        #region Application Bar

        private void editIcon_Click(object sender, EventArgs e)
        {
            editWordsControl.Visibility = System.Windows.Visibility.Visible;
            editWordsControl.FillControl(VocabData.ActiveItem.SourceWord);
        }
        
        #endregion

        #endregion

        #region Helper Methods

        private void SetBackgroundImage()
        {
            BitmapImage bmp = new BitmapImage();

            int bgIndex = Util.GetRandomNumber(9);
            String gaus = Util.GetRandomNumber(2) == 0 ? "g" : "";
            
            bmp.UriSource = new Uri("/Assets/Backgrounds/Chibi" + bgIndex + gaus + ".png", UriKind.Relative);

            (mainGrid.Background as ImageBrush).ImageSource = bmp;
        }

        private void ToggleVisibility()
        {
            showAnswerButtons = !showAnswerButtons;

            if (showAnswerButtons)
            {
                showButton.Visibility = System.Windows.Visibility.Collapsed;
                answerButtonsGrid.Visibility = System.Windows.Visibility.Visible;
                
                hiddenTextblock1.Visibility = System.Windows.Visibility.Visible;
                hiddenTextblock2.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                showButton.Visibility = System.Windows.Visibility.Visible;
                answerButtonsGrid.Visibility = System.Windows.Visibility.Collapsed;
                
                hiddenTextblock1.Visibility = System.Windows.Visibility.Collapsed;
                hiddenTextblock2.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        
        public void UpdateView()
        {
            wordsLeftValueTextblock   .Text = VocabData.ItemsLeft   .ToString();
            wordsCorrectValueTextblock.Text = VocabData.ItemsCorrect.ToString();
            wordsWrongValueTextblock  .Text = VocabData.ItemsWrong  .ToString();

            visibleTextblock    .Text = VocabData.ActiveItem.ShownText;
            hiddenTextblock1    .Text = VocabData.ActiveItem.HiddenText1;
            hiddenTextblock2    .Text = VocabData.ActiveItem.HiddenText2;
            descriptionTextblock.Text = VocabData.ActiveItem.DescriptionText;

            int jWordFontSize = 48;
            int gWordFontSize = 30;

            if(VocabData.ActiveItem.GetType() == typeof(JapGerItem))
            {
                visibleTextblock.FontSize = jWordFontSize;
                hiddenTextblock1.FontSize = jWordFontSize;
                hiddenTextblock2.FontSize = gWordFontSize;
            }
            else
            {
                visibleTextblock.FontSize = gWordFontSize;
                hiddenTextblock1.FontSize = jWordFontSize;
                hiddenTextblock2.FontSize = jWordFontSize;
            }
        }
        
        #endregion

        #region IPageUpdater

        public void UpdatePage()
        {
            //StringBuilder sb = new StringBuilder();

            //sb.AppendLine("ActiveWord.eFactor = " + VocabData.ActiveItem.EFactor);
            //sb.AppendLine("ActiveWord.Repetition = " + VocabData.ActiveItem.Repetition);
            //sb.AppendLine("ActiveWord.NextInterval = " + VocabData.ActiveItem.NextInterval);
            //sb.AppendLine("eFactor = " + VocabController.publicEfactor);
            //sb.AppendLine("Repetition = " + VocabController.publicRepetition);
            //sb.AppendLine("NextInterval = " + VocabController.publicNextInterval);

            //MessageBox.Show(sb.ToString());
        }
        
        public void EndPractice()
        {
            MessageBox.Show(AppResources.NoWordsLeft);
            
            NavigationService.Navigate(new Uri("/Pages/SelectVocabPage.xaml", UriKind.Relative));

            NavigationService.RemoveBackEntry();
            NavigationService.RemoveBackEntry();
        }

        public void RoundFinished()
        {
            if(MessageBox.Show(AppResources.Again, AppResources.RoundFinished, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                VocabController.LoadLessons();
            }
            else
            {
                NavigationService.Navigate(new Uri("/Pages/SelectVocabPage.xaml", UriKind.Relative));

                NavigationService.RemoveBackEntry();
                NavigationService.RemoveBackEntry();
            }
        }

        #endregion
    }
}