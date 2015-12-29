using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using NihongoSenpai.Data.Database;

namespace NihongoSenpai.Controls
{
    public partial class DetailWordItem : UserControl
    {
        public Word value;

        private static SolidColorBrush selectedBrush = null;
        private static SolidColorBrush deselectedBrush = null;

        private static int itemCounter = 0;
        
        public DetailWordItem()
        {
            InitializeComponent();
        }

        public DetailWordItem(Word word)
        {
            InitializeComponent();

            ++itemCounter;

            if(deselectedBrush == null)
            {
                deselectedBrush = wordTextblock.Foreground as SolidColorBrush;
                selectedBrush = new SolidColorBrush(Color.FromArgb(255, 100, 100, 255));
            }

            this.value = word;

            wordTextblock.Text = word.ToDetailString();
            descriptionTextblock.Text = word.ToDescriptionString();

            switch(word.ShowWord)
            {
                case Word.EShowFlags.dontShow    : noJapGerIcon  .Visibility = System.Windows.Visibility.Visible; break;
                case Word.EShowFlags.justGerman  : noJapaneseIcon.Visibility = System.Windows.Visibility.Visible; break;
                case Word.EShowFlags.justJapanese: noGermanIcon  .Visibility = System.Windows.Visibility.Visible; break;
                case Word.EShowFlags.showBoth    : break;
            }
        }

        /// <summary>
        /// i want to clean up the unused brushes and a destructor should be perfect for the 
        /// job, but because destructors in c# are weird (i heard XD) it is also a good testing
        /// purpose for them^^
        /// </summary>
        ~DetailWordItem()
        {
            --itemCounter;

            if(itemCounter == 0)
            {
                deselectedBrush = null;
                selectedBrush = null;
            }
        }

        public void Selected()
        {
            wordTextblock.Foreground = selectedBrush;
        }

        public void Deselected()
        {
            wordTextblock.Foreground = deselectedBrush;
        }

        public void Update()
        {
            Update(value);
        }

        public void Update(Word word)
        {
            wordTextblock.Text = word.ToDetailString();
            descriptionTextblock.Text = word.ToDescriptionString();
               
            noJapGerIcon  .Visibility = System.Windows.Visibility.Collapsed;
            noGermanIcon  .Visibility = System.Windows.Visibility.Collapsed;
            noJapaneseIcon.Visibility = System.Windows.Visibility.Collapsed;
            
            switch(word.ShowWord)
            {
                case Word.EShowFlags.dontShow    : noJapGerIcon  .Visibility = System.Windows.Visibility.Visible; break;
                case Word.EShowFlags.justGerman  : noJapaneseIcon.Visibility = System.Windows.Visibility.Visible; break;
                case Word.EShowFlags.justJapanese: noGermanIcon  .Visibility = System.Windows.Visibility.Visible; break;
                case Word.EShowFlags.showBoth    : break;
            }
        }
    }
}
