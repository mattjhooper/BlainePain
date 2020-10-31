using System;
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
            var t = trainStr.ToCharArray();

            if (IsClockwise)
                Array.Reverse (t);

            foreach(char c in t)
            {
                Coord gridPos = track.GetGridPosition(trackPos);
                grid.PutValue(gridPos, c);

                // have to reverse direction as drawing the train so the next position is opposite of the train direction
                trackPos = track.GetNextTrackPosition(trackPos, !IsClockwise);
            }
        }

        public void Move()
        {
            if (TimeRemainingAtStation > 0)
            {
                TimeRemainingAtStation--;
                return;
            }

            TrainTrackPosition = track.GetNextTrackPosition(TrainTrackPosition, IsClockwise);

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
                Coord gridPos = track.GetGridPosition(trackPos);
                trainAPositions.Add(gridPos);
                // have to reverse direction as drawing the train so the next position is opposite of the train direction
                trackPos = track.GetNextTrackPosition(trackPos, !trainA.IsClockwise);                                
            } 

            trackPos = trainB.TrainTrackPosition;
            foreach(char c in trainB.TrainString.ToCharArray())
            {
                Coord gridPos = track.GetGridPosition(trackPos);
                if (trainAPositions.Contains(gridPos))
                    return true;
                // have to reverse direction as drawing the train so the next position is opposite of the train direction
                trackPos = track.GetNextTrackPosition(trackPos, !trainB.IsClockwise); 
            } 

            return false;
        }
    }
}