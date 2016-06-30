namespace HecateMillionaire.Players
{
    using System;

    public struct PlayerResult
    {
        public string PlayerName { get; set; }
        public int Scores { get; set; }
        public DateTime Date { get; private set; }

        public PlayerResult(string name, int scores) : this()
        {
            this.PlayerName = name;
            this.Scores = scores;
            this.Date = DateTime.Now;
        }
    }
}
