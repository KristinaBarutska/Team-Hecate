namespace HecateMillionaire
{
    using GameLogic;

    public class MainHecate
    {
        /// <summary>
        /// Start game.Entry point is here.
        /// </summary>
        public static void Main()
        {
            Game game = Game.GetInstance();
            game.StartGame();
        }
    }
}