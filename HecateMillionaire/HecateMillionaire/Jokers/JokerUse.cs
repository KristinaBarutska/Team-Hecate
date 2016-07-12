
namespace HecateMillionaire.Jokers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using HecateMillionaire.Players.Contracts;
    using HecateMillionaire.Common.Console;
    using HecateMillionaire.HecateExceptions;


    public static class JokerUse
    {
        public static bool UseJoker(char answer, int rithAnswerIndex, string[] answersOfQuestion, IPlayer player)
        {
            bool flag = false; // for print only two answers when use FiftyFifty joker

            try
            {
                switch (answer)
                {
                    case '1':
                        if (player.SelectJoker(JokerType.FiftyFifty))
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                            Thread.Sleep(1000);
                        }

                        break;
                    case '2':
                        if (player.SelectJoker(JokerType.HelpFromPublic))
                        {
                            var fiftyFifty = player.Jokers[0]; // if used FiftyFifty joker

                            HelpFromPublicJoker help = new HelpFromPublicJoker(JokerType.HelpFromPublic);
                            Console.WriteLine(ConsoleConstants.PublicVoteMessage);
                            Console.WriteLine(help.Mind(rithAnswerIndex, fiftyFifty.IsUsed, answersOfQuestion));
                            Thread.Sleep(3500);
                        }
                        else
                        {
                            Thread.Sleep(1000);
                        }

                        break;
                    case '3':
                        if (player.SelectJoker(JokerType.CallFriend))
                        {
                            var fiftyFifty = player.Jokers[0]; // if used FiftyFifty joker

                            Console.WriteLine(ConsoleConstants.CallFriendMessage);
                            var friendName = Console.ReadLine();
                            CallFriendJoker frient = new CallFriendJoker(JokerType.CallFriend, friendName);
                            Console.WriteLine("{0} say: {1}", friendName, frient.Respond(fiftyFifty.IsUsed, answersOfQuestion));
                            Thread.Sleep(3500);
                        }
                        else
                        {
                            Thread.Sleep(1000);
                        }

                        break;
                    default:
                        break;
                }
            }
            catch (InvalidSecondChoiceJokerException isje)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(isje.Message);
                Console.ForegroundColor = ConsoleColor.Magenta;
            }


            return flag;
        }
    }
}
