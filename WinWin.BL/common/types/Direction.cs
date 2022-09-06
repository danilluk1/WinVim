namespace WinVim.BL {
    public class Direction {
        private static Dictionary<Directions, Direction> directions = new() {
            { Directions.TopLeft, new Direction(-1, 1, Directions.TopLeft) },
            { Directions.Top, new Direction(0, 1, Directions.Top) },
            { Directions.TopRight, new Direction(1, 1, Directions.TopRight) },
            { Directions.Right, new Direction(1, 0, Directions.Right) },
            { Directions.BottomRight, new Direction(1, -1, Directions.BottomRight) },
            { Directions.Bottom, new Direction(0, -1, Directions.Bottom) },
            { Directions.BottomLeft, new Direction(-1, -1, Directions.BottomLeft) },
            { Directions.Left, new Direction(-1, 0, Directions.Left) },
            { Directions.None, new Direction(0, 0, Directions.None)}
        };
        public int dX { get; }
        public int dY { get; }

        public Directions direction { get; }

        private Direction(int dX, int dY, Directions direction) {
            this.dX = dX;
            this.dY = dY;
            this.direction = direction;
        }

        public static Direction GetDirection(Directions dir) {
            return directions.TryGetValue(dir, out Direction? direction) ? direction 
                : throw new ArgumentException("Unknown direction");
        }
    }
}
