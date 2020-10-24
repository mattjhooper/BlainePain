using System;
using System.Text;
using BlainePain.Geometry;
using BlainePain.Rail;

namespace BlainePain
{
    public class Dinglemouse
    {
        // =======================================
        // Blaine is a pain, and that is the truth
        // =======================================
        
        private static ICoord GetStart(IGrid grid)
        {
            var pos = new Coord(0,0);
            char valueAtPos = grid.GetValue(pos);
            bool keepChecking = valueAtPos == ' ' && !grid.IsMaxExtent(pos);
            while (keepChecking)
            {
                if (pos.x < grid.MaxX)
                {
                    pos.MoveRight();
                }
                else if (pos.y < grid.MaxY)
                {
                    pos.MoveDown();
                    pos = new Coord(0, pos.y);
                }                
                valueAtPos = grid.GetValue(pos);
                keepChecking = valueAtPos == ' ' && !grid.IsMaxExtent(pos);                    
            }
            return pos;
        }
        
        private static Track GetTrack(ICoord start, IGrid grid)
        {
            bool moreTrack = true;
            ICoord pos = new Coord(start);
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
            //Console.WriteLine($"track length: {trackString.Length}");
            var grid = new Grid(trackString);
            grid.PrintGrid();

            var start = GetStart(grid);
            var track = GetTrack(start, grid);

            ICoord trainAGridPos = track.GetGridPosition(aTrainPos);
            grid.PutValue(trainAGridPos, 'A');
            ICoord trainBGridPos = track.GetGridPosition(bTrainPos);
            grid.PutValue(trainBGridPos, 'B');
            grid.PrintGrid();
        
            return 0;
        }

        public static bool UpdatePositionAndDirection(IGrid grid, ref ICoord pos, ref Direction direction)
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