using System;
using Xunit;
using BlainePain.Geometry;
using BlainePain.Navigation;
using System.Text;
using FluentAssertions;

namespace BlainePain.Test
{
    public class TestGridNavigation
    {
        private readonly Grid grid;
        public TestGridNavigation()
        {
            grid = new Grid("1N2\nWXE\n3S4");
        }
        
        [Theory]
        [InlineData(Direction.North, 1, 0, 'N')]       
        [InlineData(Direction.Northeast, 2, 0, '2')]
        [InlineData(Direction.East, 2, 1, 'E')]
        [InlineData(Direction.Southeast, 2, 2, '4')]
        [InlineData(Direction.South, 1, 2, 'S')]
        [InlineData(Direction.Southwest, 0, 2, '3')]
        [InlineData(Direction.West, 0, 1, 'W')]
        [InlineData(Direction.Northwest, 0, 0, '1')]
        public void IsInGrid(Direction direction, int expectedX, int expectedY, char expectedVal)
        {
            // Arrange
            var startPos = new Coord(1, 1);
            var sit = new GridNavigator();

            // Act
            var result = sit.CheckDirection(grid, startPos, direction);

            // Assert
            result.IsInGrid.Should().BeTrue();
            result.NewPosition.x.Should().Be(expectedX);
            result.NewPosition.y.Should().Be(expectedY);
            result.NewValue.Should().Be(expectedVal);
        }

        [Theory]
        [InlineData(Direction.North, 2, -1)]       
        [InlineData(Direction.Northeast, 3, -1)]
        [InlineData(Direction.East, 3, 0)]
        public void NotIsInGrid(Direction direction, int expectedX, int expectedY)
        {
            // Arrange
            var startPos = new Coord(2, 0);
            var sit = new GridNavigator();

            // Act
            var result = sit.CheckDirection(grid, startPos, direction);

            // Assert
            result.IsInGrid.Should().BeFalse();
            result.NewPosition.x.Should().Be(expectedX);
            result.NewPosition.y.Should().Be(expectedY);
            result.NewValue.Should().BeNull();
        }   

        [Fact] 
        public void CheckStartOfNextRowIsCorrect()
        {
            // Arrange
            var startPos = new Coord(1, 0);
            var sit = new GridNavigator();

            // Act
            var result = sit.CheckStartOfNextRow(grid, startPos);

            result.IsInGrid.Should().BeTrue();
            result.NewPosition.x.Should().Be(0);
            result.NewPosition.y.Should().Be(1);
            result.NewValue.Should().Be('W');
        }

        [Fact] 
        public void CheckFindFirstMatch()
        {
            // Arrange
            var sit = new GridNavigator();

            // Act
            var result = sit.FindFirst(grid, c => c == 'X');

            result.IsInGrid.Should().BeTrue();
            result.NewPosition.x.Should().Be(1);
            result.NewPosition.y.Should().Be(1);
            result.NewValue.Should().Be('X');
        }

        [Fact] 
        public void CheckFindFirstNoMatch()
        {
            // Arrange
            var sit = new GridNavigator();

            // Act
            var result = sit.FindFirst(grid, c => c == '-');

            result.IsInGrid.Should().BeFalse();
            result.NewPosition.x.Should().Be(2);
            result.NewPosition.y.Should().Be(2);
            result.NewValue.Should().BeNull();
        }    

    }
}
