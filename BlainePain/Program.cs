using System;
using System.Text;
using System.Threading;
using BlainePain.Geometry;
using BlainePain.Navigation;

namespace BlainePain
{
    class Program
    {
        static int Main(string[] args)
        {
            // string track = System.IO.File.ReadAllText(@"track.txt");           
            // int collideCounter;
            // collideCounter = Dinglemouse.TrainCrash(track, "Aaaa", 147, "Bbbbbbbbbbb", 288, 1000);
            // //collideCounter = Dinglemouse.TrainCrash(track, "Aaaa", 9, "Xxx", 2, 500);
            // return collideCounter;
            
            //[InlineData("|\n|\n\\-----", 0, 0, Direction.South, @"||\-----")]  
            string track = "|\n|\n\\-----";
            int x = 0;
            int y = 0;
            Direction startDirection = Direction.South;

            
            var grid = new Grid(track);
            Coord pos = new Coord(x, y);
            var direction = startDirection;
            var resultTrack = new StringBuilder();
            bool moreTrack = true;
            int i = 0;
            IGridNavigator nav = new GridNavigator();
            
            // Act
            do
            {
                char piece = grid.GetValue(pos);
                resultTrack.Append(piece);
                var res = Dinglemouse.GetNextTrackPiece(nav, grid, pos, direction);
                moreTrack = res.Found;
                pos = res.NextPos;
                direction = res.NextDir;
                i++; 

            } while (moreTrack && i < 20);


            return -1;
        }
    }
}
