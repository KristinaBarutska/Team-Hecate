namespace HecateMillionaire
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Question
    {
    
		//used for method public static List<Question> InitializeQuestions
		
	private string[] answers;
        public string QuestionText { get; set; }
        public int RightAnswerIndex { get; set; }

        public Question(string question, string[] answers, int index)
        {
            this.QuestionText = question;
            this.answers = answers;
            this.RightAnswerIndex = index;
        }

        public override string ToString()
        {
            return String.Format("{0}\nA.{1}\nB.{2}\nC.{3}\nD.{4}\n", this.QuestionText, answers[0], answers[1], answers[2], answers[3]);
        }
		
