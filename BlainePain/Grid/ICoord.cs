using System;

namespace BlainePain.Grid
{
    public interface ICoord
    {
        int x { get; }
        int y { get; }

        ICoord MoveUp();
        ICoord MoveDown();

        ICoord MoveLeft();
        ICoord MoveRight();        

        ICoord MoveTo(ICoord newPosition);      
    }
}