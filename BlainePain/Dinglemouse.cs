using System;
using BlainePain.Grid;

namespace BlainePain
{
    public class Dinglemouse
    {
        // =======================================
        // Blaine is a pain, and that is the truth
        // =======================================
        
        private static ICoord GetStart(IGrid grid)
        {
            var pos = new Coord(0,0);
            char valueAtPos = grid.GetValue(pos);
            bool keepChecking = valueAtPos == ' ' && !grid.IsMaxExtent(pos);
            while (keepChecking)
            {
                if (pos.x < grid.MaxX)
                {
                    pos.MoveRight();
                }
                else if (pos.y < grid.MaxY)
                {
                    pos.MoveDown();
                    pos = new Coord(0, pos.y);
                }                
                valueAtPos = grid.GetValue(pos);
                keepChecking = valueAtPos == ' ' && !grid.IsMaxExtent(pos);                    
            }
            return pos;
        }
        public static int TrainCrash(string track, string aTrain, int aTrainPos, string bTrain, int bTrainPos, int limit)
        {
            // Your code here!
            Console.WriteLine($"track length: {track.Length}");
            var grid = new Grid.Grid(track);
            grid.PrintGrid();

            var start = GetStart(grid);
            grid.PutValue(start, 'H');
            grid.PrintGrid();
        
            return 0;
        }
    }
}