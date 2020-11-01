using System;
using System.Collections.Generic;
using System.Threading;
using BlainePain.Geometry;
using BlainePain.Navigation;
using BlainePain.Rail;

namespace BlainePain.Extensions
{
    public static class Extension
    {
        public static bool IsCollision(this IMoveable[] moveables)
        {
            var positions = new HashSet<Coord>();

            foreach(IMoveable moveable in moveables)
                foreach(Coord position in moveable.Positions)
                    if (!positions.Add(position))
                        return true;

            return false;
        }

        public static void MoveAll(this IMoveable[] moveables)
        {
            foreach(IMoveable moveable in moveables)
                moveable.Move();
        }

        public static Coord GetStart(this IGrid grid)
        {            
            var res = new GridNavigator().FindFirst(grid, c => TrackBuilder.IsTrackPiece(c));

            if (!res.IsInGrid)
                throw new InvalidOperationException($"Grid Start could not be found.");
                
            return res.NewPosition;        
        }

        public static void Visualise(this IGrid grid, IEnumerable<IGridable> gridables, bool visualise, bool firstTime)
        {
            if (visualise)
            {               
                foreach (IGridable x in gridables)
                    x.AddToGrid(grid);
                
                grid.PrintGrid();
                if (firstTime)
                    Thread.Sleep(1000);
                else
                    Thread.Sleep(100);
            }
        }


    }
}