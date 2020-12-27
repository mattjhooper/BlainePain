
using System;
using System.Linq;

namespace BlainePain.Geometry
{
    public class Grid : IGrid
    {
        private readonly string[] grid;

        private char GetValue(Coord pos)
        {
            if (!IsInGrid(pos))
                throw new InvalidOperationException($"Invalid Position specified: [{pos.x},{pos.y}].");

            return grid[pos.y][pos.x];
        }

        private void PutValue(Coord pos, char value)
        {
            if (!IsInGrid(pos))
                throw new InvalidOperationException($"Invalid Position specified: [{pos.x},{pos.y}].");

            char[] yChars = grid[pos.y].ToCharArray();
            yChars[pos.x] = value;
            grid[pos.y] = new string(yChars);
        }

        public Grid(string gridSource)
        {
            grid = gridSource.Split('\n');
            int maxWidth = grid.Aggregate(0, (currMax, next) => next.Length > currMax ? next.Length : currMax);

            for (int line = 0; line < grid.Length; line++)
            {
                grid[line] = grid[line].PadRight(maxWidth, ' ');
            }
        }

        public int MaxX => grid[0].Length - 1;

        public int MaxY => grid.Length - 1;

        public char this[Coord pos]
        {
            get => GetValue(pos);
            set => PutValue(pos, value);
        }

        public void PrintGrid()
        {
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y <= MaxY; y++)
            {
                Console.WriteLine(grid[y]);
            }
        }

        public void ClearGrid()
        {
            Console.Clear();

            for (int y = 0; y <= MaxY; y++)
            {
                grid[y] = new string(' ', grid[y].Length);
            }
        }

        public bool IsMaxExtent(Coord pos) => pos.x == MaxX && pos.y == MaxY;

        public bool IsInGrid(Coord pos)
        {
            return 0 <= pos.x && pos.x <= MaxX && 0 <= pos.y && pos.y <= MaxY;
        }
    }
}