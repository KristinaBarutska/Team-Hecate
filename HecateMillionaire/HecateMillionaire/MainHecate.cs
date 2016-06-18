namespace HecateMillionaire
{
    using Common.Console;
    using Questions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using GameLogic;

    class MainHecate
    {
        static void Main()
        {

            Game game = new Game();
            game.StartGame();
        }
    }
}
