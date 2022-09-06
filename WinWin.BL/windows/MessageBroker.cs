using System.Runtime.InteropServices;
using WinVim.BL.Common.Events;

namespace WinVim.BL.Windows {

    public sealed class MessageBroker : IMessageBroker {
        private readonly IntPtr kbHook = IntPtr.Zero;
        private readonly NativeFeatures.HookHandle kbProc;
        private IntPtr keyboardHookCallback(int code, IntPtr wParam, IntPtr lParam) {
            if (code < 0) return NativeFeatures.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            NativeFeatures.PeekMessage(out NativeFeatures.MSG msg, IntPtr.Zero, 0, 0, 1);
            if(wParam.ToInt32() == NativeFeatures.WM_KEYDOWN || wParam.ToInt32() == NativeFeatures.WM_SYSKEYDOWN) {
                var st = Marshal.PtrToStructure<NativeFeatures.KeyboardLowLevelHookStruct>(lParam);
                KeyDown?.Invoke(kbHook, new KeyboardPressEventArgs(st.vkCode));
                Console.WriteLine(st.vkCode);
            }
            else if (wParam.ToInt32() == NativeFeatures.WM_KEYUP || wParam.ToInt32() == NativeFeatures.WM_SYSKEYUP) {
                var st = Marshal.PtrToStructure<NativeFeatures.KeyboardLowLevelHookStruct>(lParam);
                KeyUp?.Invoke(kbHook, new KeyboardPressEventArgs(st.vkCode));
            }

            return NativeFeatures.CallNextHookEx(IntPtr.Zero, 0, wParam, lParam);
        }
        public MessageBroker() {
            kbProc = keyboardHookCallback;
            kbHook = NativeFeatures.SetWindowsHookEx(NativeFeatures.WH_KEYBOARD_LL, kbProc, IntPtr.Zero, 0);
        }

        ~MessageBroker() => Dispose();
        public bool ProcessMessages(bool isInVimMode = true) {
            NativeFeatures.MSG msg;
   
            while (NativeFeatures.GetMessage(out msg, IntPtr.Zero, 0, 0) != 0) {
                //NativeFeatures.TranslateMessage(ref msg);
                //NativeFeatures.DispatchMessage(ref msg);
            }
            return false;
        }

        public event EventHandler<KeyboardPressEventArgs>? KeyDown;
        public event EventHandler<KeyboardPressEventArgs>? KeyUp;

        public void Dispose() {
            NativeFeatures.UnhookWindowsHookEx(kbHook);
            GC.SuppressFinalize(this);
        }
    }
}
