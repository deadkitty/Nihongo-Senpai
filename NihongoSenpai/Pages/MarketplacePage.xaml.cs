using System;
using System.Net;
using Microsoft.Phone.Controls;
using NihongoSenpai.Data.Database;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using NihongoSenpai.Settings;

namespace NihongoSenpai.Pages
{
    public partial class MarketplacePage : PhoneApplicationPage
    {
        #region Fields

        private List<Lesson> localLessons;
        private List<Lesson> marketplaceLessons;

        private Lesson lastLessonToDownload;

        public const String marketplaceUrl = "http://senpai-marketplace-test.azurewebsites.net/";

        #endregion

        #region Constructor

        public MarketplacePage()
        {
            InitializeComponent();
        }

        #endregion

        #region Page Load/Unload

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            DataManager.ConnectToDatabase();

            localLessons = new List<Lesson>(DataManager.Database.lessons);
            
            FillLocalLessonsListbox(localLessons);

            marketplaceLessons = new List<Lesson>();

            DownloadLessons();

            typeListPicker.SelectionChanged += typeListPicker_SelectionChanged;
            typeListPicker2.SelectionChanged += typeListPicker2_SelectionChanged;
        }

        private void PhoneApplicationPage_Unloaded(object sender, RoutedEventArgs e)
        {
            DataManager.CloseConnection();
        }

        #endregion

        #region Local Lessons Section

        private void typeListPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String searchText = searchTextbox.Text.ToLower();

            List<Lesson> matchedLessons = localLessons.FindAll(x => x.name.ToLower().Contains(searchText));

