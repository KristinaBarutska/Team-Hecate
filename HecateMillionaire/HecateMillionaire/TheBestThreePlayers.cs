namespace HecateMillionaire
{
    using Common.Console;
    using System;
    using WorkWithFile;
    using System.Linq;

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
                    var current = records[j].Split(' ');
                    sortNumber[count] = int.Parse(current[0]);
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
            
            Console.WriteLine(ConsoleConstants.StandingMessage);

            //Print only three record
            for (int i = 0; i < records.Length; i++)
            {
                if (records[i] != null)
                {
                    if (i < ConsoleConstants.BestThreePlayers)
                    { 
                        Console.WriteLine("\t{0} - > {1}",i + 1,records[i]);
                    }
                    else
                    {
                        break;
                    }
                }
            }
                        
            //print player's position only if he has already played a game
            //don't print position for default player
            if(!nameOfPlayer.Equals("Player"))
            {
                //Print player position
                Console.Write(ConsoleConstants.PositionMessage);

                var sortArr = new string[records.Length];

                for (int i = 0; i < records.Length; i++)
                {
                    var currnet = records[i].Split(' ');

                    sortArr[i] = currnet[2];
                }


                var positio = (sortArr.Where(x => x == nameOfPlayer)).Count();

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("{0} - > {1}", positio, nameOfPlayer);
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

         
    }
}
