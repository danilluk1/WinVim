using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinVim.BL {
    public sealed class Controls {
        public Control[] ControlsArr { get; set; }
        public Controls(Control[] controls) {
            this.ControlsArr = controls;
        }
    }
}
