namespace HecateMillionaire
{
    using GameLogic;

    class MainHecate
    {
        static void Main()
        {
            Game game = Game.GetInstance();
            game.StartGame();
        }
    }
}
