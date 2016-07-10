namespace HecateMillionaire.WorkWithFile
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Players;

    public static class ReadFromFile
    {
        public static string[] GetFileRecord() // Load on record
        {
            string lineStatus = null;
            FileStream file = null;
            string[] getRecord = new string[10];
            int count = 0;

            // Check file for existence
            if (File.Exists(@"..\..\Record.txt"))
            {
                file = File.OpenRead(@"..\..\Record.txt");
            }
            else
            {
                file = File.Create(@"..\..\Record.txt");
            }

            // Read from file
            using (StreamReader sr = new StreamReader(file))
            {
                while ((lineStatus = sr.ReadLine()) != null)
                {
                    getRecord[count] = lineStatus;
                    count++;

                    if (count > getRecord.Length - 1)
                    {
                        var currnetArr = new string[getRecord.Length];

                        for (int i = 0; i < getRecord.Length; i++)
                        {
                            currnetArr[i] = getRecord[i];
                        }

                        getRecord = new string[currnetArr.Length * getRecord.Length];

                        for (int i = 0; i < currnetArr.Length; i++)
                        {
                            getRecord[i] = currnetArr[i];
                        }
                    }
                }
            }

            // Take only record on getRecord arr
            var highScore = new string[count];

            for (int i = 0; i < count; i++)
            {
                highScore[i] += getRecord[i];
            }

            return highScore;
        }

        //save player results using struct PlayerResult - save player name, scores and date
        public static List<PlayerResult> GetPlayerResultFileRekord()
        {
            List<PlayerResult> resluts = new List<PlayerResult>();

            string path = GameConstants.FilePlayerResults;

            FileStream checkFile;

            if (!File.Exists(path))
            {
                checkFile = File.Create(path);
                checkFile.Close();
            }
            else
            {

                // Read from file
                using (StreamReader sr = new StreamReader(path))
                {
                    var lineStatus = "";
                    while ((lineStatus = sr.ReadLine()) != null)
                    {
                        var currentLine = lineStatus.Split(' ');

                        //take only date
                        var lineDate = (currentLine[4] + " " + currentLine[5]).Split(new char[] { '.', ' ', ':' });

                        var currentDate = new DateTime(int.Parse(lineDate[2]),
                                                       int.Parse(lineDate[1]),
                                                       int.Parse(lineDate[0]),
                                                       int.Parse(lineDate[3]),
                                                       int.Parse(lineDate[4]),
                                                       int.Parse(lineDate[5]));

                        //add in list all rezult
                        resluts.Add(new PlayerResult(currentLine[2], int.Parse(currentLine[0]), currentDate));
                    }
                }
            }

            return resluts;
        }
    }
}
