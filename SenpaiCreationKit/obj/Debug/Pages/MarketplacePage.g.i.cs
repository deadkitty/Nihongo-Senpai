﻿#pragma checksum "..\..\..\Pages\MarketplacePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CC2BBF7F37F65E2DF79E67C3B3F44942"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using SenpaiCreationKit.Controls;
using SenpaiCreationKit.Pages;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SenpaiCreationKit.Pages {
    
    
    /// <summary>
    /// MarketplacePage
    /// </summary>
    public partial class MarketplacePage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\..\Pages\MarketplacePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button uploadLessons;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Pages\MarketplacePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button updateLessons;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Pages\MarketplacePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button deleteLessons;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Pages\MarketplacePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button exitMarketplace;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Pages\MarketplacePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox localLessonsListbox;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Pages\MarketplacePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox uploadedLessonsListbox;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\Pages\MarketplacePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SenpaiCreationKit.Controls.SelectLessonsControl selectLessonsControl;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SenpaiCreationKit;component/pages/marketplacepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\MarketplacePage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 11 "..\..\..\Pages\MarketplacePage.xaml"
            ((SenpaiCreationKit.Pages.MarketplacePage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.uploadLessons = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\Pages\MarketplacePage.xaml"
            this.uploadLessons.Click += new System.Windows.RoutedEventHandler(this.uploadLessons_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.updateLessons = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\Pages\MarketplacePage.xaml"
            this.updateLessons.Click += new System.Windows.RoutedEventHandler(this.updateLessons_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.deleteLessons = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\Pages\MarketplacePage.xaml"
            this.deleteLessons.Click += new System.Windows.RoutedEventHandler(this.deleteLessons_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.exitMarketplace = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\Pages\MarketplacePage.xaml"
            this.exitMarketplace.Click += new System.Windows.RoutedEventHandler(this.exitMarketplace_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.localLessonsListbox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 7:
            this.uploadedLessonsListbox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 8:
            this.selectLessonsControl = ((SenpaiCreationKit.Controls.SelectLessonsControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

