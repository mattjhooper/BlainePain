namespace BlainePain.Geometry
{
    public interface IGrid
    {    
        int MaxX { get; }
        int MaxY { get; }
        void PrintGrid();

        void ClearGrid();
        
        char GetValue(Coord pos);
        void PutValue(Coord pos, char value);

        bool IsMaxExtent(Coord pos);

        bool IsInGrid (Coord pos);
    }
}