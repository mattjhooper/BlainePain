using System;
using BlainePain.Geometry;

namespace BlainePain.Navigation
{
    public class GridNavigator : IGridNavigator
    {
        public NavigationResult CheckDirection(IGrid grid, Coord startPos, Direction direction)
        {
            Coord newPos = direction switch
            {
                Direction.North => new Coord(startPos.x, startPos.y - 1),
                Direction.East => new Coord(startPos.x + 1, startPos.y),
                Direction.South => new Coord(startPos.x, startPos.y + 1),
                Direction.West => new Coord(startPos.x - 1, startPos.y),
                Direction.Northeast => new Coord(startPos.x + 1, startPos.y - 1),
                Direction.Southeast => new Coord(startPos.x + 1, startPos.y + 1),
                Direction.Northwest => new Coord(startPos.x - 1, startPos.y - 1),
                Direction.Southwest => new Coord(startPos.x - 1, startPos.y + 1),                
            };

            if (! grid.IsInGrid(newPos))
                return new NavigationResult(false, newPos, null);

            return new NavigationResult(true, newPos, grid.GetValue(newPos));
        }

        public NavigationResult CheckStartOfNextRow(IGrid grid, Coord startPos)
        {
            Coord newPos = new Coord(0, startPos.y + 1);

            if (! grid.IsInGrid(newPos))
                return new NavigationResult(false, newPos, null);

            return new NavigationResult(true, newPos, grid.GetValue(newPos));
        }

        public NavigationResult FindFirst(IGrid grid, Predicate<char?> predicate)
        {
            var pos = new Coord(-1,0);
            char? valueAtPos;
            bool match;
            do
            {
                var res = CheckDirection(grid, pos, Direction.East);

                if (! res.IsInGrid)
                    res = CheckStartOfNextRow(grid, pos);

                pos = res.NewPosition;
                valueAtPos = res.NewValue;
                match = predicate(valueAtPos);                                                
            } while (!match && !grid.IsMaxExtent(pos));

            return new NavigationResult(match, pos, match? valueAtPos : null);
        }
    }
}