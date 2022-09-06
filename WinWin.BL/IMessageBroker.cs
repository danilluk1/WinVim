using WinVim.BL.Common.Events;

namespace WinVim.BL {
    internal interface IMessageBroker : IDisposable {
        event EventHandler<KeyboardPressEventArgs> KeyDown;
        event EventHandler<KeyboardPressEventArgs> KeyUp;
    }
}
