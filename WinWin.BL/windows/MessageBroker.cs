using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WinVim.BL;
using WinVim.BL.common.events;
using WinVim.BL.Windows;

namespace WinWin.BL.Windows {

    public sealed class MessageBroker : IMessageBroker {
        private readonly IntPtr kbHook = IntPtr.Zero;
        private readonly NativeFeatures.HookHandle kbProc;
        private IntPtr keyboardHookCallback(int code, IntPtr wParam, IntPtr lParam) {
            if (code < 0) return NativeFeatures.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);

            if(wParam.ToInt32() == NativeFeatures.WM_KEYDOWN || wParam.ToInt32() == NativeFeatures.WM_SYSKEYDOWN) {
                var st = Marshal.PtrToStructure<NativeFeatures.KeyboardLowLevelHookStruct>(lParam);
                keyDown?.Invoke(kbHook, new KeyboardPressEventArgs(st.vkCode));
                Console.WriteLine(st.vkCode);
            }
            else if (wParam.ToInt32() == NativeFeatures.WM_KEYUP || wParam.ToInt32() == NativeFeatures.WM_SYSKEYUP) {
                var st = Marshal.PtrToStructure<NativeFeatures.KeyboardLowLevelHookStruct>(lParam);
                keyUp?.Invoke(kbHook, new KeyboardPressEventArgs(st.vkCode));
            }

            return NativeFeatures.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        public MessageBroker() {
            kbProc = keyboardHookCallback;
            kbHook = NativeFeatures.SetWindowsHookEx(NativeFeatures.WH_KEYBOARD_LL, kbProc, IntPtr.Zero, 0);
        }

        ~MessageBroker() => Dispose();

        public bool ProcessMessages() {
            if (NativeFeatures.GetMessage(out NativeFeatures.MSG msg, IntPtr.Zero, 0, 0) != 0) {
                NativeFeatures.TranslateMessage(ref msg);
                NativeFeatures.DispatchMessage(ref msg);
                //return true;
            }
            return false;
        }

        public event EventHandler<KeyboardPressEventArgs>? keyDown;
        public event EventHandler<KeyboardPressEventArgs>? keyUp;

        public void Dispose() {
            NativeFeatures.UnhookWindowsHookEx(kbHook);
            GC.SuppressFinalize(this);
        }
    }
}
