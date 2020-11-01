using System;
using System.Collections.Generic;
using BlainePain.Geometry;

namespace BlainePain.Rail
{
    public class Train : IGridable, IMoveable
    {
        private readonly Track track;
        private readonly bool isExpress;

        private readonly bool isClockwise;
        private readonly char[] train;

        public int TrainTrackPosition { get; private set; }
        public string TrainString => new string(train);

        public bool IsExpress => isExpress;

        public bool IsClockwise => isClockwise;

        public int NoOfCarriages => train.Length - 1;

        public int TimeRemainingAtStation { get; private set; }

        public IEnumerable<Coord> Positions
        {
            get
            {
                int trackPos = TrainTrackPosition;
                foreach (char c in train)
                {
                    Coord gridPos = track.GetGridPosition(trackPos);
                    yield return gridPos;

                    // have to reverse direction as drawing the train so the next position is opposite of the train direction
                    trackPos = track.GetNextTrackPosition(trackPos, !IsClockwise);
                }
            }
        }

        public Train(string trainStr, int startPos, Track track)
        {
            this.track = track;
            this.TrainTrackPosition = startPos;
            this.train = trainStr.ToCharArray();
            this.isClockwise = trainStr.ToLower()[0] == trainStr[0];

            if (this.isClockwise)
                Array.Reverse(this.train);

            this.isExpress = train[0] == 'X';
            this.TimeRemainingAtStation = 0;

        }

        public void AddToGrid(IGrid grid)
        {
            int trackPos = TrainTrackPosition;

            foreach (char c in train)
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
    }
}