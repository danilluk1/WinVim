using WinVim.BL.Common.Types;

namespace WinVim.BL {
    public class Control {
        public Direction Direction { get; set; }
        public Keys Key { get; set; }

        public Control(Direction direction, Keys key) {
            Direction = direction;
            Key = key;
        }
    }
}
