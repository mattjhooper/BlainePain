namespace BlainePain.Grid
{
    public interface IGrid
    {    
        int MaxX { get; }
        int MaxY { get; }
        void PrintGrid();
        
        char GetValue(ICoord pos);
        void PutValue(ICoord pos, char value);

        bool IsMaxExtent(ICoord pos);
    }
}