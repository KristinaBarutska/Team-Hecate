namespace HecateMillionaire
{
    using System;
    using HecateMillionaire.Jokers;

    //abstract class Joker - base class for the other jokers 
    abstract class Joker
    {
        //To Do: get text from the Joker propery and print
        //Can be invoked only one time 

        private JokerType jokerType;
        private bool isUsed;

        public bool IsUsed { get; set; }

        //abstract method that will be used by polimorphism in FiftyFifty, CallFriend, HelpFromPublic
        public abstract void UseJoker();
    }
}
