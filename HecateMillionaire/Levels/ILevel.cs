namespace HecateMillionaire.Levels
{
   using System.Collections.Generic;
   using Questions.Contracts;

   interface ILevel
   {
       int QuestionScore { get; set; }

       int TimerSeconds { get; set; }

       IList<IQuestion> QuestionsList { get; }

       //get only the questions from the current level
       IList<IQuestion> GetLevelQuestionsFromList();
   }
}
