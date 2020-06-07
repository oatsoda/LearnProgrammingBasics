using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace _0_Introduction.RunnableExample
{
    class Program
    {
        static ConsoleColor s_OrigBack;
        static ConsoleColor s_OrigFore;

        static int s_Rows;
        static int s_Cols;

        static readonly ConsoleColor s_Primary = ConsoleColor.Magenta;
        static ConsoleColor s_BallColour;
        static int s_BallDrawInteval = 50;

        static object s_Locker = new object();

        static void Main(string[] args)
        {
            s_OrigBack = s_BallColour = Console.BackgroundColor;
            s_OrigFore = Console.ForegroundColor;
            Console.CursorVisible = false;

            DisplayHello();
            (s_Rows, s_Cols) = DrawPane();

            Console.SetCursorPosition(0, s_Rows + 1);
            SetPrimary();
            Say("Press Enter to launch a ball. Press 'H' to remove a ball.");
            Console.SetCursorPosition(0, s_Rows + 2);
            Say("Press 'Y', 'B', 'C' or 'R' to change ball colour.");
            Console.SetCursorPosition(0, s_Rows + 3);
            Say("Press 'U' or 'D' to change speed.  Press 'X' to exit.");

            
            var ballTasks = new List<Tuple<Task, CancellationTokenSource>>();
            ConsoleKeyInfo input = default;

            while (true)
            {
                if (input.Key == ConsoleKey.Enter && ballTasks.Count < 10)
                {
                    var tokenSource = new CancellationTokenSource();
                    var task = Task.Run(() => BounceBall(tokenSource.Token), tokenSource.Token);
                    ballTasks.Add(new Tuple<Task, CancellationTokenSource>(task, tokenSource));
                }
                else if (ballTasks.Count > 1 && (input.KeyChar == 'h' || input.KeyChar == 'H'))
                {
                    var t = ballTasks.Last();
                    t.Item2.Cancel();
                    ballTasks.Remove(t);
                }

                else if (input.KeyChar == 'x' || input.KeyChar == 'X')
                    break;

                else if (input.KeyChar == 'y' || input.KeyChar == 'Y')
                    s_BallColour = ConsoleColor.Yellow;

                else if (input.KeyChar == 'b' || input.KeyChar == 'B')
                    s_BallColour = s_OrigBack;

                else if (input.KeyChar == 'c' || input.KeyChar == 'C')
                    s_BallColour = ConsoleColor.Cyan;

                else if (input.KeyChar == 'r' || input.KeyChar == 'R')
                    s_BallColour = ConsoleColor.Red;

                else if (s_BallDrawInteval > 40 && (input.KeyChar == 'u' || input.KeyChar == 'U'))
                    s_BallDrawInteval -= 5;

                else if (s_BallDrawInteval < 100 && (input.KeyChar == 'd' || input.KeyChar == 'D'))
                    s_BallDrawInteval += 5;

                input = Console.ReadKey(true);
            }


            if (ballTasks.Any())
            {
                ballTasks.ForEach(t => t.Item2.Cancel());
                Task.WaitAll(ballTasks.Select(t => t.Item1).ToArray());
            }

            // Exiting
            Console.SetCursorPosition(0, s_Rows + 4);
            SetPrimary();
            Say("GOODBYE");
            Console.WriteLine();
            Console.WriteLine();
        }

        static void BounceBall(CancellationToken cancellationToken)
        {
            var s = new Random().Next(0, s_Cols);
            MoveBall(s, 0, true, true, cancellationToken);
        }

        static void MoveBall(int currCol, int currRow, bool right, bool down, CancellationToken cancellationToken)
        {
            DrawBall(currCol, currRow, false);

            // Always cancel after removing previous ball location so we don't end up with stuck balls!
            if (cancellationToken.IsCancellationRequested)
                return;

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
                Console.Beep(500, 50);
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
                Console.Beep(600, 50);
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

            if (!cancellationToken.IsCancellationRequested) // Only pause if not cancelled
                Thread.Sleep(s_BallDrawInteval);

            MoveBall(currCol, currRow, right, down, cancellationToken);
        }

        static void DrawBall(int col, int row, bool show)
        {
            lock (s_Locker)
            { 
                Console.SetCursorPosition(col, row);
                Console.BackgroundColor = show ? s_BallColour : s_Primary;
                Console.Write(" ");
            }
        }

        static void SetOrig()
        {
            Console.BackgroundColor = s_OrigBack;
            Console.ForegroundColor = s_OrigFore;
        }

        static void SetPrimary()
        {
            Console.BackgroundColor = s_OrigBack;
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
    }
}
