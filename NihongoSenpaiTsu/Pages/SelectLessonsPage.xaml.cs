using NihongoSenpaiTsu.Model;
using NihongoSenpaiTsu.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace NihongoSenpaiTsu.Pages
{
    public sealed partial class SelectLessonsPage : Page
    {
        #region Fields

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        
        private ISelectLessonPage page;

        private List<Lesson> lessons;

        #endregion

        #region Properties

        /// <summary>
        /// Ruft den <see cref="NavigationHelper"/> ab, der mit dieser <see cref="Page"/> verknüpft ist.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return navigationHelper; }
        }

        /// <summary>
        /// Ruft das Anzeigemodell für diese <see cref="Page"/> ab.
        /// Dies kann in ein stark typisiertes Anzeigemodell geändert werden.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return defaultViewModel; }
        }

        #endregion

        #region Constructor

        public SelectLessonsPage()
        {
            InitializeComponent();
            
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

            page = e.Parameter as ISelectLessonPage;
            page.Initialize(this);

            lessons = page.LoadLessons();

            foreach(Lesson lesson in lessons)
            {
                LessonsListbox.Items.Add(lesson);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);

            page.SaveSettings();

            if(e.NavigationMode == NavigationMode.Back)
            {
                page.Deinitialize();
            }
        }

        #endregion

        #endregion

        #region Select Lessons

        private void LoadLessonsButton_Click(object sender, RoutedEventArgs e)
        {
            if (LessonsListbox.SelectedItems.Count > 0)
            {
                page.LoadLessons(LessonsListbox.SelectedItems as List<Lesson>);
            }
            else
            {
                MessageBox.Show("Keine Lektion ausgewählt!");
            }
        }

        #endregion

        #region Vocab Settings
        
        private void JapGerIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            page.ChangePracticeDirection(EPracticeDirection.japGer);
        }

        private void GerIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            page.ChangePracticeDirection(EPracticeDirection.ger);
        }

        private void JapIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            page.ChangePracticeDirection(EPracticeDirection.jap);
        }

        private void NewWordsPerRoundSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            AppSettings.NewWordsPerRound = (int)(e.NewValue * 2);
            NewWordsPerRoundTextblock.Text = AppSettings.NewWordsPerRound.ToString();
        }

        #endregion

        #region Flash Settings
        
        private void NewKanjisPerRoundSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            AppSettings.NewKanjiPerRound = (int)e.NewValue;
            NewKanjisPerRoundTextblock.Text = AppSettings.NewKanjiPerRound.ToString();
        }

        #endregion

        #region AppBar

        private void SelectAllButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (object item in LessonsListbox.Items)
            {
                LessonsListbox.SelectedItems.Add(item);
            }
        }

        private void SelectNoneButton_Click(object sender, RoutedEventArgs e)
        {
            LessonsListbox.SelectedItems.Clear();
        }

        #endregion
    }
}
