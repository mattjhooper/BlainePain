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
            int collideCounter;
            string track;

            string option = args.Length == 0 ? "0" : args[0];

            switch(option)
            {
                case "0":
                    track = System.IO.File.ReadAllText($"Tracks\\track{option}.txt"); 
                    collideCounter = Dinglemouse.TrainCrash(track, "Aaaa", 147, "Bbbbbbbbbbb", 288, 1000, true);
                    break;
                case "1":
                    track = System.IO.File.ReadAllText($"Tracks\\track{option}.txt"); 
                    collideCounter = Dinglemouse.TrainCrash(track, "aA", 10, "bbbbbB", 30, 200, true); 
                    break;

                case "2":
                    track = System.IO.File.ReadAllText($"Tracks\\track{option}.txt"); 
                    collideCounter = Dinglemouse.TrainCrash(track, "Eee", 10, "aaA", 20, 100, true);
                    break;  
                case "3":
                    track = System.IO.File.ReadAllText($"Tracks\\track{option}.txt"); 
                    collideCounter = Dinglemouse.TrainCrash(track, "Eee", 10, "aaA", 20, 100, true);
                    break;      
                default:
                    throw new InvalidOperationException($"Invalid option: {option}.");   

            }
            
            return collideCounter;                        
        }
    }
}
