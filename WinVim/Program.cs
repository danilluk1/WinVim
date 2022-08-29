using System;
using System.Runtime.InteropServices;
using WinVim.BL;
using WinVim.BL.common.events;
using WinWin.BL.Windows;

public class Program {
    
    static WinVim.BL.Windows.NativeFeatures.POINT point;
    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos([In] ref WinVim.BL.Windows.NativeFeatures.POINT point);

    [DllImport("user32.dll", SetLastError = true)]
    static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);
    public static void Main(string[] args) {
        MessageBroker mb = new();
        MessageHandler mh = new();
        mh.VimModeEnabled += Mh_VimModeEnabled;
        mh.VimModeDisabled += Mh_VimModeDisabled;

        mh.MouseDown += Mh_MouseDown;
        mh.MouseUp += Mh_MouseUp;
        mh.MouseLeft += Mh_MouseLeft;
        mh.MouseRight += Mh_MouseRight;

        mb.keyDown += mh.KeyDown;
        mb.keyUp += mh.KeyUp;

        mb.ProcessMessages();
    }

    private static void Mh_MouseRight() {
            GetCursorPos(ref point);
            SetCursorPos(point.X + 5, point.Y);
            Console.WriteLine(point.X + " " + point.Y);
            Console.WriteLine("Right");
    }

    private static void Mh_MouseLeft() {
        Console.WriteLine("Left");
            GetCursorPos(ref point);
            SetCursorPos(point.X - 5, point.Y);
            Console.WriteLine(point.X + " " + point.Y);
            Console.WriteLine("Left");
    }

    private static void Mh_MouseUp() {
            GetCursorPos(ref point);
            SetCursorPos(point.X, point.Y + 5);
            Console.WriteLine(point.X + " " + point.Y);
            Console.WriteLine("Up");
    }
    
    private static void Mh_MouseDown() {
            GetCursorPos(ref point);
            SetCursorPos(point.X, point.Y - 5);
            Console.WriteLine(point.X + " " + point.Y);
            Console.WriteLine("Down");
    }

    private static void Mh_VimModeDisabled() {
        GetCursorPos(ref point);
        Console.WriteLine("Disabled");
    }

    private static void Mh_VimModeEnabled() {
        GetCursorPos(ref point);
        Console.WriteLine("Enabled");
    }
}