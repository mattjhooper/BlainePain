using System;
using System.Collections.Generic;
using System.Linq;
using BlainePain.Geometry;

namespace BlainePain.Rail
{
    public class Track : IGridable
    {
        private readonly List<(Coord pos, char val)> trackPieces;
        private static readonly char?[] ValidTrackPieces = new char?[] {'-', '|', '/', '\\', '+', 'X', 'S'};

        public static bool IsTrackPiece(char? checkChar) => ValidTrackPieces.Contains(checkChar);

        public int TrackLength => trackPieces.Count;

        public override string ToString() => trackPieces.Select(p => p.val.ToString()).Aggregate("", (str, next) => str + next);
        
        public Track()
        {
            trackPieces = new List<(Coord, char)>();
        }
        public void AddTrackPiece(char piece, Coord gridPosition)
        {
            if(!IsTrackPiece(piece))
               throw new InvalidOperationException($"Invalid Track Piece specified: {piece}. At [{gridPosition.x},{gridPosition.y}].");
                 
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