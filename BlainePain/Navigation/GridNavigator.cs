using System;
using BlainePain.Geometry;

namespace BlainePain.Navigation
{
    public class GridNavigator : IGridNavigator
    {
        public NavigationResult CheckDirection(IGrid grid, Coord startPos, Direction direction)
        {
            var (x, y) = startPos;
            Coord newPos = direction switch
            {
                Direction.North => startPos with { y = y - 1 },
                Direction.East => startPos with { x = x + 1 },
                Direction.South => startPos with { y = y + 1 },
                Direction.West => startPos with { x = x - 1 },
                Direction.Northeast => new (x + 1, y - 1),
                Direction.Southeast => new (x + 1, y + 1),
                Direction.Northwest => new (x - 1, y - 1),
                Direction.Southwest => new (x - 1, y + 1), 
                _   => throw new InvalidOperationException($"Invalid Direction specified: [{direction}]."),               
            };

            if (! grid.IsInGrid(newPos))
                return new NavigationResult(false, newPos, null);

            return new NavigationResult(true, newPos, grid[newPos]);
        }

        public NavigationResult CheckStartOfNextRow(IGrid grid, Coord startPos)
        {
            Coord newPos = new Coord(0, startPos.y + 1);

            if (! grid.IsInGrid(newPos))
                return new NavigationResult(false, newPos, null);

            return new NavigationResult(true, newPos, grid[newPos]);
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