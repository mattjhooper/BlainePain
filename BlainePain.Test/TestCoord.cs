using System;
using Xunit;
using BlainePain.Geometry;
using System.Text;

namespace BlainePain.Test
{
    public class TestCoord
    {
        [Fact]
        public void CoordMovesUp()
        {
            // Arrange
            var pos = new Coord(1, 1);

            // Act
            pos.MoveUp(); 

            // Assert
            Assert.Equal(1, pos.x);
            Assert.Equal(0, pos.y);

        }

        [Fact]
        public void CoordMovesDown()
        {
            // Arrange
            var pos = new Coord(1, 1);

            // Act
            pos.MoveDown(); 

            // Assert
            Assert.Equal(1, pos.x);
            Assert.Equal(2, pos.y);

        }

        [Fact]
        public void CoordMovesLeft()
        {
            // Arrange
            var pos = new Coord(1, 1);

            // Act
            pos.MoveLeft(); 

            // Assert
            Assert.Equal(0, pos.x);
            Assert.Equal(1, pos.y);

        }

        [Fact]
        public void CoordMovesRight()
        {
            // Arrange
            var pos = new Coord(1, 1);

            // Act
            pos.MoveRight(); 

            // Assert
            Assert.Equal(2, pos.x);
            Assert.Equal(1, pos.y);

        }

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
