using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;

namespace WinVim.UI.Views {
    public partial class SettingsWindow : Window {
        public SettingsWindow() {
            InitializeComponent();
        }

        public void OnFocusLost(object? sender, EventArgs e) {
            this.Close();
        }
    }
}
