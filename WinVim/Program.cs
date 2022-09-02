﻿using System;
using System.Runtime.InteropServices;
using WinVim.BL;
using WinVim.BL.common.events;
using WinVim.BL.common.types;
using WinVim.BL.windows;
using WinVim.BL.Windows;
using WinWin.BL.Windows;

public class Program {
    
    static WinVim.BL.Windows.NativeFeatures.POINT point;
    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos([In] ref WinVim.BL.Windows.NativeFeatures.POINT point);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint SendInput(uint nInputs, NativeFeatures.INPUT[] pInputs, int cbSize);
    
    [DllImport("user32.dll")]
    private static extern IntPtr GetMessageExtraInfo();

    private static void Mh_MouseRightClick() {
        Console.WriteLine("Left Click");
        Mouse.MouseRightClick();
    }

    private static void Mh_MouseLeftClick() {
        Console.WriteLine("Left Click");
        Mouse.MouseLeftClick();
    }
    private static void Mh_MouseRight() {
        Console.WriteLine("Right");
        GetCursorPos(ref point);
        SetCursorPos(point.X + 5, point.Y);
        Console.WriteLine(point.X + " " + point.Y);
        Console.WriteLine("Left");
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
        SetCursorPos(point.X, point.Y - 5);
        Console.WriteLine(point.X + " " + point.Y);
        Console.WriteLine("Up");
    }

    private static void Mh_MouseDown() {
        GetCursorPos(ref point);
        SetCursorPos(point.X, point.Y + 5);
        Console.WriteLine(point.X + " " + point.Y);
        Console.WriteLine("Down");
    }

    private static void Mh_VimModeDisabled() {
        NativeFeatures.BlockInput(false);
        GetCursorPos(ref point);
        Console.WriteLine("Disabled");
    }

    private static void Mh_VimModeEnabled() {
        NativeFeatures.BlockInput(true);
        GetCursorPos(ref point);
        Console.WriteLine("Enabled");
    }

    public static void Main(string[] args) {
        Settings settings = Settings.GetInstance();
        settings.SpeedX = 5;
        settings.SpeedY = 5;
        settings.Controls = new List<Control> {
            new Control(Direction.GetDirection(Directions.TopLeft), Keys.Empty),
            new Control(Direction.GetDirection(Directions.Top), Keys.K),
            new Control(Direction.GetDirection(Directions.TopRight), Keys.Empty),
            new Control(Direction.GetDirection(Directions.Right), Keys.L),
            new Control(Direction.GetDirection(Directions.BottomRight), Keys.Empty),
            new Control(Direction.GetDirection(Directions.Bottom), Keys.J),
            new Control(Direction.GetDirection(Directions.BottomLeft), Keys.Empty),
            new Control(Direction.GetDirection(Directions.Left), Keys.H),
        };
        var arr = new Keys[] {Keys.LeftShift, Keys.Tab};
        settings.ToVimModeCombo = new Combination(
            arr,
            Mh_VimModeEnabled
        );
        MessageBroker mb = new();
        MessageHandler mh = new();
        mh.VimModeEnabled += Mh_VimModeEnabled;
        mh.VimModeDisabled += Mh_VimModeDisabled;

        mh.MouseDown += Mh_MouseDown;
        mh.MouseUp += Mh_MouseUp;
        mh.MouseLeft += Mh_MouseLeft;
        mh.MouseRight += Mh_MouseRight;
        mh.MouseLeftClick += Mh_MouseLeftClick;
        mh.MouseRightClick += Mh_MouseRightClick;

        mb.keyDown += mh.KeyDown;
        mb.keyUp += mh.KeyUp;

        while (true) {
            mb.ProcessMessages();
        }
    }
}