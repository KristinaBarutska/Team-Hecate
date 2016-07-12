namespace HecateMillionaire.HecateExceptions
{
    using System;

    public class InvalidAnswerException : System.Exception
    {
        public char AnswerChoice { get; set; }

        public InvalidAnswerException(string message, char choice, Exception innerException)
            : base(message, innerException) 
        {
            this.AnswerChoice = choice;
        }

        public InvalidAnswerException(string message, char choice)
            : base(message) { }
        
    }
}
