namespace HecateMillionaire
{
    using Common.Console;
    using Questions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class MainHecate
    {
        static void Main()
        {
            // ToDo: Get player name 
            System.Console.OutputEncoding = Encoding.UTF8;
          
            //initialize questions
            string filename = @"..\..\questions.txt";
            List<Question> questions = InitializeQuestions(filename);

            //17.6.2016, Kristina. Добавена е проверка за коректност на отговора
            for (int i=0;i<questios.Count;i++)
            
                System.Console.WriteLine(questions[i]);
                char answer= Char.Parse(System.Console.ReadLine());
                IsRight check = new IsRight(questions[i], answer);
                if (check.Tell())
                {
                    System.Console.WriteLine(ConsoleConstants.RightAnswerMessage);
                }
                else
                {
                    System.Console.WriteLine(ConsoleConstants.WrongAnswerMessage);
                }
                //ToDo Add 100 scores if the answaer is right
            }
        }
        //Край на промените на Кристина
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
