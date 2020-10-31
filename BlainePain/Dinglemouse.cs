using System;
using System.Linq;
using System.Text;
using System.Threading;
using BlainePain.Geometry;
using BlainePain.Rail;
using BlainePain.Navigation;

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
        
        public static int TrainCrash(string trackString, string aTrain, int aTrainPos, string bTrain, int bTrainPos, int limit)
        {
            Console.WriteLine($"aTrain: {aTrain}. aTrainPos: {aTrainPos}. bTrain: {bTrain}. bTrainPos: {bTrainPos}. Limit {limit}.");
            var grid = new Grid(trackString);
            grid.PrintGrid();

            var start = GetStart(grid);
            var track = TrackBuilder.GetTrack(new Coord(start), grid);

            var trainA = new Train(aTrain, aTrainPos, track);
            var trainB = new Train(bTrain, bTrainPos, track);

            int timer = 0;
            do
            {
                if (Train.IsCollision(trainA, trainB, track))
                {
                    Console.WriteLine($"Collision at time {timer}.");
                    return timer;                
                }                
                // track.AddToGrid(grid);
                // trainA.AddToGrid(grid);
                // trainB.AddToGrid(grid);
                // grid.PrintGrid();
                // Thread.Sleep(100);
                trainA.Move();
                trainB.Move();
                timer++;
            }
            while (timer < limit);
            
            Console.WriteLine($"No Collision before {timer}.");
            return -1;
        }
    }
}