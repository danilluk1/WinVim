using WinVim.BL.common.types;

namespace WinVim.BL {
    public sealed class Combination {
        public Keys[] Combo { get; private set; }
        public Action Action { get; private set; }

        public Combination(Keys[] combo, Action action) {
            Combo = combo;
            Action = action;
        }
    }
}
