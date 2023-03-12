using IS_naloga_1.Game_of_Life;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace IS_naloga_1
{
    internal class Game
    {
        public Cell[,] Cells { get; set; }
        public Cell[,] NCells { get; set; }
        private const int MAX_CELLS = 50;

        public Cell[,] Make2DArray(int max_size)
        {
            Cell[,] array = new Cell[max_size, max_size];

            for (int x = 0; x < max_size; x++)
            {
                for (int y = 0; y < max_size; y++)
                {
                    array[x, y] = new Cell();
                }
            }
            return array;
        }

        public Game()
        {
            Cells = Make2DArray(MAX_CELLS);
        }

        public void Run()
        {
            NCells = Cells.Clone() as Cell[,]; // Copy original grid

            // Count neighbours
            for (int i = 0; i < MAX_CELLS; i++)
            {
                for (int j = 0; j < MAX_CELLS; j++)
                {
                    NCells[i, j].NumberOfNeighbours = CountNeighours(i, j);
                }
            }
            // Execute rules
            for (int i = 0; i < MAX_CELLS; i++)
            {
                for (int j = 0; j < MAX_CELLS; j++)
                {
                    GameRules(i, j);
                }
            }
            Cells = NCells.Clone() as Cell[,]; // Overwrite original grid
        }

        public int CountNeighours(int x, int y)
        {
            int sum = 0;

            for (int i = -1; i < 2; i++) // Count around each cell for alive neighbours
            {
                for (int j = -1; j < 2; j++)
                {

                    if (x + j >= 0 && y + i >= 0 && x + j < 50 && y + i < 50) // Edges
                    {

                        if (Cells[x + j, y + i].IsAlive)
                        {
                            sum++;
                        }
                    }
                }
            }
            if (Cells[x, y].IsAlive)
            {
                sum -= 1; // Subtract the middle cell itself
            }

            return sum;
        }

        public void GameRules(int i, int j)
        {
            if (Cells[i, j].IsAlive == false && Cells[i, j].NumberOfNeighbours == 3) // Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
            {
                NCells[i, j].IsAlive = true;
            }
            else if (Cells[i, j].IsAlive && (Cells[i, j].NumberOfNeighbours == 2 || Cells[i, j].NumberOfNeighbours == 3)) // Any live cell with two or three live neighbours survives.
            {
                NCells[i, j].IsAlive = true;
            }
            else
            {
                NCells[i, j].IsAlive = false; // All other live cells die in the next generation.
            }
        }
    }
}
