using System;
using Xunit;
using BlainePain.Geometry;
using System.Text;

namespace BlainePain.Test
{
    public class TestCoord
    {
        [Fact]
        public void Pos1EqualsPos2()
        {
            Coord pos1 = new Coord(1,2);
            Coord pos2 = new Coord(1,2);

            Assert.True(pos1.Equals(pos2));
            Assert.True(pos1 == pos2);
        }

        [Fact]
        public void Pos1NotEqualsPos2()
        {
            Coord pos1 = new Coord(1,2);
            Coord pos2 = new Coord(2,1);

            Assert.False(pos1.Equals(pos2));
            Assert.True(pos1 != pos2);
        }
    }    
}
