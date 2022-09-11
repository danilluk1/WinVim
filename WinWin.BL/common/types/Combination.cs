using System.Collections.Generic;
using WinVim.BL.Common.Types;

namespace WinVim.BL {
    public sealed class Combination {
        public List<Keys> Combo { get; private set; }
        public Action Action { get; private set; }

        public Combination(List<Keys> combo, Action action) {
            Combo = combo;
            Action = action;
        }
    }
}
