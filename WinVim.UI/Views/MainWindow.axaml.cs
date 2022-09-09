using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using WinVim.UI.Views;

namespace WinVim.UI
{
    public partial class MainWindow : Window
    {
        private bool isSettingsShown = false;
        private SettingsWindow window = new SettingsWindow();
        public MainWindow()
        {
            InitializeComponent();
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
            //throw new NotImplementedException();
        }
    }
}