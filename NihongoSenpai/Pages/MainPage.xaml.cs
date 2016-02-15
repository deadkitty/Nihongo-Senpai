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
using Microsoft.Phone.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using Windows.Storage.Pickers;
using Windows.ApplicationModel.Activation;
using System.IO;
using NihongoSenpai.Model.Database;
using NihongoSenpai.Resources;

namespace NihongoSenpai.Pages
{
    public partial class MainPage : PhoneApplicationPage
    {
        #region Fields

        PhotoChooserTask photoChooser;

        bool updateDatabase = false;
        bool exportDatabase = false;

        #endregion

        #region Constructor

        public MainPage()
            : base()
        {
            InitializeComponent();

            if (AppSettings.BackgroundImage != null)
            {
                LoadBackgroundImage();
            }
        }

        #endregion
        
        #region Events

        #region General

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            AppSettings.SaveSettings();
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            AppSettings.SaveSettings();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            App app = App.Current as App;

            if (app.FileOpenPickerContinuationArgs != null && updateDatabase)
            {
                this.ContinueFileOpenPicker(app.FileOpenPickerContinuationArgs);
            }
            if (app.FileSavePickerContinuationArgs != null && exportDatabase)
            {
                this.ContinueFileSavePicker(app.FileSavePickerContinuationArgs);
            }
        }

        #endregion

        #region Exercises
        
