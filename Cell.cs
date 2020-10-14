using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperConsole
{
    public enum CellState
    {
        Covered,
        Uncovered,
        Flagged,
    }

    class Cell
    {
        public int X { get; }
        public int Y { get; }

        public CellState State { get; set; } = CellState.Covered;
        public bool IsBomb { get; set; } = false;

        public int AdjacentBombsCount { get; set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
