namespace HecateMillionaire
{
    using System;
    using System.Linq;

    using Common.Console;
    using WorkWithFile;

    /// <summary>
    /// Print the top three players with the best score.
    /// </summary>
    /// 
    public static class TheBestThreePlayers
    {
        /// <summary>
        /// Show the player record.
        /// </summary>
        /// <param name="nameOfPlayer">Player name</param>
        /// 

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

                for (int i = 0; i < records.Count; i++)
                {
                    if (records[i].PlayerName == nameOfPlayer)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("{0} - > {1}", i + 1, nameOfPlayer);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }
            }
        }
    }
}