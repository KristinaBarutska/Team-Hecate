namespace HecateMillionaire.Players.Contracts
{
    using Common;
    using Jokers;
    using Jokers.Contracts;
    using System.Collections.Generic;

    public interface IPlayer
    {
        WordsColorType Color { get; }

        IList<IJoker> Jokers { get; set; }

        string Name { get; }

        int Score { get; }

        bool SelectJoker(JokerType jokerType);
    }
}
