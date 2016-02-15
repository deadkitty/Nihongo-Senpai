using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NihongoSenpaiTsu.Model;
using Windows.UI.Xaml.Media.Imaging;
using NihongoSenpaiTsu.Common;
using Windows.UI.Xaml.Controls;

namespace NihongoSenpaiTsu.Pages
{
    public class SelectVocabLessonPage : ISelectLessonPage
    {
        #region Fields

        SenpaiDatabase db = new SenpaiDatabase();

        private SelectLessonsPage page;

        private BitmapImage japGerIconSelectedBitmap;
        private BitmapImage japGerIconBitmap;

        private BitmapImage gerIconSelectedBitmap;
        private BitmapImage gerIconBitmap;

        private BitmapImage japIconSelectedBitmap;
        private BitmapImage japIconBitmap;

        #endregion

        #region ISelectLessonPage

        public void Initialize(SelectLessonsPage page)
        {
            this.page = page;

            page.Root.Items.Remove(page.FlashcardSettingsItem);
            page.LessonsListbox.SelectionMode = SelectionMode.Extended;
            
            InitSortOrder();
            InitTypeSelection();
            InitPracticeDirection();
            InitGeneralOptions();

            db.ConnectToDatabase();
        }

        public void Deinitialize()
        {
            db.CloseConnection();
        }

        public List<Lesson> LoadLessons()
        {
            return db.Lessons.Where(x => x.Type == (int)Lesson.EType.vocab).ToList();
        }

        public void LoadLessons(List<Lesson> lessons)
        {
            
        }

        public void ChangePracticeDirection(EPracticeDirection direction)
        {
            AppSettings.PracticeDirection = direction;
            
            SelectPracticeDirectionIcons();
        }

        public void SaveSettings()
        {
            //save practice direction
            if (page.VocabRadio1.IsChecked.Value)
            {
                AppSettings.VocabSortOrder = ESortOrder.timestamp;
            }
            else if (page.VocabRadio2.IsChecked.Value)
            {
                AppSettings.VocabSortOrder = ESortOrder.byLesson;
            }
            else if (page.VocabRadio3.IsChecked.Value)
            {
                AppSettings.VocabSortOrder = ESortOrder.random;
            }

            //save load options
            AppSettings.LoadOptions  = page.Verb1Checkbox.IsChecked.Value ? 1    : 0;
            AppSettings.LoadOptions += page.Verb2Checkbox.IsChecked.Value ? 2    : 0;
            AppSettings.LoadOptions += page.Verb3Checkbox.IsChecked.Value ? 4    : 0;
            AppSettings.LoadOptions += page.IAdjCheckbox .IsChecked.Value ? 8    : 0;
            AppSettings.LoadOptions += page.NaAdjCheckbox.IsChecked.Value ? 16   : 0;
            AppSettings.LoadOptions += page.AdvCheckbox  .IsChecked.Value ? 32   : 0;
            AppSettings.LoadOptions += page.NounCheckbox .IsChecked.Value ? 64   : 0;
            AppSettings.LoadOptions += page.PartCheckbox .IsChecked.Value ? 128  : 0;
            AppSettings.LoadOptions += page.PrevCheckbox .IsChecked.Value ? 256  : 0;
            AppSettings.LoadOptions += page.SuffCheckbox .IsChecked.Value ? 512  : 0;
            AppSettings.LoadOptions += page.PhrCheckbox  .IsChecked.Value ? 1024 : 0;
            AppSettings.LoadOptions += page.OtherCheckbox.IsChecked.Value ? 2048 : 0;

            //save other options
            AppSettings.LoadAllWords = page.LoadAllWordsCheckBox.IsChecked.Value;
            AppSettings.ShowDescription = page.ShowDescCheckBox.IsChecked.Value;
        }

        #endregion

        #region Helper

