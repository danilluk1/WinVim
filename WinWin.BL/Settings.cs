using WinVim.BL.Common.Types;

namespace WinVim.BL {
    public class Settings {

        private static Settings? settings;
        public int SpeedX { get; set; } = 5;
        public int SpeedY { get; set; } = 5;
        public Combination ToVimModeCombo { get; set; }
        public bool IsInVim { get; set; } = false;
        public List<Control> Controls { get; set; } = new List<WinVim.BL.Control> {
                new WinVim.BL.Control(Direction.GetDirection(Directions.TopLeft), Keys.Empty),
                new WinVim.BL.Control(Direction.GetDirection(Directions.Top), Keys.K),
                new WinVim.BL.Control(Direction.GetDirection(Directions.TopRight), Keys.Empty),
                new WinVim.BL.Control(Direction.GetDirection(Directions.Right), Keys.L),
                new WinVim.BL.Control(Direction.GetDirection(Directions.BottomRight), Keys.Empty),
                new WinVim.BL.Control(Direction.GetDirection(Directions.Bottom), Keys.J),
                new WinVim.BL.Control(Direction.GetDirection(Directions.BottomLeft), Keys.Empty),
                new WinVim.BL.Control(Direction.GetDirection(Directions.Left), Keys.H),
                new WinVim.BL.Control(Direction.GetDirection(Directions.None), Keys.B),
                new WinVim.BL.Control(Direction.GetDirection(Directions.None), Keys.N)
        };
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
