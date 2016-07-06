namespace HecateMillionaire.GameLogic.Contracts
{
    public interface IGame
    {
        void InitiliazeGame();

        void PlayGame();

        //bool CheckPlayerAnswer(char answer);

        void OfferJoker();

        void EndGame();

        void ShowStatistics();

        void RestartGame();
    }
}
