using System;
using System.Collections.Generic;
using System.Linq;
using BlainePain.Geometry;

namespace BlainePain.Rail
{
    public class Track : IGridable
    {
        private readonly List<(Coord pos, char val)> trackPieces;

        private void CheckTrackPosition(int trackPosition)
        {
            if (trackPosition >= TrackLength)
                throw new InvalidOperationException($"Invalid Track Position specified: [{trackPosition}].");
        }
        
        public int TrackLength => trackPieces.Count;

        public override string ToString() => trackPieces.Select(p => p.val.ToString()).Aggregate("", (str, next) => str + next);
        
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
            CheckTrackPosition(trackPosition);
            return trackPieces[trackPosition].pos;
        }
        public int GetNextTrackPosition(int trackPosition, bool isClockwise)
        {
            CheckTrackPosition(trackPosition);            

            if (isClockwise)
                return trackPosition == TrackLength - 1? 0 : ++trackPosition;
            else
                return trackPosition == 0 ? TrackLength - 1 : --trackPosition;                
        }

        public char GetTrackPiece(int trackPosition)
        {
            CheckTrackPosition(trackPosition);
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