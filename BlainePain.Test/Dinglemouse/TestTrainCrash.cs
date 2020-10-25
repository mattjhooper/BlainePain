using Xunit;
using FluentAssertions;
using BlainePain;

namespace BlainePain.Test.TestDinglemouse
{    
    public class TestTrainCrash
    {
        
        private readonly string TRACK1;
        public TestTrainCrash()
        {
            TRACK1 = System.IO.File.ReadAllText(@"Tracks\track1.txt");
        }

        [Fact]
        public void Example1Works()
        {
            // Arrange
            
            // Act
            int crashTime = Dinglemouse.TrainCrash(TRACK1, "Aaaa", 147, "Bbbbbbbbbbb", 288, 1000);

            // Assert
            crashTime.Should().Be(516);

        }
    }
}