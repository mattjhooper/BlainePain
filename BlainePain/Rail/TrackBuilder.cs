using System;
using System.Linq;
using BlainePain.Geometry;
using BlainePain.Navigation;

namespace BlainePain.Rail
{
    public static class TrackBuilder
    {
        private static readonly char?[] ValidTrackPieces = new char?[] {'-', '|', '/', '\\', '+', 'X', 'S'};

        public static bool IsTrackPiece(char? checkChar) => ValidTrackPieces.Contains(checkChar);
        public static Direction checkStraight(char checkVal, Direction defaultDir, Direction option1, Direction option2 )
        {
            return checkVal switch
            {
                '/' => option1,
                '\\' => option2,
                _  => defaultDir,               
            };
        }

        public static (bool Found, Coord NextPos, Direction NextDir) GetNextTrackPiece(IGridNavigator nav, IGrid grid, Coord pos, Direction dir)
        {           
            var res = nav.CheckDirection(grid, pos, dir);
            bool found = res.IsInGrid && IsTrackPiece(res.NewValue);
            Coord nextPos = res.NewPosition;
            var nextDir = dir;
            NavigationResult check1;
            NavigationResult check2;
            
            switch (dir)
            {
                case Direction.North:
                    nextDir = res.NewValue switch
                    {
                        '/' => Direction.Northeast,
                        '\\' => Direction.Northwest,
                        _  => Direction.North,               
                    };                                  
                    break;
                case Direction.South:
                    nextDir = res.NewValue switch
                    {
                        '/' => Direction.Southwest,
                        '\\' => Direction.Southeast,
                        _  => Direction.South,               
                    };                      
                    break;
                case Direction.East:
                    nextDir = res.NewValue switch
                    {
                        '/' => Direction.Northeast,
                        '\\' => Direction.Southeast,
                        _  => Direction.East,               
                    };                      
                    break;  
                case Direction.West:
                    nextDir = res.NewValue switch
                    {
                        '/' => Direction.Southwest,
                        '\\' => Direction.Northwest,
                        _  => Direction.West,               
                    };                    
                    break;    
                
                case Direction.Northwest:
                    check1 = nav.CheckDirection(grid, pos, Direction.North);
                    check2 = nav.CheckDirection(grid, pos, Direction.West);

                    if (!found || (check1.IsInGrid && check1.NewValue == '+'))
                    {
                        found = check1.IsInGrid && IsTrackPiece(check1.NewValue);
                        nextPos = check1.NewPosition;
                        nextDir = Direction.North;
                    }
                    if (!found || (check2.IsInGrid && check2.NewValue == '+'))
                    {
                        found = check2.IsInGrid && IsTrackPiece(check2.NewValue);
                        nextPos = check2.NewPosition;
                        nextDir = Direction.West;
                    }
                    
                    break;  
                    
                case Direction.Northeast:
                    check1 = nav.CheckDirection(grid, pos, Direction.North);
                    check2 = nav.CheckDirection(grid, pos, Direction.East);

                    if (!found || (check1.IsInGrid && check1.NewValue == '+'))
                    {
                        found = check1.IsInGrid && IsTrackPiece(check1.NewValue);
                        nextPos = check1.NewPosition;
                        nextDir = Direction.North;
                    }
                    if (!found || (check2.IsInGrid && check2.NewValue == '+'))
                    {
                        found = check2.IsInGrid && IsTrackPiece(check2.NewValue);
                        nextPos = check2.NewPosition;
                        nextDir = Direction.East;
                    }
                    
                    break;    
                case Direction.Southeast:
                    check1 = nav.CheckDirection(grid, pos, Direction.South);
                    check2 = nav.CheckDirection(grid, pos, Direction.East);

                    if (!found || (check1.IsInGrid && check1.NewValue == '+'))
                    {
                        found = check1.IsInGrid && IsTrackPiece(check1.NewValue);
                        nextPos = check1.NewPosition;
                        nextDir = Direction.South;
                    }
                    if (!found || (check2.IsInGrid && check2.NewValue == '+'))
                    {
                        found = check2.IsInGrid && IsTrackPiece(check2.NewValue);
                        nextPos = check2.NewPosition;
                        nextDir = Direction.East;
                    }
                    
                    break; 
                case Direction.Southwest:
                    check1 = nav.CheckDirection(grid, pos, Direction.South);
                    check2 = nav.CheckDirection(grid, pos, Direction.West);

                    if (!found || (check1.IsInGrid && check1.NewValue == '+'))
                    {
                        found = check1.IsInGrid && IsTrackPiece(check1.NewValue);
                        nextPos = check1.NewPosition;
                        nextDir = Direction.South;
                    }
                    if (!found || (check2.IsInGrid && check2.NewValue == '+'))
                    {
                        found = check2.IsInGrid && IsTrackPiece(check2.NewValue);
                        nextPos = check2.NewPosition;
                        nextDir = Direction.West;
                    }
                    
                    break; 
                                        
                default:
                    throw new Exception("Unhandled Direction");
            }

            return (found, nextPos, nextDir);
        }
        
        public static Track GetTrack(Coord start, IGrid grid)
        {
            bool moreTrack = true;
            Coord pos = new Coord(start);
            var direction = Direction.East;
            var track = new Track();
            var nav = new GridNavigator();

            do
            {
                char piece = grid.GetValue(pos);
                if(!IsTrackPiece(piece))
                    throw new InvalidOperationException($"Invalid Track Piece specified: {piece}. At [{pos.x},{pos.y}].");                 
            
                track.AddTrackPiece(piece, new Coord(pos));

                var res = GetNextTrackPiece(nav, grid, pos, direction);
                moreTrack = res.Found;
                pos = res.NextPos;
                direction = res.NextDir;                
            } while (moreTrack && !pos.Equals(start));

            return track;
        }
    }
}