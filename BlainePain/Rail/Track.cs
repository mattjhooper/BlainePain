using System.Collections.Generic;
using BlainePain.Geometry;

namespace BlainePain.Rail
{
    public class Track : IGridable
    {
        private readonly List<(Coord pos, char val)> trackList;

        public int TrackLength => trackList.Count;
        
        public Track()
        {
            trackList = new List<(Coord, char)>();
        }
        public void AddTrackPiece(char piece, Coord gridPosition)
        {
            trackList.Add((gridPosition, piece));
        }

        public Coord GetGridPosition(int trackPosition)
        {
            return trackList[trackPosition].pos;
        }

        public char GetTrackPiece(int trackPosition)
        {
            return trackList[trackPosition].val;
        }

        public void AddToGrid(IGrid grid)
        {
            foreach((Coord pos, char val) in trackList)
            {
                grid.PutValue(pos, val);
            }
        }
    }
}