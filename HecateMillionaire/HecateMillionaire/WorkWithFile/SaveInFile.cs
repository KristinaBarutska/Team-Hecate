namespace HecateMillionaire.WorkWithFile
{
    using System.IO;
    using Players;
    using System;

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

        //save player results using struct PlayerResult - save player name, scores and date
        public static void SetPlayerResultFileRekord(Player player)
        {
            string path = GameConstants.FilePlayerResults;

            FileStream checkFile;

            // Check file for existence
            if (!File.Exists(path))
            {
                checkFile = File.Create(path);
                checkFile.Close();
            }
            else
            {
                // Write in file
                PlayerResult result = new PlayerResult(player.Name, player.Score, DateTime.Now);
                string updateScore = result.Scores.ToString() + " by " + result.PlayerName + " at " + result.Date;

                // true -> save without clear file
                using (StreamWriter file = new StreamWriter(path, true))
                {
                    file.WriteLine(updateScore);
                }
            }
        }
    }
}
