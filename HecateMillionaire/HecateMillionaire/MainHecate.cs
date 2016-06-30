namespace HecateMillionaire
{
    using GameLogic;

    using Questions;
    using System.Collections.Generic;

    public class MainHecate
    {
        /// <summary>
        /// Start game.Entry point is here.
        /// </summary>
        public static void Main()
        {
            Game game = Game.GetInstance();
            game.StartGame();

            //test questions
            /*
            Question q = new Question("dffffb", new string[] { "aaa", "bbb", "ccc", "ddd" }, 1);
            System.Console.WriteLine(q.QuestionScore);
            System.Console.WriteLine(q.TimerSeconds);

            System.Console.WriteLine("level 1");
            Question q1 = new QuestionLevel1("level1 questions", new string[] { "aaa", "bbb", "ccc", "ddd" }, 1);
            System.Console.WriteLine("score " + q1.QuestionScore);
            System.Console.WriteLine("sec " + q1.TimerSeconds);

            System.Console.WriteLine("level 2");
            Question q2 = new QuestionLevel2("level2 questions", new string[] { "aaa", "bbb", "ccc", "ddd" }, 1);
            System.Console.WriteLine("score " + q2.QuestionScore);
            System.Console.WriteLine("sec " + q2.TimerSeconds);

            System.Console.WriteLine("level 3");
            Question q3 = new QuestionLevel3("level3 questions", new string[] { "aaa", "bbb", "ccc", "ddd" }, 1);
            System.Console.WriteLine("score "+ q3.QuestionScore);
            System.Console.WriteLine("sec " + q3.TimerSeconds);

            List<Question> quest = new List<Question>() { q1, q2, q3 };

            System.Console.WriteLine();
            for (int i = 0; i < quest.Count; i++)
            {
                System.Console.WriteLine("scores " + quest[i].QuestionScore);
                System.Console.WriteLine("time " + quest[i].TimerSeconds);
            }
             */


        }
    }
}
