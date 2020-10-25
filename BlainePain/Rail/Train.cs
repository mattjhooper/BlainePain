using System.Collections.Generic;
using BlainePain.Geometry;

namespace BlainePain.Rail
{
    public class Train : IGridable
    {
        private readonly Track track;
        private readonly bool isExpress;

        private readonly bool isClockwise;
        private readonly string trainStr;

        public int TrainTrackPosition { get; private set; }
        public string TrainString => trainStr;

        public bool IsExpress => isExpress;

        public bool IsClockwise => isClockwise;

        public int NoOfCarriages => TrainString.Length - 1;

        public int TimeRemainingAtStation { get; private set; }
        
        public Train(string trainStr, int startPos, Track track)
        {
            this.track = track;
            this.TrainTrackPosition = startPos;
            this.trainStr = trainStr;
            this.isExpress = trainStr.ToLower()[0] == 'x';
            this.isClockwise = trainStr.ToLower()[0] == trainStr[0];
            this.TimeRemainingAtStation = 0;

        }

        public void AddToGrid(IGrid grid)
        {
            int trackPos = TrainTrackPosition;
            foreach(char c in trainStr.ToCharArray())
            {
                trackPos = trackPos == track.TrackLength ? 0 : trackPos;
                Coord gridPos = track.GetGridPosition(trackPos);
                grid.PutValue(gridPos, c);
                trackPos++;
            }
        }

        public void Move()
        {
            if (TimeRemainingAtStation > 0)
            {
                TimeRemainingAtStation--;
                return;
            }

            if (IsClockwise)
            {
               TrainTrackPosition = TrainTrackPosition == track.TrackLength - 1 ? 0 : TrainTrackPosition + 1;
            }
            else 
            {
               TrainTrackPosition = TrainTrackPosition == 0 ? track.TrackLength - 1: TrainTrackPosition - 1;
            }
            
            var charAtGridPos = track.GetTrackPiece(TrainTrackPosition);

            if (charAtGridPos == 'S' && !IsExpress)
                TimeRemainingAtStation = NoOfCarriages;

        }

        public static bool IsCollision(Train trainA, Train trainB, Track track)
        {
            int trackPos = trainA.TrainTrackPosition;
            var trainAPositions = new HashSet<Coord>();

            foreach(char c in trainA.TrainString.ToCharArray())
            {
                trackPos = trackPos == track.TrackLength ? 0 : trackPos;
                Coord gridPos = track.GetGridPosition(trackPos);
                trainAPositions.Add(gridPos);
                trackPos++;
            } 

            trackPos = trainB.TrainTrackPosition;
            foreach(char c in trainB.TrainString.ToCharArray())
            {
                trackPos = trackPos == track.TrackLength ? 0 : trackPos;
                Coord gridPos = track.GetGridPosition(trackPos);
                if (trainAPositions.Contains(gridPos))
                    return true;
                trackPos++;
            } 

            return false;
        }
    }
}