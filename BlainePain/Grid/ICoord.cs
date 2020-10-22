namespace BlainePain.Grid
{
    public interface ICoord
    {
        int x { get; }
        int y { get; }

        void MoveUp();
        void MoveDown();

        void MoveLeft();
        void MoveRight();
    }
}