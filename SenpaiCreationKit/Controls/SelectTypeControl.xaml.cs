using SenpaiCreationKit.Data;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
            for (int i = 0; i < (int)Word.EType.count; ++i)
            {
                typesListbox.Items.Add(Word.GetTypeString((Word.EType)i, true));
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;

            selectedWords.Clear();
        }

        private void select_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;

            foreach (Word word in selectedWords)
            {
                word.Type = typesListbox.SelectedIndex;
            }

            selectedWords.Clear();

            pageUpdater.UpdatePage();
        }
    }
}