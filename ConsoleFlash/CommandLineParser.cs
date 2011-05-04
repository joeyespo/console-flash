using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleFlash
{
    /// <summary>
    /// Provides methods for parsing the command line.
    /// </summary>
    public static class CommandLineParser
    {
        /// <summary>
        /// Parses the command line.
        /// </summary>
        public static bool Parse(string[] args, out int count, out decimal delay, out decimal rate, out bool exit)
        {
            string valueString;
            int intValue;
            decimal decimalValue;

            count = 0;
            delay = 0m;
            rate = 0m;
            exit = false;

            for(int index = 0; index < args.Length; ++index)
            {
                if(string.Compare(args[index], "/?", true) == 0 || string.Compare(args[index], "/h", true) == 0 || string.Compare(args[index], "-h", true) == 0)
                {
                    ShowHelp();
                    exit = true;
                    return true;
                }
                else if(string.Compare(args[index], "/c", true) == 0 || string.Compare(args[index], "-c", true) == 0)
                {
                    if(++index >= args.Length)
                    {
                        ShowError("A value is required for option /c");
                        return false;
                    }
                    valueString = args[index];
                    if(!int.TryParse(valueString, out intValue))
                    {
                        ShowError("'" + valueString + "' is not valid for option /c");
                        return false;
                    }
                    count = intValue;
                }
                else if(string.Compare(args[index], "/d", true) == 0 || string.Compare(args[index], "-d", true) == 0)
                {
                    if(++index >= args.Length)
                    {
                        ShowError("A value is required for option /r");
                        return false;
                    }
                    valueString = args[index];
                    if(!decimal.TryParse(valueString, out decimalValue))
                    {
                        ShowError("'" + valueString + "' is not valid for option /d");
                        return false;
                    }
                    delay = decimalValue;
                }
                else if(string.Compare(args[index], "/r", true) == 0 || string.Compare(args[index], "-r", true) == 0)
                {
                    if(++index >= args.Length)
                    {
                        ShowError("A value is required for option /r");
                        return false;
                    }
                    valueString = args[index];
                    if(!decimal.TryParse(valueString, out decimalValue))
                    {
                        ShowError("'" + valueString + "' is not valid for option /r");
                        return false;
                    }
                    rate = decimalValue;
                }
                else
                {
                    ShowError("'" + args[index] + "' is not a valid option");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Shows the specified error message, followed by the help.
        /// </summary>
        public static void ShowError(string message)
        {
            Console.WriteLine("Error: " + message);
            Console.WriteLine();
            ShowHelp();
        }

        /// <summary>
        /// Writes the help to the console.
        /// </summary>
        public static void ShowHelp()
        {
            Console.WriteLine("usage");
            Console.WriteLine("  flash [options]");
            Console.WriteLine();
            Console.WriteLine("options:");
            Console.WriteLine("  /c <integer>         The number of times to flash the window before keeping it inverted (default: 3)");
            Console.WriteLine("  /d <decimal>         The number of seconds to wait before flashing (default: zero)");
            Console.WriteLine("  /r <decimal>         The flash rate in seconds, with zero indicating use the system default (default: zero)");
            Console.WriteLine("  /h, /?               Shows this help message");
        }
    }
}
