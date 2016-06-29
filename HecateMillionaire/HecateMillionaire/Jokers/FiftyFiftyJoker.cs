namespace HecateMillionaire.Jokers
{
    public class FiftyFiftyJoker : Joker
    {
        public FiftyFiftyJoker(JokerType type)
            : base(type)
        {
        }

        public override void UseJoker()
        {
            // To Do: get a right answer + one wrong and print them
            // mark the joker as used
            this.IsUsed = true;
        }
    }
}
