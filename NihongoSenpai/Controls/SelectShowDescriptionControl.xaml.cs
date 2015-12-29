using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NihongoSenpai.Pages;

namespace NihongoSenpai.Controls
{
    public partial class SelectShowDescriptionControl : UserControl
    {
        public EditLessonPage parent;

        public int SelectedIndex
        {
            get { return showFlagListbox.SelectedIndex; }
        }

        public SelectShowDescriptionControl()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            parent.EditSelectedItems();
            this.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
