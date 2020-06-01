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
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Please enter some input, or type 'exit' to exit:");
                input = Console.ReadLine();

                // Here we run the function to add stars to your input
                var starredInput = AddStars(input);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Your starred input: " + starredInput);
            }
        }
    }
}
