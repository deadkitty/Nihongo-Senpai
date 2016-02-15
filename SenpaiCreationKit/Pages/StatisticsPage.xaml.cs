using Microsoft.Win32;
using SenpaiCreationKit.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SenpaiCreationKit.Pages
{
    /// <summary>
    /// A page just for some testing and data visualizing, but has nothing todo with the actual project
    /// and can't reached through a button or something eighter
    /// mabye i use it later for real
    /// </summary>
    public partial class StatisticsPage : Page
    {
        #region Fields

        private List<Lesson> lessons;

        List<NihongoSenpai.Model.Database.Word> words;
        List<NihongoSenpai.Model.Database.Kanji> kanjis;

        #endregion

        public StatisticsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void dataButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = ".nya|*.nya|.poyo|*.poyo";

            bool? result = fileDialog.ShowDialog();

            if (result == true)
            {
                using (FileStream stream = new FileStream(fileDialog.FileName, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        //poyo files save the currentRound Settings, so i overread them first
                        if (fileDialog.FileName.EndsWith(".poyo"))
                        {
                            sr.ReadLine();
                            sr.ReadLine();
                        }

                        lessons = new List<Lesson>();

                        words = new List<NihongoSenpai.Model.Database.Word>();
                        kanjis = new List<NihongoSenpai.Model.Database.Kanji>();

                        //read content from stream till end
                        while (!sr.EndOfStream)
                        {
                            Lesson lesson = new Lesson(sr.ReadLine());

                            for (int i = 0; i < lesson.Size; ++i)
                            {
                                switch ((Lesson.EType)lesson.Type)
                                {
                                    case Lesson.EType.vocab : words .Add(new NihongoSenpai.Model.Database.Word (sr.ReadLine())); break;
                                    case Lesson.EType.kanji : kanjis.Add(new NihongoSenpai.Model.Database.Kanji(sr.ReadLine())); break;
                                    case Lesson.EType.insert: sr.ReadLine(); break;
                                }
                            }

                            lessons.Add(lesson);
                        }
                    }
                }

                dataGrid.ItemsSource = kanjis;
                                
            }
        }
    }
}
