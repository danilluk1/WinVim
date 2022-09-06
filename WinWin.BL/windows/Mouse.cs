using System.Runtime.InteropServices;

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
