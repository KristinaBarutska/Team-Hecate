namespace HecateMillionaire.WorkWithFile
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Players;

    public static class ReadFromFile
    {

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
