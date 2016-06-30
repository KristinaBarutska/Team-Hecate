namespace HecateMillionaire.Questions
{
    using System;

    //option to use jokers - they are unlocked and player can and use them
    public class QuestionLevel2 : QuestionLevel1
    {
        public QuestionLevel2(string question, string[] answers, int index)
            : base(question, answers, index)
        {
            this.AreJokersUnlocked = true;
            this.QuestionScore = GameConstants.QuestionScoreLevel2;
            this.TimerSeconds = GameConstants.TimerLevel2;
        }


        public override int QuestionScore
        {
            get { return this.questionScore; }
            set
            {
                this.questionScore = GameConstants.QuestionScoreLevel2;
            }
        }

        public override int TimerSeconds
        {
            get { return this.timerSeconds; }
            set
            {
                this.timerSeconds = GameConstants.QuestionScoreLevel2;
            }
        }
    }
}