            switch (typeListPicker.SelectedIndex)
            {
                case 0: FillLocalLessonsListbox(matchedLessons); break;
                case 1: FillLocalLessonsListbox(matchedLessons.FindAll(x => x.type == (int)Lesson.EType.vocab )); break;
                case 2: FillLocalLessonsListbox(matchedLessons.FindAll(x => x.type == (int)Lesson.EType.kanji )); break;
                case 3: FillLocalLessonsListbox(matchedLessons.FindAll(x => x.type == (int)Lesson.EType.insert)); break;
            }
        }
        
        private void searchTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                typeListPicker_SelectionChanged(sender, null);
            }
        }

        private void deleteLessons_Click(object sender, RoutedEventArgs e)
        {
            if (localLessonsListbox.SelectedItems.Count == 0)
                return;

            if (MessageBox.Show("Wirklich? * 。*", "Lektionen Löschen", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                foreach (Lesson lesson in localLessonsListbox.SelectedItems)
                {
                    DataManager.DeleteLesson(lesson);
                }

                DataManager.SaveChanges();
            }

            localLessons = new List<Lesson>(DataManager.Database.lessons);

            typeListPicker_SelectionChanged(sender, null);
            typeListPicker2_SelectionChanged(sender, null);
        }

        #endregion

        #region Marketplace Lessons Section

        private void typeListPicker2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String searchText = searchTextbox2.Text.ToLower();

            List<Lesson> matchedLessons = marketplaceLessons.FindAll(x => x.name.ToLower().Contains(searchText));

            switch (typeListPicker2.SelectedIndex)
            {
                case 0: FillMarketplaceLessonsListbox(matchedLessons); break;
                case 1: FillMarketplaceLessonsListbox(matchedLessons.FindAll(x => x.type == (int)Lesson.EType.vocab)); break;
                case 2: FillMarketplaceLessonsListbox(matchedLessons.FindAll(x => x.type == (int)Lesson.EType.kanji)); break;
                case 3: FillMarketplaceLessonsListbox(matchedLessons.FindAll(x => x.type == (int)Lesson.EType.insert)); break;
            }
        }

        private void searchTextbox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                typeListPicker2_SelectionChanged(sender, null);
            }
        }
        
        private void downloadButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(Lesson lesson in marketplaceLessonsListbox.SelectedItems)
            {
                DownloadLesson(lesson);
            }

            lastLessonToDownload = marketplaceLessonsListbox.SelectedItems[marketplaceLessonsListbox.SelectedItems.Count - 1] as Lesson;
        }

        #endregion

        #region Helper

        private void FillLocalLessonsListbox(List<Lesson> lessons)
        {
            localLessonsListbox.Items.Clear();

            foreach (Lesson lesson in lessons)
            {
                localLessonsListbox.Items.Add(lesson);
            }
        }

        private void FillMarketplaceLessonsListbox(List<Lesson> lessons)
        {
            marketplaceLessonsListbox.Items.Clear();

            foreach (Lesson lesson in lessons)
            {
                bool addItem = true;

                foreach(Lesson local in localLessons)
                {
                    if(lesson.id == local.id)
                    {
                        addItem = false;
                        break;
                    }
                }

                if (addItem)
                {
                    marketplaceLessonsListbox.Items.Add(lesson);
                }
            }
        }

        #endregion

        #region Web

        private void DownloadLessons()
        {
            try
            {
                Uri marketplaceUri = new Uri(marketplaceUrl + "api/Lessons");

                WebClient client = new WebClient();

                client.OpenReadCompleted += new OpenReadCompletedEventHandler(lessonsDownload_OpenReadCompleted);
                client.OpenReadAsync(marketplaceUri);
            }
            catch (Exception)
            {
                MessageBox.Show("Some Error Occured");
            }
        }
        
        private void DownloadLesson(Lesson lesson)
        {
            try
            {
                Uri marketplaceUri = new Uri(marketplaceUrl + "api/Lessons/" + lesson.id);

                WebClient client = new WebClient();

                client.OpenReadCompleted += new OpenReadCompletedEventHandler(lessonDownload_OpenReadCompleted);
                client.OpenReadAsync(marketplaceUri);
            }
            catch (Exception)
            {
                MessageBox.Show("Some Error Occured");
            }
        }

        private void lessonsDownload_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Stream responseStream = e.Result;

                using (StreamReader rw = new StreamReader(responseStream))
                {
                    marketplaceLessons = JsonConvert.DeserializeObject<List<Lesson>>(rw.ReadToEnd());
                }

                FillMarketplaceLessonsListbox(marketplaceLessons);
            }
        }

        private void lessonDownload_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Stream responseStream = e.Result;
                
                using (StreamReader rw = new StreamReader(responseStream))
                {
                    Lesson downloadedLesson = JsonConvert.DeserializeObject<Lesson>(rw.ReadToEnd());

                    foreach (Word word in downloadedLesson.Words)
                    {
                        word.lastRoundJapanese = AppSettings.VocabRound;
                        word.nextRoundJapanese = AppSettings.VocabRound;
                        word.lastRoundTranslation = AppSettings.VocabRound;
                        word.nextRoundTranslation = AppSettings.VocabRound;

                        word.Lesson = downloadedLesson;
                    }

                    foreach (Kanji kanji in downloadedLesson.Kanjis)
                    {
                        kanji.lastRound = AppSettings.KanjiRound;
                        kanji.nextRound = AppSettings.KanjiRound;

                        kanji.Lesson = downloadedLesson;
                    }

                    foreach (Cloze cloze in downloadedLesson.Clozes)
                    {
                        cloze.Lesson = downloadedLesson;
                    }

                    DataManager.Database.lessons.InsertOnSubmit(downloadedLesson);

                    switch (downloadedLesson.Type)
                    {
                        case Lesson.EType.vocab: DataManager.Database.words.InsertAllOnSubmit(downloadedLesson.Words); break;
                        case Lesson.EType.kanji: DataManager.Database.kanjis.InsertAllOnSubmit(downloadedLesson.Kanjis); break;
                        case Lesson.EType.insert: DataManager.Database.clozes.InsertAllOnSubmit(downloadedLesson.Clozes); break;
                    }

                    if(lastLessonToDownload.id == downloadedLesson.id)
                    {
                        DataManager.SaveChanges();

                        localLessons = new List<Lesson>(DataManager.Database.lessons);

                        typeListPicker_SelectionChanged(sender, null);
                        typeListPicker2_SelectionChanged(sender, null);

                        pivot.SelectedIndex = 0;
                    }
                }
            }
        }

        #endregion

        #region ApplicationBar

        private void selectAll_Click(object sender, EventArgs e)
        {
            if(pivot.SelectedIndex == 0)
            {
                foreach (object item in localLessonsListbox.Items)
                {
                    localLessonsListbox.SelectedItems.Add(item);
                }
            }
            else
            {
                foreach (object item in marketplaceLessonsListbox.Items)
                {
                    marketplaceLessonsListbox.SelectedItems.Add(item);
                }
            }
        }

        private void selectNone_Click(object sender, EventArgs e)
        {
            if (pivot.SelectedIndex == 0)
            {
                localLessonsListbox.SelectedItems.Clear();
            }
            else
            {
                marketplaceLessonsListbox.SelectedItems.Clear();
            }
        }

        #endregion
    }
}