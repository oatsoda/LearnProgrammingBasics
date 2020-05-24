using System;

namespace _1_InputOutput.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // (This is a comment - it isn't part of the program!)

            // Here is an example of basic Input and Output of a Console application!
            // Try running it and see the text output to the screen.  Then see
            // that you can give the program some input and it outputs it for you.
            
            // We do this with Console.WriteLine and Console.ReadLine

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This is some output! Hello :)"); // Output something!
            Console.WriteLine();                                // Output a blank line

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Next, type some Input please, followed by the Enter key:"); // This is more Output - we want to prompt you to type some input next
            
            var input = Console.ReadLine();                     // This reads the Input - the keys you type
            // ^ This is called a "variable" - we are using this  to "store" what you will type

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You typed: " + input);           // This Outputs what you Input!
        }
    }
}
