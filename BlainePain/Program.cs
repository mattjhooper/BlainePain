using System;
using System.Threading;

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

            var direction = Direction.North;

            Console.WriteLine($"Direction: {direction}");

            direction--;

            Console.WriteLine($"Direction: {direction}");


            //Dinglemouse.TrainCrash(track, "Aaaa", 147, "Bbbbbbbbbbb", 288, 1000);
            // Console.Write(text);
            // Thread.Sleep(1000);
            // Console.Clear();
            // Console.Write(text);
            // Thread.Sleep(1000);
            // Console.Clear();
            // Console.Write(text);
            



        }
    }
}
