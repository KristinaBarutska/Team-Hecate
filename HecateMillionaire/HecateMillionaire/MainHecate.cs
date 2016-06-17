namespace HecateMillionaire
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;

    class MainHecate
    {
        static void Main()
        {
            // ToDo: Get player name 
            System.Console.OutputEncoding = Encoding.UTF8;
            
            System.Console.Title = "Hey";
            System.Console.BackgroundColor = ConsoleColor.DarkRed;
            System.Console.SetCursorPosition(60,0);
            System.Console.WriteLine("Enter your choice! =>>");
            System.Console.ForegroundColor = ConsoleColor.Cyan;

            //initialize questions
            string filename = @"..\..\questions.txt";
            List<Question> questions = InitializeQuestions(filename);

            foreach (var question in questions)
            {
                System.Console.WriteLine(question);
            }
        }

        public static List<Question> InitializeQuestions(string file)
        {
            //read text file
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

            //parse text and save it to questions list
            //parse text to questions
            string[] questions = text.ToString().Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries);

            List<Question> questionsList = new List<Question>();
            for (int i = 0; i < questions.Length; i++)
            {
                string[] currentQuestion = questions[i].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                string questionText = currentQuestion[0];
                string answersStr = currentQuestion[1];
                int indexRightQuestion = int.Parse(currentQuestion[2]);

                string[] answers = answersStr.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

                Question question = new Question(questionText, answers, indexRightQuestion);
                questionsList.Add(question);
            }
            return questionsList;
        }
    }
}
