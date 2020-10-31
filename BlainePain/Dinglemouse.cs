using System;
using System.Linq;
using System.Text;
using System.Threading;
using BlainePain.Geometry;
using BlainePain.Rail;
using BlainePain.Navigation;

namespace BlainePain
{
    public class Dinglemouse
    {
        // =======================================
        // Blaine is a pain, and that is the truth
        // =======================================
        public static Coord GetStart(IGrid grid)
        {            
            var res = new GridNavigator().FindFirst(grid, c => Track.IsTrackPiece(c));

            if (!res.IsInGrid)
                throw new InvalidOperationException($"Grid Start could not be found.");
                
            return res.NewPosition;        
        }

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
            bool found = res.IsInGrid && Track.IsTrackPiece(res.NewValue);
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
                        found = check1.IsInGrid && Track.IsTrackPiece(check1.NewValue);
                        nextPos = check1.NewPosition;
                        nextDir = Direction.North;
                    }
                    if (!found || (check2.IsInGrid && check2.NewValue == '+'))
                    {
                        found = check2.IsInGrid && Track.IsTrackPiece(check2.NewValue);
                        nextPos = check2.NewPosition;
                        nextDir = Direction.West;
                    }
                    
                    break;  
                    
                case Direction.Northeast:
                    check1 = nav.CheckDirection(grid, pos, Direction.North);
                    check2 = nav.CheckDirection(grid, pos, Direction.East);

                    if (!found || (check1.IsInGrid && check1.NewValue == '+'))
                    {
                        found = check1.IsInGrid && Track.IsTrackPiece(check1.NewValue);
                        nextPos = check1.NewPosition;
                        nextDir = Direction.North;
                    }
                    if (!found || (check2.IsInGrid && check2.NewValue == '+'))
                    {
                        found = check2.IsInGrid && Track.IsTrackPiece(check2.NewValue);
                        nextPos = check2.NewPosition;
                        nextDir = Direction.East;
                    }
                    
                    break;    
                case Direction.Southeast:
                    check1 = nav.CheckDirection(grid, pos, Direction.South);
                    check2 = nav.CheckDirection(grid, pos, Direction.East);

                    if (!found || (check1.IsInGrid && check1.NewValue == '+'))
                    {
                        found = check1.IsInGrid && Track.IsTrackPiece(check1.NewValue);
                        nextPos = check1.NewPosition;
                        nextDir = Direction.South;
                    }
                    if (!found || (check2.IsInGrid && check2.NewValue == '+'))
                    {
                        found = check2.IsInGrid && Track.IsTrackPiece(check2.NewValue);
                        nextPos = check2.NewPosition;
                        nextDir = Direction.East;
                    }
                    
                    break; 
                case Direction.Southwest:
                    check1 = nav.CheckDirection(grid, pos, Direction.South);
                    check2 = nav.CheckDirection(grid, pos, Direction.West);

                    if (!found || (check1.IsInGrid && check1.NewValue == '+'))
                    {
                        found = check1.IsInGrid && Track.IsTrackPiece(check1.NewValue);
                        nextPos = check1.NewPosition;
                        nextDir = Direction.South;
                    }
                    if (!found || (check2.IsInGrid && check2.NewValue == '+'))
                    {
                        found = check2.IsInGrid && Track.IsTrackPiece(check2.NewValue);
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

            do
            {
                char piece = grid.GetValue(pos);
                track.AddTrackPiece(piece, new Coord(pos));

                moreTrack = Dinglemouse.UpdatePositionAndDirection(grid, ref pos, ref direction);

            } while (moreTrack && !pos.Equals(start));

            return track;
        }
        public static int TrainCrash(string trackString, string aTrain, int aTrainPos, string bTrain, int bTrainPos, int limit)
        {
            Console.WriteLine($"aTrain: {aTrain}. aTrainPos: {aTrain}. bTrain: {bTrain}. bTrainPos: {bTrainPos}. Limit {limit}.");
            var grid = new Grid(trackString);
            grid.PrintGrid();

            var start = GetStart(grid);
            var track = GetTrack(new Coord(start), grid);

            var trainA = new Train(aTrain, aTrainPos, track);
            var trainB = new Train(bTrain, bTrainPos, track);

            int timer = 0;
            do
            {
                if (Train.IsCollision(trainA, trainB, track))
                    return timer;                
                // track.AddToGrid(grid);
                // trainA.AddToGrid(grid);
                // trainB.AddToGrid(grid);
                // grid.PrintGrid();
                // Thread.Sleep(100);
                trainA.Move();
                trainB.Move();
                timer++;
            }
            while (timer < limit);

            return -1;
        }

