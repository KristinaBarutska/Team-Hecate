
namespace HecateMillionaire.Jokers
{
    using System;
    using System.Text;
    class HelpFromPublicJoker : Joker
    {
        public HelpFromPublicJoker(JokerType type)
            : base(type)
        {

        }
        public override void UseJoker()
        {
            //generate random % for A,B,C,D
            //mark the joker as used
            this.IsUsed = true;
        }

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
                        if (percentNumber == 100)
                        {
                            percent[i] = rand.Next(0, percentNumber);
                            percentNumber -= percent[i];
                        }
                        else
                        {
                            percent[i] = percentNumber; //because sum of all percent = 100
                        }

                    }
                }
                //
            }
            else
            {
                //create number for procents
                for (int i = 0; i < percent.Length - 1; i++)
                {
                    percent[i] = rand.Next(0, percentNumber);
                    percentNumber -= percent[i];
                }
                //

                percent[3] = percentNumber; //because sum of all percent = 100
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
