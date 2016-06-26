namespace HecateMillionaire.BaseTable
{
    using System;
    using System.Threading;

   public static class DisplayTime
    {
       private static System.Timers.Timer timer;

       private static int countTimer;

        //ToDo: print the remaning time
        public static char CreateTimer()
        {
            countTimer = 60;

            Console.WriteLine("You have 1 minute for answer -> ... ");

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
                    var answer = Char.Parse(Console.ReadLine());

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
                            Console.WriteLine("Invaide input try again");
                            Console.WriteLine();
                            timer.Start();
                            break;
                    }

                }
                else
                {
                    if (countTimer == 0 )
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
                    Console.WriteLine(countTimer + " sec");
                }               
            }
        }

    }
}
