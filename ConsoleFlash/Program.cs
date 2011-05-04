using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleFlash
{
    /// <summary>
    /// Defines the entry point of the application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The entry point of the application.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static int Main(string[] args)
        {
            int count;
            decimal delay;
            decimal rate;
            bool exit;

            // Parse the command-line arguments
            if(!CommandLineParser.Parse(args, out count, out delay, out rate, out exit))
                return 1;

            // Flash the console window
            ConsoleFlasher.FlashConsoleWindow(count, (int)(rate * 1000), (int)(delay * 1000));
            return 0;
        }
    }
}
