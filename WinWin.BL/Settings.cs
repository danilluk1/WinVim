using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinVim.BL {
    public class Settings {
        private static Settings? settings;
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }
        public Combination? ToVimModeCombo { get; set; }
        public List<Control> Controls { get; set; }

        private Settings() { 
        }

        public static Settings GetInstance() {
            if (settings == null)
                settings = new Settings();
            return settings;
        }
    }
}
