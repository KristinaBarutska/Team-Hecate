namespace HecateMillionaire.Players
{
    using Contracts;
    using System;
    using Common;
    using Jokers;
    using WorkWithFile;

    class Player : IPlayer
    {
        private string name;
        private int scores;
        private int wordsColor;

        //player without color - for test only
        public Player(string name)
        {
            this.Name = name;
            this.Score = 0;
        }

        public Player(string name, int wordsColor)
        {
            this.Name = name;
            this.WordsColor = wordsColor;
            this.Score = 0;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (value.Length < 4)
                {
                    throw new ArgumentException(GlobalErrorMessages.InvalidPlayerNameErrorMessage);
                }
                else
                {
                    this.name = value;
                }
            }
        }

        public int Score
        {
            get
            {
                return this.scores;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(GlobalErrorMessages.InvalidScoreErrorMessage);
                }
                else
                {
                    this.scores = value;
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
                    throw new ArgumentException(GlobalErrorMessages.InvalidWordsColorChoiceErrorMessage);
                }
                this.wordsColor = value;
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
            return this.Score;
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
                    throw new ArgumentException(GlobalErrorMessages.InvalidJokerErrorMessage);
            }

            if (!joker.IsUsed)
            {
                joker.UseJoker();
            }
            else
            {
                throw new ArgumentException(GlobalErrorMessages.SecondTimeJokerErrorMessage);
            }

        }

        //this method is moved in Game - EndGame()
        //public void GameOver()
        //{
        //    if (this.Score != 0)
        //    {
        //        SaveInFile.SetFileRekord(this.Score, this.Name); //save record and name in file when game over 
        //    }
        //}

        WordsColor IPlayer.Color
        {
            get
            {
                return (WordsColor)WordsColor; //cast int to color enum and return color
            }
        }

    }
}
