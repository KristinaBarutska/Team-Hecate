namespace HecateMillionaire.WorkWithFile
{
    using System.IO;

    /// <summary>
    /// Save player records on the fail.
    /// </summary>
    public static class SaveInFile
    {
        public static void SetFileRekord(int scorePlayer, string namePlayer)
        { 
            string path = @"..\..\Record.txt";

            FileStream checkFile;

            // Check file for existence
            if (!File.Exists(@"..\..\Record.txt"))
            {
                checkFile = File.Create(@"..\..\Record.txt");
                checkFile.Close();
            }
            else
            {
                // Write in file
                string updateScore = scorePlayer.ToString() + " by " + namePlayer;

                // true -> save without clear file
                using (StreamWriter file = new StreamWriter(path, true)) 
                {
                    file.WriteLine(updateScore);
                }
            }
        }
    }
}
