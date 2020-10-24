using System;
using System.IO;

namespace StringifyTracks
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = args.Length > 0 ? args[0] : "";
            
            if (! File.Exists(fileName))
            {
                Console.WriteLine("supply a valid filename as first argument");
                return;
            }

            string track = System.IO.File.ReadAllText(fileName).Replace(@"\", @"\\").Replace(System.Environment.NewLine, "\\n");

            Console.WriteLine(track);
        }
    }
}
