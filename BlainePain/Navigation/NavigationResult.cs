using BlainePain.Geometry;

namespace BlainePain.Navigation
{
    public struct NavigationResult
    {
        public NavigationResult(bool isInGrid, Coord newPosition, char? newValue)
        {
            IsInGrid = isInGrid;
            NewPosition = newPosition;
            NewValue = newValue;
        }

        public bool IsInGrid { get; }
        public Coord NewPosition { get; }
        public char? NewValue { get; }

    }
}