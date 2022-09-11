using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using WinVim.BL;
using WinVim.BL.Common.Types;
using WinVim.BL.Windows;
using WinVim.UI.Views;

namespace WinVim.UI
{
    public partial class MainWindow : Window
    {
        static WinVim.BL.Windows.NativeFeatures.POINT point;
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos([In] ref WinVim.BL.Windows.NativeFeatures.POINT point);
        private Settings settings = Settings.GetInstance();

        private bool isSettingsShown = false;
        private SettingsWindow window = new SettingsWindow();
        public MainWindow()
        {
            InitializeComponent();
            var arr = new List<Keys> { Keys.LeftShift, Keys.LeftControl };
            settings.ToVimModeCombo = new Combination(
                arr,
                Mh_VimModeEnabled
            );
            
            Thread thread = new(KeyThread);
            thread.Start();
        }

        private void KeyThread(object? obj) {
            var mh = new MessageHandler(Settings.GetInstance());
            var mb = new MessageBroker();

            mh.MouseLeftClick += Mh_MouseLeftClick;
            mh.MouseRightClick += Mh_MouseRightClick;
            mh.MouseMove += Mh_MouseMove;

            mb.KeyDown += mh.KeyDown;
            mb.KeyUp += mh.KeyUp;
            while (true) {
                mb.ProcessMessages();
            }
        }

        private void Mh_MouseMove(Direction dir) {
            Mouse.MoveCursor(dir.dX * settings.SpeedX, dir.dY * settings.SpeedY);
        }

        private void Mh_MouseRightClick() {
            Mouse.MouseRightClick();
        }

        private void Mh_MouseLeftClick() {
            Mouse.MouseLeftClick();
        }

        private void Mh_VimModeEnabled() {
            if (!settings.IsInVim) {
                settings.IsInVim = true;
                NativeFeatures.BlockInput(true);
            } else {
                settings.IsInVim = false;
                NativeFeatures.BlockInput(false);
            }
        }

        public void OnFocusLost(object sender, RoutedEventArgs e) {
            window?.Close();
        }

        public void OnSettingsEnter(object sender, PointerEventArgs e) {
            if (isSettingsShown) {
                window.Close();
                isSettingsShown = false;
                return;
            }

            window = new SettingsWindow();
            var curScreen = Screens.ScreenFromPoint(this.Position);
            if (curScreen == null) return;

            PixelPoint settingsWindowPoint;
            if(this.Position.Y < (curScreen.Bounds.Height / 2)) {
                settingsWindowPoint = new PixelPoint(this.Position.X, this.Position.Y + 40);
            } else {
                settingsWindowPoint = new PixelPoint(this.Position.X, this.Position.Y - 160);
            }

            window.Position = settingsWindowPoint;
            window.Show();
            isSettingsShown = true;
        }

        public void MouseLeftClickBox_KeyDown(object? sender, KeyEventArgs e) {

        }

        public void OnFocus(object sender, PointerEventArgs e) {
        }
    }
}