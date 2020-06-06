using System;

namespace _3_Functions.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Functions are pieces of code which take some input and give some output

            // This one takes some text and adds stars to the beginning and end

            string AddStars(string input)
            {
                return "***" + input + "***";
            }

            // Let's try it out

            // We're using the same "While Loop" we used bfeore so that we can keep typing things in until we type in "exit"
            string input = "";
            while (input.ToLower() != "exit")
            {
                input = AskForInput("Please enter some input, or type 'exit' to exit:", ConsoleColor.Cyan);

                // Here we run the function to add stars to your input
                var starredInput = AddStars(input);

                WriteLine("Your starred input: " + starredInput, ConsoleColor.Green);
            }
        }

        // We've also created two other functions
        // One to set the colour and write a line of output...
        static void WriteLine(string output, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(output);
        }

        // And one to ask for input
        static string AskForInput(string output, ConsoleColor colour)
        {
            WriteLine(output, colour);
            return Console.ReadLine();
        }
    }
}
