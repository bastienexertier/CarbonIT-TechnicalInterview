using System;
using System.IO;
using CarbonIT.Parser;
using CarbonIT.Model;
using CarbonIT.Simulation;

namespace CarbonIT.Application
{
    class Program
    {
        static void Main()
        {
            string mapFilePath = "..\\..\\Maps\\map_1.txt";

            Map map;
            using (StreamReader sr = new StreamReader(mapFilePath))
            {
                map = MapParser.ParseMap(sr);
            }

            SimulationResult result = Simulation.Simulation.Simulate(map);

            Console.WriteLine(result.ToString());
        }
    }
}
