using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarbonIT.Model;

namespace CarbonIT.Simulation
{
    class SimulationResult
    {
        public SimulationResult(List<SimulationAdventurer> adventurers, List<Tuple<Tuple<int, int>, TreasureCell>> treasureCells)
        {
            Adventurers = adventurers;
            TreasureCells = treasureCells;
        }

        public List<SimulationAdventurer> Adventurers { get; }
        public List<Tuple<Tuple<int, int>, TreasureCell>> TreasureCells { get; }
        public override string ToString()
        {
            StringBuilder res = new StringBuilder();

            foreach (var adv in Adventurers) {
                res.AppendLine($"A - {adv.Adventurer.Name} - {adv.Position.Item1} - {adv.Position.Item2} - {adv.Orientation} - {adv.TreasureCount}");
            }
            foreach (var (coords, cell) in TreasureCells) {
                res.AppendLine($"T - {coords.Item1} - {coords.Item2} - {cell.TreasureCount}");
            }

            return res.ToString();
        }
    }

    class Simulation
    {
        public static SimulationResult Simulate(Map map)
        {
            bool shouldStop;

            List<Tuple<SimulationAdventurer, CharEnumerator>> moves = map
                .Adventurers
                .Select(adv => new Tuple<SimulationAdventurer, CharEnumerator>(SimulationAdventurer.FromAdventurer(adv), adv.MovesSequence.GetEnumerator()))
                .ToList();

            do
            {
                shouldStop = true;
                foreach (var (adventurer, adventurerMoves) in moves)
                {
                    if (!adventurerMoves.MoveNext())
                        continue;

                    shouldStop = false;

                    if (adventurerMoves.Current == 'D')
                    {
                        adventurer.Orientation = adventurer.Orientation.Right();
                        continue;
                    }

                    if (adventurerMoves.Current == 'G')
                    {
                        adventurer.Orientation = adventurer.Orientation.Left();
                        continue;
                    }

                    if (adventurerMoves.Current != 'A')
                    {
                        continue;
                    }

                    Tuple<int, int> nextPosition = adventurer.GetNextPosition();

                    bool isMoveValid = map.TryMoveAdventurer(adventurer.Adventurer, nextPosition);

                    if (!isMoveValid)
                        continue;

                    adventurer.Position = nextPosition;

                    Cell cell = map.GetCell(nextPosition);

                    if (cell is TreasureCell treasureCell && treasureCell.TryTakeTreasure())
                    {
                        adventurer.TreasureCount++;
                    }
                }
            } while (!shouldStop);

            return new SimulationResult(
                adventurers: moves.Select(e => e.Item1).ToList(),
                treasureCells: map.Cells.Select(kv => new Tuple<Tuple<int, int>, TreasureCell>(kv.Key, kv.Value as TreasureCell)).Where(c => c.Item2 != null).ToList()
            );
        }
    }
}
