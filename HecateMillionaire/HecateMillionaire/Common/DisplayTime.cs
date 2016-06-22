namespace HecateMillionaire.BaseTable
{
    using System;
    using System.Threading;

   public static class DisplayTime
    {
        //ToDo: print the remaning time
        public static void CreateTimer()
        {
            // Create a Timer object
            Timer timer = new Timer(TimerCallback, null, 0, 2000);
            // Wait for the user choice
            Console.ReadLine();
        }

        private static void TimerCallback(Object o)
        {
            // Display the date/time when this method got called.
            Console.WriteLine(DateTime.Now);
            // Force a garbage collection to occur for this demo.
            GC.Collect();
        }

    }
}
