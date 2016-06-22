namespace HecateMillionaire.Questions
{
    using System;

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
                if (value < 0 || value > this.answers.Length)
                {
                    throw new IndexOutOfRangeException(Common.GlobalErrorMessages.InvalidQuestionChoiceMessage);
                }
                this.rightAnswerIndex = value;
            }
        }

        public override string ToString()
        {
            // return String.Format("{0}\nA.{1}\nB.{2}\nC.{3}\nD.{4}\n", this.QuestionText, answers[0], answers[1], answers[2], answers[3]);

            return String.Format("{0}", this.QuestionText);
        }

        //Print answers
        public string PrintAnswers(bool flag)
        {
            if (flag) //for FiftyFifty joker
            {
                Random rand = new Random();
                var index = 0;
                var anotherIndex = 0;

                while (true)
                {
                    anotherIndex = rand.Next(0, 4);
                    index = rand.Next(0, 4);
                    if ((anotherIndex != this.RightAnswerIndex) &&
                        (index != this.RightAnswerIndex) &&
                        (index != anotherIndex))
                    {
                        break;
                    }
                }

                answers[index] = "";
                answers[anotherIndex] = "";

                return String.Format("A.{0}\nB.{1}\nC.{2}\nD.{3}\n", answers[0], answers[1], answers[2], answers[3]);
            }
            else
            {
                return String.Format("A.{0}\nB.{1}\nC.{2}\nD.{3}\n", answers[0], answers[1], answers[2], answers[3]);
            }
        }
    }
}