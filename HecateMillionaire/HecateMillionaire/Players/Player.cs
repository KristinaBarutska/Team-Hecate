namespace HecateMillionaire.Players
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using Common;
    using Jokers;
    using HecateMillionaire.WorkWithFile;
    using System.Collections;

    public class Player : IPlayer, ICloneable , IEnumerable
    {
        private string name;
        private int scores;
        private int wordsColorType;
        private List<Joker> jokers;

        //player without color - for test only
        public Player(string name)
        {
            this.Name = name;
            this.Score = 0;
            InitelisateJoker();
        }

        public Player(string name, int wordsColorType)
        {
            this.Name = name;
            this.WordsColorType = wordsColorType;
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

        public int WordsColorType
        {
            get
            {
                return this.wordsColorType;
            }

            private set
            {
                if (value < 1 && value >= 4)
                {
                    throw new ArgumentException(GlobalErrorMessages.InvalidWordsColorChoiceErrorMessage);
                }
                this.wordsColorType = value;
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

        public WordsColorType Color
        {
            get
            {
                throw new NotImplementedException();
            }
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
            jokers.Add(new CallFriendJoker(JokerType.CallFriend," "));
        }

        //TODO choose a joker type and call his method UseJoker()
        public bool SelectJoker(JokerType jokerType)
        {
            //what kind of joker ? - ask from the console

            switch (jokerType)
            {
                case JokerType.FiftyFifty:

                    if (jokers[0].IsUsed)
                    {
                        //throw new ArgumentException(GlobalErrorMessages.SecondTimeJokerErrorMessage);
                        Console.WriteLine(GlobalErrorMessages.SecondTimeJokerErrorMessage);
                        return false;
                    }
                    jokers[0].UseJoker();
                    return true;

                case JokerType.HellFromPublic:

                    if (jokers[1].IsUsed)
                    {
                        //throw new ArgumentException(GlobalErrorMessages.SecondTimeJokerErrorMessage);
                        Console.WriteLine(GlobalErrorMessages.SecondTimeJokerErrorMessage);
                        return false;
                    }
                    jokers[1].UseJoker();
                    return true;

                case JokerType.CallFriend:

                    if (jokers[2].IsUsed)
                    {
                        //throw new ArgumentException(GlobalErrorMessages.SecondTimeJokerErrorMessage);
                        Console.WriteLine(GlobalErrorMessages.SecondTimeJokerErrorMessage);
                        return false;
                    }
                    jokers[2].UseJoker();
                    return true;

                default:
                    throw new ArgumentException(GlobalErrorMessages.InvalidJokerErrorMessage);
                    
            }

        }

        //this method is moved in Game - EndGame()
        public void GameOver()
        {
            if (this.Score != 0)
            {
                SaveInFile.SetFileRekord(this.Score, this.Name); //save record and name in file when game over 
            }
        }

        public object Clone()
        {
            var currentPlayer = new Player(this.Name);

            currentPlayer.WordsColorType = this.WordsColorType;
            currentPlayer.Score = this.Score;

            currentPlayer.Jokers[0] = this.Jokers[0];
            currentPlayer.Jokers[1] = this.Jokers[1];
            currentPlayer.Jokers[2] = this.Jokers[2];

            return currentPlayer;
        }

        public IEnumerator GetEnumerator()
        {
            var props = this.GetType().GetProperties();

            foreach (var prop in props)
            {
                yield return prop;
            }
        }
    }
}
