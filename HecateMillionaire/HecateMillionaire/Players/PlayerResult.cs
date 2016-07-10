namespace HecateMillionaire.Players
{
    using System;
    using System.Text;

    public struct PlayerResult
    {
        public string PlayerName { get; set; }
        public int Scores { get; set; }
        public DateTime Date { get; private set; }

        public PlayerResult(string name, int scores, DateTime date) : this()
        {
            this.PlayerName = name;
            this.Scores = scores;
            this.Date = date;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendFormat("Score {0} by {1} at {2}", this.Scores, this.PlayerName, this.Date);

            return result.ToString();
        }
    }
}
