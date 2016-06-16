namespace HecateMillionaire.Players
{
    using Contracts;
    using System;
    using Common;

    class Player : IPlayer
    {
        private string name;
        private int scores;

        public Player(string name, int WordsColor, decimal score)
        {
            Name = name;
            this.WordsColor = WordsColor;
            this.Score = score;
        }

        public string Name
        {
            get
            {
                if (Name.Length < 4)
                {
                    throw new ArgumentException("Name must be greather than 4 symbols!");
                }
                return Name;
            }

            private set { }
        }

        public int WordsColor
        {
            get
            {
                if (wordsColor < 1 && wordsColor >= 4)
                {
                    throw new ArgumentException("Sorry, you can try only between 3 colors.");
                }
                return wordsColor;
            }

            private set { }
        }

        public decimal Score
        {
            get { return score; }
            private set { }
        }

        string IPlayer.Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        WordsColor IPlayer.Color
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        decimal IPlayer.Score
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            return; 
        }

        //public override string ToString()
        //{
        //    return String.Format("Player : {0} - {1} lv", this.name, this.scores);
        //}
    }
}
