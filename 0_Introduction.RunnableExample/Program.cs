using System;
using System.Threading;

namespace _0_Introduction.RunnableExample
{
    class Program
    {
        static ConsoleColor s_OrigBack;
        static ConsoleColor s_OrigFore;

        static int s_Rows;
        static int s_Cols;

        const ConsoleColor s_Primary = ConsoleColor.Magenta;


        static void Main(string[] args)
        {
            s_OrigBack = Console.BackgroundColor;
            s_OrigFore = Console.ForegroundColor;
            Console.CursorVisible = false;

            DisplayHello();
            (s_Rows, s_Cols) = DrawPane();
            BounceBall();
        }

        static void BounceBall()
        {
            var s = new Random().Next(0, s_Cols);
            MoveBall(s, 0, true, true);
        }

        static void MoveBall(int currCol, int currRow, bool right, bool down)
        {
            DrawBall(currCol, currRow, false);
            
            if (currCol == s_Cols && right)
            { 
                right = false;
                currCol--;
                Console.Beep(600, 50);
            }
            else if (currCol == 0 && !right)
            { 
                right = true;
                currCol++;
                Console.Beep(600, 50);
            }
            else if (right)
            {
                currCol++;
            }
            else if (!right)
            {
                currCol--;
            }

            if (currRow == s_Rows && down)
            {
                down = false;
                currRow--;
                Console.Beep(500, 50);
            }
            else if (currRow == 0 && !down)
            {
                down = true;
                currRow++;
                Console.Beep(500, 50);
            }
            else if (down)
            {
                currRow++;
            }
            else if (!down)
            {
                currRow--;
            }

            DrawBall(currCol, currRow, true);
            Thread.Sleep(50);
            MoveBall(currCol, currRow, right, down);
        }

        static void DrawBall(int col, int row, bool show)
        {
            Console.SetCursorPosition(col, row);
            Console.BackgroundColor = show ? s_OrigBack : s_Primary;
            Console.Write(" ");
        }

        static void SetOrig()
        {
            Console.BackgroundColor = s_OrigBack;
            Console.ForegroundColor = s_OrigFore;
        }

        static void SetPrimary()
        {
            Console.ForegroundColor = s_Primary;
        }

        static void DisplayHello()
        {
            SetPrimary();
            Say("HELLO");
            BlinkCursor(Console.CursorLeft, Console.CursorTop);

            SetOrig();
            Console.WriteLine();

            SetPrimary();
            Say("LET'S DRAW");
            BlinkCursor(Console.CursorLeft, Console.CursorTop);
            SetOrig();
        }

        static void Say(string text)
        {
            Console.Write($"     {text} ");
            Console.Beep(600, 100);
            Console.Beep(500, 250);
        }

        static void BlinkCursor(int left, int top)
        {
            const int blinkInterval = 500;
            const int maxTime = 3000;

            for (var i = 0; i <= maxTime / blinkInterval; i++)
            {
                Console.BackgroundColor = i % 2 == 0 ? s_Primary : s_OrigBack;
                Console.Write(" ");
                Console.SetCursorPosition(left, top);
                Thread.Sleep(blinkInterval);
            }

            Console.BackgroundColor = s_OrigBack;
            Console.Write(" ");
        }

        static (int rows, int cols) DrawPane()
        {
            Console.SetCursorPosition(0, 0);
            var mainColour = Console.BackgroundColor;

            const int rows = 20;
            
            const int loadTimeMs = 3000;
            var cols = Console.WindowWidth - 1;
            var totalCells = cols * rows;

            var timeIntervalMs = loadTimeMs / totalCells;

            var cell = 0;
            for (var row = 0; row <= rows; row++)
            { 
                for (var col = 0; col <= cols; col++)
                {
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Console.Write(" ");
                    Thread.Sleep(timeIntervalMs);
                    cell ++;
                }
            }

            Console.BackgroundColor = mainColour;

            return (rows, cols);
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
