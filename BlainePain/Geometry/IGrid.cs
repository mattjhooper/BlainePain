namespace BlainePain.Geometry
{
    public interface IGrid
    {    
        int MaxX { get; }
        int MaxY { get; }
        void PrintGrid();

        void ClearGrid();
        
        char this [Coord pos]
        {
            get;
            set;
        }

        bool IsMaxExtent(Coord pos);

        bool IsInGrid (Coord pos);
    }
}