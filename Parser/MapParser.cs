using System;
using System.Collections.Generic;
using System.IO;
using CarbonIT.Model;

namespace CarbonIT.Parser
{
    public static class MapParser
    {
        public static Map ParseMap(StreamReader file)
        {
            string line;

            int width = 0;
            int height = 0;
            Dictionary<Tuple<int, int>, Cell> cells = new Dictionary<Tuple<int, int>, Cell>();
            Dictionary<Tuple<int, int>, Adventurer> adventurers = new Dictionary<Tuple<int, int>, Adventurer>();

            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("#"))
                    continue;

                if (line.StartsWith("C"))
                {
                    if (width != 0 && height != 0)
                    {
                        continue;
                    }
                    string[] mapSize = line.Split('-');
                    int.TryParse(mapSize[1].Trim(), out width);
                    int.TryParse(mapSize[2].Trim(), out height);
                }
                else if (line.StartsWith("M"))
                {
                    string[] mountainData = line.Split('-');
                    if (mountainData.Length >= 3
                        && int.TryParse(mountainData[1].Trim(), out int x)
                        && int.TryParse(mountainData[2].Trim(), out int y))
                    {
                        cells.Add(new Tuple<int, int>(x, y), Cell.MountainCell);
                    }
                }
                else if (line.StartsWith("T"))
                {
                    string[] treasureData = line.Split('-');
                    if (treasureData.Length >= 4
                        && int.TryParse(treasureData[1].Trim(), out int x)
                        && int.TryParse(treasureData[2].Trim(), out int y)
                        && int.TryParse(treasureData[3].Trim(), out int treasureCount))
                    {
                        cells.Add(new Tuple<int, int>(x, y), new TreasureCell(treasureCount));
                    }
                }
                else if (line.StartsWith("A"))
                {
                    string[] adventurerData = line.Split('-');
                    if (adventurerData.Length >= 6
                        && int.TryParse(adventurerData[2].Trim(), out int x)
                        && int.TryParse(adventurerData[3].Trim(), out int y))
                    {
                        string name = adventurerData[1].Trim();
                        Tuple<int, int> position = new Tuple<int, int>(x, y);
                        Orientation orientation = (Orientation)Enum.Parse(typeof(Orientation), adventurerData[4].Trim());
                        string movesSequence = adventurerData[5].Trim();
                        Adventurer adv = new Adventurer(name, position, orientation, movesSequence);
                        adventurers.Add(position, adv);
                    }
                }
                else
                {
                    throw new Exception("Unknown line start.");
                }
            }

            return new Map(height, width, cells, adventurers);
        }
    }
}