        public static bool UpdatePositionAndDirection(IGrid grid, ref Coord pos, ref Direction direction)
        {
            char gridVal;
            bool movedDown = false;
            bool movedUp = false;
            bool movedLeft = false;
            bool movedRight = false;
            switch (direction)
            {
                case Direction.North:
                    if (pos.y > 0)
                    {
                        pos.MoveUp();
                        movedUp = true;
                        gridVal = grid.GetValue(pos);
                        if (gridVal == '/')
                            direction = Direction.Northeast;
                        else if (gridVal == '\\')
                            direction = Direction.Northwest;                        
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case Direction.South:
                    if (pos.y < grid.MaxY)
                    {
                        pos.MoveDown();
                        movedDown = true;
                        gridVal = grid.GetValue(pos);
                        if (gridVal == '/')
                            direction = Direction.Southwest;
                        else if (gridVal == '\\')
                            direction = Direction.Southeast;                        
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case Direction.East:
                    if (pos.x < grid.MaxX)
                    {
                        pos.MoveRight();
                        movedRight = true;
                        gridVal = grid.GetValue(pos);
                        if (gridVal == '/')
                            direction = Direction.Northeast;
                        else if (gridVal == '\\')
                            direction = Direction.Southeast;                        
                    }
                    else
                    {
                        return false;
                    }
                    break;  
                case Direction.West:
                    if (pos.x > 0)
                    {
                        pos.MoveLeft();
                        movedLeft = true;
                        gridVal = grid.GetValue(pos);
                        if (gridVal == '/')
                            direction = Direction.Southwest;
                        else if (gridVal == '\\')
                            direction = Direction.Northwest;                        
                    }
                    else
                    {
                        return false;
                    }
                    break;    
                case Direction.Northwest:
                    gridVal = ' ';
                    if (pos.y > 0)
                    {
                        pos.MoveUp();
                        movedUp = true;
                        gridVal = grid.GetValue(pos);
                    }
                    if (gridVal == ' ' && pos.x > 0)
                    {
                        pos.MoveLeft();
                        movedLeft = true;
                        gridVal = grid.GetValue(pos);
                    }
                    if (movedUp && movedLeft)
                    {
                        // Check final position
                        var checkPos = new Coord(pos).MoveDown();
                        var checkVal = grid.GetValue(checkPos);

                        if (checkVal == '+' || (gridVal == ' ' && checkVal != ' '))
                        {
                            pos.MoveTo(checkPos);
                            movedDown = true;
                            gridVal = checkVal;
                        } 
                    }
                    if (gridVal == '-' || (movedDown && gridVal == '+'))
                        direction = Direction.West;
                    else if (gridVal == '|' || gridVal == '+')
                        direction = Direction.North;
                    else if (gridVal == ' ')
                        return false;
                    
                    break;  
                    
                case Direction.Northeast:
                    gridVal = ' ';
                    if (pos.y > 0)
                    {
                        pos.MoveUp();
                        movedUp = true;
                        gridVal = grid.GetValue(pos);
                    }
                    if (gridVal == ' ' && pos.x < grid.MaxX)
                    {
                        pos.MoveRight();
                        movedRight = true;
                        gridVal = grid.GetValue(pos);
                    }
                    if (movedUp && movedRight)
                    {
                        // Check final position
                        var checkPos = new Coord(pos).MoveDown();
                        var checkVal = grid.GetValue(checkPos);

                        if (checkVal == '+' || (gridVal == ' ' && checkVal != ' '))
                        {
                            pos.MoveTo(checkPos);
                            movedDown = true;
                            gridVal = checkVal;
                        }     
                    }
                    if (gridVal == '-' || (movedDown && gridVal == '+'))
                        direction = Direction.East;
                    else if (gridVal == '|' || gridVal == '+')
                        direction = Direction.North;
                    else if (gridVal == ' ')
                        return false;
                    
                    break;    
                case Direction.Southeast:
                    gridVal = ' ';
                    if (pos.x < grid.MaxX)
                    {
                        pos.MoveRight();
                        movedRight = true;
                        gridVal = grid.GetValue(pos);
                    }
                    if (gridVal == ' ' && pos.y < grid.MaxY)
                    {
                        pos.MoveDown();
                        movedDown = true;
                        gridVal = grid.GetValue(pos);
                    }
                    if (movedRight && movedDown)
                    {
                        // Check final position
                        var checkPos = new Coord(pos).MoveLeft();
                        var checkVal = grid.GetValue(checkPos);

                        if (checkVal == '+' || (gridVal == ' ' && checkVal != ' '))
                        {
                            pos.MoveTo(checkPos);
                            movedLeft = true;
                            gridVal = checkVal;
                        }                        
                    }
                    if (gridVal == '-'|| (!movedLeft && gridVal == '+'))
                        direction = Direction.East;
                    else if (gridVal == '|' || gridVal == '+')
                        direction = Direction.South;
                    else if (gridVal == ' ')
                        return false;
                    
                    break; 
                case Direction.Southwest:
                    gridVal = ' ';
                    if (pos.x > 0)
                    {
                        pos.MoveLeft();
                        movedLeft = true;
                        gridVal = grid.GetValue(pos);
                    }
                    if (gridVal == ' ' && pos.y < grid.MaxY)
                    {
                        pos.MoveDown();
                        movedDown = true;
                        gridVal = grid.GetValue(pos);
                    }
                    if (movedLeft && movedDown)
                    {
                        // Check final position
                        var checkPos = new Coord(pos).MoveRight();
                        var checkVal = grid.GetValue(checkPos);

                        if (checkVal == '+' || (gridVal == ' ' && checkVal != ' '))
                        {
                            pos.MoveTo(checkPos);
                            movedRight = true;
                            gridVal = checkVal;
                        }                                                     
                    }
                    if (gridVal == '-' || (!movedRight && gridVal == '+'))
                        direction = Direction.West;
                    else if (gridVal == '|' || gridVal == '+')
                        direction = Direction.South;
                    else if (gridVal == ' ')
                        return false;
                    
                    break; 
                                        
                default:
                    return false;
            }
            return true;
        }
    }
}