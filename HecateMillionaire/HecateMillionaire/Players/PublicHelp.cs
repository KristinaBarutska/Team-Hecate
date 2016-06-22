﻿namespace HecateMillionaire.Players
{
    using Common;
    using System;
    using System.Text;

    class PublicHelp
    {
        public string Mind(int righAnswer, bool flag, string[] answers)
        {
            var resoult = new StringBuilder();
            int[] percent = new int[4];

            Random rand = new Random();
            var percentNumber = 100;

            //if used FiftyFifty joker
            if (flag)
            {
                //create number for procents
                for (int i = 0; i < percent.Length; i++)
                {
                    if (answers[i] != "")
                    {
                        percent[i] = rand.Next(0, percentNumber);
                        percentNumber -= percent[i];
                    }
                }
                //
            }
            else
            {
                //create number for procents
                for (int i = 0; i < percent.Length; i++)
                {
                    percent[i] = rand.Next(0, percentNumber);
                    percentNumber -= percent[i];
                }
                //
            }

            //take max number
            var maxpercent = Math.Max(Math.Max(percent[0], percent[1]), Math.Max(percent[2], percent[3]));

            //move max number to right place
            for (int i = 0; i < percent.Length; i++)
            {
                if (maxpercent == percent[i] && i != righAnswer)
                {
                    var temp = percent[i];
                    percent[i] = percent[righAnswer];
                    percent[righAnswer] = temp;
                    break;
                }
            }
            //

            //Formating print answer
            resoult.AppendFormat("{0}% ", percent[0]);
            resoult.AppendFormat(" {0}% ", percent[1]);
            resoult.AppendFormat(" {0}% ", percent[2]);
            resoult.AppendFormat(" {0}% \n", percent[3]);

            resoult.Append(" ^");
            resoult.Append("    ^");
            resoult.Append("   ^");
            resoult.Append("    ^ \n");

            resoult.Append(" A");
            resoult.Append("    B");
            resoult.Append("   C");
            resoult.Append("    D \n");
            //

            return resoult.ToString();
        }
    }
}