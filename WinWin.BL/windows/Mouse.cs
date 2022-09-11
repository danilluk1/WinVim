using System.Runtime.InteropServices;
using static WinVim.BL.Windows.NativeFeatures;

namespace WinVim.BL.Windows {
    public static class Mouse {
        public static void MouseLeftClick() {
            NativeFeatures.INPUT[] inputs = new NativeFeatures.INPUT[] {
            new NativeFeatures.INPUT {
                type = 0,
                u = new NativeFeatures.InputUnion {
                    mi = new NativeFeatures.MOUSEINPUT {
                        mouseData = 0,
                        time = 0,
                        dwFlags = NativeFeatures.MOUSEEVENTF.MOUSEEVENTF_LEFTDOWN,
                        dwExtraInfo = NativeFeatures.GetMessageExtraInfo()
                    }
                }
            },
            new NativeFeatures.INPUT {
                type = 0,
                u = new NativeFeatures.InputUnion {
                    mi = new NativeFeatures.MOUSEINPUT {
                        mouseData = 0,
                        time = 0,
                        dwFlags = NativeFeatures.MOUSEEVENTF.MOUSEEVENTF_LEFTUP,
                        dwExtraInfo = NativeFeatures.GetMessageExtraInfo()
                    }
                }
            }
        };
            NativeFeatures.SendInput(2, inputs, Marshal.SizeOf(typeof(NativeFeatures.INPUT)));
        }

        public static void SetCursorPosition(int x, int y) {
            NativeFeatures.SetCursorPos(x, y);
        }

        public static void MoveCursor(int dx, int dy) {
            POINT currentPoint = GetCursorPosition();
            SetCursorPos(currentPoint.X + dx, currentPoint.Y - dy);
        }

        public static POINT GetCursorPosition() {
            POINT point = new POINT();
            GetCursorPos(ref point);
            return point;
        }

        public static void MouseRightClick() {
            NativeFeatures.INPUT[] inputs = new NativeFeatures.INPUT[] {
            new NativeFeatures.INPUT {
                type = 0,
                u = new NativeFeatures.InputUnion {
                    mi = new NativeFeatures.MOUSEINPUT {
                        mouseData = 0,
                        time = 0,
                        dwFlags = NativeFeatures.MOUSEEVENTF.MOUSEEVENTF_RIGHTDOWN,
                        dwExtraInfo = NativeFeatures.GetMessageExtraInfo()
                    }
                }
            },
            new NativeFeatures.INPUT {
                type = 0,
                u = new NativeFeatures.InputUnion {
                    mi = new NativeFeatures.MOUSEINPUT {
                        mouseData = 0,
                        time = 0,
                        dwFlags = NativeFeatures.MOUSEEVENTF.MOUSEEVENTF_RIGHTUP,
                        dwExtraInfo = NativeFeatures.GetMessageExtraInfo()
                    }
                }
            }
        };
            NativeFeatures.SendInput(2, inputs, Marshal.SizeOf(typeof(NativeFeatures.INPUT)));
        }
    }
}
