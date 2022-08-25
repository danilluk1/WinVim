using System;
using System.Runtime.InteropServices;
using WinVim.BL.common.events;
using WinVim.BL.Windows;
using WinWin.BL;
using WinWin.BL.Windows;



public class Program {
    public static void Main(string[] args) {
        MessageBroker mb = new MessageBroker();
        mb.keyDown += KeyDown;
        mb.keyUp += KeyUp;
        mb.ProcessMessages();
    }

    private static void KeyUp(object? sender, KeyboardPressEventArgs e) {
        if (pressed.Count > 0) {
            pressed.Pop();
        }
    }

    private static Stack<int> pressed = new();
    private static bool isInVimMode = false;
    private static void KeyDown(object? sender, KeyboardPressEventArgs e) {
        Console.WriteLine(e.VirtKeyCode);
        int lastPressed = 0;
        if (pressed.Count > 0) {
            lastPressed = pressed.Pop();
        }
        pressed.Push(e.VirtKeyCode);
        int preLast = 0;
        switch (lastPressed) {
            case 162:
                if (pressed.Count == 0) return;
                preLast = pressed.Peek();
                if(preLast == 164) {
                    Console.WriteLine("Ctrl + Alt");
                    isInVimMode = true;
                }
                break;
            case 164:
                if (pressed.Count == 0) return;
                preLast = pressed.Peek();
                if(preLast == 162) {
                    Console.WriteLine("Ctrl + Alt");
                    isInVimMode = true;
                }
                break;
        }
    }
}