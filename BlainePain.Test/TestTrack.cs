using System;
using Xunit;
using BlainePain.Geometry;
using BlainePain.Rail;
using FluentAssertions;


namespace BlainePain.Test
{
    public class TestTrack
    {
        private readonly Track track;
        private readonly string trackStr = "/-------\\\n|       |\n|       |\n\\-------/\n";
        public TestTrack()
        {
            IGrid grid = new Grid(trackStr);
            Coord start = Dinglemouse.GetStart(grid);
            track = TrackBuilder.GetTrack(start, grid);
        }

        [Fact]
        public void IsTrackLength()
        {
            track.TrackLength.Should().Be(22);
        }

        [Fact]
        public void IsToString()
        {
            track.ToString().Should().Be("/-------\\||/-------\\||");
        }
        
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 0)]
        [InlineData(8, 8, 0)]
        [InlineData(9, 8, 1)]
        [InlineData(21, 0, 1)]
        public void IsGridPosition(int trackPos, int expectedX, int expectedY)
        {
            // Arrange            

            // Act    
            var pos = track.GetGridPosition(trackPos);
            // Assert
            pos.x.Should().Be(expectedX);          
            pos.y.Should().Be(expectedY);        

        }

        [Fact]
        public void InvalidGridPositionThrowsException()
        {
            Action act = () => track.GetGridPosition(22);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Invalid Track Position specified: [22].");
        }

        [Theory]
        [InlineData(0, '/')]
        [InlineData(1, '-')]
        [InlineData(8, '\\')]
        [InlineData(9, '|')]
        [InlineData(21, '|')]
        public void IsTrackPiece(int trackPos, char expectedPiece)
        {
            track.GetTrackPiece(trackPos).Should().Be(expectedPiece);            
        }

        [Fact]
        public void InvalidGetTrackPieceThrowsException()
        {
            Action act = () => track.GetTrackPiece(22);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Invalid Track Position specified: [22].");
        }

        [Theory]
        [InlineData(0, true, 1)]
        [InlineData(21, false, 20)]
        [InlineData(0, false, 21)]
        [InlineData(21, true, 0)]
        public void IsGetNextTrackPosition(int currentPos, bool isClockWise, int expectedPos)
        {
            track.GetNextTrackPosition(currentPos, isClockWise).Should().Be(expectedPos);            
        }

        [Fact]
        public void InvalidGetNextTrackPosition()
        {
            Action act = () => track.GetNextTrackPosition(22, true);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Invalid Track Position specified: [22].");

            act = () => track.GetNextTrackPosition(22, false);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Invalid Track Position specified: [22].");                
        }
    }
}
