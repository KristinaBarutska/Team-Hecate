namespace HecateMillionaire.Questions
{
    using System;
    using Contracts;
    using System.Collections.Generic;

    public class Question : IQuestion
    {
        // used for method public static List<Question> InitializeQuestions()
        private string[] answers;
        private string questionText;
        private int rightAnswerIndex;

        protected int questionScore;
        protected int timerSeconds;

        public Question(string question, string[] answers, int index)
        {
            this.QuestionText = question;
            this.answers = answers;
            this.RightAnswerIndex = index;
            this.QuestionScore = 0;
            this.TimerSeconds = 0;

        }

        //properties to be overriden in classes QuestionLevel1, QuestionLevel2 and QuestionLevel3
        public virtual int QuestionScore { get; set; }
        public virtual int TimerSeconds { get; set; }
        
        public string[] Answers
        {
            get { return this.answers; }
        }

        public string QuestionText
        {
            get
            {
                return this.questionText;
            }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(Common.GlobalErrorMessages.EmptyQuestionMessage);
                }

                this.questionText = value;
            }
        }

        public int RightAnswerIndex
        {
            get
            {
                return this.rightAnswerIndex;
            }

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
            return string.Format("{0}", this.QuestionText);
        }

        // Print answers
        public string PrintAnswers(bool flag)
        {
            // for FiftyFifty joker
            if (flag) 
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

                this.answers[index] = string.Empty;
                this.answers[anotherIndex] = string.Empty;

                return string.Format("A.{0}\nB.{1}\nC.{2}\nD.{3}\n", answers[0], answers[1], answers[2], answers[3]);
            }
            else
            {
                return string.Format("A.{0}\nB.{1}\nC.{2}\nD.{3}\n", answers[0], answers[1], answers[2], answers[3]);
            }
        }

        public Question SkipQuestion(Question questionToSkip, IList<IQuestion> questions)
        {
            int questionIndex = questions.IndexOf(questionToSkip);
            if (questionIndex + 1 < questions.Count)
            {
                return (Question) questions[questionIndex + 1];
            }
            else
            {
                Console.WriteLine("This was the last question.You can't skip it");
                return questionToSkip;
            }
        }
    }
}