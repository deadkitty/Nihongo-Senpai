using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using NihongoSenpai.Settings;
using NihongoSenpai.Resources;
using NihongoSenpai.Data.Database;
using NihongoSenpai.Controller;
using NihongoSenpai.Data;

namespace NihongoSenpai.Pages
{
    public partial class SelectVocabPage : PhoneApplicationPage
    {
        #region Fields
                
        private BitmapImage japGerIconSelectedBitmap;
        private BitmapImage japGerIconBitmap;

        private BitmapImage gerIconSelectedBitmap;
        private BitmapImage gerIconBitmap;

        private BitmapImage japIconSelectedBitmap;
        private BitmapImage japIconBitmap;

        #endregion

        #region Constructor
        
        public SelectVocabPage()
        {
            InitializeComponent();

            japGerIconSelectedBitmap = new BitmapImage(new Uri("/Assets/AppBar/japaneseGermanSelected.png", UriKind.Relative));
            japGerIconBitmap         = new BitmapImage(new Uri("/Assets/AppBar/japaneseGerman.png", UriKind.Relative));
            
            gerIconSelectedBitmap = new BitmapImage(new Uri("/Assets/AppBar/germanSelected.png", UriKind.Relative));
            gerIconBitmap         = new BitmapImage(new Uri("/Assets/AppBar/german.png", UriKind.Relative));

            japIconSelectedBitmap = new BitmapImage(new Uri("/Assets/AppBar/japaneseSelected.png", UriKind.Relative));
            japIconBitmap         = new BitmapImage(new Uri("/Assets/AppBar/japanese.png", UriKind.Relative));

            SelectPracticeDirectionIcons();

            SetTypeSelections();
            SetSortOrder();

            SetGerneralOptions();
        }

        #endregion
        
        #region Events

        #region Back, NavigatedFrom/To

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            //if i go back to main page, close the database connection
            if (e.NavigationMode == NavigationMode.Back)
            {
                DataManager.CloseConnection();

                GetSortOrder();
                GetTypeSelections();
                GetGerneralOptions();
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
                
                IQueryable<Lesson> lessons = DataManager.GetLessons(Lesson.EType.vocab);
                       
                foreach (Lesson lesson in lessons)
                {
                    setsListbox.Items.Add(lesson);
                }
            }
        }

        #endregion
        
        #region Load Lessons

        private void loadLessonsButton_Click(object sender, RoutedEventArgs e)
        {
            if (setsListbox.SelectedItems.Count > 0)
            {
                GetTypeSelections();
                GetSortOrder();
                GetGerneralOptions();

                List<Lesson> selectedLessons = new List<Lesson>();
                
                foreach(Lesson lesson in setsListbox.SelectedItems)
                {
                    selectedLessons.Add(lesson);
                }
                
                VocabController.LoadLessons(selectedLessons);

                if (VocabData.ActiveItems.Count == 0)
                {
                    VocabController.EndPractice();
                    VocabController.Deinitialize();
                    
                    MessageBox.Show(AppResources.NoWordsLeft);

                    return;
                }

                NavigationService.Navigate(new Uri("/Pages/PracticeVocabPage.xaml", UriKind.Relative));

                setsListbox.SelectedItems.Clear();
            }
            else
            {
                MessageBox.Show(AppResources.NoLessonSelected);
            }
        }
        
        #endregion

        #region Load Options

        #region General Options

        #region Sliders
        
