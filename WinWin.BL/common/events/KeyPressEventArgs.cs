namespace WinVim.BL.Common.Events {
    public class KeyboardPressEventArgs : EventArgs {
        public int VirtKeyCode { get; private set; }

        public KeyboardPressEventArgs(int vkCode) {
            VirtKeyCode = vkCode;

        }
    }
}
