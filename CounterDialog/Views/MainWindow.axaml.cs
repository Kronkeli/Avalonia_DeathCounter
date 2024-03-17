using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Diagnostics;

namespace CounterDialog.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            var childView = this.FindControl<CounterItemListView>("ChildView");
            childView.SelectionChanged += ChildView_SelectionChanged;
        }

        private void ChildView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("T‰‰ll‰kin muutos tiedossa.");            
        }
    }
}