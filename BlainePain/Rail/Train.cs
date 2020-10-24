using BlainePain.Geometry;

namespace BlainePain.Rail
{
    public class Train : IGridable
    {
        //private readonly List<(ICoord pos, char val)> trackList;

        private readonly Track track;
        private readonly bool isExpress;

        private readonly bool isClockwise;
        private readonly string trainStr;

        public int TrainTrackPosition { get; private set; }
        public string TrainString => trainStr;

        public ICoord TrainGridPosition { get; private set; }

        public bool IsExpress => isExpress;

        public bool IsClockwise => isClockwise;

        public int NoOfCarriages => TrainString.Length - 1;
        
        public Train(string trainStr, int startPos, Track track)
        {
            this.track = track;
            this.TrainTrackPosition = startPos;
            this.trainStr = trainStr;
            this.TrainGridPosition = track.GetGridPosition(TrainTrackPosition);
            this.isExpress = trainStr.ToLower()[0] == 'x';
            this.isClockwise = trainStr.ToLower()[0] == trainStr[0];

        }

        public void AddToGrid(IGrid grid)
        {
            throw new System.NotImplementedException();
        }
    }
}