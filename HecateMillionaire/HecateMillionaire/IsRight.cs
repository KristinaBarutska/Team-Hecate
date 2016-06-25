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
        private char answer;

        public Question Quest { get; set; }

        public char Answer
        {
          get { return this.answer; }
          set
            {
                //check if player answer is lowercase -> make it uppercase
                if (Char.IsLower(value))
                {
                    this.answer = Char.ToUpper(value);
                }
                else
                {
                    this.answer = value;
                }
            }
        }

        public IsRight(Question quest, char answer)
        {
            this.Quest = quest;
            this.Answer = answer;
        }

        public bool Tell()
        {
            if (Quest.RightAnswerIndex == (this.Answer - 'A'))
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
