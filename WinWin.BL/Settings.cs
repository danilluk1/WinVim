using WinVim.BL.Common.Types;

namespace WinVim.BL {
    public class Settings {
        private static Settings? settings;
        public int SpeedX { get; set; } = 5;
        public int SpeedY { get; set; } = 5;
        public Combination ToVimModeCombo { get; set; }
        public bool IsInVim { get; set; } = false;
        public List<Control> Controls { get; set; } = new List<Control>();
        public Keys MouseLeft { get; set; } = Keys.B;
        public Keys MouseRight { get; set; } = Keys.N;
        private Settings() { }

        public static Settings GetInstance() {
            if (settings == null)
                settings = new Settings();
            return settings;
        }
    }
}
