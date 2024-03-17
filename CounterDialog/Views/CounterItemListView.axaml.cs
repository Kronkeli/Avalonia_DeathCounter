using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using Avalonia.Markup.Xaml;
using CounterDialog.ViewModels;
using CounterDialog.Models;
using ReactiveUI;
using System.Reactive;

namespace CounterDialog.Views
{
    public partial class CounterItemListView : UserControl
    {
        private ComboBox _comboBox;

        public static readonly RoutedEvent<SelectionChangedEventArgs> SelectionChangedEvent =
        RoutedEvent.Register<CounterItemListView, SelectionChangedEventArgs>(nameof(SelectionChanged), RoutingStrategies.Bubble);

        public event EventHandler<SelectionChangedEventArgs>? SelectionChanged;

        public CounterItemListView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _comboBox = this.FindControl<ComboBox>("MyComboBox");
            _comboBox.SelectionChanged += Combobox_SelectionChanged;
        }

        private void Combobox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            //Debug.WriteLine("DOdiiiin");
            var viewModel = (CounterItemViewModel)DataContext;
            _comboBox.IsVisible = false;
            viewModel.UpdateActiveCounter((CounterItem)e.AddedItems[0]);
            SelectionChanged?.Invoke(this, e);
        }

        

    }
}