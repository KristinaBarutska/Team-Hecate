namespace HecateMillionaire
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public static class SaveInFile
    {
        public static void SetFileRekord(int scorePlayer, string namePlayer) //Save on record
        {

            string path = @"..\..\Record.txt";

            FileStream CheckFile;

            //Check file for existence
            if (!File.Exists(@"..\..\Record.txt"))
            {
                CheckFile = File.Create(@"..\..\Record.txt");
                CheckFile.Close();
            }
            else
            {
                //Write in file

                string UpdateScore = scorePlayer.ToString() + " by " + namePlayer;

                using (StreamWriter file = new StreamWriter(path, true)) //true -> save without clear file
                {
                    file.WriteLine(UpdateScore);
                }
            }
        }
    }
}
