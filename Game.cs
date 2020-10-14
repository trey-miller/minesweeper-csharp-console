using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinesweeperConsole
{
    public class Game
    {
        private Grid Grid { get; }
        private int Width { get; }
        private int Height { get; }
        private int BombCount { get; }

        public Game()
        {
            Width = Inputs.ReadInt("Enter Grid Width", 2, 80);
            Height = Inputs.ReadInt("Enter Grid Height", 2, 80);
            BombCount = Inputs.ReadInt("Enter Bomb Count", 1, Width * Height - 1);

            Grid = GridFactory.Create(Width, Height, BombCount);
        }

        // Begin the game. Returns when the game is over, by either a win or a lose.
        public void Start()
        {

            while (!NextTurn())
            {
                if (IsWin())
                {
                    Grid.Print();
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("You win!!!!!");
                    Console.ResetColor();
                    return;
                }
            }
            // NextTurn returned true, meaning we hit a bomb.
            Grid.Reveal();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Oops. A bomb. You lose.");
            Console.ResetColor();
        }

        /// <summary>
        /// Resets all cells to Covered state
        /// </summary>
        public void Reset()
        {
            Grid.Reset();
        }

        private bool NextTurn()
        {
            Grid.Print();
            char c = Inputs.ReadChar("Would you like to (u)ncover or (f)lag a cell? (press 'u' or 'f'): ", "ufr");
            if (c == 'r')
            { // secret cheat to reveal grid
                Grid.Reveal();
                return false;
            }
            int x = Inputs.ReadInt("Choose a column", 0, Width - 1);
            int y = Inputs.ReadInt("Choose a row", 0, Height - 1);
            if (c == 'f')
            {
                Grid.ToggleFlag(x, y);
                return false;
            }
            return Grid.Uncover(x, y);
        }

        private bool IsWin()
        {
            return Grid.Cells.Cast<Cell>().All(cell => cell.IsBomb || cell.State == CellState.Uncovered);
        }
    }
}
