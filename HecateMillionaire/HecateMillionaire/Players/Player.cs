namespace HecateMillionaire.Players
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using Common;
    using Jokers;
    using WorkWithFile;

    class Player : IPlayer
    {
        private string name;
        private int scores;
        private int wordsColor;
        private List<Joker> jokers;

        //player without color - for test only
        public Player(string name)
        {
            this.Name = name;
            this.Score = 0;
            InitelisateJoker();
        }

        public Player(string name, int wordsColor)
        {
            this.Name = name;
            this.WordsColor = wordsColor;
            this.Score = 0;
            InitelisateJoker();
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

        public List<Joker> Jokers
        {
            get { return jokers; }
            set { jokers = value; }
        }
        
        //methods from IPlayer

        //the player give up - take the money earned in the game
        public int StopGameAndTakeMoney()
        {
            return this.Score;
        }


        private void InitelisateJoker()
        {
            jokers = new List<Joker>();

            jokers.Add(new FiftyFiftyJoker(JokerType.FiftyFifty));
            jokers.Add(new HelpFromPublicJoker(JokerType.HellFromPublic));
            jokers.Add(new CallFriendJoker(JokerType.CallFriend));
        }

        //TODO choose a joker type and call his method UseJoker()
        public void SelectJoker(JokerType jokerType)
        {
            //what kind of joker ? - ask from the console

            switch (jokerType)
            {
                case JokerType.FiftyFifty:

                    if (jokers[0].IsUsed)
                    {
                        throw new ArgumentException(GlobalErrorMessages.SecondTimeJokerErrorMessage);
                    }
                    jokers[0].UseJoker();
                    break;

                case JokerType.HellFromPublic:

                    if (jokers[1].IsUsed)
                    {
                        throw new ArgumentException(GlobalErrorMessages.SecondTimeJokerErrorMessage);
                    }
                    jokers[1].UseJoker();
                    break;

                case JokerType.CallFriend:

                    if (jokers[2].IsUsed)
                    {
                        throw new ArgumentException(GlobalErrorMessages.SecondTimeJokerErrorMessage);
                    }
                    jokers[2].UseJoker();
                    break;

                default:
                    throw new ArgumentException(GlobalErrorMessages.InvalidJokerErrorMessage);
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
