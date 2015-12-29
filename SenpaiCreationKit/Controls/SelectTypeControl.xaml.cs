using SenpaiCreationKit.Data;
using System;
using System.Collections.Generic;
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

namespace SenpaiCreationKit.Controls
{
    public partial class SelectTypeControl : UserControl
    {
        public List<Word> selectedWords = new List<Word>();
        public IPageUpdater pageUpdater;

        public SelectTypeControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < (int)Word.EType.count; ++i)
            {
                typesListbox.Items.Add(Word.GetTypeString((Word.EType)i, true));
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Visibility = System.Windows.Visibility.Collapsed;

            selectedWords.Clear();
        }

        private void select_Click(object sender, RoutedEventArgs e)
        {
            Visibility = System.Windows.Visibility.Collapsed;

            foreach(Word word in selectedWords)
            {
                word.Type = (Word.EType)typesListbox.SelectedIndex;
            }
            
            selectedWords.Clear();

            pageUpdater.UpdatePage();
        }
    }
}
