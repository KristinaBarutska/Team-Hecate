namespace HecateMillionaire.HecateExceptions
{
    using System;

    using Jokers;

    public class InvalidSecondChoiceJokerException : Exception
    {
        public JokerType Joker { get; set; }

        public InvalidSecondChoiceJokerException(string message, JokerType joker, Exception innerException)
            : base(message, innerException)
        {
            this.Joker = joker;
        }

        public InvalidSecondChoiceJokerException(string message, JokerType joker)
            : base(message) { }

    }
}


