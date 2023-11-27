using System;
using System.Collections.Generic;

namespace CarbonIT.Model
{
    public class Adventurer
    {
        public Adventurer(string name, Tuple<int, int> startPosition, Orientation startOrientation, string movesSequence)
        {
            Name = name;
            StartPosition = startPosition;
            StartOrientation = startOrientation;
            MovesSequence = movesSequence;
        }

        public string Name { get; }
        public Tuple<int, int> StartPosition { get; }
        public Orientation StartOrientation { get; }
        public string MovesSequence { get; }

        public override bool Equals(object obj)
        {
            return obj is Adventurer adventurer &&
                   Name == adventurer.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }

    public class SimulationAdventurer
    {
        public SimulationAdventurer(Adventurer adventurer, Tuple<int, int> position, Orientation orientation)
        {
            Adventurer = adventurer;
            Position = position;
            Orientation = orientation;
        }

        public Adventurer Adventurer { get; }
        public Tuple<int, int> Position { get; set; }
        public Orientation Orientation { get; set; }
        public int TreasureCount { get; set; } = 0;

        public static SimulationAdventurer FromAdventurer(Adventurer adventurer) => new SimulationAdventurer(adventurer, adventurer.StartPosition, adventurer.StartOrientation);

        public Tuple<int, int> GetNextPosition()
        {
            switch (Orientation)
            {
                case Orientation.N:
                    return new Tuple<int, int>(Position.Item1, Position.Item2 - 1);
                case Orientation.E:
                    return new Tuple<int, int>(Position.Item1 + 1, Position.Item2);
                case Orientation.S:
                    return new Tuple<int, int>(Position.Item1, Position.Item2 + 1);
                case Orientation.W:
                    return new Tuple<int, int>(Position.Item1 - 1, Position.Item2);
                default:
                    throw new ArgumentOutOfRangeException($"Unknown Orientation");
            }
        }
    }
}
