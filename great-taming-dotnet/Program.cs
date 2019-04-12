using System;
using SadConsole;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;
using GreatTaming.UI;
using GreatTaming.Engine;

namespace GreatTaming
{
    class Program
    {
        static void Main(string[] args)
        {
            SadConsole.Game.Create(100, 40);
            SadConsole.Game.OnInitialize = Init;
            SadConsole.Game.OnUpdate = Update;
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        static void Init()
        {
            var ctx = GameContext.NewGame(0xDEADBEEF);
            UIManager.Register(
                new TitleScreen(),
                new MainScreen(ctx));
            UIManager.SetUI("title");
        }

        static void Update(GameTime t)
        {
            if (SadConsole.Global.KeyboardState.KeysPressed.Count > 0)
            {
                UIManager.CurrentUI.Handle();
            }
        }
    }
}
