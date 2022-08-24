using System;
using System.Runtime.InteropServices;
using WinVim.BL.Windows;
using WinWin.BL;
using WinWin.BL.Windows;

public struct POINT {
    public Int32 x;
    public Int32 Y;
}

[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct MSG {
    public IntPtr hwnd;
    public UInt32 message;
    public UIntPtr wParam;
    public UIntPtr lParam;
    public UInt32 time;
    public POINT pt;
}

public enum KeyboardMessage {
    KeyDown = 0x100,
    KeyUp = 0x101,
    SysKeyDown = 0x104,
    SysKeyUp = 0x105
}

public class NewKeyboardMessageEventArgs : EventArgs {
    public int VirtKeyCode { get; private set; }
    public KeyboardMessage MessageType { get; private set; }

    public NewKeyboardMessageEventArgs(int vkCode, KeyboardMessage msg) {
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


public class Program {
    private const int WH_KEYBOARD_LL = 13;

    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate IntPtr HookHandle(int code, IntPtr wParam, IntPtr lParam);

    [DllImport("User32.dll")]
    static extern IntPtr SetWindowsHookEx(int idHook, HookHandle lpfn, IntPtr hInstance, int threadId);

    [DllImport("User32.dll")]
    static extern bool GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

    [DllImport("user32.dll")]
    public static extern IntPtr DispatchMessage([In] ref MSG lpmsg);

    [DllImport("user32.dll")]
    public static extern bool TranslateMessage([In] ref MSG lpMsg);

    [DllImport("user32.dll")]
    public static extern IntPtr CallNextHookEx(IntPtr _, int nCode, IntPtr Wparam, IntPtr lParam);

    static IntPtr keyboardHookCallback(int code, IntPtr wParam, IntPtr lParam) {
        if(code >= 0) {
            var st = Marshal.PtrToStructure<KeyboardLowLevelHookStruct>(lParam);
        }
        return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
    }

    public static void Main(string[] args) {

        //var hook = SetWindowsHookEx(WH_KEYBOARD_LL, keyboardHookCallback, IntPtr.Zero, 0);

        MessageBroker mb = new MessageBroker();
        mb.keyboardPressed += KeyboardPressed;

        MSG msg = default;

        while (GetMessage(out msg, IntPtr.Zero, 0, 0) != false) {
            //TranslateMessage(ref msg);
        }
    }

    private static void KeyboardPressed(object? sender, NativeFeatures.KeyboardMessageEventArgs e) {
        Console.WriteLine((char)e.VirtKeyCode);
    }
}