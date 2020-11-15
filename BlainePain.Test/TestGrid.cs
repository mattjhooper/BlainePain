using System;
using Xunit;
using BlainePain.Geometry;
using System.Text;
using FluentAssertions;

namespace BlainePain.Test
{
    public class TestGrid
    {
        private readonly Grid grid;
        public TestGrid()
        {
            grid = new Grid("123\n456\n789");
        }

        [Theory]
        [InlineData("123\n456\n789", 2, 2)]
        [InlineData("1\n45\n789", 2, 2)]
        [InlineData("12345", 4, 0)]
        public void GridHasCorrectMaximums(string gridString, int desiredX, int desiredY)
        {
            // Arrange
            var grid = new Grid(gridString);

            // Act
            // Assert
            grid.MaxX.Should().Be(desiredX);
            grid.MaxY.Should().Be(desiredY);
        }

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(0, 0, true)]
        [InlineData(0, 2, true)]
        [InlineData(2, 0, true)]
        [InlineData(2, 2, true)]
        [InlineData(0, -1, false)]
        [InlineData(-1, 1, false)]
        [InlineData(0, 3, false)]
        [InlineData(3, 0, false)]
        [InlineData(3, 3, false)]
        public void GridIsInGrid(int checkX, int checkY, bool checkResult)
        {
            // Arrange
            Coord checkPos = new (checkX, checkY);

            // Act
            var isInGrid = grid.IsInGrid(checkPos);

            // Assert
            isInGrid.Should().Be(checkResult);
        }

        [Fact]
        public void GetValueForInvalidPositionThrowsException()
        {
            Action act = () => grid.GetValue(new (-1, 0));

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Invalid Position specified: [-1,0].");
        }

        [Fact]
        public void PutValueAndGetValue()
        {
            var pos = new Coord(1,1);

            // Assert
            grid.GetValue(pos).Should().Be('5');

            grid.PutValue(pos, 'X');

            grid.GetValue(pos).Should().Be('X');
        }

        [Fact]
        public void PutValueForInvalidPositionThrowsException()
        {
            Action act = () => grid.PutValue(new Coord(-1, 0), 'X');

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Invalid Position specified: [-1,0].");
        }
    }
}
