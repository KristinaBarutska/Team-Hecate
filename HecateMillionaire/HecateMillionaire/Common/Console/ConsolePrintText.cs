using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HecateMillionaire
{
    public static class ConsolePrintText
    {
        public static void Print(string[] texts)
        {
            foreach (var text in texts)
            {
                var currentCol = Console.WindowWidth / 2 - text.Length / 2;
                Console.Write(new string(' ', currentCol));
                Console.WriteLine(text);
            }
        }
    }
}
