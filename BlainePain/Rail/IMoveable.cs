using System.Collections.Generic;
using BlainePain.Geometry;

namespace BlainePain.Rail
{
    public interface IMoveable
    {
        void Move();

        IEnumerable<Coord> Positions { get; }
    }
}