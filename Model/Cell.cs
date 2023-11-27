namespace CarbonIT.Model
{
    public abstract class Cell
    {
        public static EmptyCell EmptyCell = new EmptyCell();
        public static MountainCell MountainCell = new MountainCell();

        public abstract bool IsBlocking();
    }

    public class EmptyCell : Cell
    {
        public override bool IsBlocking() => false;
    }

    public class MountainCell : Cell
    {
        public override bool IsBlocking() => true;
    }

    public class TreasureCell : Cell
    {
        public TreasureCell(int treasureCount)
        {
            TreasureCount = treasureCount;
        }

        public int TreasureCount { get; internal set; }

        public bool TryTakeTreasure()
        {
            if (TreasureCount == 0)
                return false;

            TreasureCount--;

            return true;
        }

        public override bool IsBlocking() => false;
    }
}
