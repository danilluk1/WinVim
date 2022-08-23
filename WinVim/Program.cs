using System;
using System.Runtime.InteropServices;

public class Program {
    private delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);
    [DllImport("User32.dll")]
    static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
    static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);

    [DllImport("User32.dll")]
    static extern short GetAsyncKeyState();
    public static void Main(string[] args) {
        MessageBox(IntPtr.Zero, "Command line message box", "Attention!", 0);
    }
}