        private void newWordsPerRoundSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetRoundSizeText((int)e.NewValue * 2);
        }

        private void SetRoundSizeText(int value)
        {
            newWordsPerRoundTextblock.Text = AppResources.NewWordsPerRound + value;
        }

        #endregion

        #region Set/Get Options

        private void SetGerneralOptions()
        {
            loadAllWordsCheckBox.IsChecked = AppSettings.LoadAllWords;
            showDescCheckBox    .IsChecked = AppSettings.ShowDescription;

            newWordsPerRoundSlider.Value = AppSettings.NewWordsPerRound / 2;

            SetRoundSizeText(AppSettings.NewWordsPerRound);
        }

        private void GetGerneralOptions()
        {
            AppSettings.LoadAllWords     = loadAllWordsCheckBox.IsChecked.Value;
            AppSettings.ShowDescription  = showDescCheckBox    .IsChecked.Value;

            AppSettings.NewWordsPerRound = (int)newWordsPerRoundSlider.Value * 2;
        }
        
        #endregion

        #endregion

        #region Practice Direction

        private void SelectPracticeDirectionIcons()
        {
            switch (AppSettings.VocabPracticeDirection)
            {
                case EPracticeDirection.japGer:

                    japGerIcon.Source = japGerIconSelectedBitmap;
                    japIcon.Source = japIconBitmap;
                    gerIcon.Source = gerIconBitmap;

                    break;

                case EPracticeDirection.ger:

                    japGerIcon.Source = japGerIconBitmap;
                    gerIcon.Source = gerIconSelectedBitmap;
                    japIcon.Source = japIconBitmap;

                    break;

                case EPracticeDirection.jap:

                    japGerIcon.Source = japGerIconBitmap;
                    gerIcon.Source = gerIconBitmap;
                    japIcon.Source = japIconSelectedBitmap;

                    break;
            }
        }

        private void japGerIcon_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            AppSettings.VocabPracticeDirection = EPracticeDirection.japGer;
            SelectPracticeDirectionIcons();
        }

        private void gerIcon_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            AppSettings.VocabPracticeDirection = EPracticeDirection.ger;
            SelectPracticeDirectionIcons();
        }

        private void japIcon_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            AppSettings.VocabPracticeDirection = EPracticeDirection.jap;
            SelectPracticeDirectionIcons();
        }
        
        #endregion

        #region Type Selection

        private void SetTypeSelections()
        {
            int loadOptions = AppSettings.LoadOptions;

            otherCheckbox.IsChecked = loadOptions % 2 == 1 ? true : false;
            loadOptions >>= 1;
            phrCheckbox.IsChecked = loadOptions % 2 == 1 ? true : false;
            loadOptions >>= 1;
            suffCheckbox.IsChecked = loadOptions % 2 == 1 ? true : false;
            loadOptions >>= 1;
            prevCheckbox.IsChecked = loadOptions % 2 == 1 ? true : false;
            loadOptions >>= 1;
            partCheckbox.IsChecked = loadOptions % 2 == 1 ? true : false;
            loadOptions >>= 1;
            nounCheckbox.IsChecked = loadOptions % 2 == 1 ? true : false;
            loadOptions >>= 1;
            advCheckbox.IsChecked = loadOptions % 2 == 1 ? true : false;
            loadOptions >>= 1;
            naAdjCheckbox.IsChecked = loadOptions % 2 == 1 ? true : false;
            loadOptions >>= 1;
            iAdjCheckbox.IsChecked = loadOptions % 2 == 1 ? true : false;
            loadOptions >>= 1;
            verb3Checkbox.IsChecked = loadOptions % 2 == 1 ? true : false;
            loadOptions >>= 1;
            verb2Checkbox.IsChecked = loadOptions % 2 == 1 ? true : false;
            loadOptions >>= 1;
            verb1Checkbox.IsChecked = loadOptions % 2 == 1 ? true : false;
        }

        private void GetTypeSelections()
        {
            int loadOptions = 0;

            loadOptions += verb1Checkbox.IsChecked == true ? 1 : 0;
            loadOptions <<= 1;
            loadOptions += verb2Checkbox.IsChecked == true ? 1 : 0;
            loadOptions <<= 1;
            loadOptions += verb3Checkbox.IsChecked == true ? 1 : 0;
            loadOptions <<= 1;
            loadOptions += iAdjCheckbox.IsChecked == true ? 1 : 0;
            loadOptions <<= 1;
            loadOptions += naAdjCheckbox.IsChecked == true ? 1 : 0;
            loadOptions <<= 1;
            loadOptions += advCheckbox.IsChecked == true ? 1 : 0;
            loadOptions <<= 1;
            loadOptions += nounCheckbox.IsChecked == true ? 1 : 0;
            loadOptions <<= 1;
            loadOptions += partCheckbox.IsChecked == true ? 1 : 0;
            loadOptions <<= 1;
            loadOptions += prevCheckbox.IsChecked == true ? 1 : 0;
            loadOptions <<= 1;
            loadOptions += suffCheckbox.IsChecked == true ? 1 : 0;
            loadOptions <<= 1;
            loadOptions += phrCheckbox.IsChecked == true ? 1 : 0;
            loadOptions <<= 1;
            loadOptions += otherCheckbox.IsChecked == true ? 1 : 0;

            AppSettings.LoadOptions = loadOptions;
        }
        
        #endregion

        #region Sort Order

        private void SetSortOrder()
        {
            switch (AppSettings.SortOrder)
            {
                case ESortOrder.timestamp: radio1.IsChecked = true; break;
                case ESortOrder.byLesson : radio2.IsChecked = true; break;
                case ESortOrder.random   : radio3.IsChecked = true; break;
            }
        }

        private void GetSortOrder()
        {
            if (radio1.IsChecked.Value)
            {
                AppSettings.SortOrder = ESortOrder.timestamp;
            }
            else if (radio2.IsChecked.Value)
            {
                AppSettings.SortOrder = ESortOrder.byLesson;
            }
            else if (radio3.IsChecked.Value)
            {
                AppSettings.SortOrder = ESortOrder.random;
            }
        }
        
        #endregion

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

        #endregion
    }
}