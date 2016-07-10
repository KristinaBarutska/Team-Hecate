namespace HecateMillionaire.WorkWithFile
{
    using System;
    using System.IO;

    using Players;

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
                var dateSet = string.Format("{0}.{1}.{2} {3}:{4}:{5}", result.Date.Day,
                                                                      result.Date.Month,
                                                                      result.Date.Year,
                                                                      result.Date.Hour,
                                                                      result.Date.Minute,
                                                                      result.Date.Second);
                string updateScore = result.Scores.ToString() + " by " + result.PlayerName + " at " + dateSet;

                // true -> save without clear file
                using (StreamWriter file = new StreamWriter(path, true))
                {
                    file.WriteLine(updateScore);
                }
            }
        }
    }
}