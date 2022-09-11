using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Win32.Interop;
using System;
using System.Collections.Generic;
using WinVim.BL;
using WinVim.BL.Common.Types;
using WinVim.UI.Models;
using WinVim.UI.ViewModels;

namespace WinVim.UI.Views {
    public partial class SettingsWindow : Window {
        public SettingsWindow() {
            InitializeComponent();
            DataContext = new SettingsViewModel(Settings.GetInstance());
        }

        public void OnFocusLost(object? sender, EventArgs e) {
            this.Close();
        }

        public void MouseLeftClickBox_KeyDown(object sender, KeyEventArgs e) {
            LeftClickComboTextBox.Text = e.Key.ToString();
            System.Diagnostics.Debug.WriteLine(KeyConverter.VirtualKeyFromKey(e.Key));
        }

        public void MouseLeftClickBox_Click(object sender, GotFocusEventArgs e) {
            Settings.GetInstance().MouseLeft = Keys.B;
        }

        public void MouseRightClickBox_KeyDown(object sender, KeyEventArgs e) {
            LeftClickComboTextBox.Text = e.Key.ToString();
            System.Diagnostics.Debug.WriteLine(KeyConverter.VirtualKeyFromKey(e.Key));
        }

        public void MouseRightClickBox_Click(object sender, GotFocusEventArgs e) {
            Settings.GetInstance().MouseRight = Keys.N;
        }

        private List<Key> currentCombo = new();
        public void EnableVimBox_KeyUp(object sender, KeyEventArgs e) {
            Settings.GetInstance().ToVimModeCombo.Combo.Clear();
            foreach (Key key in currentCombo) {
                Settings.GetInstance().ToVimModeCombo.Combo.Add(KeyConverter.VirtualKeyFromKey(key));
                EnableVimTextBox.Text += key.ToString() + " ";
            }
            EnableVimTextBox.Text = EnableVimTextBox.Text.Trim();
            currentCombo.Clear();
            Settings.GetInstance().ToVimModeCombo.Combo.Sort();
        }
        public void EnableVimBox_KeyDown(object sender, KeyEventArgs e) {
            EnableVimTextBox.Text = "";
            currentCombo.Add(e.Key);
        }

        public void EnableVimBox_Click(object sender, GotFocusEventArgs e) {
            EnableVimTextBox.Text = "";
            currentCombo.Clear();
        }
    }
}
