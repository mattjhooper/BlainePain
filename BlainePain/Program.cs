﻿using System;
using System.Text;
using System.Threading;
using BlainePain.Grid;

namespace BlainePain
{
    public enum Direction
    {
        North,
        Northeast,
        East,
        Southeast,
        South,
        Southwest,
        West,
        Northwest

    }

    

    class Program
    {
        static void Main(string[] args)
        {
            string track = System.IO.File.ReadAllText(@"track.txt");           
            Dinglemouse.TrainCrash(track, "Aaaa", 147, "Bbbbbbbbbbb", 288, 1000);
            // // Console.Write(text);
            // Thread.Sleep(1000);
            // Console.Clear();
            // Console.Write(text);
            // Thread.Sleep(1000);
            // Console.Clear();
            // Console.Write(text);

            // [InlineData("   /\n---+---\n   /", 3, 0, Direction.Southwest, @"/+/")]
            // string track = "   /\n---+---\n   /";
            // int x = 3;
            // int y = 0;
            // Direction startDirection = Direction.Southwest;

            
            // var grid = new Grid.Grid(track);
            // ICoord pos = new Coord(x, y);
            // var direction = startDirection;
            // var resultTrack = new StringBuilder();
            // bool moreTrack = true;
            // int i = 0;
            
            // // Act
            // do
            // {
            //     char piece = grid.GetValue(pos);
            //     resultTrack.Append(piece);
            //     moreTrack = Dinglemouse.UpdatePositionAndDirection(grid, ref pos, ref direction);
            //     i++; 

            // } while (moreTrack && i < 20);



        }
    }
}
