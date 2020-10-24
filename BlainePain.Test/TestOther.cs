using System;
using Xunit;
using BlainePain.Geometry;
using System.Text;

namespace BlainePain.Test
{
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
            var grid = new Grid(track);
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
