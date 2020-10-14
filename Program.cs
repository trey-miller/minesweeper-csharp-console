using System;

namespace MinesweeperConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Minesweeper C# Console Edition!");
            Game game = new Game();
            while (true)
            {
                game.Start();

                char key = Inputs.ReadChar(
                    "Press 'n' to try a (n)ew game, 'r' to (r)etry this game, or 'x' to e(x)it.",
                    "nrx");

                if (key == 'n')
                {
                    game = new Game();
                }
                else if (key == 'r')
                {
                    game.Reset();
                }
                else // if (key == 'x')
                {
                    break;
                }
            }
        }
    }
}
