namespace HecateMillionaire
{
    using System;

    public static class ConsolePrintText
    {
        public static void Print(string[] texts)
        {
            foreach (var text in texts)
            {
                var currentCol = (Console.WindowWidth / 2) - (text.Length / 2) + 1;
                Console.Write(new string(' ', currentCol));
                Console.WriteLine(text);
            }
        }
    }
}
