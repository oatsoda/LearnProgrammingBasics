using System;

namespace _5_IfConditional.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // An "if" statement allows a program to make a choice.

            // Here is an example program that uses an "if" statement to output different things 
            // depending on what was input

            // We're using the same "While Loop" we used bfeore so that we can keep typing things in until we type in "exit"
            string input = "";
            while (input.ToLower() != "exit")
            {
                input = AskForInput("Please type your name [type 'exit' to exit]:", ConsoleColor.Cyan);

                // Here we check "if" the input value is a specific value
                if (input.ToLower() == "gracie")
                {
                    WriteLine("**HELLO GRACIE**", ConsoleColor.Magenta);
                }
                else // This means "otherwise" and will occur when the above "if" is not true
                {
                    WriteLine("Hello " + input, ConsoleColor.Gray);
                }
            }
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
