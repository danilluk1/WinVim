using WinVim.BL.common.events;
using WinVim.BL.common.types;

namespace WinVim.BL {

    public class MessageHandler {
        private readonly Stack<Keys> pressedKeys = new();
        private readonly Keys[] ctrlAlt = { Keys.LeftMenu, Keys.LeftShift };

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


        
        private void switchKey(Keys key) {
            switch (key) {
                case Keys.Escape:
                    Console.WriteLine("Esc");
                    VimModeDisabled?.Invoke();
                break;
                case Keys.H:
                    MouseLeft?.Invoke();
                    break;
                case Keys.J:
                    MouseDown?.Invoke();
                    break;
                case Keys.K:
                    MouseUp?.Invoke();
                    break;
                case Keys.L:
                    MouseRight?.Invoke();
                    break;
                case Keys.B:
                    MouseLeftClick?.Invoke();
                    break;
                case Keys.N:
                    MouseRightClick?.Invoke();
                    break;
            }
        }

        private void switchCombination(Keys[] pressed) {
            foreach (Combination combo in combinations) {
                Array.Sort(pressed);
                if(Enumerable.SequenceEqual(pressed, combo.Combo)) {
                    combo.Action?.Invoke();
                }
            }
        }

        public void KeyDown(object? sender, KeyboardPressEventArgs e) {
            pressedKeys.Push((Keys)e.VirtKeyCode);
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
