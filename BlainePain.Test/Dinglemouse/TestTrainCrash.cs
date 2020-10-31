using Xunit;
using FluentAssertions;
using BlainePain;
using System.Collections.Generic;

namespace BlainePain.Test.TestDinglemouse
{    
    public class TestTrainCrash
    {
        
        private readonly List<string> tracks;
        public TestTrainCrash()
        {
            tracks = new List<string>();
            tracks.Add(System.IO.File.ReadAllText(@"Tracks\track0.txt"));
            tracks.Add(System.IO.File.ReadAllText(@"Tracks\track1.txt"));
            tracks.Add(System.IO.File.ReadAllText(@"Tracks\track2.txt"));
        }

        [Fact]
        public void Example1Works()
        {
            // Arrange
            
            // Act
            int crashTime = Dinglemouse.TrainCrash(tracks[0], "Aaaa", 147, "Bbbbbbbbbbb", 288, 1000);

            // Assert
            crashTime.Should().Be(516);

        }

        [Fact]
        public void CrashBeforeStarted()
        {
            // Arrange
            
            // Act
            int crashTime = Dinglemouse.TrainCrash(tracks[1], "Eeeeeeeeeeeeeeeeeeeeeeeeeeeeeee", 7, "Xxxx", 0, 100);

            // Assert
            crashTime.Should().Be(0);

        }

        [Fact]
        public void CrashCabooser()
        {
            // Arrange
            
            // Act
            int crashTime = Dinglemouse.TrainCrash(tracks[2], "aA", 10, "bbbbbB", 30, 200);

            // Assert
            crashTime.Should().Be(157);

        }
    }
}