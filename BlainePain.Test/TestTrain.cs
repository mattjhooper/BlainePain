using System;
using Xunit;
using BlainePain.Geometry;
using BlainePain.Rail;
using FluentAssertions;


namespace BlainePain.Test
{
    public class TestTrain
    {
        private readonly Track track;
        public TestTrain()
        {
            IGrid grid = new Grid("/-------\\\n|       |\n|       |\n\\-------/\n");
            Coord start = Dinglemouse.GetStart(grid);
            track = TrackBuilder.GetTrack(start, grid);
        }
        [Fact]
        public void IsClockwise()
        {
            // Arrange
            var sit = new Train("aaaA", 2, track);

            // Act            
            // Assert
            Assert.True(sit.IsClockwise);            

        }

        [Fact]
        public void IsAntiClockwise()
        {
            // Arrange
            var sit = new Train("Baaaa", 2, track);

            // Act            
            // Assert
            Assert.False(sit.IsClockwise);            

        }

        [Theory]
        [InlineData("xxxxX")]
        [InlineData("Xx")]
        public void IsExpress(string trainStr)
        {
            // Arrange
            var sit = new Train(trainStr, 2, track);

            // Act            
            // Assert
            Assert.True(sit.IsExpress);            

        }

        [Theory]
        [InlineData("xxxxX")]
        [InlineData("Xxxxx")]
        public void IsTrainString(string trainStr)
        {
            // Act            
            // Assert
            // Arrange
            new Train(trainStr, 2, track).TrainString.Should().Be("Xxxxx");           
        }

        [Theory]
        [InlineData("aaaA")]
        [InlineData("Bbbb")]
        [InlineData("cccccC")]
        public void IsSuburban (string trainStr)
        {
            // Arrange
            var sit = new Train(trainStr, 2, track);

            // Act            
            // Assert
            Assert.False(sit.IsExpress);            

        }

        [Theory]
        [InlineData("aaaA", 3)]
        [InlineData("Aaa", 2)]
        [InlineData("bbbbB", 4)]
        [InlineData("Bbbbbb", 5)]
        public void HasCorrectLength(string trainStr, int noOfCarriages)
        {
            // Arrange
            var sit = new Train(trainStr, 2, track);

            // Act            
            // Assert
            Assert.Equal(noOfCarriages, sit.NoOfCarriages);            
        }
    }
}
