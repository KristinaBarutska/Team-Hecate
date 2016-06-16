using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HecateMillionaire
{
    public static class ReadFromFile
    {
        public static string[] GetFileRecord() //Load on record
        {
            String lineStatus = null;
            FileStream file = null;
            String[] getRecord = new String[10];
            int count = 0;

            //Check file for existence
            if (File.Exists(@"..\..\Record.txt"))
            {
                file = File.OpenRead(@"..\..\Record.txt");
            }
            else
            {
                file = File.Create(@"..\..\Record.txt");
            }


            //Read from file
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
            //

            //Take only record on getRecord arr
            var highScore = new string[count];

            for (int i = 0; i < count; i++)
            {
                highScore[i] += getRecord[i];
            }
            //

            return highScore;
        }
    }
}
