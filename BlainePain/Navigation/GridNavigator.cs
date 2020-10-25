using BlainePain.Geometry;

namespace BlainePain.Navigation
{
    public class GridNavigator : IGridNavigator
    {
        public (bool IsInGrid, Coord NewPosition, char? NewValue) CheckDirection(IGrid grid, Coord startPos, Direction direction)
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
                return (false, newPos, null);

            return (true, newPos, grid.GetValue(newPos));
        }
    }
}