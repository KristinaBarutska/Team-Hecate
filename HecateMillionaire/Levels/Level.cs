namespace HecateMillionaire.Levels
{
    using System;
    using System.Collections.Generic;
    using Questions.Contracts;
    using System.IO;
    using System.Text;
    using Questions;

    public abstract class Level
    {
        private IList<IQuestion> questionsList;

        public Level()
        {
           this.questionsList = ReadQuestionFromFile(GameConstants.FileQuestions);
        }

        public virtual int QuestionScore { get ; set; }

        public virtual int TimerSeconds { get ; set; }

        public virtual IList<IQuestion> QuestionsList
        {
            get
            {
                return this.questionsList;
            }
        }

        //TODO - DECIDE IF IT WILL BE HERE
        private static IList<IQuestion> ReadQuestionFromFile(string file)
        {
            // read text file
            StreamReader reader = new StreamReader(file);
            StringBuilder text = new StringBuilder();
            using (reader)
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    text.Append(line);
                    text.Append(Environment.NewLine);
                    line = reader.ReadLine();
                }
            }

            // parse text to questions
            string[] questions = text.ToString().Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries);

            IList<IQuestion> questionsList = new List<IQuestion>();
            for (int i = 0; i < questions.Length; i++)
            {
                string[] currentQuestion = questions[i].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                string questionText = currentQuestion[0];
                string answersStr = currentQuestion[1];
                int indexRightQuestion = int.Parse(currentQuestion[2]);

                string[] answers = answersStr.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

                IQuestion question = new Question(questionText, answers, indexRightQuestion - 1);

                questionsList.Add(question);
            }
            return questionsList;
        }

        public abstract IList<IQuestion> GetLevelQuestionsFromList();
    }
}
