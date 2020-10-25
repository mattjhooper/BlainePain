
using System;
using System.Linq;

namespace BlainePain.Geometry
{
    public class Grid : IGrid
    {
        private readonly string[] grid;

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

        public char GetValue(ICoord pos) => grid[pos.y][pos.x];
        
        public void PrintGrid()
        {
            //Console.Clear();  

            Console.SetCursorPosition(0, 0);              
            for (int y = 0; y <= MaxY; y++)
            {
                Console.WriteLine(grid[y]);
            }
        }

        public void PutValue(ICoord pos, char value)
        {
            char[] yChars = grid[pos.y].ToCharArray();
            yChars[pos.x] = value;
            grid[pos.y] = new string(yChars);
        }    

        public bool IsMaxExtent(ICoord pos) => pos.x == MaxX && pos.y == MaxY;
    }
}