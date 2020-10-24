using System.Collections.Generic;
using BlainePain.Geometry;

namespace BlainePain.Rail
{
    public class Track
    {
        private readonly List<(ICoord pos, char val)> trackList;
        
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
    }
}