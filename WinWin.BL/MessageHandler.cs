using WinVim.BL.Common.Events;
using WinVim.BL.Common.Types;

namespace WinVim.BL {

    public class MessageHandler {
        private readonly Settings settings;
        private readonly Stack<Keys> pressedKeys = new();

        public event Action? MouseLeftClick;
        public event Action? MouseRightClick;
        public event Action<Direction>? MouseMove;
        public MessageHandler(Settings settings) {
            this.settings = settings;
        }

        
        private void SwitchKey(Keys key) {
            if (!settings.IsInVim) return;

            var control = settings.Controls.FirstOrDefault((control) => control.Key == key);
            if (control == null) return;

            if(control.Key == settings.MouseLeft) {
                MouseLeftClick?.Invoke();
                return;
            }
            else if(control.Key == settings.MouseRight) {
                MouseRightClick?.Invoke();
                return;
            }

            MouseMove?.Invoke(control.Direction);
        }

        private void SwitchCombination(Keys[] pressed) {
            var combo = settings.ToVimModeCombo;
            if (combo == null) return;

            Array.Sort(pressed);
            if (Enumerable.SequenceEqual(pressed, combo?.Combo)) {
                combo.Action?.Invoke();
            }
        }

        public void KeyDown(object? sender, KeyboardPressEventArgs e) {
            pressedKeys.Push((Keys)e.VirtKeyCode);
            var pressed = pressedKeys.ToArray();
            SwitchKey(pressedKeys.Peek());
            if (pressed.Length > 1) {
                SwitchCombination(pressed);
                pressedKeys.Clear();
            }
        }

        public void KeyUp(object? sender, KeyboardPressEventArgs e) {
            if (pressedKeys.Count > 0)
                pressedKeys.Clear();
        }
    }
}
