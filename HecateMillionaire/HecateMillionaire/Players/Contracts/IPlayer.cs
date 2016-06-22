namespace HecateMillionaire.Players.Contracts
{
    using Common;
    using Jokers;

    public interface IPlayer
    {
        string Name { get; }

        WordsColorType Color { get; }

        int Score { get; }

        int StopGameAndTakeMoney();
        bool SelectJoker(JokerType jokerType);
    }
}
