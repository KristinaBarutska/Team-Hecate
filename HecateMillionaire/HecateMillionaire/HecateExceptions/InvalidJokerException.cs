namespace HecateMillionaire.HecateExceptions
{
    using System;

    class InvalidJokerException : Exception
    {
        public char JokerChoice { get; set; }

        public InvalidJokerException(string message, char choice, Exception innerException)
            : base(message, innerException)
        {
            this.JokerChoice = choice;
        }

        public InvalidJokerException(string message, char choice)
            : base(message) { }
    }
}
