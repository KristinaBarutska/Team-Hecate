namespace HecateMillionaire.Questions.Contracts
{
    public interface IQuestion
    {
        
        int QuestionScore { get; set; }

        int TimerSeconds { get; set; }

        string[] Answers { get; }

        string QuestionText { get;}

        int RightAnswerIndex { get; set; }
        
        string PrintAnswers(bool flag);
    }
}
