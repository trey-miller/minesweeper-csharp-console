using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperConsole
{
    static class GridPrinter
    {        
        /// <summary>
        /// Prints the grid in its current state, or revealing all if showAll is true
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="showAll">Shows all cells as if all were uncovered</param>
        public static void Print(Cell[,] cells, bool showAll = false)
        {
            for (int y = 0; y <= cells.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= cells.GetUpperBound(0); x++)
                {
                    GetSprite(cells[x, y], showAll).Draw();
                }
                Console.Write('\n');
            }
            Console.ResetColor();
        }

        private static CellSprite GetSprite(Cell cell, bool showAll = false)
        {
            if (!showAll)
            {
                if (cell.State == CellState.Covered)
                    return new CellSprite
                    {
                        BG = ConsoleColor.White,
                        FG = ConsoleColor.DarkGray,
                        Char = '?',
                    };
                if (cell.State == CellState.Flagged)
                    return new CellSprite
                    {
                        BG = ConsoleColor.White,
                        FG = ConsoleColor.Red,
                        Char = 'F',
                    };
            }
            if (cell.IsBomb)
                return new CellSprite
                {
                    BG = ConsoleColor.Red,
                    FG = ConsoleColor.White,
                    Char = 'B',
                };
            return new CellSprite
            {
                Char = cell.AdjacentBombsCount.ToString()[0],
            };
        }


        class CellSprite
        {
            public ConsoleColor BG { get; set; } = ConsoleColor.Black;
            public ConsoleColor FG { get; set; } = ConsoleColor.White;
            public char Char { get; set; }

            public void Draw()
            {
                Console.BackgroundColor = BG;
                Console.ForegroundColor = FG;
                Console.Write(Char);
            }
        }
    }
}
