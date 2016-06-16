using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hecate
{
    public class Question
    {
        private string quest;
        private string answerOne;
        private string answerTwo;
        private string answerThree;
        private string answerFour;
        private string indexOfRightAnswer;

        public Question(string question, string answerOne, string answerTwo, string answerThree, string answerFour, string indexOfRightAnswer)
        {
            this.Quest = question;
            this.AnswerOne = answerOne;
            this.AnswerTwo = answerTwo;
            this.AnswerThree = answerThree;
            this.AnswerFour = answerFour;
            this.indexOfRightAnswer = indexOfRightAnswer;
        }
        #region Properties
        public string Quest
        {
            get
            {
                return this.quest;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.quest = value;
                }
                else
                {
                    throw new ArgumentNullException("The question must not be empty!");
                }
            }
        }

        public string AnswerOne
        {
            get
            {
                return this.answerOne;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.answerOne = value;
                }
                else
                {
                    throw new ArgumentNullException("The answer must not be empty!");
                }
            }
        }
        public string AnswerTwo
        {
            get
            {
                return this.answerTwo;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.answerTwo = value;
                }
                else
                {
                    throw new ArgumentNullException("The answer must not be empty!");
                }
            }
        }
        public string AnswerThree
        {
            get
            {
                return this.answerThree;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.answerThree = value;
                }
                else
                {
                    throw new ArgumentNullException("The answer must not be empty!");
                }
            }
        }
        public string AnswerFour
        {
            get
            {
                return this.answerFour;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.answerFour = value;
                }
                else
                {
                    throw new ArgumentNullException("The answer must not be empty!");
                }
            }
        }
        public string EndexOfRightAnswer
        {
            get
            {
                return this.indexOfRightAnswer;
            }

            set
            {
                if (indexOfRightAnswer=="A"|| indexOfRightAnswer == "B"|| indexOfRightAnswer == "C"|| indexOfRightAnswer == "D")
                {
                    this.indexOfRightAnswer = value;
                }
                else
                {
                    throw new ArgumentNullException("The value must be A, B, C or D!");
                }
            }
        }
        #endregion
        public override string ToString()
        {
            var output = new StringBuilder();

            output.Append(string.Format("Въпрос:\t\t {0}", this.quest))
                .Append("\n\n");
            output.Append(string.Format("A\t {0}", this.answerOne))
                .Append("\n");
            output.Append(string.Format("B\t {0}", this.answerTwo))
                .Append("\n");
            output.Append(string.Format("C\t {0}", this.answerThree))
                .Append("\n");
            output.Append(string.Format("D\t {0}", this.answerFour))
                .Append("\n");

            return output.ToString();
        }

    }
}
