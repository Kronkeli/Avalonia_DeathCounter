using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using CounterDialog.ViewModels;
using CounterDialog.Views;
using CounterDialog.Services;
using Avalonia.Input;
using System.Runtime.InteropServices;
using System;
using System.Windows.Forms;
using Keys = Avalonia.Input.Key;
using System.Windows.Input;

public class GlobalHotkey
{
    private const int WM_HOTKEY = 0x0312;

    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    private readonly IntPtr _handle;
    private readonly int _id;
    private readonly Action _action;

    public GlobalHotkey(System.Windows.Input.Key key, Action action)
    {
        _handle = IntPtr.Zero;
        _id = GetHashCode();
        _action = action;

        // AXAML-PREVIEWIA VARTEN KOMMENTOITAVA
        RegisterHotKey(_handle, _id, 0, (uint)KeyInterop.VirtualKeyFromKey(key));
        ((ClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current.ApplicationLifetime).MainWindow.KeyDown += MainWindow_KeyDown;

    }

    public void HandleHotkeyMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
    {
        if (msg == WM_HOTKEY && (int)wParam == _id)
        {
            _action.Invoke();
        }
    }

    private void MainWindow_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        if (e.Key == Avalonia.Input.Key.NumPad0)
        {
            _action.Invoke();
        }
    }

    ~GlobalHotkey()
    {
        UnregisterHotKey(_handle, _id);
    }
}


namespace CounterDialog
{
    public partial class App : Avalonia.Application
    {
        private GlobalHotkey _hotkey;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnHotkeyPressed()
        {
            Console.WriteLine("Hotkey pressed!");
            var service = new CounterDialog.Services.CounterService();
            service.IncrementCounter("Morgoth");
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();

            // Initialize the GlobalHotkey with desired key and action
            _hotkey = new GlobalHotkey(System.Windows.Input.Key.NumPad0, OnHotkeyPressed);
        }
    }
}