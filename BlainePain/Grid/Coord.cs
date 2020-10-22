namespace BlainePain.Grid
{
    public class Coord : ICoord
    {
        public int x { get; private set; }
        public int y { get; private set; }

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void MoveUp() => y--;

        public void MoveDown() => y++;
        
        public void MoveLeft() => x--;
        public void MoveRight() => x++;           
    }
}