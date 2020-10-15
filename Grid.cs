using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinesweeperConsole
{
    /// <summary>
    /// Holds the 2D array of Cells, and updates their state when the player does something.
    /// </summary>
    class Grid
    {
        public Cell[,] Cells { get; }

        public Grid(Cell[,] cells)
        {
            Cells = cells;
        }

        public void Print()
        {
            GridPrinter.Print(Cells);
        }

        public void Reveal()
        {
            GridPrinter.Print(Cells, true);
        }

        public void Reset()
        {
            foreach (Cell cell in Cells)
            {
                cell.State = CellState.Covered;
            }
        }

        /// <summary>
        /// User uncovers a cell
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>if true, user guessed a bomb, and Game Over</returns>
        public bool Uncover(int x, int y)
        {
            var cell = Cells[x, y];
            if (cell.IsBomb)
                return true;

            UncoverCellAndNeighbors(x, y);

            return false;
        }

        /// <summary>
        /// Toggles between Flagged and Covered. Does nothing if cell is already Uncovered.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ToggleFlag(int x, int y)
        {
            var cell = Cells[x, y];
            if (cell.State == CellState.Uncovered)
                return;

            cell.State = cell.State == CellState.Flagged
                ? CellState.Covered
                : CellState.Flagged;
        }

        private void UncoverCellAndNeighbors(int x, int y)
        {
            var maxX = Cells.GetUpperBound(0);
            var maxY = Cells.GetUpperBound(1);

            var cellsToExpose = new Queue<Cell>();
            cellsToExpose.Enqueue(Cells[x, y]);

            while (cellsToExpose.Count > 0)
            {
                var cell = cellsToExpose.Dequeue();
                if (cell.State == CellState.Uncovered || cell.State == CellState.Flagged)
                    continue;

                cell.State = CellState.Uncovered;

                if (cell.AdjacentBombsCount > 0)
                    continue; // only check neighbors if zero adjacent bombs

                // add adjacent to check
                if (cell.X > 0)
                    cellsToExpose.Enqueue(Cells[cell.X - 1, cell.Y]);
                if (cell.Y > 0)
                    cellsToExpose.Enqueue(Cells[cell.X, cell.Y - 1]);
                if (cell.X < maxX)
                    cellsToExpose.Enqueue(Cells[cell.X + 1, cell.Y]);
                if (cell.Y < maxY)
                    cellsToExpose.Enqueue(Cells[cell.X, cell.Y + 1]);
            }
        }
    }
}
