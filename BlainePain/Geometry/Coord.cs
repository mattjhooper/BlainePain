using System;

namespace BlainePain.Geometry
{
    public record Coord (int x, int y)
    {
        public override string ToString() => $"[{x},{y}]";
    }  
}