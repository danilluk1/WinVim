using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WinVim.BL;
using WinVim.BL.Windows;

namespace WinWin.BL.Windows {
    public sealed class MessageBroker : IMessageBroker {
        private IntPtr KeyboardHook { get; set; } = IntPtr.Zero;
        private NativeFeatures.HookHandle kbProc;
        IntPtr KeyboardHookCallback(int code, IntPtr wParam, IntPtr lParam) {
            if (code >= 0) {

                switch (wParam.ToInt32()) {
                    case NativeFeatures.WM_KEYDOWN:
                        Console.WriteLine("KeyDown");
                        break;
                }
                var st = Marshal.PtrToStructure<NativeFeatures.KeyboardLowLevelHookStruct>(lParam);
                keyboardPressed?.Invoke(KeyboardHook, new NativeFeatures.KeyboardMessageEventArgs(st.vkCode, (NativeFeatures.KeyboardMessage)wParam));
            }
            return NativeFeatures.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        public MessageBroker() {
            kbProc = KeyboardHookCallback;
            KeyboardHook = NativeFeatures.SetWindowsHookEx(NativeFeatures.WH_KEYBOARD_LL, kbProc, IntPtr.Zero, 0);
            Console.WriteLine(KeyboardHook);
        }

        public event EventHandler<NativeFeatures.KeyboardMessageEventArgs>? keyboardPressed;


        public void Init() {
            KeyboardHook = NativeFeatures.SetWindowsHookEx(NativeFeatures.WH_KEYBOARD_LL, KeyboardHookCallback, IntPtr.Zero, 0);
        }

        public void Dispose() {
            NativeFeatures.UnhookWindowsHookEx(KeyboardHook);
        }
    }
}
