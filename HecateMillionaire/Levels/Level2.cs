namespace HecateMillionaire.Levels
{
    using System.Collections.Generic;
    using Questions.Contracts;
    using Questions;
    using Levels;
    using Players;

    //option to use jokers - they are unlocked and player can and use them
    public class Level2 : Level1
    {
        private int reduceScoresLevel2Coefficient;

        public Level2()
            : base()
        {
            this.QuestionScore = GameConstants.QuestionScoreLevel2;
            this.TimerSeconds = GameConstants.TimerLevel2;
        }

        public int ReduceScoresLevel2Coefficient
        {
            get { return this.reduceScoresLevel2Coefficient; }
            set
            {
                this.reduceScoresLevel2Coefficient = value;
            }
        }

        public int ApplyReduceScores(Player player)
        {
            player.Score /= this.ReduceScoresLevel2Coefficient;
            return player.Score;
        }

        
        public override IList<IQuestion> QuestionsList
        {
            get
            {
                IList<IQuestion> list = base.QuestionsList;
                return new List<IQuestion>(list);
            }
        }

        //get only questions from level2
        public override IList<IQuestion> GetLevelQuestionsFromList()
        {
            IList<IQuestion> allQuestions = base.QuestionsList;
            IList<IQuestion> levelQuestions = new List<IQuestion>();

            for (int i = 0; i < allQuestions.Count; i++)
            {
                Question question = (Question)(allQuestions[i]);
                if (i >= GameConstants.NumberOfQuestionPerLevel && i < GameConstants.NumberOfQuestionPerLevel * 2)
                {
                    levelQuestions.Add(question);
                }
            }
            return levelQuestions;
        }
    }
}
