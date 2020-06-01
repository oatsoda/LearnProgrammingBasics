using System;

namespace _2_WhileLoop.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // This time we declare our "variable" first so that we can use it in multipe places
            string input = "";

            // This is a "While Loop" which means everything inside will execute over and 
            // over again until the statement is incorrect.

            while (input.ToLower() != "exit") 
            {                         

                // The next set of lines are the same as we saw in the first tutorial - ask for input and print that as output
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Please enter some input, or type 'exit' to exit:");

                input = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You typed: " + input);
                // End


            } // <-- This is the end of the loop
        }
    }
}
