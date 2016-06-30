namespace HecateMillionaire.Questions
{
    using System;
    using System.Collections.Generic;

    //option to use jokers and to skip one question
    public class QuestionLevel3 : QuestionLevel2
    {
        public bool IsSkipped { get; set; }

        public QuestionLevel3(string question, string[] answers, int index)
            : base(question, answers, index)
        {
            this.IsSkipped = false;
            this.QuestionScore = GameConstants.QuestionScoreLevel3;
            this.TimerSeconds = GameConstants.TimerLevel3;
        }

        //overrire properties QuestionScore & TimerSeconds
        public override int QuestionScore
        {
            get { return this.questionScore; }
            set
            {
                this.questionScore = GameConstants.QuestionScoreLevel3;
            }
        }

        public override int TimerSeconds
        {
            get { return this.timerSeconds; }
            set
            {
                this.timerSeconds = GameConstants.QuestionScoreLevel3;
            }
        }

        public QuestionLevel3 SkipQuestion(QuestionLevel3 questionToSkip, List<Question> questions)
        {
            int questionIndex = questions.IndexOf(questionToSkip);
            if (questionIndex + 1 < questions.Count)
            {
                return questions[questionIndex + 1] as QuestionLevel3;
            }
            else
            {
                Console.WriteLine("This was the last question.You can't skip it");
                return questionToSkip;
            }
        }
    }
}
