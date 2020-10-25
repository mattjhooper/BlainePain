using BlainePain.Geometry;

namespace BlainePain.Navigation
{
    public interface IGridNavigator
    {
        (bool IsInGrid, Coord NewPosition, char? NewValue) CheckDirection(IGrid grid, Coord startPos, Direction direction);
    }
}