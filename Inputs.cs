using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperConsole
{
    public static class Inputs
    {
        /// <summary>
        /// Will continue to prompt until an integer between min and max (inclusive) is given
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write($"{prompt} ({min} to {max}): ");
                string inputStr = Console.ReadLine();
                if (!int.TryParse(inputStr, out int value) || value < min || value > max)
                {
                    Console.WriteLine($"You must enter an integer between {min} and {max}.");
                }
                else
                {
                    return value;
                }
            }
        }

        /// <summary>
        /// Will continue to prompt until a character in options is given
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static char ReadChar(string prompt, string options)
        {
            while (true)
            {
                Console.Write(prompt);
                string inputStr = Console.ReadLine().ToLower();
                if (inputStr.Length < 1 || !options.Contains(inputStr[0]))
                {
                    Console.WriteLine("I did not understand that input.");
                }
                else
                {
                    return inputStr[0];
                }
            }
        }
    }
}
