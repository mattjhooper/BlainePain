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
            char?[] validChars;
            
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
                    validChars = new char?[] { '\\', 'X', 'S'};
                    if (found && validChars.Contains(res.NewValue))
                        break;

                    res = nav.CheckDirection(grid, pos, Direction.North);
                    validChars = new char?[] { '|', '+', 'S'};
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                    {
                        found = true;
                        nextPos = res.NewPosition;
                        nextDir = Direction.North;
                        break;
                    }

                    res = nav.CheckDirection(grid, pos, Direction.West);
                    validChars = new char?[] { '-', '+', 'S'};
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                    {
                        found = true;
                        nextPos = res.NewPosition;
                        nextDir = Direction.West;
                        break;
                    }                    
                    
                    break; 
                    
                case Direction.Northeast:
                    validChars = new char?[] { '/', 'X', 'S'};
                    if (found && validChars.Contains(res.NewValue))
                        break;

                    res = nav.CheckDirection(grid, pos, Direction.North);
                    validChars = new char?[] { '|', '+', 'S'};
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                    {
                        found = true;
                        nextPos = res.NewPosition;
                        nextDir = Direction.North;
                        break;
                    }

                    res = nav.CheckDirection(grid, pos, Direction.East);
                    validChars = new char?[] { '-', '+', 'S'};
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                    {
                        found = true;
                        nextPos = res.NewPosition;
                        nextDir = Direction.East;
                        break;
                    }                    
                    
                    break;     
                case Direction.Southeast:
                    validChars = new char?[] { '\\', 'X', 'S'};
                    if (found && validChars.Contains(res.NewValue))
                        break;

                    res = nav.CheckDirection(grid, pos, Direction.South);
                    validChars = new char?[] { '|', '+', 'S'};
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                    {
                        found = true;
                        nextPos = res.NewPosition;
                        nextDir = Direction.South;
                        break;
                    }

                    res = nav.CheckDirection(grid, pos, Direction.East);
                    validChars = new char?[] { '-', '+', 'S'};
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                    {
                        found = true;
                        nextPos = res.NewPosition;
                        nextDir = Direction.East;
                        break;
                    }                    
                    
                    break; 
                case Direction.Southwest:
                    validChars = new char?[] { '/', 'X', 'S'};
                    if (found && validChars.Contains(res.NewValue))
                        break;

                    res = nav.CheckDirection(grid, pos, Direction.South);
                    validChars = new char?[] { '|', '+', 'S'};
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                    {
                        found = true;
                        nextPos = res.NewPosition;
                        nextDir = Direction.South;
                        break;
                    }

                    res = nav.CheckDirection(grid, pos, Direction.West);
                    validChars = new char?[] { '-', '+', 'S'};
                    if (res.IsInGrid && validChars.Contains(res.NewValue))
                    {
                        found = true;
                        nextPos = res.NewPosition;
                        nextDir = Direction.West;
                        break;
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