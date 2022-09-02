namespace WinVim.BL {
    public class Direction {
        private static Dictionary<Directions, Direction> directions = new() {
            { Directions.TopLeft, new Direction(-1, 1) },
            { Directions.Top, new Direction(0, 1) },
            { Directions.TopRight, new Direction(1, 1) },
            { Directions.Right, new Direction(1, 0) },
            { Directions.BottomRight, new Direction(1, -1) },
            { Directions.Bottom, new Direction(0, -1) },
            { Directions.BottomLeft, new Direction(-1, -1) },
            { Directions.Left, new Direction(-1, 0) },
        };
        public int dX { get; }
        public int dY { get; }

        private Direction(int dX, int dY) {
            this.dX = dX;
            this.dY = dY;
        }

        public static Direction GetDirection(Directions dir) {
            return directions.TryGetValue(dir, out Direction? direction) ? direction 
                : throw new ArgumentException("Unknown direction");
        }
    }
}
