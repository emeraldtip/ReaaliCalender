﻿#pragma checksum "..\..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E7FE18C83974AC7B312DC78274043509790B61CF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using KaLENDERFINAL;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace KaLENDERFINAL {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 44 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddTaskButton;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Calendar cal;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scroll;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel dateStack;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scroll1;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel unDoneStack;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scroll2;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel progressStack;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scroll3s;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel doneStack;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddTaskButton2;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/KaLENDERFINAL;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.AddTaskButton = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\MainWindow.xaml"
            this.AddTaskButton.Click += new System.Windows.RoutedEventHandler(this.addTaskButton);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cal = ((System.Windows.Controls.Calendar)(target));
            
            #line 50 "..\..\..\MainWindow.xaml"
            this.cal.SelectedDatesChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.Calendar_SelectedDatesChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.scroll = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 4:
            this.dateStack = ((System.Windows.Controls.StackPanel)(target));
            
            #line 63 "..\..\..\MainWindow.xaml"
            this.dateStack.MouseEnter += new System.Windows.Input.MouseEventHandler(this.focc);
            
            #line default
            #line hidden
            
            #line 64 "..\..\..\MainWindow.xaml"
            this.dateStack.GotFocus += new System.Windows.RoutedEventHandler(this.bruh);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 71 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.tested);
            
            #line default
            #line hidden
            return;
            case 6:
            this.scroll1 = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 7:
            this.unDoneStack = ((System.Windows.Controls.StackPanel)(target));
            
            #line 88 "..\..\..\MainWindow.xaml"
            this.unDoneStack.MouseEnter += new System.Windows.Input.MouseEventHandler(this.focc);
            
            #line default
            #line hidden
            
            #line 89 "..\..\..\MainWindow.xaml"
            this.unDoneStack.GotFocus += new System.Windows.RoutedEventHandler(this.bruh);
            
            #line default
            #line hidden
            return;
            case 8:
            this.scroll2 = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 9:
            this.progressStack = ((System.Windows.Controls.StackPanel)(target));
            
            #line 94 "..\..\..\MainWindow.xaml"
            this.progressStack.MouseEnter += new System.Windows.Input.MouseEventHandler(this.focc);
            
            #line default
            #line hidden
            
            #line 95 "..\..\..\MainWindow.xaml"
            this.progressStack.GotFocus += new System.Windows.RoutedEventHandler(this.bruh);
            
            #line default
            #line hidden
            return;
            case 10:
            this.scroll3s = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 11:
            this.doneStack = ((System.Windows.Controls.StackPanel)(target));
            
            #line 100 "..\..\..\MainWindow.xaml"
            this.doneStack.MouseEnter += new System.Windows.Input.MouseEventHandler(this.focc);
            
            #line default
            #line hidden
            
            #line 101 "..\..\..\MainWindow.xaml"
            this.doneStack.GotFocus += new System.Windows.RoutedEventHandler(this.bruh);
            
            #line default
            #line hidden
            return;
            case 12:
            this.AddTaskButton2 = ((System.Windows.Controls.Button)(target));
            
            #line 104 "..\..\..\MainWindow.xaml"
            this.AddTaskButton2.Click += new System.Windows.RoutedEventHandler(this.addTaskButton);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

