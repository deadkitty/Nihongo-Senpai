﻿#pragma checksum "D:\Soi Fon\Visual Studio\Projects\NihongoSenpai\NihongoSenpai\Controls\StatisticsItem.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7C3FB76AE42B24423DBAC29EE2A88CDD"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace NihongoSenpai.Controls {
    
    
    public partial class StatisticsItem : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.StackPanel LayoutRoot;
        
        internal System.Windows.Controls.TextBlock wordTextblock;
        
        internal System.Windows.Controls.Grid valueGrid;
        
        internal System.Windows.Controls.TextBlock correctTextblock;
        
        internal System.Windows.Controls.TextBlock gerJapTextblock;
        
        internal System.Windows.Controls.TextBlock wrongTextblock;
        
        internal System.Windows.Controls.ProgressBar progressBar;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/NihongoSenpai;component/Controls/StatisticsItem.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.StackPanel)(this.FindName("LayoutRoot")));
            this.wordTextblock = ((System.Windows.Controls.TextBlock)(this.FindName("wordTextblock")));
            this.valueGrid = ((System.Windows.Controls.Grid)(this.FindName("valueGrid")));
            this.correctTextblock = ((System.Windows.Controls.TextBlock)(this.FindName("correctTextblock")));
            this.gerJapTextblock = ((System.Windows.Controls.TextBlock)(this.FindName("gerJapTextblock")));
            this.wrongTextblock = ((System.Windows.Controls.TextBlock)(this.FindName("wrongTextblock")));
            this.progressBar = ((System.Windows.Controls.ProgressBar)(this.FindName("progressBar")));
        }
    }
}

