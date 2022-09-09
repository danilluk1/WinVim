using Avalonia.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WinVim.BL;
using WinVim.BL.Common.Types;
using WinVim.UI.Models;

namespace WinVim.UI.ViewModels {
    internal class SettingsViewModel : INotifyPropertyChanged {
        private Settings settings;

        public SettingsViewModel(Settings settings) {
            this.settings = settings;
        }

        public int SpeedX {
            get { return this.settings.SpeedX; }
            set { 
                this.settings.SpeedX = value;
                OnPropertyChanged("SpeedX");
            }
        }

        public int SpeedY {
            get { return this.settings.SpeedY; }
            set {
                this.settings.SpeedY = value;
                OnPropertyChanged("SpeedY");
            }
        }

        public Key MouseLeft {
            get { return KeyConverter.KeyFromVirtualKey((int)this.settings.MouseLeft); }
            set {
                this.settings.MouseLeft = KeyConverter.VirtualKeyFromKey(value);
                OnPropertyChanged("MouseLeft");
            }
        }

        public Keys MouseRight {
            get { return this.settings.MouseRight; }
            set {
                this.settings.MouseRight = value;
                OnPropertyChanged("MouseRight");
            }
        }

        public Combination ToVimModeCombo {
            get { return this.settings.ToVimModeCombo; }
            set {
                this.settings.ToVimModeCombo = value;
                OnPropertyChanged("ToVimModeCombo");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") {
            if(PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