        private void vocabButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/SelectVocabPage.xaml", UriKind.Relative));
        }

        private void insertButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/SelectInsertPage.xaml", UriKind.Relative));
        }

        private void conjugationButton_Click(object sender, RoutedEventArgs e)
        {
            //select page überarbeiten, adjektive, nomen etc. hinzufügen
            NavigationService.Navigate(new Uri("/Pages/SelectConjugationPage.xaml", UriKind.Relative));
        }

        private void combineButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/SelectCombinePage.xaml", UriKind.Relative));
        }

        private void flashcardsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/SelectFlashcardsPage.xaml", UriKind.Relative));
        }
        
        #endregion

        #region Explanations
        
        private void showWordsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/SelectLessonPage.xaml?pageType=vocab", UriKind.Relative));
        }

        private void showKanjiButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/SelectLessonPage.xaml?pageType=kanji", UriKind.Relative));
        }

        private void showGrammarButton_Click(object sender, RoutedEventArgs e)
        {
            //not realy implemented yet
            NavigationService.Navigate(new Uri("/Pages/ShowGrammarPage.xaml", UriKind.Relative));
        }

        private void searchWordsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/SearchWordsPage.xaml", UriKind.Relative));
        }        

        private void marketplaceButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/MarketplacePage.xaml", UriKind.Relative));
        }

        #endregion

        #region Others

        #region Add Content/Import/Export
        
        private void addContentButton_Click(object sender, RoutedEventArgs e)
        {
            updateDatabase = true;

            FileOpenPicker picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".nya");
            picker.ContinuationData["Operation"] = "AddContent";
            picker.PickSingleFileAndContinue();
        }

        private void importButton_Click(object sender, RoutedEventArgs e)
        {
            updateDatabase = true;

            FileOpenPicker picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".poyo");
            picker.ContinuationData["Operation"] = "UpdateDatabase";
            picker.PickSingleFileAndContinue();
        }

        private void exportButton_Click(object sender, RoutedEventArgs e)
        {
            exportDatabase = true;

            FileSavePicker picker = new FileSavePicker();
            picker.ContinuationData["Operation"] = "ExportDatabase";
            picker.FileTypeChoices.Add("TextFile", new List<string>() { ".poyo" });

            String time = DateTime.Now.ToString();
            time = time.Replace(' ', '_');
            time = time.Replace(":", "");
            time = time.Replace(".", "");

            String fileName = "SenpaiExport_" + time;

            picker.SuggestedFileName = fileName;
            picker.PickSaveFileAndContinue();
        }

        public async void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {
            //Import data
            if ((args.ContinuationData["Operation"] as string) == "UpdateDatabase" &&
                 args.Files != null &&
                 args.Files.Count > 0)
            {
                StorageFile file = args.Files[0];

                if (file.Name.EndsWith("poyo"))
                {
                    IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);

                    DataManager.ConnectToDatabase();

                    List<Lesson> lessons;

                    int kanjiRound;
                    int vocabRound;

                    String importStatus = DataManager.ImportFromFile(fileStream.AsStream(), out lessons, out vocabRound, out kanjiRound);

                    if(MessageBox.Show(importStatus, "Import", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        String updateStatus = DataManager.UpdateDatabase(lessons);

                        AppSettings.VocabRound = vocabRound;
                        AppSettings.KanjiRound = kanjiRound;

                        AppSettings.SaveSettings();

                        MessageBox.Show(updateStatus);
                    }
                    else
                    {
                        MessageBox.Show("Import Abgebrochen");
                    }
                    DataManager.CloseConnection();

                    fileStream.Dispose();
                }
            }
            //Add New Content
            else if ((args.ContinuationData["Operation"] as string) == "AddContent" &&
                 args.Files != null &&
                 args.Files.Count > 0)
            {
                StorageFile file = args.Files[0];

                if (file.Name.EndsWith("nya"))
                {
                    IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);

                    DataManager.ConnectToDatabase();
                    String updateStatus = DataManager.AddContentFromFile(fileStream.AsStream());
                    DataManager.CloseConnection();

                    fileStream.Dispose();

                    MessageBox.Show(updateStatus);
                }
            }

            updateDatabase = false;
        }

        public async void ContinueFileSavePicker(FileSavePickerContinuationEventArgs args)
        {
            if ((args.ContinuationData["Operation"] as string) == "ExportDatabase" && args.File != null)
            {
                StorageFile file = args.File;

                IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.ReadWrite);

                DataManager.ConnectToDatabase();
                String exportStatus = DataManager.ExportToFile(fileStream.AsStream());
                DataManager.CloseConnection();

                fileStream.Dispose();

                MessageBox.Show(exportStatus);
            }
            else
            {
                MessageBox.Show("Export fehlgeschlagen!");
            }

            exportDatabase = false;
        }
        
        #endregion

        #region Reset Database

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(AppResources.Really, AppResources.ResetAll, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                DataManager.ConnectToDatabase();
                DataManager.ResetDatabase();
                DataManager.CloseConnection();
                                
                AppSettings.TimeStamp = 0;
                AppSettings.VocabRound = 0;
                AppSettings.KanjiRound = 0;
                AppSettings.SaveSettings();

                MessageBox.Show(AppResources.DatabaseReseted);
            }
        }
        
        #endregion

        #region Change Background
        
        private void selectBGButton_Click(object sender, RoutedEventArgs e)
        {
            photoChooser = new PhotoChooserTask();
            photoChooser.Completed += new EventHandler<PhotoResult>(photoChooser_Completed);
            photoChooser.Show();
        }

        void photoChooser_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                String filename = "bg.png";

                using (IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (isoStorage.FileExists(filename))
                    {
                        isoStorage.DeleteFile(filename);
                    }

                    IsolatedStorageFileStream fs = isoStorage.CreateFile(filename);
                    BitmapImage bmp = new BitmapImage();
                    bmp.SetSource(e.ChosenPhoto);
                    WriteableBitmap wBmp = new WriteableBitmap(bmp);
                    wBmp.SaveJpeg(fs, wBmp.PixelWidth, wBmp.PixelHeight, 0, 100);
                    fs.Close();
                }

                AppSettings.BackgroundImage = filename;
                LoadBackgroundImage();
            }
        }

        private void LoadBackgroundImage()
        {
            using (IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                BitmapImage bmp = new BitmapImage();
                IsolatedStorageFileStream fs = isoStorage.OpenFile(AppSettings.BackgroundImage, System.IO.FileMode.Open);
                bmp.SetSource(fs);

                ImageBrush brush = new ImageBrush();
                brush.ImageSource = bmp;
                brush.Stretch = Stretch.UniformToFill;
                MenuRoot.Background = brush;

                fs.Close();
            }
        }
        
        #endregion

        #endregion

        #endregion
    }
}