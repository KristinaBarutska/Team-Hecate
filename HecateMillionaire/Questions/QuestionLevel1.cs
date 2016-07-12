namespace HecateMillionaire.Questions
{
    using System;

    //jokers are locked - player can't see and use them 
    public class QuestionLevel1 : Question
    {
        public bool AreJokersUnlocked { get; set; }

        public QuestionLevel1(string question, string[] answers, int index)
            : base(question, answers, index)
        {
            this.AreJokersUnlocked = false;
            this.QuestionScore = GameConstants.QuestionScoreLevel1;
            this.TimerSeconds = GameConstants.TimerLevel1;
        }

        public override int QuestionScore
        {
            get { return this.questionScore; }
            set
            {
                this.questionScore = GameConstants.QuestionScoreLevel1;
            }
        }

        public override int TimerSeconds
        {
            get { return this.timerSeconds; }
            set
            {
                this.timerSeconds = GameConstants.TimerLevel1;
            }
        }
    }
}
