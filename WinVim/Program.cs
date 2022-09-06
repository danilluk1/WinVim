using System.Runtime.InteropServices;
using WinVim.BL;
using WinVim.BL.Common.Types;
using WinVim.BL.Windows;

public class Program {
    
    static WinVim.BL.Windows.NativeFeatures.POINT point;
    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos([In] ref WinVim.BL.Windows.NativeFeatures.POINT point);

    private static Settings settings = Settings.GetInstance();
    private static void Mh_MouseRightClick() {
        Console.WriteLine("Right Click");
        Mouse.MouseRightClick();
    }

    private static void Mh_MouseLeftClick() {
        Console.WriteLine("Left Click");
        Mouse.MouseLeftClick();
    }

    private static void Mh_VimModeEnabled() {
        var settings = Settings.GetInstance();

        if (!settings.IsInVim) {
            Console.WriteLine("Enabled");
            settings.IsInVim = true;
            NativeFeatures.BlockInput(true);
        } else {
            Console.WriteLine("Disabled");
            settings.IsInVim = false;
            NativeFeatures.BlockInput(false);
        }
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
            new Control(Direction.GetDirection(Directions.None), Keys.B),
            new Control(Direction.GetDirection(Directions.None), Keys.N)
        };
        //In asc order
        var arr = new Keys[] {Keys.LeftShift, Keys.LeftControl};
        settings.ToVimModeCombo = new Combination(
            arr,
            Mh_VimModeEnabled
        );

        MessageBroker mb = new();
        MessageHandler mh = new(settings);

        mh.VimModeEnabled += Mh_VimModeEnabled;
        mh.MouseLeftClick += Mh_MouseLeftClick;
        mh.MouseRightClick += Mh_MouseRightClick;
        mh.MouseMove += Mh_MouseMove;

        mb.KeyDown += mh.KeyDown;
        mb.KeyUp += mh.KeyUp;

        while (true) {
            mb.ProcessMessages();
        }
    }

    private static void Mh_MouseMove(Direction dir) {
        Console.WriteLine("Left");
        GetCursorPos(ref point);
        SetCursorPos(point.X + dir.dX * settings.SpeedX, point.Y - dir.dY * settings.SpeedY);
        Console.WriteLine(point.X + " " + point.Y);
        Console.WriteLine(dir);
    }
}