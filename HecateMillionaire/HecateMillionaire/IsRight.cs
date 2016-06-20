using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HecateMillionaire.Questions;

namespace HecateMillionaire
{
    class IsRight
    {
        //ToDo Адекватни проверки за входните данни
        public Question Quest { get; set; }
        public char Answer { get; set; }

        public IsRight(Question quest, char answer)
        {
            this.Quest = quest;
            this.Answer = answer;
        }

        public bool Tell()
        {
            if (Quest.RightAnswerIndex == (Answer - 'A'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
