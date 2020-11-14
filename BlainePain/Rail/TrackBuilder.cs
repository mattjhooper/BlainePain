using System;
using System.Linq;
using BlainePain.Geometry;
using BlainePain.Navigation;

namespace BlainePain.Rail
{
    public static class TrackBuilder
    {
        private static readonly char?[] ValidTrackPieces = new char?[] { '-', '|', '/', '\\', '+', 'X', 'S' };

        public static bool IsTrackPiece(char? checkChar) => ValidTrackPieces.Contains(checkChar);
        public static Direction checkStraight(char? checkVal, Direction defaultDir, Direction option1, Direction option2)
        {
            return checkVal switch
            {
                '/' => option1,
                '\\' => option2,
                _ => defaultDir,
            };
        }

        public static (bool Found, Coord NextPos, Direction NextDir) GetNextTrackPiece(IGridNavigator nav, IGrid grid, Coord pos, Direction dir)
        {
            var res = nav.CheckDirection(grid, pos, dir);
            var nextDir = dir;
            char?[] validChars;

            switch (dir)
            {
                case Direction.North:
                    nextDir = checkStraight(res.NewValue, Direction.North, Direction.Northeast, Direction.Northwest);
                    break;
                case Direction.South:
                    nextDir = checkStraight(res.NewValue, Direction.South, Direction.Southwest, Direction.Southeast);
                    break;
                case Direction.East:
                    nextDir = checkStraight(res.NewValue, Direction.East, Direction.Northeast, Direction.Southeast);
                    break;
                case Direction.West:
                    nextDir = checkStraight(res.NewValue, Direction.West, Direction.Southwest, Direction.Northwest);
                    break;

                case Direction.Northwest:
                    validChars = new char?[] { '\\', 'X', 'S' };
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                        break;

                    nextDir = Direction.North;
                    validChars = new char?[] { '|', '+', 'S' };
                    res = nav.CheckDirection(grid, pos, nextDir);
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                        break;

                    nextDir = Direction.West;
                    validChars = new char?[] { '-', '+', 'S' };
                    res = nav.CheckDirection(grid, pos, nextDir);
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                        break;

                    break;

                case Direction.Northeast:
                    validChars = new char?[] { '/', 'X', 'S' };
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                        break;

                    nextDir = Direction.North;
                    validChars = new char?[] { '|', '+', 'S' };
                    res = nav.CheckDirection(grid, pos, nextDir);
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                        break;

                    nextDir = Direction.East;
                    validChars = new char?[] { '-', '+', 'S' };
                    res = nav.CheckDirection(grid, pos, nextDir);
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                        break;

                    break;
                case Direction.Southeast:
                    validChars = new char?[] { '\\', 'X', 'S' };
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                        break;

                    nextDir = Direction.South;
                    validChars = new char?[] { '|', '+', 'S' };
                    res = nav.CheckDirection(grid, pos, nextDir);
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                        break;

                    nextDir = Direction.East;
                    validChars = new char?[] { '-', '+', 'S' };
                    res = nav.CheckDirection(grid, pos, nextDir);
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                        break;

                    break;
                case Direction.Southwest:
                    validChars = new char?[] { '/', 'X', 'S' };
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                        break;

                    nextDir = Direction.South;
                    validChars = new char?[] { '|', '+', 'S' };
                    res = nav.CheckDirection(grid, pos, nextDir);
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                        break;

                    nextDir = Direction.West;
                    validChars = new char?[] { '-', '+', 'S' };
                    res = nav.CheckDirection(grid, pos, nextDir);
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                        break;

                    break;

                default:
                    throw new Exception("Unhandled Direction");
            }

            return (res.IsInGrid && IsTrackPiece(res.NewValue), res.NewPosition, nextDir);
        }

        public static Track GetTrack(Coord start, IGrid grid)
        {
            bool moreTrack = true;
            Coord pos = start;
            var direction = Direction.East;
            var track = new Track();
            var nav = new GridNavigator();

            do
            {
                char piece = grid.GetValue(pos);
                if (!IsTrackPiece(piece))
                    throw new InvalidOperationException($"Invalid Track Piece specified: {piece}. At [{pos.x},{pos.y}].");

                track.AddTrackPiece(piece, pos);

                var res = GetNextTrackPiece(nav, grid, pos, direction);
                moreTrack = res.Found;
                pos = res.NextPos;
                direction = res.NextDir;
            } while (moreTrack && !pos.Equals(start));

            return track;
        }
    }
}