using System.Collections.Generic;
using BlainePain.Geometry;

namespace BlainePain.Rail
{
    public class Track : IGridable
    {
        private readonly List<(Coord pos, char val)> trackPieces;

        public int TrackLength => trackPieces.Count;
        
        public Track()
        {
            trackPieces = new List<(Coord, char)>();
        }
        public void AddTrackPiece(char piece, Coord gridPosition)
        {
            trackPieces.Add((gridPosition, piece));
        }

        public Coord GetGridPosition(int trackPosition)
        {
            return trackPieces[trackPosition].pos;
        }

        public char GetTrackPiece(int trackPosition)
        {
            return trackPieces[trackPosition].val;
        }

        public void AddToGrid(IGrid grid)
        {
            foreach((Coord pos, char val) in trackPieces)
            {
                grid.PutValue(pos, val);
            }
        }
    }
}