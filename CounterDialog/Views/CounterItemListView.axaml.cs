using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using Avalonia.Markup.Xaml;
using CounterDialog.ViewModels;
using CounterDialog.Models;
using ReactiveUI;
using System.Reactive;
using Avalonia.Input;
using System.Windows;
using System.Diagnostics;

namespace CounterDialog.Views
{
    public partial class CounterItemListView : UserControl
    {
        private ComboBox _comboBox;
        private Button _diedButton;
        private Button _ggButton;

        public event EventHandler<SelectionChangedEventArgs>? SelectionChanged;
        public event EventHandler<Avalonia.Interactivity.RoutedEventArgs>? Shutdown;

        public static readonly RoutedEvent<SelectionChangedEventArgs> SelectionChangedEvent =
        Avalonia.Interactivity.RoutedEvent.Register<CounterItemListView, SelectionChangedEventArgs>(nameof(SelectionChanged), RoutingStrategies.Bubble);

        public static readonly RoutedEvent<Avalonia.Interactivity.RoutedEventArgs> ShutdownEvent =
        Avalonia.Interactivity.RoutedEvent.Register<CounterItemListView, Avalonia.Interactivity.RoutedEventArgs>(nameof(Shutdown), RoutingStrategies.Bubble);


        public CounterItemListView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _comboBox = this.FindControl<ComboBox>("MyComboBox");
            _comboBox.SelectionChanged += Combobox_SelectionChanged;
            _diedButton = this.FindControl<Button>("DiedButton");
            _ggButton = this.FindControl<Button>("GGButton");
            //_ggButton.Click += _ggButton_Click;
        }

        private void _ggButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            //Application.Current.Shutdown();
            Debug.WriteLine("Attempting to shutdown main window");
            Shutdown?.Invoke(this, e);
        }

        private void Combobox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            //Debug.WriteLine("DOdiiiin");
            var viewModel = (CounterItemViewModel)DataContext;
            _comboBox.IsVisible = false;
            viewModel.UpdateActiveCounter((CounterItem)e.AddedItems[0]);
            HotKeyManager.SetHotKey(_diedButton, new KeyGesture(Key.NumPad0, KeyModifiers.Alt));
            SelectionChanged?.Invoke(this, e);
        }
    }
}