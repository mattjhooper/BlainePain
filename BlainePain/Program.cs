﻿using System;
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
            string track = System.IO.File.ReadAllText(@"Tracks\track1.txt");           
            int collideCounter;
            //collideCounter = Dinglemouse.TrainCrash(track, "Aaaa", 147, "Bbbbbbbbbbb", 288, 1000);
            collideCounter = Dinglemouse.TrainCrash(track, "Eeeeeeeeeeeeeeeeeeeeeeeeeeeeeee", 7, "Xxxx", 0, 100);
            return collideCounter;                        
        }
    }
}
