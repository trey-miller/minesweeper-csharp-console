using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperConsole
{
    static class GridFactory
    {
        /// <summary>
        /// Creates a grid with the specifications
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="numBombs"></param>
        /// <returns>The grid</returns>
        public static Grid Create(int width, int height, int numBombs)
        {
            if (numBombs >= width * height)
            {
                throw new ArgumentException(
                    "Number of bombs must be less than total spaces on the grid."
                    + $"numBombs: {numBombs}, number of spaces ({width} x {height}): {width * height}");
            }

            var rand = new Random();
            var cells = new Cell[width, height];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    cells[x, y] = new Cell(x, y);

            while (numBombs > 0)
            {
                var x = rand.Next(0, width);
                var y = rand.Next(0, height);

                if (!cells[x, y].IsBomb)
                {
                    cells[x, y].IsBomb = true;
                    numBombs--;

                    // increment adjacent cells' AdjacentBombsCount
                    if (x > 0) // cells to the left
                    {
                        cells[x - 1, y].AdjacentBombsCount++;
                        if (y > 0)
                            cells[x - 1, y - 1].AdjacentBombsCount++;
                        if (y < height - 1)
                            cells[x - 1, y + 1].AdjacentBombsCount++;
                    }
                    if (x < width - 1) // cells to the right
                    {
                        cells[x + 1, y].AdjacentBombsCount++;
                        if (y > 0)
                            cells[x + 1, y - 1].AdjacentBombsCount++;
                        if (y < height - 1)
                            cells[x + 1, y + 1].AdjacentBombsCount++;
                    }
                    if (y > 0) // cell above
                    {
                        cells[x, y - 1].AdjacentBombsCount++;
                    }
                    if (y < height - 1) // cell below
                    {
                        cells[x, y + 1].AdjacentBombsCount++;
                    }
                }
            }

            return new Grid(cells);
        }
    }
}
