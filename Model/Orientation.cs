namespace CarbonIT.Model
{
    public enum Orientation : short
    {
        N = 0,
        E = 1,
        S = 2,
        W = 3
    }

    public static class OrientationExtensions
    {
        public static Orientation Right(this Orientation orientation)
        {
            return (Orientation)(((short)orientation + 5) % 4);
        }
        public static Orientation Left(this Orientation orientation)
        {
            return (Orientation)(((short)orientation + 3) % 4);
        }
    }
}
