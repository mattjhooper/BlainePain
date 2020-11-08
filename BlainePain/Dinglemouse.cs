using System;
using BlainePain.Geometry;
using BlainePain.Rail;
using BlainePain.Extensions;
using System.Linq;

namespace BlainePain
{
    public class Dinglemouse
    {
        // =======================================
        // Blaine is a pain, and that is the truth
        // =======================================
        public static int TrainCrash(string trackString, string aTrain, int aTrainPos, string bTrain, int bTrainPos, int limit, bool visualise = false)
        {
            Console.WriteLine($"aTrain: {aTrain}. aTrainPos: {aTrainPos}. bTrain: {bTrain}. bTrainPos: {bTrainPos}. Limit {limit}.");
            var grid = new Grid(trackString);
            grid.PrintGrid();

            var start = grid.GetStart();
            var track = TrackBuilder.GetTrack(start, grid);

            var trainA = new Train(aTrain, aTrainPos, track);
            var trainB = new Train(bTrain, bTrainPos, track);
            
            var gridables = new IGridable[] {track, trainA, trainB};
            var moveables = gridables.OfType<IMoveable>();            
            
            grid.ClearGrid();
            
            int timer = 0;
            do
            {
                grid.Visualise(gridables, visualise, timer == 0);

                if (moveables.IsCollision())
                {
                    Console.WriteLine($"Collision at time {timer}.");
                    return timer;                
                } 
                
                moveables.MoveAll();
                timer++;
            }
            while (timer <= limit);
            
            Console.WriteLine($"No Collision before {timer}.");
            return -1;
        }
    }
}