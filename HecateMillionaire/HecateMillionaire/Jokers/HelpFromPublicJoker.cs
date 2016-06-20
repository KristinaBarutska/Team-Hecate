namespace HecateMillionaire.Jokers
{
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
    }
}
