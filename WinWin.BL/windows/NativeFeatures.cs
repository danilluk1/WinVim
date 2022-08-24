using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinVim.BL.Windows {
    public static class NativeFeatures {
        public static readonly int WH_KEYBOARD_LL = 13;
        public const int WM_KEYDOWN = 0x0100;

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct MSG {
            public IntPtr hwnd;
            public UInt32 message;
            public UIntPtr wParam;
            public UIntPtr lParam;
            public UInt32 time;
            public POINT pt;
        }

        public struct POINT {
            public Int32 x;
            public Int32 Y;
        }

        public enum KeyboardMessage {
            KeyDown = 0x100,
            KeyUp = 0x101,
            SysKeyDown = 0x104,
            SysKeyUp = 0x105
        }

        public class KeyboardMessageEventArgs : EventArgs {
            public int VirtKeyCode { get; private set; }
            public KeyboardMessage MessageType { get; private set; }

            public KeyboardMessageEventArgs(int vkCode, KeyboardMessage msg) {
                VirtKeyCode = vkCode;
                MessageType = msg;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct KeyboardLowLevelHookStruct {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate IntPtr HookHandle(int code, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        internal static extern IntPtr SetWindowsHookEx(int idHook, HookHandle lpfn, IntPtr hInstance, int threadId);

        [DllImport("User32.dll")]
        internal static extern bool GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport("user32.dll")]
        internal static extern IntPtr DispatchMessage([In] ref MSG lpmsg);

        [DllImport("user32.dll")]
        internal static extern bool TranslateMessage([In] ref MSG lpMsg);

        [DllImport("user32.dll")]
        internal static extern IntPtr CallNextHookEx(IntPtr _, int nCode, IntPtr Wparam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern int UnhookWindowsHookEx(IntPtr hHook);
    }
}
