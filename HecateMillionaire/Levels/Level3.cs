namespace HecateMillionaire.Levels
{
    using System.Collections.Generic;
    using Questions.Contracts;
    using Questions;
    using Levels;
    using Players;

    //option to use jokers and to skip one question
    public class Level3 : Level2
    {
        private int bonusChampionCoefficient;

        public Level3()
            : base()
        {
            this.QuestionScore = GameConstants.QuestionScoreLevel3;
            this.TimerSeconds = GameConstants.TimerLevel3;
        }

        public int BonusChampionCoefficient
        {
            get { return this.bonusChampionCoefficient; }
            private set
            {
                this.bonusChampionCoefficient = GameConstants.BonusChampionCoefficient;
            }
        }

        public int ApplyBonusScores(Player player)
        {
            player.Score *= this.BonusChampionCoefficient;
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

        //get only questions from level3
        public override IList<IQuestion> GetLevelQuestionsFromList()
        {
            IList<IQuestion> allQuestions = base.QuestionsList;
            IList<IQuestion> levelQuestions = new List<IQuestion>();

            for (int i = 0; i < allQuestions.Count; i++)
            {
                Question question = (Question)(allQuestions[i]);
                if (i >= GameConstants.NumberOfQuestionPerLevel * 2)
                {
                    levelQuestions.Add(question);
                }
            }
            return levelQuestions;
        }
    }
}
