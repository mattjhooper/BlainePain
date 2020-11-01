using System;
using System.Linq;
using System.Text;
using System.Threading;
using BlainePain.Geometry;
using BlainePain.Rail;
using BlainePain.Navigation;
using System.Collections.Generic;

namespace BlainePain
{
    public class Dinglemouse
    {
        // =======================================
        // Blaine is a pain, and that is the truth
        // =======================================
        public static Coord GetStart(IGrid grid)
        {            
            var res = new GridNavigator().FindFirst(grid, c => TrackBuilder.IsTrackPiece(c));

            if (!res.IsInGrid)
                throw new InvalidOperationException($"Grid Start could not be found.");
                
            return res.NewPosition;        
        }

        public static void Visualise(IGrid grid, List<IGridable> gridables, bool visualise, bool firstTime)
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

        public static List<IGridable> SetupGridables(params IGridable[] list)
        {
            var gridables = new List<IGridable>();
            foreach (IGridable x in list)
                    gridables.Add(x);
            
            return gridables;
        }
        
        public static int TrainCrash(string trackString, string aTrain, int aTrainPos, string bTrain, int bTrainPos, int limit, bool visualise = false)
        {
            Console.WriteLine($"aTrain: {aTrain}. aTrainPos: {aTrainPos}. bTrain: {bTrain}. bTrainPos: {bTrainPos}. Limit {limit}.");
            var grid = new Grid(trackString);
            grid.PrintGrid();

            var start = GetStart(grid);
            var track = TrackBuilder.GetTrack(new Coord(start), grid);

            var trainA = new Train(aTrain, aTrainPos, track);
            var trainB = new Train(bTrain, bTrainPos, track);

            var gridables = SetupGridables(track, trainA, trainB);
            
            grid.ClearGrid();
            
            int timer = 0;
            do
            {
                Visualise(grid, gridables, visualise, timer == 0);

                if (Train.IsCollision(trainA, trainB, track))
                {
                    Console.WriteLine($"Collision at time {timer}.");
                    return timer;                
                } 
                
                trainA.Move();
                trainB.Move();
                timer++;
            }
            while (timer <= limit);
            
            Console.WriteLine($"No Collision before {timer}.");
            return -1;
        }
    }
}