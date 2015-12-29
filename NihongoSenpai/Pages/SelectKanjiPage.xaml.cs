using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NihongoSenpai.Data.Database;
using System.Windows.Media;
using NihongoSenpai.Data;

namespace NihongoSenpai.Pages
{
    public partial class SelectKanjiPage : PhoneApplicationPage
    {
        private int columnCount;
        private int rowCount;
        private int lastRowEmptyFieldsCount;

        //after coming back from ShowKanjiPage the Page_Loaded is called again and will try to add more kanjis, so i have to prevent this
        private bool kanjisLoaded = false;

        public SelectKanjiPage()
        {
            InitializeComponent();

            columnCount = LayoutRoot.ColumnDefinitions.Count;

            //for JLPT5 we have 103 kanji so we need 21 rows, the last row has 3 kanjis, without lastRowCount we would just get 20 rows
            //so i add the empty fields of the last row to the Kanjis array length
            lastRowEmptyFieldsCount = columnCount - (AppData.Kanjis.Count() % columnCount);
            rowCount = (AppData.Kanjis.Count() + lastRowEmptyFieldsCount) / columnCount;

            setNameTextblock.Text = AppData.SelectedLesson.name;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if(!kanjisLoaded)
            {
                //add new rows to the grid
                double rowHeight = LayoutRoot.ActualWidth / columnCount;
                for (int i = 0; i < rowCount; ++i)
                {
                    RowDefinition rd = new RowDefinition();
                    rd.Height = new GridLength(rowHeight);
                    LayoutRoot.RowDefinitions.Add(rd);
                }

                //add kanji´s to the rows
                for (int i = 0; i < rowCount - 1; ++i)
                {
                    for (int j = 0; j < columnCount; ++j)
                    {
                        Button b = new Button();
                        b.Foreground = new SolidColorBrush(Colors.Black);
                        b.Content = AppData.Kanjis[i * columnCount + j].kanji;
                        b.Click += new RoutedEventHandler(kanjiButton_Click);

                        //i have no need for a tab index in a phone application, so i use it to give
                        //my buttons some more informations like and ID so that i can navigate to the
                        //correct kanji on the DetailKanjiPage
                        b.TabIndex = i * columnCount + j;
                        LayoutRoot.Children.Add(b);
                        Grid.SetColumn(b, j);
                        Grid.SetRow(b, i);
                    }
                }

                //add last row kanjis to the grid
                for (int i = 0; i < columnCount - lastRowEmptyFieldsCount; ++i)
                {
                    Button b = new Button();
                    b.Foreground = new SolidColorBrush(Colors.Black);
                    b.Content = AppData.Kanjis[(rowCount - 1) * columnCount + i].kanji;
                    b.Click += new RoutedEventHandler(kanjiButton_Click);
                    b.TabIndex = (rowCount - 1) * columnCount + i;
                    LayoutRoot.Children.Add(b);
                    Grid.SetColumn(b, i);
                    Grid.SetRow(b, rowCount - 1);
                }

                kanjisLoaded = true;
            }
        }

        private void kanjiButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            AppData.SelectedKanji = AppData.Kanjis[b.TabIndex];
            NavigationService.Navigate(new Uri("/Pages/ShowKanjiPage.xaml", UriKind.Relative));
        }

        private void resetLessonIcon_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Lektion wirklich zurücksetzen???", "", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                DataManager.ResetLesson(AppData.SelectedLesson);
            }
        }
    }
}