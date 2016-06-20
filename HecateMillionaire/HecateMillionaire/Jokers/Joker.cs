namespace HecateMillionaire.Jokers
{
    //abstract class Joker - base class for the other jokers 
    public abstract class Joker
    {
        //To Do: get text from the Joker propery and print
        //Can be invoked only one time 

        private JokerType type;

        public Joker(JokerType type)
        {
            this.Type = type;
        }

        public bool IsUsed { get; set; }

        //abstract method that will be used by polimorphism in FiftyFifty, CallFriend, HelpFromPublic
        public abstract void UseJoker();

        public JokerType Type
        {
            get { return type; }
            set { type = value; }
        }
        
    }
}
