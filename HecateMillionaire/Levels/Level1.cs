namespace HecateMillionaire.Levels
{
    using System.Collections.Generic;
    using Questions.Contracts;
    using Questions;

    //jokers are locked - player can't see and use them 
    public class Level1 : Level,ILevel
    {
        public Level1() : base()
        {
            this.QuestionScore = GameConstants.QuestionScoreLevel1;
            this.TimerSeconds = GameConstants.TimerLevel1;
        }

        public override IList<IQuestion> QuestionsList
        {
            get
            {
                IList<IQuestion> list = base.QuestionsList;
                return new List<IQuestion>(list);
            }
        }

        //get only questions from level1
        public override IList<IQuestion> GetLevelQuestionsFromList()
        {
            IList<IQuestion> allQuestions = base.QuestionsList;
            IList<IQuestion> levelQuestions = new List<IQuestion>();

            for (int i = 0; i < allQuestions.Count; i++)
            {
                Question question = (Question)(allQuestions[i]);
                if (i < GameConstants.NumberOfQuestionPerLevel)
                {
                    levelQuestions.Add(question);
                }
            }
            return levelQuestions;
        }
    }
    
}
