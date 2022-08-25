using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinVim.BL.common.events {
    public class KeyboardPressEventArgs : EventArgs {
        public int VirtKeyCode { get; private set; }

        public KeyboardPressEventArgs(int vkCode) {
            VirtKeyCode = vkCode;

        }
    }
}
