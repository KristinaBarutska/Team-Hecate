namespace HecateMillionaire.BaseTable
{
    using System;

    using Common;
    using Common.Console;

    /// <summary>
    /// Display the remaining time for the question to be answered.
    /// </summary>
    /// 
    public static class DisplayTime
    {
        private static System.Timers.Timer timer;

        private static int countTimer;

        public static char CreateTimer()
        {
            countTimer = 60;

            Console.WriteLine(ConsoleConstants.TimeForAnswerMessage);

            // Create a timer with a two second interval.
            timer = new System.Timers.Timer(1000);

            // Hook up the Elapsed event for the timer.
            timer.Elapsed += OnTimedEvent;

            // Start
            timer.Start();
            var positionForAnswerLeft = Console.WindowWidth - (ConsoleConstants.TimeForAnswerMessage.Length - 10);
            var positionForAnswerTop = Console.WindowHeight / 2;
            Console.SetCursorPosition(positionForAnswerLeft, positionForAnswerTop);

            while (true)
            {
                // Chek for input from console
                if (Console.KeyAvailable)
                {
                    timer.Stop();
                    var answer = char.Parse(Console.ReadLine().Trim().ToUpper());

                    switch (answer)
                    {
                        case 'A': timer.Dispose(); return answer;
                        case 'B': timer.Dispose(); return answer;
                        case 'C': timer.Dispose(); return answer;
                        case 'D': timer.Dispose(); return answer;
                        case '1': timer.Dispose(); return answer;
                        case '2': timer.Dispose(); return answer;
                        case '3': timer.Dispose(); return answer;
                        default:
                            Console.WriteLine(GlobalErrorMessages.InvalidInputMessage);
                            Console.SetCursorPosition(positionForAnswerLeft - 1, positionForAnswerTop);
                            ClearToEndOfCurrentLine();
                            timer.Start();
                            break;
                    }
                }
                else
                {
                    if (countTimer == 0)
                    {
                        return default(char);
                    }
                }
            }
        }

        public static void ClearToEndOfCurrentLine()
        {
            int currentLeft = Console.CursorLeft;
            int currentTop = Console.CursorTop;
            Console.Write(new string(' ', Console.WindowWidth - currentLeft));
            Console.SetCursorPosition(currentLeft + 1, currentTop);
        }

        private static void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            countTimer -= 1;

            if (countTimer < 5)
            {
                if (countTimer == 0)
                {
                    timer.Dispose();
                }
                else
                {
                    Console.WriteLine(countTimer + " sec");
                }
            }
        }
    }
}