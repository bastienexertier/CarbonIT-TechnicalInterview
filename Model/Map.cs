using System;
using System.Collections.Generic;
using System.Linq;

namespace CarbonIT.Model
{
    public class Map
    {
        private readonly Dictionary<Tuple<int, int>, Adventurer> _adventurers;
        private readonly Dictionary<Adventurer, Tuple<int, int>> _adventurersPositions;

        public Map(int height, int width, Dictionary<Tuple<int, int>, Cell> cells, Dictionary<Tuple<int, int>, Adventurer> adventurers)
        {
            Height = height;
            Width = width;
            Cells = cells;
            _adventurers = adventurers;
            _adventurersPositions = adventurers.ToDictionary(kv => kv.Value, kv => kv.Key);
        }

        public int Height { get; }
        public int Width { get; }
        public IEnumerable<Adventurer> Adventurers => _adventurers.Values;
        public Dictionary<Tuple<int, int>, Cell> Cells { get; }

        public Cell GetCell(Tuple<int, int> position)
        {
            if (Cells.TryGetValue(position, out var cell))
                return cell;

            return Cell.EmptyCell;
        }

        public bool TryMoveAdventurer(Adventurer adventurer, Tuple<int, int> newPosition)
        {
            if (IsCellBlocked(newPosition))
                return false;
            
            if (_adventurersPositions.TryGetValue(adventurer, out Tuple<int, int> oldPosition))
            {
                _adventurers.Remove(oldPosition);
            }

            _adventurersPositions[adventurer] = newPosition;
            _adventurers[newPosition] = adventurer;

            return true;
        }

        public bool IsCellBlocked(Tuple<int, int> coords)
        {
            (int x, int y) = coords;
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                return true;

            if (Cells.TryGetValue(coords, out Cell cell) && cell.IsBlocking())
                return true;

            if (_adventurers.ContainsKey(coords))
                return true;

            return false;
        }
    }
}
