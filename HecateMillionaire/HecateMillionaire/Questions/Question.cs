namespace HecateMillionaire.Questions
{
    using System;
    using GameLogic;

    class Question
    {
        //used for method public static List<Question> InitializeQuestions()

        private string[] answers;
        private string questionText;
        private int rightAnswerIndex;

        public Question(string question, string[] answers, int index)
        {
            this.QuestionText = question;
            this.answers = answers;
            this.RightAnswerIndex = index;
            this.QuestionScore = GameConstants.QUESTION_SCORE;
        }

        public int QuestionScore { get; private set; }

        public string[] Answers
        {
            get { return this.answers; }
        }

        public string QuestionText
        {
            get { return this.questionText; }
            private set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(Common.GlobalErrorMessages.EmptyQuestionMessage);
                }
                this.questionText = value;
            }
        }

        public int RightAnswerIndex
        {
            get { return this.rightAnswerIndex; }
            set
            {
                if (value < 1 || value > this.answers.Length)
                {
                    throw new IndexOutOfRangeException(Common.GlobalErrorMessages.InvalidQuestionChoiceMessage);
                }
                this.rightAnswerIndex = value;
            }
        }


        public override string ToString()
        {
            return String.Format("{0}\nA.{1}\nB.{2}\nC.{3}\nD.{4}\n", this.QuestionText, answers[0], answers[1], answers[2], answers[3]);
        }
    }
}