using NihongoSenpaiTsu.Common;
using NihongoSenpaiTsu.Model;
using NihongoSenpaiTsu.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace NihongoSenpaiTsu
{
    /// <summary>
    /// Stellt das anwendungsspezifische Verhalten bereit, um die Standardanwendungsklasse zu ergänzen.
    /// </summary>
    public sealed partial class App : Application
    {
        private TransitionCollection transitions;

        /// <summary>
        /// Initialisiert das Singletonanwendungsobjekt. Dies ist die erste Zeile von erstelltem Code
        /// und daher das logische Äquivalent von main() bzw. WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;

            AppSettings.Initialize();

            if(AppSettings.FirstStart)
            {
                SenpaiDatabase database = new SenpaiDatabase();

                database.ConnectToDatabase();
                database.CreateDatabase();
                database.CloseConnection();


                //List<Lesson> lessons = new List<Lesson>();

                //lessons.Add(new Lesson("1|Minna No Nihongo L 1|0|32"));
                //lessons.Add(new Lesson("2|Minna No Nihongo L 2|0|44"));
                //lessons.Add(new Lesson("3|Minna No Nihongo L 3|0|39"));
                //lessons.Add(new Lesson("4|Minna No Nihongo L 4|0|50"));
                //lessons.Add(new Lesson("51|Minna no Nihongo Fukushuu J Test|1|3"));
                //lessons.Add(new Lesson("53|Genki L3 Kanji|3|15"));
                //lessons.Add(new Lesson("54|Genki L4 Kanji|3|14"));
                //lessons.Add(new Lesson("55|Genki L5 Kanji|3|14"));
                //lessons.Add(new Lesson("56|Genki L6 Kanji|3|15"));
                //lessons.Add(new Lesson("75|Tobira L1|0|109"));
                //lessons.Add(new Lesson("76|Tobira L1 Kanjis|3|37"));
                //lessons.Add(new Lesson("77|Tobira L2|0|73"));
                //lessons.Add(new Lesson("78|Tobira L2 Kanjis|3|34"));
                //lessons.Add(new Lesson("79|Tobira L3|0|84"));
                //lessons.Add(new Lesson("80|Tobira L3 Kanjis|3|36"));

            }

            SenpaiDatabase db = new SenpaiDatabase();

            db.ConnectToDatabase();
            db.DeleteDatabase();
            db.CreateDatabase();
            db.CloseConnection();
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Anwendung durch den Endbenutzer normal gestartet wird. Weitere Einstiegspunkte
        /// werden verwendet, wenn die Anwendung zum Öffnen einer bestimmten Datei, zum Anzeigen
        /// von Suchergebnissen usw. gestartet wird.
        /// </summary>
        /// <param name="e">Details über Startanforderung und -prozess.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // App-Initialisierung nicht wiederholen, wenn das Fenster bereits Inhalte enthält.
            // Nur sicherstellen, dass das Fenster aktiv ist.
            if (rootFrame == null)
            {
                // Frame erstellen, der als Navigationskontext fungiert und zum Parameter der ersten Seite navigieren.
                rootFrame = new Frame();

                // Verknüpfen Sie den Frame mit einem SuspensionManager-Schlüssel.
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

                // TODO: diesen Wert auf eine Cachegröße ändern, die für Ihre Anwendung geeignet ist
                rootFrame.CacheSize = 1;

                // Standardsprache festlegen
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Den gespeicherten Sitzungszustand nur bei Bedarf wiederherstellen.
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        // Fehler beim Wiederherstellen des Zustands.
                        // Annehmen, dass kein Zustand vorhanden ist und Vorgang fortsetzen.
                    }
                }

                // Den Frame im aktuellen Fenster platzieren.
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // Entfernt die Drehkreuznavigation für den Start.
                if (rootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;

                // Wenn der Navigationsstapel nicht wiederhergestellt wird, zur ersten Seite navigieren
                // und die neue Seite konfigurieren, indem die erforderlichen Informationen als Navigationsparameter
                // Parameter.
                if (!rootFrame.Navigate(typeof(MainPage), e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // Sicherstellen, dass das aktuelle Fenster aktiv ist.
            Window.Current.Activate();
        }

        /// <summary>
        /// Stellt die Inhaltsübergänge nach dem Start der App wieder her.
        /// </summary>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Ausführung der Anwendung angehalten wird.  Der Anwendungszustand wird gespeichert,
        /// ohne zu wissen, ob die Anwendung beendet oder fortgesetzt wird und die Speicherinhalte dabei
        /// unbeschädigt bleiben.
        /// </summary>
        /// <param name="sender">Die Quelle der Anhalteanforderung.</param>
        /// <param name="e">Details zur Anhalteanforderung.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }
    }
}
