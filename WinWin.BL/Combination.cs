using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinVim.BL {
    public class Combination {
        public int[] Combo { get; private set; }
        public Action Action { get; private set; }

        public Combination(int[] combo, Action action) {
            Combo = combo;
            Action = action;
        }
    }
}
