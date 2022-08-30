using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinVim.BL.common.events;
using WinVim.BL.common.types;

namespace WinVim.BL {
    public class Combination {
        public int[] Combo { get; private set; }
        public Action Action { get; private set; }

        public Combination(int[] combo, Action action) {
            Combo = combo;
            Action = action;
        }
    }

    public class MessageHandler {
        private readonly Stack<int> pressedKeys = new();
        private readonly int[] ctrlAlt = { 162, 164 };

        private readonly List<Combination> combinations = new();
        public event Action? VimModeEnabled;
        public event Action? VimModeDisabled;
        public event Action? MouseLeft;
        public event Action? MouseDown;
        public event Action? MouseUp;
        public event Action? MouseRight;
        public event Action? MouseLeftClick;
        public event Action? MouseRightClick;

        public MessageHandler() {
            combinations.Add(new Combination(ctrlAlt, ctrlAltPressed));
        }
        private void ctrlAltPressed() {
            VimModeEnabled?.Invoke();
            Console.WriteLine("Ctrl + Alt");
        }


        
        private void switchKey(int key) {
            switch (key) {
                case (int)Keys.ESC:
                    Console.WriteLine("Esc");
                    VimModeDisabled?.Invoke();
                break;
                case (int)Keys.H:
                    MouseLeft?.Invoke();
                    break;
                case (int)Keys.J:
                    MouseDown?.Invoke();
                    break;
                case (int)Keys.K:
                    MouseUp?.Invoke();
                    break;
                case (int)Keys.L:
                    MouseRight?.Invoke();
                    break;
                case (int)Keys.B:
                    MouseLeftClick?.Invoke();
                    break;
                case (int)Keys.N:
                    MouseRightClick?.Invoke();
                    break;
            }
        }

        private void switchCombination(int[] pressed) {
            foreach (Combination combo in combinations) {
                Array.Sort(pressed);
                if(Enumerable.SequenceEqual(pressed, combo.Combo)) {
                    combo.Action?.Invoke();
                }
            }
        }

        public void KeyDown(object? sender, KeyboardPressEventArgs e) {
            pressedKeys.Push(e.VirtKeyCode);
            var pressed = pressedKeys.ToArray();
            switchKey(pressedKeys.Peek());
            if (pressed.Length > 1) {
                switchCombination(pressed);
                pressedKeys.Clear();
            }
        }

        public void KeyUp(object? sender, KeyboardPressEventArgs e) {
            if (pressedKeys.Count > 0)
                pressedKeys.Clear();
        }
    }
}
