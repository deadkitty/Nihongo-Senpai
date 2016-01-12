﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NihongoSenpai.Data.Database;
using NihongoSenpai.Controller;

namespace NihongoSenpai.Pages
{
    public partial class SelectInsertPage : PhoneApplicationPage
    {
        #region Constructor

        public SelectInsertPage()
        {
            InitializeComponent();
        }
        
        #endregion

        #region Events

        #region Load Lessons

        private void loadLessonsButton_Click(object sender, RoutedEventArgs e)
        {
            if (setsListbox.SelectedItems.Count > 0)
            {
                List<Lesson> selectedLessons = new List<Lesson>();
                
                foreach(Lesson lesson in setsListbox.SelectedItems)
                {
                    selectedLessons.Add(lesson);
                }

                InsertController.LoadLessons(selectedLessons);

                NavigationService.Navigate(new Uri("/Pages/PracticeInsertPage.xaml", UriKind.Relative));

                setsListbox.SelectedItems.Clear();
            }
            else
            {
                MessageBox.Show("Keine Lektion ausgewählt!");
            }
        }
        
        #endregion

        #region Back, NavigatedFrom/To

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            //if i go back to main page, close the database connection
            if (e.NavigationMode == NavigationMode.Back)
            {
                DataManager.CloseConnection();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //if i come from main Page open database connection and initialize lessons
            if (e.NavigationMode == NavigationMode.New)
            {
                DataManager.ConnectToDatabase();

                IQueryable<Lesson> lessons = DataManager.GetLessons(Lesson.EType.insert);

                foreach (Lesson l in lessons)
                {
                    setsListbox.Items.Add(l);
                }
            }
        }

        #endregion

        #region ApplicationBar

        private void selectAll_Click(object sender, EventArgs e)
        {
            foreach (object item in setsListbox.Items)
            {
                setsListbox.SelectedItems.Add(item);
            }
        }

        private void selectNone_Click(object sender, EventArgs e)
        {
            setsListbox.SelectedItems.Clear();
        }

        #endregion

        #endregion
    }
}