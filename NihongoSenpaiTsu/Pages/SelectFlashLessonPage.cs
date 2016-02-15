using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NihongoSenpaiTsu.Model;
using NihongoSenpaiTsu.Common;

namespace NihongoSenpaiTsu.Pages
{
    public class SelectFlashLessonPage : ISelectLessonPage
    {
        #region Fields

        private SelectLessonsPage page;

        #endregion

        #region ISelectLessonPage

        public void Initialize(SelectLessonsPage page)
        {
            this.page = page;

            page.Root.Items.Remove(page.VocabSettingsItem);
            
            //Load Settings
            switch (AppSettings.FlashSortOrder)
            {
                case ESortOrder.timestamp: page.FlashRadio1.IsChecked = true; break;
                case ESortOrder.byLesson : page.FlashRadio2.IsChecked = true; break;
                case ESortOrder.random   : page.FlashRadio3.IsChecked = true; break;
            }

            page.NewKanjisPerRoundSlider.Value = AppSettings.NewKanjiPerRound;
            page.LoadAllKanjisCheckBox.IsChecked = AppSettings.LoadAllKanji;
        }

        public List<Lesson> LoadLessons()
        {
            return new List<Lesson>();
        }

        public void LoadLessons(List<Lesson> lessons)
        {

        }

        public void ChangePracticeDirection(EPracticeDirection direction)
        {
            throw new NotImplementedException();
        }

        public void SaveSettings()
        {
            //save practice direction
            if (page.FlashRadio1.IsChecked.Value)
            {
                AppSettings.FlashSortOrder = ESortOrder.timestamp;
            }
            else if (page.FlashRadio2.IsChecked.Value)
            {
                AppSettings.FlashSortOrder = ESortOrder.byLesson;
            }
            else if (page.FlashRadio3.IsChecked.Value)
            {
                AppSettings.FlashSortOrder = ESortOrder.random;
            }

            AppSettings.LoadAllKanji = page.LoadAllKanjisCheckBox.IsChecked.Value;
        }

        public void Deinitialize()
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}
