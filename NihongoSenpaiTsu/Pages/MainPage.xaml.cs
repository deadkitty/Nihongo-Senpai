using NihongoSenpaiTsu.Common;
using NihongoSenpaiTsu.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace NihongoSenpaiTsu.Pages
{
    public sealed partial class MainPage : Page
    {
        #region Fields

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        private StorageFile pickedFile = null;

        #endregion

        #region Constructor

        public MainPage()
        {
            this.InitializeComponent();

            navigationHelper = new NavigationHelper(this);
            navigationHelper.LoadState += NavigationHelper_LoadState;
            navigationHelper.SaveState += NavigationHelper_SaveState;
        }

        #endregion

        #region NavigationHelper

        /// <summary>
        /// Füllt die Seite mit Inhalt auf, der bei der Navigation übergeben wird. Gespeicherte Zustände werden ebenfalls
        /// bereitgestellt, wenn eine Seite aus einer vorherigen Sitzung neu erstellt wird.
        /// </summary>
        /// <param name="sender">
        /// Die Quelle des Ereignisses, normalerweise <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Ereignisdaten, die die Navigationsparameter bereitstellen, die an
        /// <see cref="Frame.Navigate(Type, Object)"/> als diese Seite ursprünglich angefordert wurde und
        /// ein Wörterbuch des Zustands, der von dieser Seite während einer früheren
        /// beibehalten wurde.  Der Zustand ist beim ersten Aufrufen einer Seite NULL.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {

        }

        /// <summary>
        /// Behält den dieser Seite zugeordneten Zustand bei, wenn die Anwendung angehalten oder
        /// die Seite im Navigationscache verworfen wird. Die Werte müssen den Serialisierungsanforderungen
        /// von <see cref="SuspensionManager.SessionState"/> entsprechen.
        /// </summary>
        /// <param name="sender">Die Quelle des Ereignisses, normalerweise <see cref="NavigationHelper"/></param>
        /// <param name="e">Ereignisdaten, die ein leeres Wörterbuch zum Auffüllen bereitstellen
        /// serialisierbarer Zustand.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {

        }

        #region NavigationHelper-Registrierung

        /// <summary>
        /// Die in diesem Abschnitt bereitgestellten Methoden werden einfach verwendet,
        /// damit NavigationHelper auf die Navigationsmethoden der Seite reagieren kann.
        /// <para>
        /// Platzieren Sie seitenspezifische Logik in Ereignishandlern für  
        /// <see cref="NavigationHelper.LoadState"/>
        /// und <see cref="NavigationHelper.SaveState"/>.
        /// Der Navigationsparameter ist in der LoadState-Methode zusätzlich 
        /// zum Seitenzustand verfügbar, der während einer früheren Sitzung gesichert wurde.
        /// </para>
        /// </summary>
        /// <param name="e">Stellt Daten für Navigationsmethoden und -ereignisse bereit.
        /// Handler, bei denen die Navigationsanforderung nicht abgebrochen werden kann.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        #endregion
        
        #region Exercises

        private void VocabButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SelectLessonsPage), new SelectVocabLessonPage());
        }

        private void KanjiButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SelectLessonsPage), new SelectFlashLessonPage());
        }

        private void GrammarButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime origin = new DateTime(2016, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = DateTime.Now - origin;

            MessageBox.Show("Hours Since 2016: " + Math.Floor(diff.TotalHours));
        }

        private void CombineButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ConjugationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Explanations

        private void ShowVocabButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowKanjiButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowGrammarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchWordsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Others

        private void AddContentButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".nya");
            //picker.ContinuationData["Operation"] = "AddContent";
            picker.PickSingleFileAndContinue();
        }

        private void RestoreDbButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveDbButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResetDbButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeBgButton_Click(object sender, RoutedEventArgs e)
        {

        }


        public async void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {
            pickedFile = args.Files[0];

            IRandomAccessStream fileStream = await pickedFile.OpenAsync(FileAccessMode.Read);

            DataManager.ConnectToDatabase();

            List<Lesson> lessons;

            String importStatus = DataManager.ImportFromFile(fileStream.AsStream(), out lessons);

            MessageBox.Show(importStatus, "Import", MessageBox.EMessageBoxButtons.OkCancel, ImportLesson);

            DataManager.CloseConnection();
        }

        public async void ImportLesson(MessageBox.EMessageBoxResult result)
        {
            if(result == MessageBox.EMessageBoxResult.ok)
            {
                IRandomAccessStream fileStream = await pickedFile.OpenAsync(FileAccessMode.Read);

                DataManager.ConnectToDatabase();
                String updateStatus = DataManager.AddContentFromFile(fileStream.AsStream());
                DataManager.CloseConnection();

                fileStream.Dispose();

                MessageBox.Show(updateStatus); 
            }
            else
            {
                MessageBox.Show("Import Abgebrochen");
            }

            pickedFile = null;
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
        }

        #endregion
    }
}
