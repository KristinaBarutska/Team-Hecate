namespace HecateMillionaire.Players.Contracts
{
    using Common;
    using Jokers;

    public interface IPlayer
    {
        string Name { get; }

        WordsColor Color { get; }

        int Score { get; }

        int StopGameAndTakeMoney();
        void SelectJoker(JokerType jokerType);
        void GameOver();
    }
}
