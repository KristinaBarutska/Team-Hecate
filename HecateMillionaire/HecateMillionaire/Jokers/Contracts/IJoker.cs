namespace HecateMillionaire.Jokers.Contracts
{
    public interface IJoker
    {
        JokerType Type { get; set; }

        bool IsUsed { get; set; }

        void UseJoker();
    }
}
