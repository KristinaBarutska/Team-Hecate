namespace HecateMillionaire
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class TheBestThreePlayers
    {
        //ToDo: print top three players with the best score and set background-color to see where is the player
        public static void Show(string nameOfPlayer)
        {
            var records = ReadFromFile.GetFileRecord();
            int[] sortNumber = new int[records.Length];

            int count = 0;

            for (int j = 0; j < records.Length; j++)
            {
                if (records[j] != null)
                {
                    sortNumber[count] = int.Parse(records[j].Substring(0, 4)); //take 4 digits
                    count++;
                }
                else
                {
                    break;
                }
            }

            string TempString; //for sorting strin arr
            int TempInt; //for sorting int arr

            //Sort record
            for (int j = 0; j < records.Length; j++)
            {
                for (int k = 0; k < records.Length; k++)
                {

                    if (sortNumber[j] > sortNumber[k])
                    {
                        TempString = records[j];
                        records[j] = records[k];
                        records[k] = TempString;

                        TempInt = sortNumber[j];
                        sortNumber[j] = sortNumber[k];
                        sortNumber[k] = TempInt;
                    }

                }
            }
            //

            System.Console.WriteLine("Standing:");

            //Print only three record
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i] != null)
                {
                    if (i < 3)
                    {
                        System.Console.WriteLine(i + 1 + " -> " + records[i]);

                    }
                    else
                    {
                        break;
                    }
                }
            }
            //

            //Print player position
            System.Console.WriteLine("\nYour position is: ");

            for (int i = 0; i < records.Length; i++)
            {
                var currnet = records[i].Split(' ');
                if (nameOfPlayer == currnet[2])
                {
                    System.Console.BackgroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(i + 1 + " -> " + records[i]);
                    System.Console.BackgroundColor = ConsoleColor.Black;
                    break;
                }
            }
            //

        }
    }
}
