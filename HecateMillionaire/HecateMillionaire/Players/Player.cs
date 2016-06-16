namespace HecateMillionaire.Players
{
    using Contracts;
    using System;
    using Common;
    using Jokers;

    class Player : IPlayer
    {
        private string name;
        private int scores;
        private int wordsColor;

        public Player(string name, int WordsColor, int scores)
        {
            Name = name;
            this.WordsColor = WordsColor;
            this.Scores = scores;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (Name.Length < 4)
                {
                    throw new ArgumentException("Name must be greather than 4 symbols!");
                }
                else
                {
                    this.name = value;
                }
            }
        }

        public int WordsColor
        {
            get
            {
                return this.wordsColor;                
            }

            private set
            {
                if (value < 1 && value >= 4)
                {
                    throw new ArgumentException("Sorry, you can try only between 3 colors.");
                }
                this.wordsColor = value;
            }
        }

        public int Scores
        {
            get
            {
                return this.scores;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Invalid scores.Scores can't be negative.");
                }
                else
                {
                    this.scores = value;
                }
            }
        }

        public override string ToString()
        {
            return String.Format("Player : {0} - {1} lv", this.name, this.scores);
        }

        //methods from IPlayer

        //the player give up - take the money earned in the game
        public int StopGameAndTakeMoney()
        {
            return this.Scores;
        }

        //TODO choose a joker type and call his method UseJoker()
        public void SelectJoker(JokerType jokerType)
        {
            //what kind of joker ? - ask from the console
            Joker joker;

            switch (jokerType)
            {
                case JokerType.FiftyFifty:
                    joker = new FiftyFiftyJoker();
                    break;
                case JokerType.HellFromPublic:
                    joker = new HelpFromPublicJoker();
                    break;
                case JokerType.CallFriend:
                    joker = new CallFriendJoker();
                    break;
                default:
                    throw new ArgumentException("Invalid joker type");
            }

            if (!joker.IsUsed)
            {
                joker.UseJoker();
            }
            else
            {
                throw new ArgumentException("You already used this joker");
            }

        }

        public void LoseGame()
        {
            this.Scores = 0;
        }

        public void WinGame()
        {
            //save player's scores in the file
        }
    }
}
