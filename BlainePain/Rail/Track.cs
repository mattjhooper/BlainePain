using System.Collections.Generic;
using BlainePain.Geometry;

namespace BlainePain.Rail
{
    public class Track : IGridable
    {
        private readonly List<(ICoord pos, char val)> trackList;

        public int TrackLength => trackList.Count;
        
        public Track()
        {
            trackList = new List<(ICoord, char)>();
        }
        public void AddTrackPiece(char piece, ICoord gridPosition)
        {
            trackList.Add((gridPosition, piece));
        }

        public ICoord GetGridPosition(int trackPosition)
        {
            return trackList[trackPosition].pos;
        }

        public char GetTrackPiece(int trackPosition)
        {
            return trackList[trackPosition].val;
        }

        public void AddToGrid(IGrid grid)
        {
            foreach((ICoord pos, char val) in trackList)
            {
                grid.PutValue(pos, val);
            }
        }
    }
}