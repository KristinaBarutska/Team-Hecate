namespace HecateMillionaire.Players.Contracts
{
    using Common;

    public interface IPlayer
    {
        string Name { get; }

        WordsColor Color { get; }

        decimal Score { get; }
        

    }
}
