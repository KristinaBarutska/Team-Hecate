namespace HecateMillionaire
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Common.Console;
    using WorkWithFile;
    using HecateMillionaire.Players;

    /// <summary>
    /// Print the top three players with the best score.
    /// </summary>
    /// 
    public static class TheBestThreePlayers
    {
        /// <summary>
        /// Show the player record.
        /// </summary>
        /// <param name="nameOfPlayer"></param>
        /// 
        public static void ShowRecord(string nameOfPlayer)
        {

            var records = ReadFromFile.GetFileRecord();
            int[] sortNumber = new int[records.Length];

            int count = 0;

            for (int j = 0; j < records.Length; j++)
            {
                if (records[j] != null)
                {
                    var current = records[j].Split(' ');
                    sortNumber[count] = int.Parse(current[0]);
                    count++;
                }
                else
                {
                    break;
                }
            }

            string tempString; // for sorting strin arr
            int tempInt; // for sorting int arr

            // Sort record
            for (int j = 0; j < records.Length; j++)
            {
                for (int k = 0; k < records.Length; k++)
                {
                    if (sortNumber[j] > sortNumber[k])
                    {
                        tempString = records[j];
                        records[j] = records[k];
                        records[k] = tempString;

                        tempInt = sortNumber[j];
                        sortNumber[j] = sortNumber[k];
                        sortNumber[k] = tempInt;
                    }
                }
            }

            //Console.WriteLine(ConsoleConstants.StandingMessage);

            // Print only three records
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i] != null)
                {
                    if (i < ConsoleConstants.BestThreePlayers)
                    {
                        Console.WriteLine("\t{0} - > {1}", i + 1, records[i]);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // print player's position only if he has already played a game
            // don't print position for default player
            if (!nameOfPlayer.Equals("Player"))
            {
                // Print player position
                Console.Write(ConsoleConstants.PositionMessage);

                var sortArr = new string[records.Length];

                for (int i = 0; i < records.Length; i++)
                {
                    var currnet = records[i].Split(' ');

                    sortArr[i] = currnet[2];
                }

                var positio = sortArr.Where(x => x == nameOfPlayer).Count();

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("{0} - > {1}", positio, nameOfPlayer);
                Console.BackgroundColor = ConsoleColor.Black;
            }

        }

        public static void ShowPlayerResultFileRekord(string nameOfPlayer)
        {
            var records = ReadFromFile.GetPlayerResultFileRekord().OrderByDescending(x => x.Scores).ToList();

            Console.WriteLine(ConsoleConstants.StandingMessage);

            // Print only three records
            for (int i = 0; i < records.Count; i++)
            {

                if (i < ConsoleConstants.BestThreePlayers)
                {
                    Console.WriteLine("\t{0} - > {1}", i + 1, records[i]);
                }
                else
                {
                    break;
                }

            }

            // print player's position only if he has already played a game
            // don't print position for default player
            if (!nameOfPlayer.Equals("Player"))
            {
                // Print player position
                Console.Write(ConsoleConstants.PositionMessage);

                //for (int i = 0; i < records.Count; i++)
                //{
                //    if (records[i].PlayerName == nameOfPlayer)
                //    {
                //        Console.BackgroundColor = ConsoleColor.Red;
                //        Console.ForegroundColor = ConsoleColor.Black;
                //        Console.WriteLine("{0} - > {1}", i + 1, nameOfPlayer);
                //        Console.BackgroundColor = ConsoleColor.Black;
                //    }
                //}

               // var positio = records.Where(item => item.PlayerName == "nbmbmbm").Count();

                //Console.BackgroundColor = ConsoleColor.Red;
                //Console.ForegroundColor = ConsoleColor.Black;
                //Console.WriteLine("{0} - > {1}", positio, nameOfPlayer);
                //Console.BackgroundColor = ConsoleColor.Black;
            }
        }
    }
}