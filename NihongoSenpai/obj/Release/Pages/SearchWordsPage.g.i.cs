﻿#pragma checksum "D:\Soi Fon\Visual Studio\Projects\Nihongo-Senpai\NihongoSenpai\Pages\SearchWordsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2182857ED283C090320275769B9FB98F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using NihongoSenpai.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace NihongoSenpai.Pages {
    
    
    public partial class SearchWordsPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.TextBox searchTextbox;
        
        internal System.Windows.Controls.ListBox matchedItemsListbox;
        
        internal NihongoSenpai.Controls.EditWordControl editWordsControl;
        
        internal System.Windows.Controls.Button editButton;
        
        internal System.Windows.Controls.Button showLessonButton;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/NihongoSenpai;component/Pages/SearchWordsPage.xaml", System.UriKind.Relative));
            this.searchTextbox = ((System.Windows.Controls.TextBox)(this.FindName("searchTextbox")));
            this.matchedItemsListbox = ((System.Windows.Controls.ListBox)(this.FindName("matchedItemsListbox")));
            this.editWordsControl = ((NihongoSenpai.Controls.EditWordControl)(this.FindName("editWordsControl")));
            this.editButton = ((System.Windows.Controls.Button)(this.FindName("editButton")));
            this.showLessonButton = ((System.Windows.Controls.Button)(this.FindName("showLessonButton")));
        }
    }
}

