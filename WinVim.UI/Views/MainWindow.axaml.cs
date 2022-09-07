using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;
using WinVim.UI.Views;

namespace WinVim.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void OnSettingsClick(object sender, RoutedEventArgs e) {
            SettingsWindow window = new SettingsWindow();
            window.Show();
        }
    }
}