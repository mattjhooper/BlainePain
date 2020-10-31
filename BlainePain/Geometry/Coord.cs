using System;

namespace BlainePain.Geometry
{
    public struct Coord
    {
        public int x { get; private set; }
        public int y { get; private set; }

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Coord(Coord coord) : this(coord.x, coord.y) {}            

        public override bool Equals (Object obj)
        {
            if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else {
                Coord p = (Coord) obj;
                return (x == p.x) && (y == p.y);
            } 
        }

        public override int GetHashCode()
        {
            return Tuple.Create(x, y).GetHashCode();
        }

        public static bool operator ==(Coord lhs, Coord rhs)
        {
            // Check for null on left side.
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    // null == null = true.
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Coord lhs, Coord rhs)
        {
            return !(lhs == rhs);
        }
    }
}