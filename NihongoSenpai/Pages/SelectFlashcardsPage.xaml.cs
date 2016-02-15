using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NihongoSenpai.Settings;
using NihongoSenpai.Resources;
using NihongoSenpai.Model.Database;
using NihongoSenpai.Controller;
using NihongoSenpai.Model;

namespace NihongoSenpai.Pages
{
    public partial class SelectFlashcardsPage : PhoneApplicationPage
    {
        #region Constructor

        public SelectFlashcardsPage()
        {
            InitializeComponent();

            LoadOptions();

            SetRoundSizeText(AppSettings.NewKanjiPerRound);
        }

        #endregion
                
        #region Back, NavigatedFrom/To

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            //if i go back to main page, close the database connection
            if (e.NavigationMode == NavigationMode.Back)
            {
                DataManager.CloseConnection();
            
                SaveOptions();
            }
            
            AppSettings.SaveSettings();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            //if i come from main Page open database connection and initialize lessons
            if(e.NavigationMode == NavigationMode.New)
            {
                DataManager.ConnectToDatabase();

                IQueryable<Lesson> lessons = DataManager.GetLessons(Lesson.EType.kanji);

                foreach (Lesson l in lessons)
                {
                    setsListbox.Items.Add(l);
                }
            }
        }

        #endregion
        
        #region Load Sets

        private void loadLessonsButton_Click(object sender, RoutedEventArgs e)
        {
            if (setsListbox.SelectedItems.Count > 0)
            {
                List<Lesson> selectedLessons = new List<Lesson>();
                
                foreach(Lesson lesson in setsListbox.SelectedItems)
                {
                    selectedLessons.Add(lesson);
                }

                SaveOptions();

                FlashcardsController.LoadLessons(selectedLessons);

                if(FlashcardsData.ActiveKanjis.Count == 0)
                {
                    MessageBox.Show(AppResources.NoKanjisLeft);
                    FlashcardsController.Deinitialize();
                }
                else
                {
                    NavigationService.Navigate(new Uri("/Pages/PracticeFlashcardsPage.xaml", UriKind.Relative));
                }

                setsListbox.SelectedItems.Clear();
            }
            else
            {
                MessageBox.Show("Keine Lektion ausgewählt!");
            }
        }

        #endregion

        #region ApplicationBar

        private void selectAll_Click(object sender, EventArgs e)
        {
            foreach(object item in setsListbox.Items)
            {
                setsListbox.SelectedItems.Add(item);
            }
        }

        private void selectNone_Click(object sender, EventArgs e)
        {
            setsListbox.SelectedItems.Clear();
        }

        #endregion

        #region Settings

        private void newKanjiPerRoundSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetRoundSizeText((int)e.NewValue);
        }

        private void SetRoundSizeText(int value)
        {
            newKanjiPerRoundTextblock.Text = AppResources.NewWordsPerRound + value;
        }

        private void LoadOptions()
        {
            newKanjiPerRoundSlider.Value = AppSettings.NewKanjiPerRound;
            loadAllKanjiCheckBox.IsChecked = AppSettings.LoadAllKanji;

            switch (AppSettings.SortOrderFlash)
            {
                case ESortOrder.timestamp: radio1.IsChecked = true; break;
                case ESortOrder.byLesson: radio2.IsChecked = true; break;
                case ESortOrder.random: radio3.IsChecked = true; break;
            }
        }

        private void SaveOptions()
        {
            AppSettings.NewKanjiPerRound = (int)newKanjiPerRoundSlider.Value;
            AppSettings.LoadAllKanji = loadAllKanjiCheckBox.IsChecked.Value;

            if (radio1.IsChecked.Value)
            {
                AppSettings.SortOrderFlash = ESortOrder.timestamp;
            }
            else if (radio2.IsChecked.Value)
            {
                AppSettings.SortOrderFlash = ESortOrder.byLesson;
            }
            else if (radio3.IsChecked.Value)
            {
                AppSettings.SortOrderFlash = ESortOrder.random;
            }
        }

        #endregion
    }
}