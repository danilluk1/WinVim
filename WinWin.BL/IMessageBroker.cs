using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinVim.BL.Windows;

namespace WinVim.BL {
    internal interface IMessageBroker : IDisposable {
        event EventHandler<NativeFeatures.KeyboardMessageEventArgs> keyboardPressed;
        void Init();
    }
}
