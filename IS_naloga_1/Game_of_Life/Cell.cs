namespace IS_naloga_1.Game_of_Life
{
    internal class Cell
    {
        public bool IsAlive { get; set; } = false;
        public int NumberOfNeighbours { get; set; } = 0;
    }
}
