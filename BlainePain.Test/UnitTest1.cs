using System;
using Xunit;
using BlainePain.Grid;
using System.Text;

namespace BlainePain.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }
    }

    public class TestGrid
    {
        [Fact]
        public void GridHasCorrectMaximums()
        {
            // Arrange
            var grid = new Grid.Grid("123\n456\n789");

            // Act
            var maxX = grid.MaxX;            
            var maxY = grid.MaxY; 

            // Assert
            Assert.Equal(2, maxX);
            Assert.Equal(2, maxY);

        }
    }

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

    public class TestFindNext
    {
        [Theory]
        [InlineData("----------", 0, 0, Direction.East, "----------")]
        [InlineData("|\n|\n|", 0, 0, Direction.South, "|||")]
        [InlineData("----------", 9, 0, Direction.West, "----------")]        
        [InlineData("|\n|\n|", 0, 2, Direction.North, "|||")]
        [InlineData("\\\n \\\n  \\", 0, 0, Direction.Southeast, @"\\\")]
        [InlineData("\\\n \\\n  \\", 2, 2, Direction.Northwest, @"\\\")]
        [InlineData("  /\n /\n/", 0, 2, Direction.Northeast, @"///")]
        [InlineData("  /\n /\n/", 2, 0, Direction.Southwest, @"///")]
        [InlineData("|\n|\n\\-----", 0, 0, Direction.South, @"||\-----")]        
        [InlineData("|\n|\n\\-----", 5, 2, Direction.West, @"-----\||")]
        [InlineData("     |\n     |\n-----/", 0, 2, Direction.East, @"-----/||")]
        [InlineData("     |\n     |\n-----/", 5, 0, Direction.South, @"||/-----")]  
        [InlineData("/-----\n|\n|", 0, 2, Direction.North, @"||/-----")]
        [InlineData("/-----\n|\n|", 5, 0, Direction.West, @"-----/||")] 
        [InlineData("-----\\ \n     | \n     | ", 0, 0, Direction.East, @"-----\||")]
        [InlineData("-----\\ \n     | \n     | ", 5, 2, Direction.North, @"||\-----")] 
        [InlineData("-----\\\n      \\-----", 0, 0, Direction.East, @"-----\\-----")]
        [InlineData("-----\\\n      \\-----", 11, 1, Direction.West, @"-----\\-----")]
        [InlineData(" |\n /\n/\n|", 1, 0, Direction.South, @"|//|")]
        [InlineData(" |\n /\n/\n|", 0, 3, Direction.North, @"|//|")]
        [InlineData("   |\n---+---\n   |", 3, 0, Direction.South, @"|+|")]
        [InlineData("   |\n---+---\n   |", 0, 1, Direction.East, @"---+---")]
        [InlineData("  \\ /\n   X\n  / \\", 2, 0, Direction.Southeast, @"\X\")]
        [InlineData("  \\ /\n   X\n  / \\", 2, 2, Direction.Northeast, @"/X/")]
        [InlineData("   /\n---+---\n   /", 3, 2, Direction.North, @"/+/")]
        [InlineData("   /\n---+---\n   /", 3, 0, Direction.Southwest, @"/+/")]
        [InlineData("   | /\n  /+/\n / |", 1, 2, Direction.Northeast, @"//+//")]
        public void TestTracks(string track, int x, int y, Direction startDirection, String desiredResult)
        {
            // Arrange
            var grid = new Grid.Grid(track);
            ICoord pos = new Coord(x, y);
            var direction = startDirection;
            var resultTrack = new StringBuilder();
            bool moreTrack = true;
            int i = 0;
            
            // Act
            do
            {
                char piece = grid.GetValue(pos);
                resultTrack.Append(piece);
                moreTrack = Dinglemouse.UpdatePositionAndDirection(grid, ref pos, ref direction);
                i++; 

            } while (moreTrack && i < 20);
            
            // Assert
            Assert.Equal(desiredResult, resultTrack.ToString());            
        }
    }
}
