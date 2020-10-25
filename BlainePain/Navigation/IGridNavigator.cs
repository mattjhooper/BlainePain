using System;
using BlainePain.Geometry;

namespace BlainePain.Navigation
{
    public interface IGridNavigator
    {
        NavigationResult CheckDirection(IGrid grid, Coord startPos, Direction direction);

        NavigationResult CheckStartOfNextRow(IGrid grid, Coord startPos);

        NavigationResult FindFirst(IGrid grid, Predicate<char?> predicate);
    }
}