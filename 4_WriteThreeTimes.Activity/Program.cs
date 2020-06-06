using System;

namespace _4_WriteThreeTimes.Activity
{
    class Program
    {
        static void Main(string[] args)
        {
            // ACTIVITY

            // Write a program that:
            //  - Asks for some input
            
            // The program then outputs what was typed:
            //  - Three times
            //  - Each time in a different colour

            // The program exits when the user types "X"

            // There are some helper functions already created to help you


            string input = "";

            // We will need a "while" loop that is checking if the user typed "X"

                // We will need to Ask for some input and store that in our variable

                // We will need to write the output three times

        }



        // Write a line of output in a chosen colour
        static void WriteLine(string output, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(output);
        }

        // Ask for some input using a chosen colour
        static string AskForInput(string output, ConsoleColor colour)
        {
            WriteLine(output, colour);
            return Console.ReadLine();
        }
    }
}
