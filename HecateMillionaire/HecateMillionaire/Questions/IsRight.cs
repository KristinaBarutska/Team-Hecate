﻿namespace HecateMillionaire
{
    using System;
    using HecateMillionaire.Questions;

    public class IsRight
    {
        // ToDo Адекватни проверки за входните данни
        private char answer;

        public Question Quest { get; set; }

        public char Answer
        {
            get
            {
                return this.answer;
            }

            set
            {
                // check if player answer is lowercase -> make it uppercase
                if (char.IsLower(value))
                {
                    this.answer = char.ToUpper(value);
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
            this.Answer = char.ToUpper(answer);
        }

        public bool Tell()
        {
            if (this.Quest.RightAnswerIndex == (this.Answer - 'A'))
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
