namespace HecateMillionaire.BaseTable
{
    using System;

    using HecateExceptions;

    /// <summary>
    /// Display the remaining time for the question to be answered.
    /// </summary>
    public static class DisplayTime
    {
        private static System.Timers.Timer timer;

        private static int countTimer;

        public static char CreateTimer(int time)
        {
            var textTime = "";

            if (time > 60)
            {
                var minutes = time / 60;
                var second = time % 60;
                countTimer = time;
                textTime = string.Format("{0} minutes {1} second", minutes, second);
            }
            else
            {
                countTimer = time;
                textTime = string.Format("{0} second", time);
            }

            //TODO - change the timer according to question level
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("\nYou have {0} for answer -> ... ", textTime);

            // Create a timer with a two second interval.
            timer = new System.Timers.Timer(1000);

            // Hook up the Elapsed event for the timer.
            timer.Elapsed += OnTimedEvent;

            //Start
            timer.Start();

            while (true)
            {
                if (Console.KeyAvailable) //chek for input from console
                {
                    timer.Stop();
                    //make the choice uppercase 
                    var answer = Char.ToUpper(Char.Parse(Console.ReadLine().Trim()));

                    try
                    {
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
                                //make red color for warning
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine();
                                timer.Start();
                                //invalid answer choice
                                if (Char.IsLetter(answer))
                                {
                                    throw new InvalidAnswerException(String.Format("Enter a letter A,B,C or D.\n{0} is not valid answer.", answer), answer);
                                }
                                else
                                {
                                    //invalid joker choice
                                    throw new InvalidJokerException(String.Format("Enter a digit 1,2 or 3.\n{0} is not valid joker.", answer), answer);
                                }
                        }
                    }
                    catch (InvalidAnswerException iae)
                    {
                        Console.WriteLine(iae.Message);
                    }
                    catch (InvalidJokerException ije)
                    {
                        Console.WriteLine(ije.Message);
                    }
                    finally
                    {
                        //change text color again
                        Console.ForegroundColor = ConsoleColor.Magenta;
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

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have " + countTimer + " sec");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }
            }
        }
    }
}