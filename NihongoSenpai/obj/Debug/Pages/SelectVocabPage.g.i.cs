﻿#pragma checksum "D:\Google Drive\Schule\9. Semester\Mobile Computing II\NihongoSenpai\NihongoSenpai\Pages\SelectVocabPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9D84158E629F17A3ED8C52FBBA2F3278"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.34209
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
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
    
    
    public partial class SelectVocabPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal Microsoft.Phone.Controls.PivotItem selectLessonsPivot;
        
        internal System.Windows.Controls.ListBox setsListbox;
        
        internal System.Windows.Controls.Button loadLessonsButton;
        
        internal Microsoft.Phone.Controls.PivotItem settingsPivot;
        
        internal System.Windows.Controls.Grid sortOrderGrid;
        
        internal System.Windows.Controls.RadioButton radio1;
        
        internal System.Windows.Controls.RadioButton radio2;
        
        internal System.Windows.Controls.RadioButton radio3;
        
        internal System.Windows.Controls.Grid typeSelectionGrid;
        
        internal System.Windows.Controls.CheckBox verb1Checkbox;
        
        internal System.Windows.Controls.CheckBox verb2Checkbox;
        
        internal System.Windows.Controls.CheckBox verb3Checkbox;
        
        internal System.Windows.Controls.CheckBox iAdjCheckbox;
        
        internal System.Windows.Controls.CheckBox naAdjCheckbox;
        
        internal System.Windows.Controls.CheckBox advCheckbox;
        
        internal System.Windows.Controls.CheckBox nounCheckbox;
        
        internal System.Windows.Controls.CheckBox partCheckbox;
        
        internal System.Windows.Controls.CheckBox prevCheckbox;
        
        internal System.Windows.Controls.CheckBox suffCheckbox;
        
        internal System.Windows.Controls.CheckBox phrCheckbox;
        
        internal System.Windows.Controls.CheckBox otherCheckbox;
        
        internal System.Windows.Controls.Grid learnDirectionGrid;
        
        internal System.Windows.Controls.Image japGerIcon;
        
        internal System.Windows.Controls.Image gerIcon;
        
        internal System.Windows.Controls.Image japIcon;
        
        internal System.Windows.Controls.Grid generalOptionsGrid;
        
        internal System.Windows.Controls.CheckBox loadAllWordsCheckBox;
        
        internal System.Windows.Controls.Slider newWordsPerRoundSlider;
        
        internal System.Windows.Controls.TextBlock newWordsPerRoundTextblock;
        
        internal System.Windows.Controls.CheckBox showDescCheckBox;
        
        internal Microsoft.Phone.Shell.ApplicationBarMenuItem selectAll;
        
        internal Microsoft.Phone.Shell.ApplicationBarMenuItem selectNone;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/NihongoSenpai;component/Pages/SelectVocabPage.xaml", System.UriKind.Relative));
            this.selectLessonsPivot = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("selectLessonsPivot")));
            this.setsListbox = ((System.Windows.Controls.ListBox)(this.FindName("setsListbox")));
            this.loadLessonsButton = ((System.Windows.Controls.Button)(this.FindName("loadLessonsButton")));
            this.settingsPivot = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("settingsPivot")));
            this.sortOrderGrid = ((System.Windows.Controls.Grid)(this.FindName("sortOrderGrid")));
            this.radio1 = ((System.Windows.Controls.RadioButton)(this.FindName("radio1")));
            this.radio2 = ((System.Windows.Controls.RadioButton)(this.FindName("radio2")));
            this.radio3 = ((System.Windows.Controls.RadioButton)(this.FindName("radio3")));
            this.typeSelectionGrid = ((System.Windows.Controls.Grid)(this.FindName("typeSelectionGrid")));
            this.verb1Checkbox = ((System.Windows.Controls.CheckBox)(this.FindName("verb1Checkbox")));
            this.verb2Checkbox = ((System.Windows.Controls.CheckBox)(this.FindName("verb2Checkbox")));
            this.verb3Checkbox = ((System.Windows.Controls.CheckBox)(this.FindName("verb3Checkbox")));
            this.iAdjCheckbox = ((System.Windows.Controls.CheckBox)(this.FindName("iAdjCheckbox")));
            this.naAdjCheckbox = ((System.Windows.Controls.CheckBox)(this.FindName("naAdjCheckbox")));
            this.advCheckbox = ((System.Windows.Controls.CheckBox)(this.FindName("advCheckbox")));
            this.nounCheckbox = ((System.Windows.Controls.CheckBox)(this.FindName("nounCheckbox")));
            this.partCheckbox = ((System.Windows.Controls.CheckBox)(this.FindName("partCheckbox")));
            this.prevCheckbox = ((System.Windows.Controls.CheckBox)(this.FindName("prevCheckbox")));
            this.suffCheckbox = ((System.Windows.Controls.CheckBox)(this.FindName("suffCheckbox")));
            this.phrCheckbox = ((System.Windows.Controls.CheckBox)(this.FindName("phrCheckbox")));
            this.otherCheckbox = ((System.Windows.Controls.CheckBox)(this.FindName("otherCheckbox")));
            this.learnDirectionGrid = ((System.Windows.Controls.Grid)(this.FindName("learnDirectionGrid")));
            this.japGerIcon = ((System.Windows.Controls.Image)(this.FindName("japGerIcon")));
            this.gerIcon = ((System.Windows.Controls.Image)(this.FindName("gerIcon")));
            this.japIcon = ((System.Windows.Controls.Image)(this.FindName("japIcon")));
            this.generalOptionsGrid = ((System.Windows.Controls.Grid)(this.FindName("generalOptionsGrid")));
            this.loadAllWordsCheckBox = ((System.Windows.Controls.CheckBox)(this.FindName("loadAllWordsCheckBox")));
            this.newWordsPerRoundSlider = ((System.Windows.Controls.Slider)(this.FindName("newWordsPerRoundSlider")));
            this.newWordsPerRoundTextblock = ((System.Windows.Controls.TextBlock)(this.FindName("newWordsPerRoundTextblock")));
            this.showDescCheckBox = ((System.Windows.Controls.CheckBox)(this.FindName("showDescCheckBox")));
            this.selectAll = ((Microsoft.Phone.Shell.ApplicationBarMenuItem)(this.FindName("selectAll")));
            this.selectNone = ((Microsoft.Phone.Shell.ApplicationBarMenuItem)(this.FindName("selectNone")));
        }
    }
}

