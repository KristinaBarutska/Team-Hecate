namespace HecateMillionaire.Jokers
{
    class CallFriendJoker : Joker
    {

        public override void UseJoker()
        {
            //create a Friend 
            //generate his answer - he knows the answer, he is not sure, he don't know the answer
            //mark the joker as used
            this.IsUsed = true;
        }
    }
}