        private void InitSortOrder()
        {
            switch(AppSettings.VocabSortOrder)
            {
                case ESortOrder.timestamp: page.VocabRadio1.IsChecked = true; break;
                case ESortOrder.byLesson : page.VocabRadio2.IsChecked = true; break;
                case ESortOrder.random   : page.VocabRadio3.IsChecked = true; break;
            }
        }

        private void InitTypeSelection()
        {
            page.Verb1Checkbox.IsChecked = (AppSettings.LoadOptions & 1)    == 1;
            page.Verb2Checkbox.IsChecked = (AppSettings.LoadOptions & 2)    == 2;
            page.Verb3Checkbox.IsChecked = (AppSettings.LoadOptions & 4)    == 4;
            page.IAdjCheckbox .IsChecked = (AppSettings.LoadOptions & 8)    == 8;
            page.NaAdjCheckbox.IsChecked = (AppSettings.LoadOptions & 16)   == 16;
            page.AdvCheckbox  .IsChecked = (AppSettings.LoadOptions & 32)   == 32;
            page.NounCheckbox .IsChecked = (AppSettings.LoadOptions & 64)   == 64;
            page.PartCheckbox .IsChecked = (AppSettings.LoadOptions & 128)  == 128;
            page.PrevCheckbox .IsChecked = (AppSettings.LoadOptions & 256)  == 256;
            page.SuffCheckbox .IsChecked = (AppSettings.LoadOptions & 512)  == 512;
            page.PhrCheckbox  .IsChecked = (AppSettings.LoadOptions & 1024) == 1024;
            page.OtherCheckbox.IsChecked = (AppSettings.LoadOptions & 2048) == 2048;
        }

        private void InitPracticeDirection()
        {
            japGerIconSelectedBitmap = new BitmapImage(new Uri("ms-appx:/Assets/AppBar/japaneseGermanSelected.png", UriKind.RelativeOrAbsolute));
            japGerIconBitmap = new BitmapImage(new Uri("ms-appx:/Assets/AppBar/japaneseGerman.png", UriKind.RelativeOrAbsolute));

            gerIconSelectedBitmap = new BitmapImage(new Uri("ms-appx:/Assets/AppBar/germanSelected.png", UriKind.RelativeOrAbsolute));
            gerIconBitmap = new BitmapImage(new Uri("ms-appx:/Assets/AppBar/german.png", UriKind.RelativeOrAbsolute));

            japIconSelectedBitmap = new BitmapImage(new Uri("ms-appx:/Assets/AppBar/japaneseSelected.png", UriKind.RelativeOrAbsolute));
            japIconBitmap = new BitmapImage(new Uri("ms-appx:/Assets/AppBar/japanese.png", UriKind.RelativeOrAbsolute));

            SelectPracticeDirectionIcons();
        }

        private void InitGeneralOptions()
        {
            page.LoadAllWordsCheckBox.IsChecked = AppSettings.LoadAllWords;
            page.NewWordsPerRoundSlider.Value = AppSettings.NewWordsPerRound / 2;
            page.ShowDescCheckBox.IsChecked = AppSettings.ShowDescription;
        }

        private void SelectPracticeDirectionIcons()
        {
            switch (AppSettings.PracticeDirection)
            {
                case EPracticeDirection.japGer:

                    page.JapGerIcon.Source = japGerIconSelectedBitmap;
                    page.JapIcon.Source = japIconBitmap;
                    page.GerIcon.Source = gerIconBitmap;

                    break;

                case EPracticeDirection.ger:

                    page.JapGerIcon.Source = japGerIconBitmap;
                    page.GerIcon.Source = gerIconSelectedBitmap;
                    page.JapIcon.Source = japIconBitmap;

                    break;

                case EPracticeDirection.jap:

                    page.JapGerIcon.Source = japGerIconBitmap;
                    page.GerIcon.Source = gerIconBitmap;
                    page.JapIcon.Source = japIconSelectedBitmap;

                    break;
            }
        }

        #endregion
    }
}
