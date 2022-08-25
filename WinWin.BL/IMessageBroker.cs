using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinVim.BL.common.events;

namespace WinVim.BL {
    internal interface IMessageBroker : IDisposable {
        event EventHandler<KeyboardPressEventArgs> keyDown;
        event EventHandler<KeyboardPressEventArgs> keyUp;
    }
}
