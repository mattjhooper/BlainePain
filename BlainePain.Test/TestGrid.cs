using System;
using Xunit;
using BlainePain.Geometry;
using System.Text;

namespace BlainePain.Test
{    
    public class TestGrid
    {
        [Fact]
        public void GridHasCorrectMaximums()
        {
            // Arrange
            var grid = new Grid("123\n456\n789");

            // Act
            var maxX = grid.MaxX;            
            var maxY = grid.MaxY; 

            // Assert
            Assert.Equal(2, maxX);
            Assert.Equal(2, maxY);

        }
    }
}
